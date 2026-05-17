using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante_SoftGourmet.Clases.Modelos;
using Product = Restaurante_SoftGourmet.Clases.Modelos.ProductModel;
using Restaurante_SoftGourmet.Clases.Colecciones;
using Restaurante_SoftGourmet.Clases.Carrito;


using System.Windows.Forms;
using System.Globalization;

namespace Restaurante_SoftGourmet
{
    public partial class ModuloComensal : Form
    {
        private const string ConnectionString = "Server=.;Database=SoftGourmetDB;Trusted_Connection=True;";

        private Timer _clockTimer;
        private readonly CartStorage _cart = new CartStorage();

        // Tabla en memoria con las mesas cargadas desde la BD
        private DataTable _mesasTable;

        // Añadir campo de filtro (colocar junto a otros campos privados)
        private int? _cartFilterCategoriaId = null;


        public ModuloComensal()
        {
            InitializeComponent();
            this.Load += ModuloComensal_Load;
            this.FormClosing += ModuloComensal_FormClosing;
        }

        private void ModuloComensal_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategories();

                // Cargar mesas disponibles (incluye opción "Ninguna")
                LoadMesas();

                // llenar combobox para filtrar el carrito por categoría
                LoadCartCategories();
                // mostrar carrito (filtrado inicialmente en "Todas")
                RenderCartItems();

