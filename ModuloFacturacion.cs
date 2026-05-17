using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurante_SoftGourmet.Clases.Carrito;

namespace Restaurante_SoftGourmet
{
    public partial class ModuloFacturacion : Form
    {
        private const string ConnectionString = "Server=.;Database=SoftGourmetDB;Trusted_Connection=True;";

        private Timer _clockTimer;
        private DataTable _pedidosTable;
        private DataTable _detallesTable;
        private int? _selectedPedidoId;

        // Campos para impresión
        private int _printRowIndex;
        private System.Collections.Generic.List<System.Tuple<string, int, decimal, decimal>> _printItems;
        private decimal _printSubtotal;
        private decimal _printImpuesto;
        private decimal _printPropina;
        private decimal _printTotal;
        private int _printPedidoId;
        private string _printMesa;
        private string _printFecha;
        private string _printEstado;
        private string _printPrioridad;

        public ModuloFacturacion()
        {
            InitializeComponent();
            this.Load += ModuloFacturacion_Load;
            this.FormClosing += ModuloFacturacion_FormClosing;
        }

        private void ModuloFacturacion_Load(object sender, EventArgs e)
        {
            try
            {
                // configurar controles
                dgvPedidos.ReadOnly = true;
                dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPedidos.MultiSelect = false;
                dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPedidos.AllowUserToAddRows = false;

                dgvDetallesPedido.ReadOnly = true;
                dgvDetallesPedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDetallesPedido.MultiSelect = false;
                dgvDetallesPedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDetallesPedido.AllowUserToAddRows = false;

                cboMetodoPago.Items.Clear();
                cboMetodoPago.Items.Add("Efectivo");
                cboMetodoPago.Items.Add("Tarjeta (Débito/Crédito)");
                cboMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;

                txtDineroRecibido.Enabled = false;

                // eventos
                dgvPedidos.SelectionChanged -= dgvPedidos_SelectionChanged;
                dgvPedidos.SelectionChanged += dgvPedidos_SelectionChanged;

                cboMetodoPago.SelectedIndexChanged -= cboMetodoPago_SelectedIndexChanged;
                cboMetodoPago.SelectedIndexChanged += cboMetodoPago_SelectedIndexChanged;

                txtDineroRecibido.TextChanged -= txtDineroRecibido_TextChanged;
                txtDineroRecibido.TextChanged += txtDineroRecibido_TextChanged;

                btnGenerarFactura.Click -= btnGenerarFactura_Click;
                btnImprimirTicket.Click -= btnImprimirTicket_Click;
                btnCancelar.Click -= btnCancelar_Click;
                btnBusqueda1.Click -= btnBusqueda1_Click;
                btnBusqueda2.Click -= btnBusqueda2_Click;

                btnGenerarFactura.Click += btnGenerarFactura_Click;
                btnImprimirTicket.Click += btnImprimirTicket_Click;
                btnCancelar.Click += btnCancelar_Click;
                btnBusqueda1.Click += btnBusqueda1_Click;
                btnBusqueda2.Click += btnBusqueda2_Click;

                // iniciar reloj
                UpdateFechaActual();
                _clockTimer = new Timer { Interval = 1000 };
                _clockTimer.Tick += ClockTimer_Tick;
                _clockTimer.Start();

                LoadPedidos();
                ClearSelectionVisual();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inicializando módulo de facturación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModuloFacturacion_FormClosing(object sender, FormClosingEventArgs e)
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

        private void UpdateFechaActual()
        {
            try
            {
                var ahora = DateTime.Now;
                if (lblFechaActual1 != null)
                    lblFechaActual1.Text = ahora.ToString("dddd, d 'de' MMMM 'de' yyyy HH:mm:ss", new CultureInfo("es-ES"));
            }
            catch
            {
                if (lblFechaActual1 != null)
                    lblFechaActual1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }


        private void LoadPedidos()
        {
            try
            {
                // Cargar todos los pedidos (incluye entregados y no entregados)
                const string sql = @"SELECT p.PedidoID, p.MesaID, m.NumeroMesa, p.FechaHora, p.EstadoCocina, p.Prioridad,
                                   (SELECT COUNT(*) FROM DetallePedido d WHERE d.PedidoID = p.PedidoID) AS ItemsCount
                                   FROM Pedidos p
                                   LEFT JOIN Mesas m ON p.MesaID = m.MesaID
                                   ORDER BY p.Prioridad DESC, p.FechaHora ASC;";

                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand(sql, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    _pedidosTable = new DataTable();
                    conn.Open();
                    da.Fill(_pedidosTable);
                    dgvPedidos.DataSource = _pedidosTable;

                    // Ajustes visuales: ocultar columnas internas si existen
                    if (_pedidosTable.Columns.Contains("MesaID"))
                        dgvPedidos.Columns["MesaID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando pedidos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPedidos.SelectedRows.Count == 0)
            {
                ClearSelectionVisual();
                return;
            }

            try
            {
                var row = dgvPedidos.SelectedRows[0];
                if (row == null) { ClearSelectionVisual(); return; }

                // intentar obtener PedidoID
                object val = null;
                if (row.Cells["PedidoID"] != null)
                    val = row.Cells["PedidoID"].Value;
                else if (row.Cells[0] != null)
                    val = row.Cells[0].Value;

                int pid;
                if (val == null || !int.TryParse(Convert.ToString(val), out pid))
                {
                    ClearSelectionVisual();
                    return;
                }

                _selectedPedidoId = pid;

                // poblar labels de cabecera
                object numMesaObj = null;
                if (row.Cells["NumeroMesa"] != null) numMesaObj = row.Cells["NumeroMesa"].Value;
                else if (row.Cells["NumeroMesa"] == null && row.Cells["MesaID"] != null) numMesaObj = row.Cells["MesaID"].Value;

                lblNMesa.Text = (numMesaObj == DBNull.Value || numMesaObj == null) ? "N/A" : Convert.ToString(numMesaObj);

                object fechaObj = row.Cells["FechaHora"] != null ? row.Cells["FechaHora"].Value : null;
                if (fechaObj != null && fechaObj != DBNull.Value)
                {
                    DateTime f = Convert.ToDateTime(fechaObj);
                    lblFechaPedido.Text = f.ToString("g", new CultureInfo("es-ES"));
                }
                else
                {
                    lblFechaPedido.Text = "";
                }

                lblEstado.Text = row.Cells["EstadoCocina"] != null && row.Cells["EstadoCocina"].Value != null ? Convert.ToString(row.Cells["EstadoCocina"].Value) : "";
                lblPrioridad.Text = row.Cells["Prioridad"] != null && row.Cells["Prioridad"].Value != null ? Convert.ToString(row.Cells["Prioridad"].Value) : "";

                LoadPedidoDetalles(pid);

                // --- Nueva validación: si el pedido ya está facturado, bloquear btnGenerarFactura ---
                if (!string.IsNullOrWhiteSpace(lblEstado.Text) && lblEstado.Text.Trim().Equals("Facturado", StringComparison.OrdinalIgnoreCase))
                {
                    btnGenerarFactura.Enabled = false;
                }
                else
                {
                    btnGenerarFactura.Enabled = true;
                }

                // Permitir imprimir si existen detalles del pedido; btnImprimirTicket permanece disponible aun si está facturado
                btnImprimirTicket.Enabled = (_detallesTable != null && _detallesTable.Rows.Count > 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error procesando selección: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPedidoDetalles(int pedidoId)
        {
            try
            {
                const string sql = @"SELECT d.DetalleID, d.PedidoID, d.ProductoID, prod.Nombre AS ProductoNombre, d.Cantidad, d.NotasEspeciales, prod.Precio
                                     FROM DetallePedido d
                                     LEFT JOIN Productos prod ON d.ProductoID = prod.ProductoID
                                     WHERE d.PedidoID = @pid;";

                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand(sql, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@pid", pedidoId);
                    _detallesTable = new DataTable();
                    conn.Open();
                    da.Fill(_detallesTable);
                    dgvDetallesPedido.DataSource = _detallesTable;

                    // columnas no necesarias
                    if (_detallesTable.Columns.Contains("DetalleID")) dgvDetallesPedido.Columns["DetalleID"].Visible = false;
                    if (_detallesTable.Columns.Contains("PedidoID")) dgvDetallesPedido.Columns["PedidoID"].Visible = false;
                    if (_detallesTable.Columns.Contains("ProductoID")) dgvDetallesPedido.Columns["ProductoID"].Visible = false;

                    // calcular totales
                    decimal subtotal = 0m;
                    int totalItems = 0;
                    foreach (DataRow r in _detallesTable.Rows)
                    {
                        decimal precio = r["Precio"] == DBNull.Value ? 0m : Convert.ToDecimal(r["Precio"]);
                        int cantidad = r["Cantidad"] == DBNull.Value ? 0 : Convert.ToInt32(r["Cantidad"]);
                        subtotal += precio * cantidad;
                        totalItems += cantidad;
                    }

                    lblTotalItems.Text = Convert.ToString(totalItems);
                    lblSubTotalDGV.Text = subtotal.ToString("C2", new CultureInfo("es-ES"));

                    // totales (subTotal, impuesto 13%, propina 10%)
                    lblSubTotal.Text = subtotal.ToString("C2", new CultureInfo("es-ES"));
                    decimal impuesto = Math.Round(subtotal * 0.13m, 2);
                    decimal propina = Math.Round(subtotal * 0.10m, 2);
                    lblImpuesto.Text = impuesto.ToString("C2", new CultureInfo("es-ES"));
                    lblPropina.Text = propina.ToString("C2", new CultureInfo("es-ES"));

                    decimal total = subtotal + impuesto + propina;
                    // actualizar lblTotal para mostrar el total correcto
                    if (lblTotal != null)
                        lblTotal.Text = total.ToString("C2", new CultureInfo("es-ES"));

                    // lblCambio y txtDineroRecibido se mantendrán según método de pago
                    lblCambio.Text = "0,00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMetodoPago.SelectedItem == null) return;

            string metodo = Convert.ToString(cboMetodoPago.SelectedItem);
            if (metodo == "Efectivo")
            {
                txtDineroRecibido.Enabled = true;
            }
            else
            {
                txtDineroRecibido.Enabled = false;
                txtDineroRecibido.Text = string.Empty;
                lblCambio.Text = "0,00";
                errorProvider1.SetError(txtDineroRecibido, "");
            }
        }

        private void txtDineroRecibido_TextChanged(object sender, EventArgs e)
        {
            // calcular cambio al digitar
            if (_detallesTable == null) return;

            decimal subtotal = 0m;
            foreach (DataRow r in _detallesTable.Rows)
            {
                decimal precio = r["Precio"] == DBNull.Value ? 0m : Convert.ToDecimal(r["Precio"]);
                int cantidad = r["Cantidad"] == DBNull.Value ? 0 : Convert.ToInt32(r["Cantidad"]);
                subtotal += precio * cantidad;
            }
            decimal impuesto = Math.Round(subtotal * 0.13m, 2);
            decimal propina = Math.Round(subtotal * 0.10m, 2);
            decimal total = subtotal + impuesto + propina;

            // actualizar lblTotal también cuando se recalculan los montos
            if (lblTotal != null)
                lblTotal.Text = total.ToString("C2", new CultureInfo("es-ES"));

            if (string.IsNullOrWhiteSpace(txtDineroRecibido.Text))
            {
                lblCambio.Text = "0,00";
                errorProvider1.SetError(txtDineroRecibido, "");
                return;
            }

            decimal recibido;
            if (!decimal.TryParse(txtDineroRecibido.Text, System.Globalization.NumberStyles.Number, CultureInfo.CurrentCulture, out recibido))
            {
                errorProvider1.SetError(txtDineroRecibido, "Ingrese una cantidad válida.");
                lblCambio.Text = "0,00";
                return;
            }

            if (recibido < total)
            {
                errorProvider1.SetError(txtDineroRecibido, "Dinero insuficiente.");
                lblCambio.Text = "0,00";
                return;
            }

            errorProvider1.SetError(txtDineroRecibido, "");
            decimal cambio = recibido - total;
            lblCambio.Text = cambio.ToString("C2", new CultureInfo("es-ES"));
        }

        private void btnBusqueda1_Click(object sender, EventArgs e)
        {
            // busca en dgvPedidos por PedidoID o NumeroMesa o EstadoCocina
            if (_pedidosTable == null) return;
            string term = txtBusqueda1.Text.Trim();
            if (string.IsNullOrEmpty(term))
            {
                dgvPedidos.DataSource = _pedidosTable;
                return;
            }

            // si es número, buscar por PedidoID o NumeroMesa
            int n;
            DataView dv = _pedidosTable.DefaultView;
            if (int.TryParse(term, out n))
            {
                dv.RowFilter = "PedidoID = " + n.ToString() + " OR NumeroMesa = " + n.ToString();
            }
            else
            {
                // buscar por texto en EstadoCocina o FechaHora
                string escaped = term.Replace("'", "''");
                dv.RowFilter = "EstadoCocina LIKE '%" + escaped + "%' OR Convert(FechaHora, 'System.String') LIKE '%" + escaped + "%'";
            }
            dgvPedidos.DataSource = dv;
        }

        private void btnBusqueda2_Click(object sender, EventArgs e)
        {
            // buscar en detalles por nombre de producto
            if (_detallesTable == null) return;
            string term = txtBusqueda2.Text.Trim();
            DataView dv = _detallesTable.DefaultView;
            if (string.IsNullOrEmpty(term))
            {
                dv.RowFilter = "";
            }
            else
            {
                string escaped = term.Replace("'", "''");
                dv.RowFilter = "ProductoNombre LIKE '%" + escaped + "%'";
            }
            dgvDetallesPedido.DataSource = dv;
        }

        private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            // validaciones básicas y avanzadas
            if (!_selectedPedidoId.HasValue)
            {
                MessageBox.Show("Seleccione un pedido antes de generar la factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_detallesTable == null || _detallesTable.Rows.Count == 0)
            {
                MessageBox.Show("El pedido no contiene ítems.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboMetodoPago.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un método de pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // calcular totales
            decimal subtotal = 0m;
            foreach (DataRow r in _detallesTable.Rows)
            {
                decimal precio = r["Precio"] == DBNull.Value ? 0m : Convert.ToDecimal(r["Precio"]);
                int cantidad = r["Cantidad"] == DBNull.Value ? 0 : Convert.ToInt32(r["Cantidad"]);
                subtotal += precio * cantidad;
            }
            decimal impuesto = Math.Round(subtotal * 0.13m, 2);
            decimal propina = Math.Round(subtotal * 0.10m, 2);
            decimal total = subtotal + impuesto + propina;

            // actualizar lblTotal antes de generar la factura
            if (lblTotal != null)
                lblTotal.Text = total.ToString("C2", new CultureInfo("es-ES"));

            string metodo = Convert.ToString(cboMetodoPago.SelectedItem);
            decimal dineroRecibido = 0m;
            decimal cambio = 0m;

            if (metodo == "Efectivo")
            {
                if (string.IsNullOrWhiteSpace(txtDineroRecibido.Text) || !decimal.TryParse(txtDineroRecibido.Text, System.Globalization.NumberStyles.Number, CultureInfo.CurrentCulture, out dineroRecibido))
                {
                    MessageBox.Show("Ingrese la cantidad recibida en efectivo válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dineroRecibido < total)
                {
                    MessageBox.Show("Dinero recibido insuficiente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cambio = dineroRecibido - total;
            }

            // insertar factura en BD con validaciones previas y transacción
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // 1) Verificar si ya existe una factura para este pedido (la columna PedidoID ahora es UNIQUE)
                    using (var checkCmd = new SqlCommand("SELECT COUNT(1) FROM Facturas WHERE PedidoID = @pid", conn))
                    {
                        checkCmd.Parameters.AddWithValue("@pid", _selectedPedidoId.Value);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Ya existe una factura para este pedido. No se puede generar una segunda factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 2) Verificar disponibilidad de los productos (tener en cuenta el TRIGGER que actualiza 'Disponible')
                    var productIds = _detallesTable.Rows.Cast<DataRow>()
                                        .Select(r => r["ProductoID"] == DBNull.Value ? 0 : Convert.ToInt32(r["ProductoID"]))
                                        .Where(id => id > 0)
                                        .Distinct()
                                        .ToList();

                    if (productIds.Count > 0)
                    {
                        var sb = new StringBuilder();
                        for (int i = 0; i < productIds.Count; i++)
                        {
                            if (i > 0) sb.Append(", ");
                            sb.Append("@p" + i);
                        }

                        string sqlUnavailable = $"SELECT ProductoID, Nombre FROM Productos WHERE ProductoID IN ({sb.ToString()}) AND Disponible = 0;";
                        using (var cmdU = new SqlCommand(sqlUnavailable, conn))
                        {
                            for (int i = 0; i < productIds.Count; i++)
                                cmdU.Parameters.AddWithValue("@p" + i, productIds[i]);

                            using (var rdr = cmdU.ExecuteReader())
                            {
                                if (rdr.HasRows)
                                {
                                    var nombres = new System.Collections.Generic.List<string>();
                                    while (rdr.Read())
                                    {
                                        nombres.Add(rdr["Nombre"] == DBNull.Value ? ("ID:" + rdr["ProductoID"].ToString()) : Convert.ToString(rdr["Nombre"]));
                                    }
                                    MessageBox.Show("No se puede generar la factura porque los siguientes productos no están disponibles:\r\n- " + string.Join("\r\n- ", nombres), "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }

                    using (var tran = conn.BeginTransaction())
                    {
                        // insertar en Facturas — columna Total es computada por la BD; FechaEmision usa DEFAULT GETDATE()
                        int facturaId = 0;
                        using (var cmd = new SqlCommand(@"INSERT INTO Facturas (PedidoID, Subtotal, Impuesto, Propina, MetodoPago)
                                                         VALUES (@pid, @subtotal, @impuesto, @propina, @metodo);
                                                         SELECT CAST(scope_identity() AS int);", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@pid", _selectedPedidoId.Value);
                            cmd.Parameters.AddWithValue("@subtotal", subtotal);
                            cmd.Parameters.AddWithValue("@impuesto", impuesto);
                            cmd.Parameters.AddWithValue("@propina", propina);
                            cmd.Parameters.AddWithValue("@metodo", metodo ?? (object)DBNull.Value);

                            object obj = cmd.ExecuteScalar();
                            if (obj != null && obj != DBNull.Value)
                                facturaId = Convert.ToInt32(obj);
                        }

                        if (facturaId <= 0)
                        {
                            tran.Rollback();
                            MessageBox.Show("No se pudo generar la factura en la base de datos. Verifique que exista la tabla Facturas y los permisos de inserción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // insertar líneas DetalleFactura
                        foreach (DataRow r in _detallesTable.Rows)
                        {
                            int productoId = r["ProductoID"] == DBNull.Value ? 0 : Convert.ToInt32(r["ProductoID"]);
                            int cantidad = r["Cantidad"] == DBNull.Value ? 0 : Convert.ToInt32(r["Cantidad"]);
                            decimal precio = r["Precio"] == DBNull.Value ? 0m : Convert.ToDecimal(r["Precio"]);
                            decimal sub = Math.Round(precio * cantidad, 2);

                            using (var cmd2 = new SqlCommand(@"INSERT INTO DetalleFactura (FacturaID, ProductoID, Cantidad, Precio, Subtotal)
                                                             VALUES (@fid, @prod, @cant, @precio, @sub);", conn, tran))
                            {
                                cmd2.Parameters.AddWithValue("@fid", facturaId);
                                cmd2.Parameters.AddWithValue("@prod", productoId);
                                cmd2.Parameters.AddWithValue("@cant", cantidad);
                                cmd2.Parameters.AddWithValue("@precio", precio);
                                cmd2.Parameters.AddWithValue("@sub", sub);
                                cmd2.ExecuteNonQuery();
                            }
                        }

                        // actualizar estado del pedido como 'Facturado' (si aplica)
                        using (var cmd3 = new SqlCommand("UPDATE Pedidos SET EstadoCocina = 'Facturado' WHERE PedidoID = @pid", conn, tran))
                        {
                            cmd3.Parameters.AddWithValue("@pid", _selectedPedidoId.Value);
                            cmd3.ExecuteNonQuery();
                        }

                        // actualizar estado de la mesa a 'Libre' para que quede disponible (si hay mesa)
                        using (var cmd4 = new SqlCommand(@"UPDATE Mesas 
                                                          SET Estado = 'Libre' 
                                                          WHERE MesaID = (SELECT MesaID FROM Pedidos WHERE PedidoID = @pid)", conn, tran))
                        {
                            cmd4.Parameters.AddWithValue("@pid", _selectedPedidoId.Value);
                            cmd4.ExecuteNonQuery();
                        }

                        tran.Commit();

                        MessageBox.Show("Factura generada correctamente. ID factura: " + facturaId.ToString(), "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // recargar pedidos para reflejar cambios
                        LoadPedidos();
                        ClearSelectionVisual();
                    }
                }
            }
            catch (SqlException ex)
            {
                // manejo específico de violación de UNIQUE/PK
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Error: ya existe un registro con la misma clave única. Verifique si el pedido ya fue facturado.", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Error SQL generando factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimirTicket_Click(object sender, EventArgs e)
        {
            if (!_selectedPedidoId.HasValue)
            {
                MessageBox.Show("Seleccione un pedido para imprimir.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_detallesTable == null || _detallesTable.Rows.Count == 0)
            {
                MessageBox.Show("No hay detalles para imprimir.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Preparar datos de impresión (sin librerías externas)
                _printItems = new System.Collections.Generic.List<System.Tuple<string, int, decimal, decimal>>();
                _printSubtotal = 0m;
                foreach (DataRow dr in _detallesTable.Rows)
                {
                    string nombre = dr["ProductoNombre"] == DBNull.Value ? "" : Convert.ToString(dr["ProductoNombre"]);
                    int cant = dr["Cantidad"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Cantidad"]);
                    decimal precio = dr["Precio"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["Precio"]);
                    decimal sub = precio * cant;
                    _printItems.Add(System.Tuple.Create(nombre, cant, precio, sub));
                    _printSubtotal += sub;
                }

                _printImpuesto = Math.Round(_printSubtotal * 0.13m, 2);
                _printPropina = Math.Round(_printSubtotal * 0.10m, 2);
                _printTotal = _printSubtotal + _printImpuesto + _printPropina;

                _printPedidoId = _selectedPedidoId.Value;
                _printMesa = lblNMesa.Text;
                _printFecha = lblFechaPedido.Text;
                _printEstado = lblEstado.Text;
                _printPrioridad = lblPrioridad.Text;
                _printRowIndex = 0;

                // crear documento de impresión y mostrar vista previa
                var pd = new PrintDocument();
                pd.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
                pd.PrintPage += PrintDocument_PrintPage;

                using (var preview = new PrintPreviewDialog())
                {
                    preview.Document = pd;
                    // ajustar tamaño del cuadro de vista previa para que no explote en pantallas pequeñas
                    preview.Width = 900;
                    preview.Height = 700;
                    preview.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error preparando impresión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Márgenes y puntos iniciales
            var g = e.Graphics;
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float width = e.MarginBounds.Width;

            // Fuentes
            using (var headerFont = new Font("Segoe UI", 14f, FontStyle.Bold))
            using (var normalFont = new Font("Segoe UI", 10f, FontStyle.Regular))
            using (var smallFont = new Font("Segoe UI", 9f, FontStyle.Regular))
            using (var boldFont = new Font("Segoe UI", 10f, FontStyle.Bold))
            {
                // Encabezado centrado
                string title = "SoftGourmet - Ticket de Venta";
                var sizeTitle = g.MeasureString(title, headerFont);
                g.DrawString(title, headerFont, Brushes.Black, x + (width - sizeTitle.Width) / 2f, y);
                y += sizeTitle.Height + 10f;

                // Información del pedido
                g.DrawString("Pedido: " + _printPedidoId.ToString(), boldFont, Brushes.Black, x, y);
                y += boldFont.GetHeight(g) + 2f;
                g.DrawString("Mesa: " + _printMesa, normalFont, Brushes.Black, x, y);
                y += normalFont.GetHeight(g) + 2f;
                g.DrawString("Fecha: " + _printFecha, normalFont, Brushes.Black, x, y);
                y += normalFont.GetHeight(g) + 2f;
                g.DrawString("Estado: " + _printEstado + "    Prioridad: " + _printPrioridad, normalFont, Brushes.Black, x, y);
                y += normalFont.GetHeight(g) + 8f;

                // Tabla: definir anchos
                float colProducto = width * 0.55f;
                float colCant = width * 0.12f;
                float colPrecio = width * 0.16f;
                float colSub = width - (colProducto + colCant + colPrecio);

                // Cabecera de tabla
                g.DrawString("Producto", boldFont, Brushes.Black, x, y);
                g.DrawString("Cant.", boldFont, Brushes.Black, x + colProducto, y);
                g.DrawString("Precio", boldFont, Brushes.Black, x + colProducto + colCant, y);
                g.DrawString("Subtotal", boldFont, Brushes.Black, x + colProducto + colCant + colPrecio, y);
                y += boldFont.GetHeight(g) + 6f;

                // Línea separadora
                g.DrawLine(Pens.Black, x, y, x + width, y);
                y += 4f;

                // Imprimir filas hasta agotar espacio
                while (_printRowIndex < _printItems.Count)
                {
                    var it = _printItems[_printRowIndex];
                    string nombre = it.Item1;
                    string cant = it.Item2.ToString();
                    string precio = it.Item3.ToString("C2", new CultureInfo("es-ES"));
                    string sub = it.Item4.ToString("C2", new CultureInfo("es-ES"));

                    // Ajustar nombre para que quepa en columna (break if necessary)
                    var layoutRect = new RectangleF(x, y, colProducto, normalFont.GetHeight(g) * 3f);
                    var sf = new StringFormat { FormatFlags = StringFormatFlags.LineLimit };
                    g.DrawString(nombre, normalFont, Brushes.Black, layoutRect, sf);

                    // Cantidad, precio y subtotal
                    g.DrawString(cant, normalFont, Brushes.Black, x + colProducto, y);
                    g.DrawString(precio, normalFont, Brushes.Black, x + colProducto + colCant, y);
                    g.DrawString(sub, normalFont, Brushes.Black, x + colProducto + colCant + colPrecio, y);

                    // Medir altura ocupada por la celda de producto
                    var sizeProducto = g.MeasureString(nombre, normalFont, (int)colProducto);
                    float rowHeight = Math.Max(sizeProducto.Height, normalFont.GetHeight(g));
                    y += rowHeight + 6f;

                    // Si nos acercamos al final de la página, pausar y continuar en la siguiente
                    if (y + 100f > e.MarginBounds.Bottom)
                    {
                        _printRowIndex++;
                        e.HasMorePages = true;
                        return;
                    }

                    _printRowIndex++;
                }

                // Espacio antes de totales
                y += 8f;
                g.DrawLine(Pens.Black, x, y, x + width, y);
                y += 6f;

                // Totales a la derecha
                string sSub = "SubTotal: " + _printSubtotal.ToString("C2", new CultureInfo("es-ES"));
                string sImp = "Impuesto (13%): " + _printImpuesto.ToString("C2", new CultureInfo("es-ES"));
                string sProp = "Propina (10%): " + _printPropina.ToString("C2", new CultureInfo("es-ES"));
                string sTotal = "Total: " + _printTotal.ToString("C2", new CultureInfo("es-ES"));

                var totalX = x + width - 0f; // dibujar alineado a la derecha
                var totalRightAlign = new StringFormat { Alignment = StringAlignment.Far };

                g.DrawString(sSub, boldFont, Brushes.Black, new RectangleF(x, y, width, boldFont.GetHeight(g)), totalRightAlign);
                y += boldFont.GetHeight(g) + 2f;
                g.DrawString(sImp, normalFont, Brushes.Black, new RectangleF(x, y, width, normalFont.GetHeight(g)), totalRightAlign);
                y += normalFont.GetHeight(g) + 2f;
                g.DrawString(sProp, normalFont, Brushes.Black, new RectangleF(x, y, width, normalFont.GetHeight(g)), totalRightAlign);
                y += normalFont.GetHeight(g) + 2f;
                g.DrawString(sTotal, boldFont, Brushes.Black, new RectangleF(x, y, width, boldFont.GetHeight(g)), totalRightAlign);
                y += boldFont.GetHeight(g) + 8f;

                // Pie
                g.DrawString("Gracias por su preferencia", smallFont, Brushes.Black, x + (width / 2f) - 60f, y);
                y += smallFont.GetHeight(g) + 2f;
            }

            // No hay más páginas
            e.HasMorePages = false;
            // resetear index para próxima impresión
            _printRowIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Cancelar y limpiar campos?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            ClearSelectionVisual();
            LoadPedidos();
        }


        private void ClearSelectionVisual()
        {
            _selectedPedidoId = null;
            dgvPedidos.ClearSelection();
            dgvDetallesPedido.DataSource = null;

            lblNMesa.Text = "";
            lblFechaPedido.Text = "";
            lblEstado.Text = "";
            lblPrioridad.Text = "";
            lblTotalItems.Text = "0";
            lblSubTotalDGV.Text = (0m).ToString("C2", new CultureInfo("es-ES"));
            lblSubTotal.Text = (0m).ToString("C2", new CultureInfo("es-ES"));
            lblImpuesto.Text = (0m).ToString("C2", new CultureInfo("es-ES"));
            lblPropina.Text = (0m).ToString("C2", new CultureInfo("es-ES"));
            lblCambio.Text = (0m).ToString("C2", new CultureInfo("es-ES"));
            // asegurar que lblTotal se reinicie también
            if (lblTotal != null)
                lblTotal.Text = (0m).ToString("C2", new CultureInfo("es-ES"));

            // Deshabilitar botones por defecto cuando no hay selección
            if (btnGenerarFactura != null)
                btnGenerarFactura.Enabled = false;
            if (btnImprimirTicket != null)
                btnImprimirTicket.Enabled = false;

            txtDineroRecibido.Text = "";
            txtDineroRecibido.Enabled = false;
            cboMetodoPago.SelectedIndex = -1;
            errorProvider1.SetError(txtDineroRecibido, "");
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
