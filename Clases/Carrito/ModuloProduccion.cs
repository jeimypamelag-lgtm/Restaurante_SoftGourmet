using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurante_SoftGourmet.Clases.Carrito
{
    public partial class ModuloProduccion : Form
    {
        private const string ConnectionString = "Server=.;Database=SoftGourmetDB;Trusted_Connection=True;";

        private Timer _clockTimer;
        private FlowLayoutPanel _flowPedidos;
        private int? _selectedPedidoId;
        private Panel _selectedPedidoPanel;

        // Implementación mínima de lista enlazada propia (no usar List<T>)
        private sealed class SimpleListNode<T>
        {
            public T Value;
            public SimpleListNode<T> Next;
            public SimpleListNode(T value) { Value = value; Next = null; }
        }

        private sealed class SimpleList<T> : IEnumerable<T>
        {
            private SimpleListNode<T> _head;
            private SimpleListNode<T> _tail;
            private int _count;

            public int Count => _count;

            public void Add(T item)
            {
                var n = new SimpleListNode<T>(item);
                if (_head == null) { _head = _tail = n; }
                else { _tail.Next = n; _tail = n; }
                _count++;
            }

            public IEnumerator<T> GetEnumerator()
            {
                var current = _head;
                while (current != null)
                {
                    yield return current.Value;
                    current = current.Next;
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public ModuloProduccion()
        {
            InitializeComponent();
            this.Load += ModuloProduccion_Load;
            this.FormClosing += ModuloProduccion_FormClosing;
        }

        private void ModuloProduccion_Load(object sender, EventArgs e)
        {
            try
            {
                // preparar panel contenedor para pedidos
                _flowPedidos = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    AutoScroll = true
                };
                _flowPedidos.Resize += FlowPedidos_Resize;
                pnlOrdenes.Controls.Clear();
                pnlOrdenes.Controls.Add(_flowPedidos);

                LoadPedidos();

                UpdateFechaActual();
                _clockTimer = new Timer { Interval = 1000 };
                _clockTimer.Tick += ClockTimer_Tick;
                _clockTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inicializando módulo de producción: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModuloProduccion_FormClosing(object sender, FormClosingEventArgs e)
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
                if (lblFechaActual2 != null)
                    lblFechaActual2.Text = ahora.ToString("dddd, d 'de' MMMM 'de' yyyy HH:mm:ss", new CultureInfo("es-ES"));
            }
            catch
            {
                if (lblFechaActual2 != null)
                    lblFechaActual2.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }

        private (int pedidosCount, int detallesCount) GetTableCounts()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Pedidos; SELECT COUNT(*) FROM DetallePedido;", conn))
                {
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        int pedidos = 0, detalles = 0;
                        if (rdr.Read()) pedidos = rdr.GetInt32(0);
                        if (rdr.NextResult() && rdr.Read()) detalles = rdr.GetInt32(0);
                        return (pedidos, detalles);
                    }
                }
            }
            catch
            {
                return (0, 0);
            }
        }

        private void LoadPedidos()
        {
            try
            {
                var lista = new List<Restaurante_SoftGourmet.Clases.Modelos.OrderModel>();

                const string sql = @"SELECT p.PedidoID, p.MesaID, m.NumeroMesa, p.FechaHora, p.EstadoCocina, p.Prioridad,
                                   (SELECT COUNT(*) FROM DetallePedido d WHERE d.PedidoID = p.PedidoID) AS ItemsCount
                                   FROM Pedidos p
                                   LEFT JOIN Mesas m ON p.MesaID = m.MesaID
                                   WHERE p.EstadoCocina <> 'Entregado'
                                   ORDER BY p.Prioridad DESC, p.FechaHora ASC;";

                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        int rowCount = 0;
                        while (rdr.Read())
                        {
                            rowCount++;
                            var om = new Restaurante_SoftGourmet.Clases.Modelos.OrderModel
                            {
                                PedidoID = rdr.GetInt32(0),
                                MesaID = rdr.IsDBNull(1) ? (int?)null : rdr.GetInt32(1),
                                NumeroMesa = rdr.IsDBNull(2) ? (int?)null : rdr.GetInt32(2),
                                FechaHora = rdr.IsDBNull(3) ? DateTime.MinValue : rdr.GetDateTime(3),
                                EstadoCocina = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4),
                                Prioridad = rdr.IsDBNull(5) ? 1 : rdr.GetInt32(5),
                                ItemsCount = rdr.IsDBNull(6) ? 0 : rdr.GetInt32(6)
                            };
                            lista.Add(om);
                        }

                        if (rowCount == 0)
                        {
                            var counts = GetTableCounts();
                            MessageBox.Show("No se encontraron pedidos (Pedidos=" + counts.pedidosCount.ToString() + ", DetallePedido=" + counts.detallesCount.ToString() + "). Verifique la base de datos y la cadena de conexión: " + ConnectionString, "Sin pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                RenderPedidos(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando pedidos: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderPedidos(List<Restaurante_SoftGourmet.Clases.Modelos.OrderModel> pedidos)
        {
            _flowPedidos.SuspendLayout();
            _flowPedidos.Controls.Clear();

            int panelWidth = _flowPedidos.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10;
            if (panelWidth <= 0) panelWidth = 560;

            if (pedidos == null || pedidos.Count == 0)
            {
                var lbl = new Label
                {
                    AutoSize = false,
                    Text = "No hay pedidos en cola.",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 40
                };
                _flowPedidos.Controls.Add(lbl);
            }
            else
            {
                foreach (var p in pedidos)
                {
                    var pnl = new Panel
                    {
                        Width = panelWidth,
                        Height = 80,
                        Tag = p.PedidoID,
                        BackColor = Color.FromArgb(248, 244, 227),
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(5)
                    };

                    var lblPedido = new Label { Text = "Pedido #" + p.PedidoID.ToString(), Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(8, 6), AutoSize = true };
                    var lblMesa = new Label { Text = "Mesa: " + (p.NumeroMesa.HasValue ? p.NumeroMesa.Value.ToString() : "N/A"), Location = new Point(160, 8), AutoSize = true };
                    var lblFecha = new Label { Text = p.FechaHora.ToString("g"), Location = new Point(8, 30), AutoSize = true };
                    var lblItems = new Label { Text = p.ItemsCount.ToString() + " items", Location = new Point(250, 30), AutoSize = true };
                    var lblPrioridad = new Label { Text = "Prioridad: " + p.Prioridad.ToString(), Location = new Point(8, 52), AutoSize = true };
                    var lblEstado = new Label { Text = p.EstadoCocina, Location = new Point(160, 52), AutoSize = true };

                    pnl.Controls.Add(lblPedido);
                    pnl.Controls.Add(lblMesa);
                    pnl.Controls.Add(lblFecha);
                    pnl.Controls.Add(lblItems);
                    pnl.Controls.Add(lblPrioridad);
                    pnl.Controls.Add(lblEstado);

                    // click handler (panel + children)
                    pnl.Click += PedidoPanel_Click;
                    foreach (Control c in pnl.Controls) c.Click += PedidoPanel_Click;

                    _flowPedidos.Controls.Add(pnl);
                }
            }

            _flowPedidos.ResumeLayout();
        }

        private void PedidoPanel_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            Panel pnl = null;
            if (c is Panel) pnl = (Panel)c;
            else if (c.Parent is Panel) pnl = c.Parent as Panel;

            if (pnl == null || pnl.Tag == null) return;

            int pedidoId;
            if (!int.TryParse(pnl.Tag.ToString(), out pedidoId)) return;

            SelectPedido(pnl, pedidoId);
        }

        private void SelectPedido(Panel panel, int pedidoId)
        {
            // desmarcar anterior
            if (_selectedPedidoPanel != null)
            {
                _selectedPedidoPanel.BackColor = SystemColors.Control;
            }

            _selectedPedidoPanel = panel;
            _selectedPedidoId = pedidoId;
            _selectedPedidoPanel.BackColor = Color.LightBlue;

            LoadPedidoDetalles(pedidoId);
        }

        private void LoadPedidoDetalles(int pedidoId)
        {
            try
            {
                // obtener cabecera
                Restaurante_SoftGourmet.Clases.Modelos.OrderModel header = null;
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand("SELECT p.PedidoID, p.MesaID, m.NumeroMesa, p.FechaHora, p.EstadoCocina, p.Prioridad FROM Pedidos p LEFT JOIN Mesas m ON p.MesaID = m.MesaID WHERE p.PedidoID = @pid", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", pedidoId);
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            header = new Restaurante_SoftGourmet.Clases.Modelos.OrderModel
                            {
                                PedidoID = rdr.GetInt32(0),
                                MesaID = rdr.IsDBNull(1) ? (int?)null : rdr.GetInt32(1),
                                NumeroMesa = rdr.IsDBNull(2) ? (int?)null : rdr.GetInt32(2),
                                FechaHora = rdr.IsDBNull(3) ? DateTime.MinValue : rdr.GetDateTime(3),
                                EstadoCocina = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4),
                                Prioridad = rdr.IsDBNull(5) ? 1 : rdr.GetInt32(5)
                            };
                        }
                        else
                        {
                            MessageBox.Show("Pedido #" + pedidoId.ToString() + " no existe en la BD.", "Pedido no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ClearSelection();
                            return;
                        }
                    }
                }

                // obtener detalles
                var detalles = new SimpleList<Restaurante_SoftGourmet.Clases.Modelos.OrderDetailModel>();
                int detallesLeidos = 0;
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand("SELECT d.DetalleID, d.PedidoID, d.ProductoID, prod.Nombre AS ProductoNombre, d.Cantidad, d.NotasEspeciales, prod.Precio FROM DetallePedido d LEFT JOIN Productos prod ON d.ProductoID = prod.ProductoID WHERE d.PedidoID = @pid", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", pedidoId);
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            detallesLeidos++;
                            var od = new Restaurante_SoftGourmet.Clases.Modelos.OrderDetailModel
                            {
                                DetalleID = rdr.GetInt32(0),
                                PedidoID = rdr.GetInt32(1),
                                ProductoID = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                                ProductoNombre = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                                Cantidad = rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4),
                                NotasEspeciales = rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5),
                                Precio = rdr.IsDBNull(6) ? 0m : rdr.GetDecimal(6)
                            };
                            detalles.Add(od);
                        }
                    }
                }

                if (detallesLeidos == 0)
                {
                    MessageBox.Show("Pedido #" + pedidoId.ToString() + " existe pero no tiene líneas en DetallePedido.", "Sin ítems", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                RenderPedidoSeleccionado(header, detalles);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando detalles de pedido: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderPedidoSeleccionado(Restaurante_SoftGourmet.Clases.Modelos.OrderModel header, SimpleList<Restaurante_SoftGourmet.Clases.Modelos.OrderDetailModel> detalles)
        {
            pnlOrdenSeleccionada.Controls.Clear();

            if (header == null)
            {
                pnlOrdenSeleccionada.Controls.Add(new Label
                {
                    Text = "Seleccione un pedido",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 14, FontStyle.Bold)
                });
                return;
            }

            // 🔴 HEADER
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(139, 0, 0)
            };

            headerPanel.Controls.Add(new Label
            {
                Text = "Pedido #" + header.PedidoID.ToString() + "  |  Mesa: " + (header.NumeroMesa.HasValue ? header.NumeroMesa.Value.ToString() : ""),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            });

            // TOTAL
            decimal total = 0;

            var totalLabel = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            // FLOW (entre header y total)
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(248, 244, 227)

            };

            bool esPrimero = true;

            foreach (var it in detalles)
            {
                decimal subtotal = it.Precio * it.Cantidad;
                total += subtotal;

                var itemPanel = new Panel
                {
                    Width = Math.Max(200, flow.ClientSize.Width - 25),
                    Height = 80,
                    BackColor = Color.FromArgb(245, 245, 245),
                    Margin = esPrimero ? new Padding(5, 80, 5, 5) : new Padding(5)
                };

                esPrimero = false;

                var lblNombre = new Label
                {
                    Text = it.ProductoNombre,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Location = new Point(10, 5),
                    AutoSize = true
                };

                var lblCantidad = new Label
                {
                    Text = "x" + it.Cantidad.ToString(),
                    Location = new Point(10, 35),
                    AutoSize = true
                };

                var lblPrecio = new Label
                {
                    Text = it.Precio.ToString("C2"),
                    Location = new Point(100, 35),
                    AutoSize = true
                };

                var lblNotas = new Label
                {
                    Text = string.IsNullOrEmpty(it.NotasEspeciales) ? "" : "Nota: " + it.NotasEspeciales,
                    Location = new Point(10, 55),
                    AutoSize = true,
                    ForeColor = Color.Gray
                };

                itemPanel.Controls.Add(lblNombre);
                itemPanel.Controls.Add(lblCantidad);
                itemPanel.Controls.Add(lblPrecio);
                itemPanel.Controls.Add(lblNotas);

                flow.Controls.Add(itemPanel);
            }

            totalLabel.Text = "TOTAL: " + total.ToString("C2");

            pnlOrdenSeleccionada.Controls.Add(flow);        // primero fill
            pnlOrdenSeleccionada.Controls.Add(totalLabel);  // luego bottom
            pnlOrdenSeleccionada.Controls.Add(headerPanel); // al final top

            headerPanel.BringToFront();
            totalLabel.BringToFront();
        }

        private void btnPriorizar_Click(object sender, EventArgs e)
        {
            if (!_selectedPedidoId.HasValue)
            {
                MessageBox.Show("Seleccione un pedido para priorizar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand("UPDATE Pedidos SET Prioridad = (SELECT ISNULL(MAX(Prioridad), 0) + 1 FROM Pedidos) WHERE PedidoID = @pid", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", _selectedPedidoId.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadPedidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error priorizando pedido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEntregar_Click(object sender, EventArgs e)
        {
            try
            {
                int? toDeliver = null;
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand("SELECT TOP 1 PedidoID FROM Pedidos WHERE EstadoCocina <> 'Entregado' ORDER BY Prioridad DESC, FechaHora ASC", conn))
                {
                    conn.Open();
                    var obj = cmd.ExecuteScalar();
                    if (obj != null && obj != DBNull.Value)
                        toDeliver = Convert.ToInt32(obj);
                }

                if (!toDeliver.HasValue)
                {
                    MessageBox.Show("No hay pedidos para entregar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand("UPDATE Pedidos SET EstadoCocina = 'Entregado' WHERE PedidoID = @pid", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", toDeliver.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                // si el pedido entregado estaba seleccionado, limpiar selección
                if (_selectedPedidoId.HasValue && _selectedPedidoId.Value == toDeliver.Value)
                    ClearSelection();

                LoadPedidos();
                MessageBox.Show("Se entregó la comida del pedido #" + toDeliver.Value, "Entregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error entregando pedido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!_selectedPedidoId.HasValue)
            {
                MessageBox.Show("Seleccione un pedido para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("¿Eliminar pedido seleccionado? Esta operación es irreversible.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var delDetalles = new SqlCommand("DELETE FROM DetallePedido WHERE PedidoID = @pid", conn))
                    {
                        delDetalles.Parameters.AddWithValue("@pid", _selectedPedidoId.Value);
                        delDetalles.ExecuteNonQuery();
                    }
                    using (var delPedido = new SqlCommand("DELETE FROM Pedidos WHERE PedidoID = @pid", conn))
                    {
                        delPedido.Parameters.AddWithValue("@pid", _selectedPedidoId.Value);
                        delPedido.ExecuteNonQuery();
                    }
                }

                ClearSelection();
                LoadPedidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error eliminando pedido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearSelection()
        {
            if (_selectedPedidoPanel != null)
            {
                _selectedPedidoPanel.BackColor = SystemColors.Control;
                _selectedPedidoPanel = null;
            }
            _selectedPedidoId = null;
            pnlOrdenSeleccionada.Controls.Clear();
        }

        private void FlowPedidos_Resize(object sender, EventArgs e)
        {
            int panelWidth = _flowPedidos.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10;
            if (panelWidth <= 0) panelWidth = 560;
            foreach (Control c in _flowPedidos.Controls)
            {
                if (c is Panel p)
                {
                    p.Width = panelWidth;
                }
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
