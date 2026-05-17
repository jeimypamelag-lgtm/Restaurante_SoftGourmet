using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using Restaurante_SoftGourmet.Clases.Carrito;

namespace Restaurante_SoftGourmet
{
    public partial class ModuloAlmacen : Form
    {
        // Definir la cadena de conexión usada por los métodos que abren SqlConnection
        private const string ConnectionString = "Server=.;Database=SoftGourmetDB;Trusted_Connection=True;";

        // Reloj para actualizar la fecha/hora en pantalla
        private Timer _clockTimer;

        public ModuloAlmacen()
        {
            InitializeComponent();
            // Asegurar que el Load esté enlazado para poblar el ComboBox al abrir el formulario
            this.Load += ModuloAlmacen_Load;
            this.FormClosing += ModuloAlmacen_FormClosing;

            // Opcional: asegurar estilo y visibilidad del combo (evita que el usuario pueda editar texto)
            if (cboSeleccionarTabla != null)
            {
                cboSeleccionarTabla.DropDownStyle = ComboBoxStyle.DropDownList;
                cboSeleccionarTabla.Visible = true;
            }
        }


        private string _currentTable;
        private readonly string[] _tables = new[] { "Insumos", "Productos", "Recetas", "Mesas", "Categorias" };

        private void ModuloAlmacen_Load(object sender, EventArgs e)
        {
            // Inicializar combo y grid
            cboSeleccionarTabla.Items.Clear();
            cboSeleccionarTabla.Items.AddRange(_tables);
            cboSeleccionarTabla.SelectedIndexChanged -= CboSeleccionarTabla_SelectedIndexChanged;
            cboSeleccionarTabla.SelectedIndexChanged += CboSeleccionarTabla_SelectedIndexChanged;
            cboSeleccionarTabla.SelectedIndex = 0;

            dgvBD.ReadOnly = true;
            dgvBD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBD.MultiSelect = false;
            dgvBD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBD.AllowUserToAddRows = false;

            btnIngresar.Click -= btnIngresar_Click;
            btnEditar.Click -= btnEditar_Click;
            btnIngresar.Click += btnIngresar_Click;
            btnEditar.Click += btnEditar_Click;

            // cargar datos iniciales
            LoadSelectedTableData();

            // Inicializar y arrancar reloj (misma lógica que en FrmModuloComensal)
            try
            {
                UpdateFechaActual();
                _clockTimer = new Timer { Interval = 1000 };
                _clockTimer.Tick += ClockTimer_Tick;
                _clockTimer.Start();
            }
            catch
            {
                // no bloquear la carga si el reloj falla
            }
        }

        private void ModuloAlmacen_FormClosing(object sender, FormClosingEventArgs e)
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

                // Intentar actualizar lblFechaActual1 (y compatibilidad con otros nombres)
                var l1 = FindControlRecursive(this, "lblFechaActual1") as Label;
                var l2 = FindControlRecursive(this, "lblFechaActual2") as Label;

                if (l1 != null) l1.Text = texto;
                if (l2 != null) l2.Text = texto;

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


        private void CboSeleccionarTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedTableData();
        }

        private void LoadSelectedTableData()
        {
            _currentTable = cboSeleccionarTabla.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(_currentTable)) return;

            var dt = new DataTable();
            string sql = null;

            switch (_currentTable)
            {
                case "Insumos":
                    sql = "SELECT InsumoID, Nombre, StockActual, StockMinimo, UnidadMedida FROM Insumos ORDER BY InsumoID";
                    break;
                case "Productos":
                    sql = @"SELECT p.ProductoID, p.Nombre, p.Precio, p.ImagenRuta, p.CategoriaID, c.Nombre AS Categoria, p.Disponible, p.Descripcion
                            FROM Productos p LEFT JOIN Categorias c ON p.CategoriaID = c.CategoriaID
                            ORDER BY p.ProductoID";
                    break;
                case "Recetas":
                    sql = @"SELECT r.RecetaID, r.ProductoID, p.Nombre AS Producto, r.InsumoID, i.Nombre AS Insumo, r.CantidadRequerida
                            FROM Recetas r
                            LEFT JOIN Productos p ON r.ProductoID = p.ProductoID
                            LEFT JOIN Insumos i ON r.InsumoID = i.InsumoID
                            ORDER BY r.RecetaID";
                    break;
                case "Mesas":
                    sql = "SELECT MesaID, NumeroMesa, Estado, Capacidad FROM Mesas ORDER BY NumeroMesa";
                    break;
                case "Categorias":
                    sql = "SELECT CategoriaID, Nombre, ImagenRuta FROM Categorias ORDER BY CategoriaID";
                    break;
                default:
                    return;
            }

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand(sql, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                dgvBD.DataSource = dt;
                dgvBD.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // limpiar panel de edición al cambiar tabla
            pnlEditar.Controls.Clear();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            BuildEditor(isEdit: false, selectedRow: null);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvBD.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila del grid para editar.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvBD.SelectedRows[0];
            BuildEditor(isEdit: true, selectedRow: row);
        }

