namespace Restaurante_SoftGourmet
{
    partial class ModuloAlmacen
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
            this.tlpAllContainer = new System.Windows.Forms.TableLayoutPanel();
            this.AllContainer = new System.Windows.Forms.Panel();
            this.dgvBD = new System.Windows.Forms.DataGridView();
            this.pnlEditar = new System.Windows.Forms.Panel();
            this.SectionInfo3 = new System.Windows.Forms.Panel();
            this.lblFechaActual1 = new System.Windows.Forms.Label();
            this.SectionInfo2 = new System.Windows.Forms.Panel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.SectionInfo1 = new System.Windows.Forms.Panel();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.cboSeleccionarTabla = new System.Windows.Forms.ComboBox();
            this.tlpAllContainer.SuspendLayout();
            this.AllContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBD)).BeginInit();
            this.SectionInfo3.SuspendLayout();
            this.SectionInfo2.SuspendLayout();
            this.SectionInfo1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAllContainer
            // 
            this.tlpAllContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.tlpAllContainer.ColumnCount = 1;
            this.tlpAllContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAllContainer.Controls.Add(this.AllContainer, 0, 0);
            this.tlpAllContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAllContainer.Location = new System.Drawing.Point(0, 0);
            this.tlpAllContainer.Name = "tlpAllContainer";
            this.tlpAllContainer.RowCount = 1;
            this.tlpAllContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAllContainer.Size = new System.Drawing.Size(800, 450);
            this.tlpAllContainer.TabIndex = 1;
            // 
            // AllContainer
            // 
            this.AllContainer.Controls.Add(this.dgvBD);
            this.AllContainer.Controls.Add(this.pnlEditar);
            this.AllContainer.Controls.Add(this.SectionInfo3);
            this.AllContainer.Controls.Add(this.SectionInfo2);
            this.AllContainer.Controls.Add(this.SectionInfo1);
            this.AllContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllContainer.Location = new System.Drawing.Point(3, 3);
            this.AllContainer.Name = "AllContainer";
            this.AllContainer.Size = new System.Drawing.Size(794, 444);
            this.AllContainer.TabIndex = 0;
            // 
            // dgvBD
            // 
            this.dgvBD.AllowUserToOrderColumns = true;
            this.dgvBD.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.dgvBD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBD.Location = new System.Drawing.Point(0, 100);
            this.dgvBD.Name = "dgvBD";
            this.dgvBD.Size = new System.Drawing.Size(474, 294);
            this.dgvBD.TabIndex = 4;
            // 
            // pnlEditar
            // 
            this.pnlEditar.AutoScroll = true;
            this.pnlEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.pnlEditar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlEditar.Location = new System.Drawing.Point(474, 100);
            this.pnlEditar.Name = "pnlEditar";
            this.pnlEditar.Size = new System.Drawing.Size(320, 294);
            this.pnlEditar.TabIndex = 3;
            // 
            // SectionInfo3
            // 
            this.SectionInfo3.BackColor = System.Drawing.Color.DarkRed;
            this.SectionInfo3.Controls.Add(this.lblFechaActual1);
            this.SectionInfo3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SectionInfo3.Location = new System.Drawing.Point(0, 394);
            this.SectionInfo3.Name = "SectionInfo3";
            this.SectionInfo3.Size = new System.Drawing.Size(794, 50);
            this.SectionInfo3.TabIndex = 2;
            // 
            // lblFechaActual1
            // 
            this.lblFechaActual1.AutoSize = true;
            this.lblFechaActual1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblFechaActual1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.lblFechaActual1.Location = new System.Drawing.Point(9, 14);
            this.lblFechaActual1.Name = "lblFechaActual1";
            this.lblFechaActual1.Size = new System.Drawing.Size(97, 21);
            this.lblFechaActual1.TabIndex = 6;
            this.lblFechaActual1.Text = "Fecha Actual";
            // 
            // SectionInfo2
            // 
            this.SectionInfo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.SectionInfo2.Controls.Add(this.btnEditar);
            this.SectionInfo2.Controls.Add(this.btnIngresar);
            this.SectionInfo2.Dock = System.Windows.Forms.DockStyle.Top;
            this.SectionInfo2.Location = new System.Drawing.Point(0, 50);
            this.SectionInfo2.Name = "SectionInfo2";
            this.SectionInfo2.Size = new System.Drawing.Size(794, 50);
            this.SectionInfo2.TabIndex = 1;
            // 
            // btnEditar
            // 
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.btnEditar.Location = new System.Drawing.Point(130, 10);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(110, 30);
            this.btnEditar.TabIndex = 12;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnIngresar
            // 
            this.btnIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.btnIngresar.Location = new System.Drawing.Point(9, 10);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(110, 30);
            this.btnIngresar.TabIndex = 11;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = true;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // SectionInfo1
            // 
            this.SectionInfo1.BackColor = System.Drawing.Color.DarkRed;
            this.SectionInfo1.Controls.Add(this.btnRegresar);
            this.SectionInfo1.Controls.Add(this.label15);
            this.SectionInfo1.Controls.Add(this.cboSeleccionarTabla);
            this.SectionInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.SectionInfo1.Location = new System.Drawing.Point(0, 0);
            this.SectionInfo1.Name = "SectionInfo1";
            this.SectionInfo1.Size = new System.Drawing.Size(794, 50);
            this.SectionInfo1.TabIndex = 0;
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
            this.btnRegresar.TabIndex = 13;
            this.btnRegresar.Text = "◀";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.label15.Location = new System.Drawing.Point(452, 12);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 20);
            this.label15.TabIndex = 15;
            this.label15.Text = "Categoria :";
            // 
            // cboSeleccionarTabla
            // 
            this.cboSeleccionarTabla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSeleccionarTabla.BackColor = System.Drawing.Color.DarkRed;
            this.cboSeleccionarTabla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSeleccionarTabla.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cboSeleccionarTabla.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.cboSeleccionarTabla.FormattingEnabled = true;
            this.cboSeleccionarTabla.Location = new System.Drawing.Point(543, 9);
            this.cboSeleccionarTabla.Name = "cboSeleccionarTabla";
            this.cboSeleccionarTabla.Size = new System.Drawing.Size(242, 28);
            this.cboSeleccionarTabla.TabIndex = 14;
            // 
            // ModuloAlmacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlpAllContainer);
            this.Name = "ModuloAlmacen";
            this.Text = "ModuloAlmacen";
            this.tlpAllContainer.ResumeLayout(false);
            this.AllContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBD)).EndInit();
            this.SectionInfo3.ResumeLayout(false);
            this.SectionInfo3.PerformLayout();
            this.SectionInfo2.ResumeLayout(false);
            this.SectionInfo1.ResumeLayout(false);
            this.SectionInfo1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAllContainer;
        private System.Windows.Forms.Panel AllContainer;
        private System.Windows.Forms.DataGridView dgvBD;
        private System.Windows.Forms.Panel pnlEditar;
        private System.Windows.Forms.Panel SectionInfo3;
        private System.Windows.Forms.Label lblFechaActual1;
        private System.Windows.Forms.Panel SectionInfo2;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Panel SectionInfo1;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboSeleccionarTabla;
    }
}