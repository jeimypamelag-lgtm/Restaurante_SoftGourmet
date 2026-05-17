namespace Restaurante_SoftGourmet.Clases.Carrito
{
    partial class ModuloProduccion
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlOrdenes = new System.Windows.Forms.Panel();
            this.pnlOrdenSeleccionada = new System.Windows.Forms.Panel();
            this.SectionInfo3 = new System.Windows.Forms.Panel();
            this.lblFechaActual2 = new System.Windows.Forms.Label();
            this.SectionInfo2 = new System.Windows.Forms.Panel();
            this.btnPriorizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEntregar = new System.Windows.Forms.Button();
            this.SectionInfo1 = new System.Windows.Forms.Panel();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.tlpAllContainer.SuspendLayout();
            this.pnlContainer.SuspendLayout();
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
            this.tlpAllContainer.Controls.Add(this.pnlContainer, 0, 0);
            this.tlpAllContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAllContainer.Location = new System.Drawing.Point(0, 0);
            this.tlpAllContainer.Name = "tlpAllContainer";
            this.tlpAllContainer.RowCount = 1;
            this.tlpAllContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAllContainer.Size = new System.Drawing.Size(800, 450);
            this.tlpAllContainer.TabIndex = 1;
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.pnlOrdenes);
            this.pnlContainer.Controls.Add(this.pnlOrdenSeleccionada);
            this.pnlContainer.Controls.Add(this.SectionInfo3);
            this.pnlContainer.Controls.Add(this.SectionInfo2);
            this.pnlContainer.Controls.Add(this.SectionInfo1);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(3, 3);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(794, 444);
            this.pnlContainer.TabIndex = 0;
            // 
            // pnlOrdenes
            // 
            this.pnlOrdenes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.pnlOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrdenes.Location = new System.Drawing.Point(0, 100);
            this.pnlOrdenes.Name = "pnlOrdenes";
            this.pnlOrdenes.Size = new System.Drawing.Size(524, 294);
            this.pnlOrdenes.TabIndex = 5;
            // 
            // pnlOrdenSeleccionada
            // 
            this.pnlOrdenSeleccionada.AutoScroll = true;
            this.pnlOrdenSeleccionada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.pnlOrdenSeleccionada.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlOrdenSeleccionada.Location = new System.Drawing.Point(524, 100);
            this.pnlOrdenSeleccionada.Name = "pnlOrdenSeleccionada";
            this.pnlOrdenSeleccionada.Size = new System.Drawing.Size(270, 294);
            this.pnlOrdenSeleccionada.TabIndex = 4;
            // 
            // SectionInfo3
            // 
            this.SectionInfo3.BackColor = System.Drawing.Color.DarkRed;
            this.SectionInfo3.Controls.Add(this.lblFechaActual2);
            this.SectionInfo3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SectionInfo3.Location = new System.Drawing.Point(0, 394);
            this.SectionInfo3.Name = "SectionInfo3";
            this.SectionInfo3.Size = new System.Drawing.Size(794, 50);
            this.SectionInfo3.TabIndex = 2;
            // 
            // lblFechaActual2
            // 
            this.lblFechaActual2.AutoSize = true;
            this.lblFechaActual2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblFechaActual2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.lblFechaActual2.Location = new System.Drawing.Point(9, 14);
            this.lblFechaActual2.Name = "lblFechaActual2";
            this.lblFechaActual2.Size = new System.Drawing.Size(97, 21);
            this.lblFechaActual2.TabIndex = 5;
            this.lblFechaActual2.Text = "Fecha Actual";
            // 
            // SectionInfo2
            // 
            this.SectionInfo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.SectionInfo2.Controls.Add(this.btnPriorizar);
            this.SectionInfo2.Controls.Add(this.btnEliminar);
            this.SectionInfo2.Controls.Add(this.btnEntregar);
            this.SectionInfo2.Dock = System.Windows.Forms.DockStyle.Top;
            this.SectionInfo2.Location = new System.Drawing.Point(0, 50);
            this.SectionInfo2.Name = "SectionInfo2";
            this.SectionInfo2.Size = new System.Drawing.Size(794, 50);
            this.SectionInfo2.TabIndex = 1;
            // 
            // btnPriorizar
            // 
            this.btnPriorizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPriorizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPriorizar.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnPriorizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.btnPriorizar.Location = new System.Drawing.Point(155, 9);
            this.btnPriorizar.Name = "btnPriorizar";
            this.btnPriorizar.Size = new System.Drawing.Size(140, 30);
            this.btnPriorizar.TabIndex = 16;
            this.btnPriorizar.Text = "Priorizar";
            this.btnPriorizar.UseVisualStyleBackColor = true;
            this.btnPriorizar.Click += new System.EventHandler(this.btnPriorizar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnEliminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.btnEliminar.Location = new System.Drawing.Point(301, 9);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(140, 30);
            this.btnEliminar.TabIndex = 15;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEntregar
            // 
            this.btnEntregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntregar.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnEntregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(39)))));
            this.btnEntregar.Location = new System.Drawing.Point(9, 9);
            this.btnEntregar.Name = "btnEntregar";
            this.btnEntregar.Size = new System.Drawing.Size(140, 30);
            this.btnEntregar.TabIndex = 14;
            this.btnEntregar.Text = "Entregar Comida";
            this.btnEntregar.UseVisualStyleBackColor = true;
            this.btnEntregar.Click += new System.EventHandler(this.btnEntregar_Click);
            // 
            // SectionInfo1
            // 
            this.SectionInfo1.BackColor = System.Drawing.Color.DarkRed;
            this.SectionInfo1.Controls.Add(this.btnRegresar);
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
            this.btnRegresar.TabIndex = 14;
            this.btnRegresar.Text = "◀";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // ModuloProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlpAllContainer);
            this.Name = "ModuloProduccion";
            this.Text = "ModuloProduccion";
            this.tlpAllContainer.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.SectionInfo3.ResumeLayout(false);
            this.SectionInfo3.PerformLayout();
            this.SectionInfo2.ResumeLayout(false);
            this.SectionInfo1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAllContainer;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlOrdenes;
        private System.Windows.Forms.Panel pnlOrdenSeleccionada;
        private System.Windows.Forms.Panel SectionInfo3;
        private System.Windows.Forms.Label lblFechaActual2;
        private System.Windows.Forms.Panel SectionInfo2;
        private System.Windows.Forms.Button btnPriorizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnEntregar;
        private System.Windows.Forms.Panel SectionInfo1;
        private System.Windows.Forms.Button btnRegresar;
    }
}