                UpdateFechaActual();
                _clockTimer = new Timer { Interval = 1000 };
                _clockTimer.Tick += ClockTimer_Tick;
                _clockTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inicializando: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModuloComensal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_clockTimer != null)
            {
                _clockTimer.Stop();
                _clockTimer.Tick -= ClockTimer_Tick;
                _clockTimer.Dispose();
                _clockTimer = null;
            }
        }

        private void ClockTimer_Tick(object sender, EventArgs e) => UpdateFechaActual();

        // Busca un control por nombre recursivamente dentro de un contenedor
        private Control FindControlRecursive(Control parent, string name)
        {
            if (parent == null || string.IsNullOrEmpty(name)) return null;
            foreach (Control c in parent.Controls)
            {
                if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase))
                    return c;
                var found = FindControlRecursive(c, name);
                if (found != null) return found;
            }
            return null;
        }


        private void UpdateFechaActual()
        {
            try
            {
                var ahora = DateTime.Now;
                var texto = ahora.ToString("dddd, d 'de' MMMM 'de' yyyy HH:mm:ss", new CultureInfo("es-ES"));

                // Intentar actualizar ambos labels por nombre (busca recursivamente)
                var l1 = FindControlRecursive(this, "lblFechaActual1") as Label;
                var l2 = FindControlRecursive(this, "lblFechaActual2") as Label;

                if (l1 != null) l1.Text = texto;
                if (l2 != null) l2.Text = texto;

                // si no existen, intentar nombre alternativo por compatibilidad
                if (l1 == null)
                {
                    var alt = FindControlRecursive(this, "lblFechaActual") as Label;
                    if (alt != null) alt.Text = texto;
                }
            }
            catch
            {
                var fallback = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                var l1 = FindControlRecursive(this, "lblFechaActual1") as Label;
                var l2 = FindControlRecursive(this, "lblFechaActual2") as Label;
                if (l1 != null) l1.Text = fallback;
                if (l2 != null) l2.Text = fallback;
            }
        }


        private void LoadCategories()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("SELECT CategoriaID, Nombre FROM Categorias ORDER BY Nombre", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            var newRow = dt.NewRow();
            newRow["CategoriaID"] = 0;
            newRow["Nombre"] = "Todas";
            dt.Rows.InsertAt(newRow, 0);

            cboCategoria.SelectedIndexChanged -= cboCategoria_SelectedIndexChanged;
            cboCategoria.DisplayMember = "Nombre";
            cboCategoria.ValueMember = "CategoriaID";
            cboCategoria.DataSource = dt;
            cboCategoria.SelectedIndex = 0;
            cboCategoria.SelectedIndexChanged += cboCategoria_SelectedIndexChanged;

            var products = LoadProductsFromDb(null);
            RenderProducts(products);
        }

        private ProductCollection LoadProductsFromDb(int? categoriaId = null)
        {
            var collection = new ProductCollection();
            string sql = "SELECT ProductoID, Nombre, Precio, ImagenRuta, Disponible, Descripcion, CategoriaID FROM Productos";
            if (categoriaId.HasValue && categoriaId.Value > 0) sql += " WHERE CategoriaID = @CategoriaID";
            sql += " ORDER BY ProductoID";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (categoriaId.HasValue && categoriaId.Value > 0)
                    cmd.Parameters.AddWithValue("@CategoriaID", categoriaId.Value);

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var prod = new Product
                        {
                            ProductoID = rdr.GetInt32(0),
                            Nombre = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                            Precio = rdr.IsDBNull(2) ? 0m : rdr.GetDecimal(2),
                            ImagenRuta = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                            Disponible = !rdr.IsDBNull(4) && rdr.GetBoolean(4),
                            Descripcion = rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5)
                        };
                        if (!rdr.IsDBNull(6))
                            prod.CategoriaID = rdr.GetInt32(6);

                        collection.Add(prod);
                    }
                }
            }

            return collection;
        }


        private void RenderProducts(ProductCollection products)
        {
            // limpiar dinámicos previos
            for (int i = SectionProducts1.Controls.Count - 1; i >= 0; i--)
            {
                var c = SectionProducts1.Controls[i];
                if (c.Tag != null && c.Tag.ToString() == "dynamicProduct")
                    SectionProducts1.Controls.RemoveAt(i);
            }

            pnlSubSectionProduct1.Visible = false;
            pnlSubSectionProduct2.Visible = false;

            const int panelWidth = 350;
            const int panelHeight = 300;
            const int spacingY = 20;
            const int startY = 7;
            const int xLeft = 40;
            const int xRight = 445;

            var arr = products.ToArray();

            SectionProducts1.SuspendLayout();
            for (int index = 0; index < arr.Length; index++)
            {
                var p = arr[index];
                int column = index % 2;
                int row = index / 2;
                int x = (column == 0) ? xLeft : xRight;
                int y = startY + row * (panelHeight + spacingY);

                var pnl = new Panel
                {
                    Size = new Size(panelWidth, panelHeight),
                    Location = new Point(x, y),
                    Tag = "dynamicProduct",
                    BackColor = Color.FromArgb(248, 244, 227)
                };

                pnl.Paint += (s, e) =>
                {
                    using (Pen pen = new Pen(Color.FromArgb(201, 162, 39), 3))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
                    }
                };

                var imgProducto = new PictureBox
                {
                    Location = new Point(10, 10),
                    Size = new Size(160, 160),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    TabStop = false,
                    Name = "imgProducto_dyn_" + p.ProductoID.ToString()
                };
                if (!string.IsNullOrWhiteSpace(p.ImagenRuta))
                {
                    try
                    {
                        var path = p.ImagenRuta;
                        if (!Path.IsPathRooted(path))
                            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path.Replace('/', Path.DirectorySeparatorChar));
                        if (File.Exists(path))
                            imgProducto.Image = Image.FromFile(path);
                    }
                    catch { }
                }

                var lblNombre = new Label { AutoSize = true, Location = new Point(175, 10), Name = "lblNombre_dyn_" + p.ProductoID.ToString(), Text = p.Nombre, Font = new Font("Segoe UI", 12F), ForeColor = Color.FromArgb(201, 162, 39) };
                var lblPrecio = new Label { AutoSize = true, Location = new Point(175, 40), Name = "lblPrecio_dyn_" + p.ProductoID.ToString(), Text = p.Precio.ToString("C2"), Font = new Font("Segoe UI", 12F) };
                var imgDisponibilidad = new PictureBox { Location = new Point(10, 170), Size = new Size(160, 20), TabStop = false, Name = "imgDisponibilidad_dyn_" + p.ProductoID.ToString(), BackColor = p.Disponible ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60) };
                var lblDescripcion = new Label { Location = new Point(10, 205), Size = new Size(280, 85), Name = "lblDescripcion_dyn_" + p.ProductoID.ToString(), Text = p.Descripcion, Font = new Font("Segoe UI", 9F) };

                var btnAgregar = new Button
                {
                    Cursor = Cursors.Hand,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 11.25F),
                    Location = new Point(175, 130),
                    Name = "btnAgregar_dyn_" + p.ProductoID.ToString(),
                    Size = new Size(110, 30),
                    Text = "Agregar",
                    Tag = p
                };
                btnAgregar.Click += BtnAgregar_AddToCart_Click;

                var nudCantidad = new NumericUpDown { BorderStyle = BorderStyle.None, Location = new Point(175, 95), Name = "nudCantidad_dyn_" + p.ProductoID.ToString(), Size = new Size(145, 25), Minimum = 1, Maximum = 100, Value = 1 };
                var lblCantidadTitle = new Label { AutoSize = true, Location = new Point(175, 70), Name = "lblCantidadTitle_dyn_" + p.ProductoID.ToString(), Text = "Cantidad :", Font = new Font("Segoe UI", 12F) };

                pnl.Controls.Add(lblCantidadTitle);
                pnl.Controls.Add(nudCantidad);
                pnl.Controls.Add(btnAgregar);
                pnl.Controls.Add(lblDescripcion);
                pnl.Controls.Add(imgDisponibilidad);
                pnl.Controls.Add(lblPrecio);
                pnl.Controls.Add(lblNombre);
                pnl.Controls.Add(imgProducto);

                SectionProducts1.Controls.Add(pnl);
            }
            SectionProducts1.ResumeLayout(false);
            SectionProducts1.Refresh();
        }



        private void BtnAgregar_AddToCart_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn && btn.Tag is Product p)) return;

            int cantidad = 1;
            var parentPanel = btn.Parent as Control;
            if (parentPanel != null)
            {
                var found = parentPanel.Controls.Find("nudCantidad_dyn_" + p.ProductoID.ToString(), false);
                if (found.Length > 0 && found[0] is NumericUpDown n) cantidad = (int)n.Value;
            }

            _cart.AddOrIncrement(p, cantidad);
            RenderCartItems();

            if (TABControlador != null)
            {
                foreach (TabPage tp in TABControlador.TabPages)
                {
                    if (tp.Name == "SectionCart" || tp.Text.ToLower().Contains("cart") || tp.Text.ToLower().Contains("carrito"))
                    {
                        TABControlador.SelectedTab = tp;
                        break;
                    }
                }
            }
        }

        private void LoadCartCategories()
        {
            try
            {
                var dt = new DataTable();
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand("SELECT CategoriaID, Nombre FROM Categorias ORDER BY Nombre", conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                var newRow = dt.NewRow();
                newRow["CategoriaID"] = 0;
                newRow["Nombre"] = "Todas";
                dt.Rows.InsertAt(newRow, 0);

                cboOrdenarCategoria.SelectedIndexChanged -= cboOrdenarCategoria_SelectedIndexChanged;
                cboOrdenarCategoria.DisplayMember = "Nombre";
                cboOrdenarCategoria.ValueMember = "CategoriaID";
                cboOrdenarCategoria.DataSource = dt;
                cboOrdenarCategoria.SelectedIndex = 0;
                cboOrdenarCategoria.SelectedIndexChanged += cboOrdenarCategoria_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando categorías (filtro carrito): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Evento para aplicar filtro desde el combo
        private void cboOrdenarCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboOrdenarCategoria.SelectedValue == null)
                {
                    _cartFilterCategoriaId = null;
                }
                else
                {
                    if (int.TryParse(cboOrdenarCategoria.SelectedValue.ToString(), out int val) && val > 0)
                        _cartFilterCategoriaId = val;
                    else
                        _cartFilterCategoriaId = null;
                }

                RenderCartItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error aplicando filtro en carrito: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Modificar RenderCartItems para respetar _cartFilterCategoriaId (reemplaza el método existente)
        private void RenderCartItems()
        {
            for (int i = SectionProductsCart1.Controls.Count - 1; i >= 0; i--)
            {
                var c = SectionProductsCart1.Controls[i];
                if (c.Tag != null && c.Tag.ToString() == "dynamicCart")
                    SectionProductsCart1.Controls.RemoveAt(i);
            }

            pnlSubSectionProductCart1.Visible = false;
            pnlSubSectionProductCart2.Visible = false;

            const int panelWidth = 350;
            const int panelHeight = 300;
            const int spacingY = 20;
            const int startY = 7;
            const int xLeft = 40;
            const int xRight = 445;

            var items = _cart.ToArray();
            SectionProductsCart1.SuspendLayout();

            int visibleIndex = 0;
            for (int index = 0; index < items.Length; index++)
            {
                var item = items[index];

                // Intentar obtener CategoriaID del producto (si existe)
                int? categoriaId = null;
                try
                {
                    if (item.Producto != null)
                    {
                        // ProductModel tiene CategoriaID (se rellenó al cargar productos)
                        categoriaId = item.Producto.CategoriaID;
                    }
                }
                catch { categoriaId = null; }

                // Aplicar filtro si hay categoría seleccionada
                if (_cartFilterCategoriaId.HasValue)
                {
                    if (!categoriaId.HasValue || categoriaId.Value != _cartFilterCategoriaId.Value)
                        continue;
                }

                int column = visibleIndex % 2;
                int row = visibleIndex / 2;
                int x = (column == 0) ? xLeft : xRight;
                int y = startY + row * (panelHeight + spacingY);

                var pnl = new Panel { Size = new Size(panelWidth, panelHeight), Location = new Point(x, y), Tag = "dynamicCart", BackColor = Color.FromArgb(248, 244, 227) };

                pnl.Paint += (s, e) =>
                {
                    using (Pen pen = new Pen(Color.FromArgb(201, 162, 39), 3))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
                    }
                };

                var img = new PictureBox { Location = new Point(10, 10), Size = new Size(160, 160), SizeMode = PictureBoxSizeMode.Zoom, TabStop = false, Name = "imgProductoCart_dyn_" + item.ProductoID.ToString() };
                if (!string.IsNullOrWhiteSpace(item.Producto?.ImagenRuta))
                {
                    try
                    {
                        var path = item.Producto.ImagenRuta;
                        if (!Path.IsPathRooted(path))
                            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path.Replace('/', Path.DirectorySeparatorChar));
                        if (File.Exists(path)) img.Image = Image.FromFile(path);
                    }
                    catch { }
                }

                var lblNombre = new Label { AutoSize = true, Location = new Point(175, 10), Text = item.Producto?.Nombre ?? "", Font = new Font("Segoe UI", 12F), Name = "lblNombreCart_dyn_" + item.ProductoID.ToString(), ForeColor = Color.FromArgb(201, 162, 39) };
                var lblPrecio = new Label { AutoSize = true, Location = new Point(175, 40), Text = (item.Producto != null ? item.Producto.Precio.ToString("C2") : ""), Font = new Font("Segoe UI", 12F), Name = "lblPrecioCart_dyn_" + item.ProductoID.ToString() };
                var imgDisp = new PictureBox { Location = new Point(10, 170), Size = new Size(160, 20), BackColor = (item.Producto != null && item.Producto.Disponible) ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60), TabStop = false, Name = "imgDisponibilidadCart_dyn_" + item.ProductoID.ToString() };
                var lblDescripcion = new Label { Location = new Point(10, 205), Size = new Size(280, 85), Text = item.Producto?.Descripcion ?? "", Font = new Font("Segoe UI", 9F), Name = "lblDescripcionCart_dyn_" + item.ProductoID.ToString() };

                var btnEliminar = new Button { Cursor = Cursors.Hand, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11.25F), Location = new Point(175, 130), Name = "btnEliminarCart_dyn_" + item.ProductoID.ToString(), Size = new Size(110, 30), Text = "Eliminar", Tag = item.ProductoID };
                btnEliminar.Click += BtnEliminarCart_Click;

                var nudCantidad = new NumericUpDown { BorderStyle = BorderStyle.None, Location = new Point(175, 95), Name = "nudCantidadCart_dyn_" + item.ProductoID.ToString(), Size = new Size(145, 25), Minimum = 1, Maximum = 999, Value = item.Cantidad, Tag = item.ProductoID };
                nudCantidad.ValueChanged += NudCantidadCart_ValueChanged;

                var lblCantidadTitle = new Label { AutoSize = true, Location = new Point(175, 70), Text = "Cantidad :", Font = new Font("Segoe UI", 12F), Name = "lblCantidadCartTitle_dyn_" + item.ProductoID.ToString() };

                pnl.Controls.Add(lblCantidadTitle);
                pnl.Controls.Add(nudCantidad);
                pnl.Controls.Add(btnEliminar);
                pnl.Controls.Add(lblDescripcion);
                pnl.Controls.Add(imgDisp);
                pnl.Controls.Add(lblPrecio);
                pnl.Controls.Add(lblNombre);
                pnl.Controls.Add(img);

                SectionProductsCart1.Controls.Add(pnl);

                visibleIndex++;
            }

            SectionProductsCart1.ResumeLayout(false);
            SectionProductsCart1.Refresh();
        }


        private void NudCantidadCart_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown nud)
            {
                if (nud.Tag != null && int.TryParse(nud.Tag.ToString(), out int pid))
                {
                    _cart.UpdateQuantity(pid, (int)nud.Value);
                }
            }
        }

        private void BtnEliminarCart_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag != null)
            {
                int productoId = 0;
                if (btn.Tag is int id) productoId = id;
                else if (!int.TryParse(btn.Tag.ToString(), out productoId)) return;

                _cart.Remove(productoId);
                RenderCartItems();
            }
        }


        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Product p) BtnAgregar_AddToCart_Click(sender, e);
        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboCategoria.SelectedValue == null) return;
                int selected = 0;
                if (!int.TryParse(cboCategoria.SelectedValue.ToString(), out selected)) selected = 0;
                var products = (selected > 0) ? LoadProductsFromDb(selected) : LoadProductsFromDb(null);
                RenderProducts(products);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtrando por categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Carga mesas con Estado = 'Libre' en cboMesa e inserta una opción "Ninguna"
        private void LoadMesas()
        {
            try
            {
                _mesasTable = new DataTable();
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand("SELECT MesaID, NumeroMesa, Capacidad FROM Mesas WHERE Estado = 'Libre' ORDER BY NumeroMesa", conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(_mesasTable);
                }

                // añadir columna de display (string) y rellenar con el número de mesa
                if (!_mesasTable.Columns.Contains("Display"))
                    _mesasTable.Columns.Add("Display", typeof(string));

                foreach (DataRow r in _mesasTable.Rows)
                {
                    var numero = r["NumeroMesa"];
                    r["Display"] = (numero == DBNull.Value) ? string.Empty : numero.ToString();
                }

                // insertar opción "Ninguna" al inicio
                var none = _mesasTable.NewRow();
                none["MesaID"] = 0;
                none["NumeroMesa"] = DBNull.Value;
                none["Capacidad"] = DBNull.Value;
                none["Display"] = "Ninguna";
                _mesasTable.Rows.InsertAt(none, 0);

                cboMesa.SelectedIndexChanged -= cboMesa_SelectedIndexChanged;
                cboMesa.DisplayMember = "Display";
                cboMesa.ValueMember = "MesaID";
                cboMesa.DataSource = _mesasTable;
                cboMesa.SelectedIndex = 0;
                cboMesa.SelectedIndexChanged += cboMesa_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando mesas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Al seleccionar una mesa actualiza lblCapacidad1, lblCapacidad2 y lblMesa
        private void cboMesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboMesa.SelectedItem == null)
                {
                    // limpiar si no hay selección
                    lblCapacidad1.Text = string.Empty;
                    lblCapacidad2.Text = string.Empty;
                    lblMesa.Text = string.Empty;
                    return;
                }

                // si DataRowView (venimos del DataTable including "Ninguna")
                if (cboMesa.SelectedItem is DataRowView drv)
                {
                    var mesaIdObj = drv["MesaID"];
                    int mesaId = (mesaIdObj == DBNull.Value) ? 0 : Convert.ToInt32(mesaIdObj);

                    if (mesaId == 0)
                    {
                        // opción "Ninguna"
                        lblCapacidad1.Text = string.Empty;
                        lblCapacidad2.Text = string.Empty;
                        lblMesa.Text = "Ninguna";
                        return;
                    }

                    var numeroObj = drv["NumeroMesa"];
                    var capObj = drv["Capacidad"];
                    var numeroStr = numeroObj == DBNull.Value ? string.Empty : numeroObj.ToString();
                    var capStr = capObj == DBNull.Value ? "0" : capObj.ToString();
                    lblCapacidad1.Text = capStr;
                    lblCapacidad2.Text = capStr;
                    lblMesa.Text = numeroStr;
                    return;
                }

                // fallback: usar SelectedValue (por si se enlazó a otro tipo)
                int mesaIdValue;
                if (cboMesa.SelectedValue != null && int.TryParse(cboMesa.SelectedValue.ToString(), out mesaIdValue))
                {
                    if (mesaIdValue == 0)
                    {
                        lblCapacidad1.Text = string.Empty;
                        lblCapacidad2.Text = string.Empty;
                        lblMesa.Text = "Ninguna";
                        return;
                    }

                    // Obtener datos actualizados de la BD (por si cambió el estado o capacidad)
                    using (var conn = new SqlConnection(ConnectionString))
                    using (var cmd = new SqlCommand("SELECT NumeroMesa, Capacidad FROM Mesas WHERE MesaID = @MesaID", conn))
                    {
                        cmd.Parameters.AddWithValue("@MesaID", mesaIdValue);
                        conn.Open();
                        using (var rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                var numero = rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0);
                                var capacidad = rdr.IsDBNull(1) ? 0 : rdr.GetInt32(1);

                                // Actualizar etiquetas (referencias generadas por el diseñador)
                                lblCapacidad1.Text = lblCapacidad2.Text = capacidad.ToString();
                                lblMesa.Text = numero.ToString();
                            }
                            else
                            {
                                // Si no existe, limpiar
                                lblCapacidad1.Text = string.Empty;
                                lblCapacidad2.Text = string.Empty;
                                lblMesa.Text = string.Empty;
                            }
                        }
                    }
                    return;
                }

                // no se pudo resolver: limpiar
                lblCapacidad1.Text = string.Empty;
                lblCapacidad2.Text = string.Empty;
                lblMesa.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar mesa: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


       

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            //FrmMenu regresar
            MenuPrincipal frmMenu = new MenuPrincipal();
            frmMenu.Show();
            this.Close();
        }

        private void btnRealizarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar carrito
                if (_cart.Count == 0)
                {
                    MessageBox.Show("El carrito está vacío. Agrega productos antes de realizar un pedido.", "Carrito vacío", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Validar mesa seleccionada (no "Ninguna")
                int mesaId = 0;
                if (cboMesa.SelectedValue == null || !int.TryParse(cboMesa.SelectedValue.ToString(), out mesaId) || mesaId <= 0)
                {
                    MessageBox.Show("Debe seleccionar una mesa antes de realizar el pedido.", "Seleccione mesa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // llevar foco al combo
                    TABControlador.SelectedTab = SectionOrder;
                    cboMesa.Focus();
                    return;
                }

                // comprobar disponibilidad actual en la BD usando los IDs del carrito
                var cartItems = _cart.ToArray();
                var productIds = cartItems.Select(i => i.ProductoID).Distinct().ToArray();
                if (productIds.Length > 0)
                {
                    using (var conn = new SqlConnection(ConnectionString))
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        // crear parámetros seguros para la cláusula IN
                        var paramNames = productIds.Select((id, idx) =>
                        {
                            var name = "@p" + idx;
                            cmd.Parameters.AddWithValue(name, id);
                            return name;
                        }).ToArray();

                        cmd.CommandText = "SELECT ProductoID, Nombre, Disponible FROM Productos WHERE ProductoID IN (" + string.Join(",", paramNames) + ")";

                        conn.Open();
                        var unavailable = new System.Collections.Generic.List<string>();
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                int pid = rdr.GetInt32(0);
                                string nombre = rdr.IsDBNull(1) ? ("ProductoID " + pid) : rdr.GetString(1);
                                bool disponible = !rdr.IsDBNull(2) && rdr.GetBoolean(2);
                                if (!disponible)
                                {
                                    unavailable.Add(string.Format("{0} (ID:{1})", nombre, pid));
                                }
                            }
                        }

                        if (unavailable.Count > 0)
                        {
                            MessageBox.Show("No se puede realizar el pedido porque los siguientes productos no están disponibles:\r\n- " + string.Join("\r\n- ", unavailable), "Producto no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }


                // Insertar pedido y detalles en BD dentro de transacción
                int newPedidoId = 0;
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // Insert Pedido (FechaHora se toma GETDATE())
                            using (var cmd = new SqlCommand("INSERT INTO Pedidos (MesaID, FechaHora, EstadoCocina, Prioridad) VALUES (@MesaID, GETDATE(), 'En Cola', 1); SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@MesaID", mesaId);
                                var obj = cmd.ExecuteScalar();
                                newPedidoId = (obj == null || obj == DBNull.Value) ? 0 : Convert.ToInt32(obj);
                            }

                            if (newPedidoId <= 0) throw new Exception("No se pudo insertar el pedido.");

                            // Insertar DetallePedido por cada item en el carrito
                            var items = _cart.ToArray();
                            foreach (var it in items)
                            {
                                // INSERTAR DETALLE PEDIDO
                                using (var cmdDet = new SqlCommand(@"
                                    INSERT INTO DetallePedido
                                    (PedidoID, ProductoID, Cantidad, NotasEspeciales)
                                    VALUES
                                    (@PedidoID, @ProductoID, @Cantidad, @Notas)", conn, tran))
                                {
                                    cmdDet.Parameters.AddWithValue("@PedidoID", newPedidoId);
                                    cmdDet.Parameters.AddWithValue("@ProductoID", it.ProductoID);
                                    cmdDet.Parameters.AddWithValue("@Cantidad", it.Cantidad);
                                    cmdDet.Parameters.AddWithValue("@Notas", DBNull.Value);

                                    cmdDet.ExecuteNonQuery();
                                }

                                // =========================================
                                // DESCONTAR STOCK DE INSUMOS
                                // =========================================

                                using (var cmdStock = new SqlCommand(@"
                                    UPDATE I
                                    SET I.StockActual =
                                        I.StockActual - (R.CantidadRequerida * @Cantidad)

                                    FROM Insumos I
                                    INNER JOIN Recetas R
                                        ON I.InsumoID = R.InsumoID

                                    WHERE R.ProductoID = @ProductoID
                                ", conn, tran))
                                {
                                    cmdStock.Parameters.AddWithValue("@Cantidad", it.Cantidad);
                                    cmdStock.Parameters.AddWithValue("@ProductoID", it.ProductoID);

                                    cmdStock.ExecuteNonQuery();
                                }
                            }

                            // Marcar mesa como ocupada (opcional pero recomendable)
                            using (var cmdUpd = new SqlCommand("UPDATE Mesas SET Estado = 'Ocupado' WHERE MesaID = @MesaID", conn, tran))
                            {
                                cmdUpd.Parameters.AddWithValue("@MesaID", mesaId);
                                cmdUpd.ExecuteNonQuery();
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            try { tran.Rollback(); } catch { }
                            throw;
                        }
                    }
                }

                // RECARGAR PRODUCTOS para reflejar cambios en stock (si corresponde)
                int selected = 0;
                if (cboCategoria.SelectedValue != null)
                    int.TryParse(cboCategoria.SelectedValue.ToString(), out selected);

                var products = (selected > 0)
                    ? LoadProductsFromDb(selected)
                    : LoadProductsFromDb(null);

                RenderProducts(products);

                // Limpieza del carrito y actualización UI
                _cart.Clear();
                RenderCartItems();

                MessageBox.Show("Pedido registrado correctamente. Nº Pedido: " + newPedidoId.ToString(), "Pedido creado", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar el pedido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