        private void BuildEditor(bool isEdit, DataGridViewRow selectedRow)
        {
            pnlEditar.Controls.Clear();
            pnlEditar.AutoScroll = true;

            var title = new Label
            {
                Text = (isEdit ? "Editar: " : "Ingresar: ") + _currentTable,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 32,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlEditar.Controls.Add(title);

            // helper: crear control y devolverlo (name = campo, tag = campo)
            Control CreateLabelledControl(string labelText, Control inputControl, int top)
            {
                var lbl = new Label { Text = labelText, Location = new Point(8, top), AutoSize = true, Font = new Font("Segoe UI", 9F) };
                inputControl.Location = new Point(120, top - 3);
                inputControl.Width = 200;
                pnlEditar.Controls.Add(lbl);
                pnlEditar.Controls.Add(inputControl);
                return inputControl;
            }

            // helper para cargar lookups
            DataTable LoadLookup(string sql)
            {
                var dt = new DataTable();
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand(sql, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }

            string pkName = null;
            object pkValue = null;
            int topPos = 40;

            switch (_currentTable)
            {
                case "Insumos":
                    pkName = "InsumoID";
                    var tbNombreI = new TextBox { Tag = "Nombre" };
                    var tbStockActual = new TextBox { Tag = "StockActual" };
                    var tbStockMinimo = new TextBox { Tag = "StockMinimo" };
                    var tbUnidad = new TextBox { Tag = "UnidadMedida" };

                    if (isEdit && selectedRow != null)
                    {
                        pkValue = selectedRow.Cells["InsumoID"].Value;
                        tbNombreI.Text = Convert.ToString(selectedRow.Cells["Nombre"].Value);
                        tbStockActual.Text = Convert.ToString(selectedRow.Cells["StockActual"].Value);
                        tbStockMinimo.Text = Convert.ToString(selectedRow.Cells["StockMinimo"].Value);
                        tbUnidad.Text = Convert.ToString(selectedRow.Cells["UnidadMedida"].Value);
                    }

                    CreateLabelledControl("Nombre:", tbNombreI, topPos); topPos += 30;
                    CreateLabelledControl("Stock actual:", tbStockActual, topPos); topPos += 30;
                    CreateLabelledControl("Stock mínimo:", tbStockMinimo, topPos); topPos += 30;
                    CreateLabelledControl("Unidad:", tbUnidad, topPos); topPos += 30;
                    break;

                case "Categorias":
                    pkName = "CategoriaID";
                    var tbNombreC = new TextBox { Tag = "Nombre" };
                    var tbImagenC = new TextBox { Tag = "ImagenRuta" };

                    if (isEdit && selectedRow != null)
                    {
                        pkValue = selectedRow.Cells["CategoriaID"].Value;
                        tbNombreC.Text = Convert.ToString(selectedRow.Cells["Nombre"].Value);
                        tbImagenC.Text = Convert.ToString(selectedRow.Cells["ImagenRuta"].Value);
                    }

                    CreateLabelledControl("Nombre:", tbNombreC, topPos); topPos += 30;
                    CreateLabelledControl("Imagen ruta:", tbImagenC, topPos); topPos += 30;
                    break;

                case "Productos":
                    pkName = "ProductoID";
                    var tbNombreP = new TextBox { Tag = "Nombre" };
                    var tbPrecio = new TextBox { Tag = "Precio" };
                    var tbImagenP = new TextBox { Tag = "ImagenRuta" };
                    var cboCat = new ComboBox { Tag = "CategoriaID", DropDownStyle = ComboBoxStyle.DropDownList };
                    var chkDisp = new CheckBox { Tag = "Disponible", Text = "Disponible" };
                    var tbDesc = new TextBox { Tag = "Descripcion", Multiline = true, Height = 60 };

                    // cargar categorias
                    var dtCats = LoadLookup("SELECT CategoriaID, Nombre FROM Categorias ORDER BY Nombre");
                    cboCat.DisplayMember = "Nombre";
                    cboCat.ValueMember = "CategoriaID";
                    cboCat.DataSource = dtCats;

                    if (isEdit && selectedRow != null)
                    {
                        pkValue = selectedRow.Cells["ProductoID"].Value;
                        tbNombreP.Text = Convert.ToString(selectedRow.Cells["Nombre"].Value);
                        tbPrecio.Text = Convert.ToString(selectedRow.Cells["Precio"].Value);
                        tbImagenP.Text = Convert.ToString(selectedRow.Cells["ImagenRuta"].Value);

                        // lectura segura de 'Disponible'
                        var dispVal = selectedRow.Cells["Disponible"].Value;
                        bool dispon = false;
                        if (dispVal is bool bb) dispon = bb;
                        else if (dispVal != null && dispVal != DBNull.Value)
                        {
                            var s = dispVal.ToString();
                            dispon = s == "1" || s.Equals("true", StringComparison.OrdinalIgnoreCase) || s.Equals("yes", StringComparison.OrdinalIgnoreCase);
                        }
                        chkDisp.Checked = dispon;

                        tbDesc.Text = Convert.ToString(selectedRow.Cells["Descripcion"].Value);

                        // seleccionar categoría si existe
                        var catVal = selectedRow.Cells["CategoriaID"].Value;
                        if (catVal != null && catVal != DBNull.Value && int.TryParse(catVal.ToString(), out int catId))
                        {
                            try
                            {
                                cboCat.SelectedValue = catId;
                            }
                            catch
                            {
                                // fallback: encontrar fila en dtCats
                                foreach (DataRow dr in dtCats.Rows)
                                {
                                    if (Convert.ToInt32(dr["CategoriaID"]) == catId)
                                    {
                                        cboCat.SelectedValue = catId;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    CreateLabelledControl("Nombre:", tbNombreP, topPos); topPos += 30;
                    CreateLabelledControl("Precio:", tbPrecio, topPos); topPos += 30;
                    CreateLabelledControl("Imagen ruta:", tbImagenP, topPos); topPos += 30;
                    CreateLabelledControl("Categoría:", cboCat, topPos); topPos += 30;
                    CreateLabelledControl("", chkDisp, topPos); topPos += 30;
                    CreateLabelledControl("Descripción:", tbDesc, topPos); topPos += 70;
                    break;

                case "Recetas":
                    pkName = "RecetaID";
                    var cboProductos = new ComboBox { Tag = "ProductoID", DropDownStyle = ComboBoxStyle.DropDownList };
                    var cboInsumos = new ComboBox { Tag = "InsumoID", DropDownStyle = ComboBoxStyle.DropDownList };
                    var tbCantidadReq = new TextBox { Tag = "CantidadRequerida" };

                    var dtProds = LoadLookup("SELECT ProductoID, Nombre FROM Productos ORDER BY Nombre");
                    var dtInsum = LoadLookup("SELECT InsumoID, Nombre FROM Insumos ORDER BY Nombre");

                    cboProductos.DisplayMember = "Nombre"; cboProductos.ValueMember = "ProductoID"; cboProductos.DataSource = dtProds;
                    cboInsumos.DisplayMember = "Nombre"; cboInsumos.ValueMember = "InsumoID"; cboInsumos.DataSource = dtInsum;

                    if (isEdit && selectedRow != null)
                    {
                        pkValue = selectedRow.Cells["RecetaID"].Value;
                        // intentar seleccionar por id
                        object prodVal = selectedRow.Cells["ProductoID"].Value;
                        object insVal = selectedRow.Cells["InsumoID"].Value;
                        if (prodVal != null && int.TryParse(prodVal.ToString(), out int pid))
                            cboProductos.SelectedValue = pid;
                        if (insVal != null && int.TryParse(insVal.ToString(), out int iid))
                            cboInsumos.SelectedValue = iid;
                        tbCantidadReq.Text = Convert.ToString(selectedRow.Cells["CantidadRequerida"].Value);
                    }

                    CreateLabelledControl("Producto:", cboProductos, topPos); topPos += 30;
                    CreateLabelledControl("Insumo:", cboInsumos, topPos); topPos += 30;
                    CreateLabelledControl("Cantidad req.:", tbCantidadReq, topPos); topPos += 30;
                    break;

                case "Mesas":
                    pkName = "MesaID";
                    var nudNumero = new TextBox { Tag = "NumeroMesa" };
                    var cboEstado = new ComboBox { Tag = "Estado", DropDownStyle = ComboBoxStyle.DropDownList };
                    cboEstado.Items.AddRange(new object[] { "Libre", "Ocupado", "Reservada" });
                    var nudCapacidad = new TextBox { Tag = "Capacidad" };

                    if (isEdit && selectedRow != null)
                    {
                        pkValue = selectedRow.Cells["MesaID"].Value;
                        nudNumero.Text = Convert.ToString(selectedRow.Cells["NumeroMesa"].Value);
                        cboEstado.SelectedItem = Convert.ToString(selectedRow.Cells["Estado"].Value);
                        nudCapacidad.Text = Convert.ToString(selectedRow.Cells["Capacidad"].Value);
                    }

                    CreateLabelledControl("Número mesa:", nudNumero, topPos); topPos += 30;
                    CreateLabelledControl("Estado:", cboEstado, topPos); topPos += 30;
                    CreateLabelledControl("Capacidad:", nudCapacidad, topPos); topPos += 30;
                    break;
            }

            // botones guardar / cancelar
            var btnGuardar = new Button { Text = "Guardar", Width = 90, Location = new Point(120, topPos + 10) };
            btnGuardar.Tag = Tuple.Create(_currentTable, isEdit, pkName, pkValue);
            btnGuardar.Click += BtnGuardar_Click;
            pnlEditar.Controls.Add(btnGuardar);

            var btnCancelar = new Button { Text = "Cancelar", Width = 90, Location = new Point(230, topPos + 10) };
            btnCancelar.Click += (s, e) => pnlEditar.Controls.Clear();
            pnlEditar.Controls.Add(btnCancelar);
        }


        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var info = btn.Tag as Tuple<string, bool, string, object>;
            if (info == null) return;

            string table = info.Item1;
            bool isEdit = info.Item2;
            string pkName = info.Item3;
            object pkValue = info.Item4;

            // recolectar valores desde pnlEditar: controles con Tag = columna
            var columns = new List<string>();
            var parameters = new Dictionary<string, object>();

            foreach (Control c in pnlEditar.Controls)
            {
                if (c.Tag == null) continue;
                string col = c.Tag.ToString();
                object val = null;

                if (c is TextBox tb)
                    val = string.IsNullOrWhiteSpace(tb.Text) ? (object)DBNull.Value : (object)tb.Text.Trim();
                else if (c is CheckBox ch)
                    val = ch.Checked ? 1 : 0;
                else if (c is ComboBox cb)
                    val = cb.SelectedValue ?? (object)DBNull.Value;
                else
                    continue;

                columns.Add(col);
                parameters[col] = val;
            }

            if (columns.Count == 0)
            {
                MessageBox.Show("No hay campos para guardar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    if (!isEdit)
                    {
                        // INSERT
                        var colsJoined = string.Join(", ", columns);
                        var paramsJoined = string.Join(", ", columns.Select(c => "@" + c));
                        cmd.CommandText = $"INSERT INTO {table} ({colsJoined}) VALUES ({paramsJoined})";
                        foreach (var kv in parameters) cmd.Parameters.AddWithValue("@" + kv.Key, kv.Value ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // UPDATE
                        if (string.IsNullOrEmpty(pkName) || pkValue == null)
                        {
                            MessageBox.Show("Clave primaria no encontrada para la operación de edición.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        var setList = string.Join(", ", columns.Select(c => $"{c} = @{c}"));
                        cmd.CommandText = $"UPDATE {table} SET {setList} WHERE {pkName} = @pk";
                        foreach (var kv in parameters) cmd.Parameters.AddWithValue("@" + kv.Key, kv.Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@pk", pkValue);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Operación realizada correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlEditar.Controls.Clear();
                LoadSelectedTableData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error guardando: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            //FrmMenu regresar
            MenuEmpleados frmMenu = new MenuEmpleados();
            frmMenu.Show();
            this.Close();
        }
    }
}
