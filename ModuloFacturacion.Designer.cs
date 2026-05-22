namespace Restaurante_SoftGourmet
{
    partial class ModuloFacturacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuloFacturacion));
            this.tlpAllContainer = new System.Windows.Forms.TableLayoutPanel();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlOrdenes = new System.Windows.Forms.Panel();
            this.gbDetallesPedido = new System.Windows.Forms.GroupBox();
            this.Linea = new System.Windows.Forms.Panel();
            this.lblSubTotalDGV = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblTotalItems = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dgvDetallesPedido = new System.Windows.Forms.DataGridView();
            this.btnBusqueda2 = new System.Windows.Forms.Button();
            this.txtBusqueda2 = new System.Windows.Forms.TextBox();
            this.gbResumenPago = new System.Windows.Forms.GroupBox();
            this.cboMetodoPago = new System.Windows.Forms.ComboBox();
            this.pnlResumen = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblPropina = new System.Windows.Forms.Label();
            this.lblImpuesto = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCambio = new System.Windows.Forms.Label();
            this.txtDineroRecibido = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnImprimirTicket = new System.Windows.Forms.Button();
            this.btnGenerarFactura = new System.Windows.Forms.Button();
            this.gbPedidoSeleccionado = new System.Windows.Forms.GroupBox();
            this.lblPrioridad = new System.Windows.Forms.Label();
            this.lblFechaPedido = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblNMesa = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbPedidos = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.btnBusqueda1 = new System.Windows.Forms.Button();
            this.txtBusqueda1 = new System.Windows.Forms.TextBox();
            this.SectionInfo1 = new System.Windows.Forms.Panel();
            this.lblFechaActual1 = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tlpAllContainer.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.pnlOrdenes.SuspendLayout();
            this.gbDetallesPedido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesPedido)).BeginInit();
            this.gbResumenPago.SuspendLayout();
            this.pnlResumen.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbPedidoSeleccionado.SuspendLayout();
            this.gbPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.SectionInfo1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpAllContainer
            // 
            this.tlpAllContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.tlpAllContainer.ColumnCount = 1;
            this.tlpAllContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAllContainer.Controls.Add(this.pnlContainer, 0, 0);
            this.tlpAllContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAllContainer.Location = new System.Drawing.Point(0, 0);
            this.tlpAllContainer.Name = "tlpAllContainer";
            this.tlpAllContainer.RowCount = 1;
            this.tlpAllContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAllContainer.Size = new System.Drawing.Size(1370, 585);
            this.tlpAllContainer.TabIndex = 2;
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.pnlOrdenes);
            this.pnlContainer.Controls.Add(this.SectionInfo1);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(3, 3);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1364, 579);
            this.pnlContainer.TabIndex = 0;
            // 
            // pnlOrdenes
            // 
            this.pnlOrdenes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.pnlOrdenes.Controls.Add(this.gbDetallesPedido);
            this.pnlOrdenes.Controls.Add(this.gbResumenPago);
            this.pnlOrdenes.Controls.Add(this.gbPedidoSeleccionado);
            this.pnlOrdenes.Controls.Add(this.gbPedidos);
            this.pnlOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrdenes.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.pnlOrdenes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.pnlOrdenes.Location = new System.Drawing.Point(0, 50);
            this.pnlOrdenes.Name = "pnlOrdenes";
            this.pnlOrdenes.Size = new System.Drawing.Size(1364, 529);
            this.pnlOrdenes.TabIndex = 5;
            // 
            // gbDetallesPedido
            // 
            this.gbDetallesPedido.Controls.Add(this.Linea);
            this.gbDetallesPedido.Controls.Add(this.lblSubTotalDGV);
            this.gbDetallesPedido.Controls.Add(this.label15);
            this.gbDetallesPedido.Controls.Add(this.label16);
            this.gbDetallesPedido.Controls.Add(this.lblTotalItems);
            this.gbDetallesPedido.Controls.Add(this.label18);
            this.gbDetallesPedido.Controls.Add(this.label19);
            this.gbDetallesPedido.Controls.Add(this.button2);
            this.gbDetallesPedido.Controls.Add(this.dgvDetallesPedido);
            this.gbDetallesPedido.Controls.Add(this.btnBusqueda2);
            this.gbDetallesPedido.Controls.Add(this.txtBusqueda2);
            this.gbDetallesPedido.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.gbDetallesPedido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.gbDetallesPedido.Location = new System.Drawing.Point(510, 6);
            this.gbDetallesPedido.Name = "gbDetallesPedido";
            this.gbDetallesPedido.Size = new System.Drawing.Size(450, 445);
            this.gbDetallesPedido.TabIndex = 8;
            this.gbDetallesPedido.TabStop = false;
            this.gbDetallesPedido.Text = "2. DETALLE DEL PEDIDO";
            // 
            // Linea
            // 
            this.Linea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Linea.BackColor = System.Drawing.Color.White;
            this.Linea.ForeColor = System.Drawing.Color.White;
            this.Linea.Location = new System.Drawing.Point(192, 346);
            this.Linea.Name = "Linea";
            this.Linea.Size = new System.Drawing.Size(91, 80);
            this.Linea.TabIndex = 23;
            this.Linea.Paint += new System.Windows.Forms.PaintEventHandler(this.Linea_Paint);
            // 
            // lblSubTotalDGV
            // 
            this.lblSubTotalDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotalDGV.AutoSize = true;
            this.lblSubTotalDGV.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubTotalDGV.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubTotalDGV.Location = new System.Drawing.Point(345, 387);
            this.lblSubTotalDGV.Name = "lblSubTotalDGV";
            this.lblSubTotalDGV.Size = new System.Drawing.Size(77, 21);
            this.lblSubTotalDGV.TabIndex = 22;
            this.lblSubTotalDGV.Text = "Ej. $00.00";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label15.Location = new System.Drawing.Point(345, 362);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 21);
            this.label15.TabIndex = 20;
            this.label15.Text = "Subtotal:";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 36F);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label16.Location = new System.Drawing.Point(289, 355);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 65);
            this.label16.TabIndex = 21;
            this.label16.Text = "💲";
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.AutoSize = true;
            this.lblTotalItems.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalItems.ForeColor = System.Drawing.Color.DimGray;
            this.lblTotalItems.Location = new System.Drawing.Point(72, 385);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(38, 21);
            this.lblTotalItems.TabIndex = 19;
            this.lblTotalItems.Text = "Ej. 4";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label18.Location = new System.Drawing.Point(72, 362);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(108, 21);
            this.label18.TabIndex = 17;
            this.label18.Text = "Total de ítems:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label19.Location = new System.Drawing.Point(10, 350);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(94, 65);
            this.label19.TabIndex = 18;
            this.label19.Text = "📋";
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(6, 334);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(348, 105);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dgvDetallesPedido
            // 
            this.dgvDetallesPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallesPedido.Location = new System.Drawing.Point(5, 65);
            this.dgvDetallesPedido.Name = "dgvDetallesPedido";
            this.dgvDetallesPedido.Size = new System.Drawing.Size(439, 265);
            this.dgvDetallesPedido.TabIndex = 2;
            // 
            // btnBusqueda2
            // 
            this.btnBusqueda2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBusqueda2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBusqueda2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBusqueda2.ForeColor = System.Drawing.Color.DimGray;
            this.btnBusqueda2.Location = new System.Drawing.Point(279, 28);
            this.btnBusqueda2.Name = "btnBusqueda2";
            this.btnBusqueda2.Size = new System.Drawing.Size(29, 29);
            this.btnBusqueda2.TabIndex = 1;
            this.btnBusqueda2.Text = "🔍";
            this.btnBusqueda2.UseVisualStyleBackColor = true;
            this.btnBusqueda2.Click += new System.EventHandler(this.btnBusqueda2_Click);
            // 
            // txtBusqueda2
            // 
            this.txtBusqueda2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.txtBusqueda2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusqueda2.Location = new System.Drawing.Point(5, 28);
            this.txtBusqueda2.Name = "txtBusqueda2";
            this.txtBusqueda2.Size = new System.Drawing.Size(275, 29);
            this.txtBusqueda2.TabIndex = 0;
            // 
            // gbResumenPago
            // 
            this.gbResumenPago.Controls.Add(this.cboMetodoPago);
            this.gbResumenPago.Controls.Add(this.pnlResumen);
            this.gbResumenPago.Controls.Add(this.groupBox2);
            this.gbResumenPago.Controls.Add(this.label34);
            this.gbResumenPago.Controls.Add(this.groupBox1);
            this.gbResumenPago.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.gbResumenPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.gbResumenPago.Location = new System.Drawing.Point(978, 12);
            this.gbResumenPago.Name = "gbResumenPago";
            this.gbResumenPago.Size = new System.Drawing.Size(360, 445);
            this.gbResumenPago.TabIndex = 8;
            this.gbResumenPago.TabStop = false;
            this.gbResumenPago.Text = "3. RESUMEN DE PAGO";
            // 
            // cboMetodoPago
            // 
            this.cboMetodoPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.cboMetodoPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMetodoPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.cboMetodoPago.FormattingEnabled = true;
            this.cboMetodoPago.Location = new System.Drawing.Point(131, 177);
            this.cboMetodoPago.Name = "cboMetodoPago";
            this.cboMetodoPago.Size = new System.Drawing.Size(223, 29);
            this.cboMetodoPago.TabIndex = 11;
            this.cboMetodoPago.SelectedIndexChanged += new System.EventHandler(this.cboMetodoPago_SelectedIndexChanged);
            // 
            // pnlResumen
            // 
            this.pnlResumen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlResumen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.pnlResumen.Controls.Add(this.lblTotal);
            this.pnlResumen.Controls.Add(this.lblPropina);
            this.pnlResumen.Controls.Add(this.lblImpuesto);
            this.pnlResumen.Controls.Add(this.lblSubTotal);
            this.pnlResumen.Controls.Add(this.label38);
            this.pnlResumen.Controls.Add(this.panel2);
            this.pnlResumen.Controls.Add(this.label37);
            this.pnlResumen.Controls.Add(this.label36);
            this.pnlResumen.Controls.Add(this.label35);
            this.pnlResumen.Location = new System.Drawing.Point(6, 28);
            this.pnlResumen.Name = "pnlResumen";
            this.pnlResumen.Size = new System.Drawing.Size(348, 140);
            this.pnlResumen.TabIndex = 10;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTotal.Location = new System.Drawing.Point(240, 110);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(59, 21);
            this.lblTotal.TabIndex = 29;
            this.lblTotal.Text = "$00.00";
            // 
            // lblPropina
            // 
            this.lblPropina.AutoSize = true;
            this.lblPropina.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblPropina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblPropina.Location = new System.Drawing.Point(240, 70);
            this.lblPropina.Name = "lblPropina";
            this.lblPropina.Size = new System.Drawing.Size(58, 21);
            this.lblPropina.TabIndex = 28;
            this.lblPropina.Text = "$00.00";
            // 
            // lblImpuesto
            // 
            this.lblImpuesto.AutoSize = true;
            this.lblImpuesto.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblImpuesto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblImpuesto.Location = new System.Drawing.Point(241, 41);
            this.lblImpuesto.Name = "lblImpuesto";
            this.lblImpuesto.Size = new System.Drawing.Size(58, 21);
            this.lblImpuesto.TabIndex = 27;
            this.lblImpuesto.Text = "$00.00";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblSubTotal.Location = new System.Drawing.Point(241, 11);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(58, 21);
            this.lblSubTotal.TabIndex = 26;
            this.lblSubTotal.Text = "$00.00";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.DarkRed;
            this.label38.Location = new System.Drawing.Point(10, 110);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(131, 21);
            this.label38.TabIndex = 25;
            this.label38.Text = "TOTAL A PAGAR:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(10, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(328, 3);
            this.panel2.TabIndex = 24;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label37.Location = new System.Drawing.Point(10, 70);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(112, 21);
            this.label37.TabIndex = 10;
            this.label37.Text = "Propina (10%):";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label36.Location = new System.Drawing.Point(10, 40);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(123, 21);
            this.label36.TabIndex = 9;
            this.label36.Text = "Impuesto (13%):";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label35.Location = new System.Drawing.Point(10, 10);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(71, 21);
            this.label35.TabIndex = 8;
            this.label35.Text = "Subtotal:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCambio);
            this.groupBox2.Controls.Add(this.txtDineroRecibido);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.label32);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.groupBox2.Location = new System.Drawing.Point(6, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 88);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pago en Efectivo";
            // 
            // lblCambio
            // 
            this.lblCambio.AutoSize = true;
            this.lblCambio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCambio.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblCambio.Location = new System.Drawing.Point(282, 59);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(53, 17);
            this.lblCambio.TabIndex = 11;
            this.lblCambio.Text = "$000.00";
            // 
            // txtDineroRecibido
            // 
            this.txtDineroRecibido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.txtDineroRecibido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDineroRecibido.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDineroRecibido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.txtDineroRecibido.Location = new System.Drawing.Point(154, 28);
            this.txtDineroRecibido.Name = "txtDineroRecibido";
            this.txtDineroRecibido.Size = new System.Drawing.Size(184, 25);
            this.txtDineroRecibido.TabIndex = 10;
            this.txtDineroRecibido.TextChanged += new System.EventHandler(this.txtDineroRecibido_TextChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.DimGray;
            this.label29.Location = new System.Drawing.Point(43, 60);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(56, 17);
            this.label29.TabIndex = 8;
            this.label29.Text = "Cambio:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label30.Location = new System.Drawing.Point(6, 56);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(32, 21);
            this.label30.TabIndex = 9;
            this.label30.Text = "💱";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.DimGray;
            this.label31.Location = new System.Drawing.Point(43, 30);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(105, 17);
            this.label31.TabIndex = 7;
            this.label31.Text = "Dinero Recibido:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label32.Location = new System.Drawing.Point(6, 27);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(32, 21);
            this.label32.TabIndex = 7;
            this.label32.Text = "💸";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label34.Location = new System.Drawing.Point(2, 180);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(123, 21);
            this.label34.TabIndex = 6;
            this.label34.Text = "Método de Pago";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btnImprimirTicket);
            this.groupBox1.Controls.Add(this.btnGenerarFactura);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.groupBox1.Location = new System.Drawing.Point(6, 306);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 133);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Acciones";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Maroon;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(230, 34);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 80);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "🚫  Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnImprimirTicket
            // 
            this.btnImprimirTicket.BackColor = System.Drawing.Color.Navy;
            this.btnImprimirTicket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimirTicket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnImprimirTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirTicket.ForeColor = System.Drawing.Color.White;
            this.btnImprimirTicket.Location = new System.Drawing.Point(124, 34);
            this.btnImprimirTicket.Name = "btnImprimirTicket";
            this.btnImprimirTicket.Size = new System.Drawing.Size(100, 80);
            this.btnImprimirTicket.TabIndex = 1;
            this.btnImprimirTicket.Text = "🖨 Imprimir Ticket";
            this.btnImprimirTicket.UseVisualStyleBackColor = false;
            this.btnImprimirTicket.Click += new System.EventHandler(this.btnImprimirTicket_Click);
            // 
            // btnGenerarFactura
            // 
            this.btnGenerarFactura.BackColor = System.Drawing.Color.DarkGreen;
            this.btnGenerarFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarFactura.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnGenerarFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarFactura.ForeColor = System.Drawing.Color.White;
            this.btnGenerarFactura.Location = new System.Drawing.Point(18, 34);
            this.btnGenerarFactura.Name = "btnGenerarFactura";
            this.btnGenerarFactura.Size = new System.Drawing.Size(100, 80);
            this.btnGenerarFactura.TabIndex = 0;
            this.btnGenerarFactura.Text = "📃  Generar Factura";
            this.btnGenerarFactura.UseVisualStyleBackColor = false;
            this.btnGenerarFactura.Click += new System.EventHandler(this.btnGenerarFactura_Click);
            // 
            // gbPedidoSeleccionado
            // 
            this.gbPedidoSeleccionado.Controls.Add(this.lblPrioridad);
            this.gbPedidoSeleccionado.Controls.Add(this.lblFechaPedido);
            this.gbPedidoSeleccionado.Controls.Add(this.label10);
            this.gbPedidoSeleccionado.Controls.Add(this.label11);
            this.gbPedidoSeleccionado.Controls.Add(this.label12);
            this.gbPedidoSeleccionado.Controls.Add(this.label13);
            this.gbPedidoSeleccionado.Controls.Add(this.lblEstado);
            this.gbPedidoSeleccionado.Controls.Add(this.lblNMesa);
            this.gbPedidoSeleccionado.Controls.Add(this.label4);
            this.gbPedidoSeleccionado.Controls.Add(this.label5);
            this.gbPedidoSeleccionado.Controls.Add(this.label3);
            this.gbPedidoSeleccionado.Controls.Add(this.label2);
            this.gbPedidoSeleccionado.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.gbPedidoSeleccionado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.gbPedidoSeleccionado.Location = new System.Drawing.Point(13, 340);
            this.gbPedidoSeleccionado.Name = "gbPedidoSeleccionado";
            this.gbPedidoSeleccionado.Size = new System.Drawing.Size(360, 140);
            this.gbPedidoSeleccionado.TabIndex = 7;
            this.gbPedidoSeleccionado.TabStop = false;
            this.gbPedidoSeleccionado.Text = "Información del Pedido Seleccionado";
            // 
            // lblPrioridad
            // 
            this.lblPrioridad.AutoSize = true;
            this.lblPrioridad.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrioridad.ForeColor = System.Drawing.Color.DimGray;
            this.lblPrioridad.Location = new System.Drawing.Point(221, 105);
            this.lblPrioridad.Name = "lblPrioridad";
            this.lblPrioridad.Size = new System.Drawing.Size(32, 17);
            this.lblPrioridad.TabIndex = 17;
            this.lblPrioridad.Text = "Ej. 3";
            // 
            // lblFechaPedido
            // 
            this.lblFechaPedido.AutoSize = true;
            this.lblFechaPedido.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaPedido.ForeColor = System.Drawing.Color.DimGray;
            this.lblFechaPedido.Location = new System.Drawing.Point(221, 49);
            this.lblFechaPedido.Name = "lblFechaPedido";
            this.lblFechaPedido.Size = new System.Drawing.Size(126, 17);
            this.lblFechaPedido.TabIndex = 16;
            this.lblFechaPedido.Text = "Ej. 00/00/0000 00:00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label10.Location = new System.Drawing.Point(221, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 17);
            this.label10.TabIndex = 14;
            this.label10.Text = "Prioridad:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label11.Location = new System.Drawing.Point(185, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 21);
            this.label11.TabIndex = 15;
            this.label11.Text = "🚩";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label12.Location = new System.Drawing.Point(221, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 17);
            this.label12.TabIndex = 12;
            this.label12.Text = "Hora:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label13.Location = new System.Drawing.Point(185, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 21);
            this.label13.TabIndex = 13;
            this.label13.Text = "⏱";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.Color.DimGray;
            this.lblEstado.Location = new System.Drawing.Point(43, 105);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(125, 17);
            this.lblEstado.TabIndex = 11;
            this.lblEstado.Text = "Ej. Pendiente o Listo";
            // 
            // lblNMesa
            // 
            this.lblNMesa.AutoSize = true;
            this.lblNMesa.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNMesa.ForeColor = System.Drawing.Color.DimGray;
            this.lblNMesa.Location = new System.Drawing.Point(43, 49);
            this.lblNMesa.Name = "lblNMesa";
            this.lblNMesa.Size = new System.Drawing.Size(39, 17);
            this.lblNMesa.TabIndex = 10;
            this.lblNMesa.Text = "Ej. 13";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label4.Location = new System.Drawing.Point(43, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Estado Cocina:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label5.Location = new System.Drawing.Point(6, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 21);
            this.label5.TabIndex = 9;
            this.label5.Text = "👨‍🍳";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label3.Location = new System.Drawing.Point(43, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mesa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "📅";
            // 
            // gbPedidos
            // 
            this.gbPedidos.Controls.Add(this.label20);
            this.gbPedidos.Controls.Add(this.label1);
            this.gbPedidos.Controls.Add(this.dgvPedidos);
            this.gbPedidos.Controls.Add(this.btnBusqueda1);
            this.gbPedidos.Controls.Add(this.txtBusqueda1);
            this.gbPedidos.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.gbPedidos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.gbPedidos.Location = new System.Drawing.Point(3, 6);
            this.gbPedidos.Name = "gbPedidos";
            this.gbPedidos.Size = new System.Drawing.Size(488, 300);
            this.gbPedidos.TabIndex = 0;
            this.gbPedidos.TabStop = false;
            this.gbPedidos.Text = "1. PEDIDOS LISTOS PARA FACTURAR";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label20.ForeColor = System.Drawing.Color.DimGray;
            this.label20.Location = new System.Drawing.Point(177, 270);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 21);
            this.label20.TabIndex = 7;
            this.label20.Text = "Ej. 10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label1.Location = new System.Drawing.Point(6, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Total de pedidos listos:";
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Location = new System.Drawing.Point(5, 65);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.Size = new System.Drawing.Size(477, 197);
            this.dgvPedidos.TabIndex = 2;
            this.dgvPedidos.SelectionChanged += new System.EventHandler(this.dgvPedidos_SelectionChanged);
            // 
            // btnBusqueda1
            // 
            this.btnBusqueda1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBusqueda1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBusqueda1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBusqueda1.ForeColor = System.Drawing.Color.DimGray;
            this.btnBusqueda1.Location = new System.Drawing.Point(279, 28);
            this.btnBusqueda1.Name = "btnBusqueda1";
            this.btnBusqueda1.Size = new System.Drawing.Size(29, 29);
            this.btnBusqueda1.TabIndex = 1;
            this.btnBusqueda1.Text = "🔍";
            this.btnBusqueda1.UseVisualStyleBackColor = true;
            this.btnBusqueda1.Click += new System.EventHandler(this.btnBusqueda1_Click);
            // 
            // txtBusqueda1
            // 
            this.txtBusqueda1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.txtBusqueda1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusqueda1.Location = new System.Drawing.Point(5, 28);
            this.txtBusqueda1.Name = "txtBusqueda1";
            this.txtBusqueda1.Size = new System.Drawing.Size(275, 29);
            this.txtBusqueda1.TabIndex = 0;
            // 
            // SectionInfo1
            // 
            this.SectionInfo1.BackColor = System.Drawing.Color.DarkRed;
            this.SectionInfo1.Controls.Add(this.lblFechaActual1);
            this.SectionInfo1.Controls.Add(this.btnRegresar);
            this.SectionInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.SectionInfo1.Location = new System.Drawing.Point(0, 0);
            this.SectionInfo1.Name = "SectionInfo1";
            this.SectionInfo1.Size = new System.Drawing.Size(1364, 50);
            this.SectionInfo1.TabIndex = 0;
            // 
            // lblFechaActual1
            // 
            this.lblFechaActual1.AutoSize = true;
            this.lblFechaActual1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblFechaActual1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.lblFechaActual1.Location = new System.Drawing.Point(57, 13);
            this.lblFechaActual1.Name = "lblFechaActual1";
            this.lblFechaActual1.Size = new System.Drawing.Size(97, 21);
            this.lblFechaActual1.TabIndex = 15;
            this.lblFechaActual1.Text = "Fecha Actual";
            // 
            // btnRegresar
            // 
            this.btnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.btnRegresar.Location = new System.Drawing.Point(9, 9);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(42, 30);
            this.btnRegresar.TabIndex = 14;
            this.btnRegresar.Text = "◀";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ModuloFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 585);
            this.Controls.Add(this.tlpAllContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModuloFacturacion";
            this.Text = "Facturacion";
            this.tlpAllContainer.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.pnlOrdenes.ResumeLayout(false);
            this.gbDetallesPedido.ResumeLayout(false);
            this.gbDetallesPedido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesPedido)).EndInit();
            this.gbResumenPago.ResumeLayout(false);
            this.gbResumenPago.PerformLayout();
            this.pnlResumen.ResumeLayout(false);
            this.pnlResumen.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.gbPedidoSeleccionado.ResumeLayout(false);
            this.gbPedidoSeleccionado.PerformLayout();
            this.gbPedidos.ResumeLayout(false);
            this.gbPedidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.SectionInfo1.ResumeLayout(false);
            this.SectionInfo1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAllContainer;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlOrdenes;
        private System.Windows.Forms.GroupBox gbDetallesPedido;
        private System.Windows.Forms.Panel Linea;
        private System.Windows.Forms.Label lblSubTotalDGV;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblTotalItems;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dgvDetallesPedido;
        private System.Windows.Forms.Button btnBusqueda2;
        private System.Windows.Forms.TextBox txtBusqueda2;
        private System.Windows.Forms.GroupBox gbResumenPago;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnImprimirTicket;
        private System.Windows.Forms.Button btnGenerarFactura;
        private System.Windows.Forms.ComboBox cboMetodoPago;
        private System.Windows.Forms.Panel pnlResumen;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblPropina;
        private System.Windows.Forms.Label lblImpuesto;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblCambio;
        private System.Windows.Forms.TextBox txtDineroRecibido;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox gbPedidoSeleccionado;
        private System.Windows.Forms.Label lblPrioridad;
        private System.Windows.Forms.Label lblFechaPedido;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblNMesa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbPedidos;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.Button btnBusqueda1;
        private System.Windows.Forms.TextBox txtBusqueda1;
        private System.Windows.Forms.Panel SectionInfo1;
        private System.Windows.Forms.Label lblFechaActual1;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}