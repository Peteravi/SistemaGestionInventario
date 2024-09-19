namespace Sistemadegestiondeinventario
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Releasing resources
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelInputFields = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelId = new System.Windows.Forms.Label();
            this.labelCategoria = new System.Windows.Forms.Label();
            this.labelProveedor = new System.Windows.Forms.Label();

            // Initialize components
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelInputFields.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();

            // DataGridView configuration
            ConfigureDataGridView();

            // Input panel configuration
            ConfigureInputPanel();

            // Buttons panel configuration
            ConfigureButtonsPanel();

            // Form1 configuration
            ConfigureForm();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelInputFields.ResumeLayout(false);
            this.panelInputFields.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void ConfigureForm()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(230, 230, 230); // Light gray background
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelInputFields);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Gestión de Inventario";
        }

        private void ConfigureDataGridView()
        {
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 320);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(740, 300);
            this.dataGridView1.TabIndex = 0;

            // Column configuration
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.RowHeadersVisible = false;

            // Header style
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(66, 133, 244); // Blue
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            // Row style
            this.dataGridView1.RowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.dataGridView1.RowsDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dataGridView1.RowsDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void ConfigureInputPanel()
        {
            this.panelInputFields.BackColor = System.Drawing.Color.White;
            this.panelInputFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInputFields.Controls.Add(this.txtName);
            this.panelInputFields.Controls.Add(this.txtQuantity);
            this.panelInputFields.Controls.Add(this.txtPrice);
            this.panelInputFields.Controls.Add(this.txtId);
            this.panelInputFields.Controls.Add(this.cmbCategoria);
            this.panelInputFields.Controls.Add(this.cmbProveedor);
            this.panelInputFields.Controls.Add(this.labelName);
            this.panelInputFields.Controls.Add(this.labelQuantity);
            this.panelInputFields.Controls.Add(this.labelPrice);
            this.panelInputFields.Controls.Add(this.labelId);
            this.panelInputFields.Controls.Add(this.labelCategoria);
            this.panelInputFields.Controls.Add(this.labelProveedor);
            this.panelInputFields.Location = new System.Drawing.Point(30, 30);
            this.panelInputFields.Name = "panelInputFields";
            this.panelInputFields.Size = new System.Drawing.Size(740, 180);
            this.panelInputFields.TabIndex = 1;

            // TextBox and ComboBox configurations
            ConfigureInputFields();
        }
        #region Panel de Campos de Entrada
        private void ConfigureInputFields()
        {
            // txtName
            this.txtName.Location = new System.Drawing.Point(100, 10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 22);
            this.txtName.TabIndex = 1;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.ForeColor = System.Drawing.Color.Black;

            // txtQuantity
            this.txtQuantity.Location = new System.Drawing.Point(100, 50);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(200, 22);
            this.txtQuantity.TabIndex = 2;
            this.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuantity.BackColor = System.Drawing.Color.White;
            this.txtQuantity.ForeColor = System.Drawing.Color.Black;

            // txtPrice
            this.txtPrice.Location = new System.Drawing.Point(100, 90);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(200, 22);
            this.txtPrice.TabIndex = 3;
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrice.BackColor = System.Drawing.Color.White;
            this.txtPrice.ForeColor = System.Drawing.Color.Black;

            // txtId
            this.txtId.Location = new System.Drawing.Point(450, 10);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(200, 22);
            this.txtId.TabIndex = 4;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.BackColor = System.Drawing.Color.White;
            this.txtId.ForeColor = System.Drawing.Color.Black;

            // cmbCategoria
            this.cmbCategoria.Location = new System.Drawing.Point(450, 50);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(200, 22);
            this.cmbCategoria.TabIndex = 5;
            this.cmbCategoria.BackColor = System.Drawing.Color.White;
            this.cmbCategoria.ForeColor = System.Drawing.Color.Black;

            // cmbProveedor
            this.cmbProveedor.Location = new System.Drawing.Point(450, 90);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(200, 22);
            this.cmbProveedor.TabIndex = 6;
            this.cmbProveedor.BackColor = System.Drawing.Color.White;
            this.cmbProveedor.ForeColor = System.Drawing.Color.Black;

            // Labels configuration
            ConfigureLabels();
        }

        private void ConfigureLabels()
        {
            // labelName
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(20, 10);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(75, 15);
            this.labelName.TabIndex = 7;
            this.labelName.Text = "Nombre:";
            this.labelName.ForeColor = System.Drawing.Color.Black; // Negro
            this.labelName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); // Fuente mejorada

            // labelQuantity
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Location = new System.Drawing.Point(20, 50);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(75, 15);
            this.labelQuantity.TabIndex = 8;
            this.labelQuantity.Text = "Cantidad:";
            this.labelQuantity.ForeColor = System.Drawing.Color.Black; // Negro
            this.labelQuantity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); // Fuente mejorada

            // labelPrice
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(20, 90);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(48, 15);
            this.labelPrice.TabIndex = 9;
            this.labelPrice.Text = "Precio:";
            this.labelPrice.ForeColor = System.Drawing.Color.Black; // Negro
            this.labelPrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); // Fuente mejorada

            // labelId
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(350, 10);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(22, 15);
            this.labelId.TabIndex = 10;
            this.labelId.Text = "ID:";
            this.labelId.ForeColor = System.Drawing.Color.Black; // Negro
            this.labelId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); // Fuente mejorada

            // labelCategoria
            this.labelCategoria.AutoSize = true;
            this.labelCategoria.Location = new System.Drawing.Point(350, 50);
            this.labelCategoria.Name = "labelCategoria";
            this.labelCategoria.Size = new System.Drawing.Size(75, 15);
            this.labelCategoria.TabIndex = 11;
            this.labelCategoria.Text = "Categoría:";
            this.labelCategoria.ForeColor = System.Drawing.Color.Black; // Negro
            this.labelCategoria.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); // Fuente mejorada

            // labelProveedor
            this.labelProveedor.AutoSize = true;
            this.labelProveedor.Location = new System.Drawing.Point(350, 90);
            this.labelProveedor.Name = "labelProveedor";
            this.labelProveedor.Size = new System.Drawing.Size(80, 15);
            this.labelProveedor.TabIndex = 12;
            this.labelProveedor.Text = "Proveedor:";
            this.labelProveedor.ForeColor = System.Drawing.Color.Black; // Negro
            this.labelProveedor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); // Fuente mejorada
        }

        private void ConfigureButtonsPanel()
        {
            this.panelButtons.BackColor = System.Drawing.Color.White;
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButtons.Controls.Add(this.btnAdd);
            this.panelButtons.Controls.Add(this.btnUpdate);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnShow);
            this.panelButtons.Controls.Add(this.btnExport);
            this.panelButtons.Controls.Add(this.btnImport);
            this.panelButtons.Controls.Add(this.btnFilter);
            this.panelButtons.Location = new System.Drawing.Point(30, 220);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(740, 90);
            this.panelButtons.TabIndex = 2;

            // Button configurations
            ConfigureButtons();
        }
        #endregion

        #region Panel de Botones
        private void ConfigureButtons()
        {
            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(10, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Añadir";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 204, 102); // Verde brillante
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnUpdate
            this.btnUpdate.Location = new System.Drawing.Point(120, 10);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 30);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Actualizar";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(0, 123, 255); // Azul brillante
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(230, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(255, 87, 34); // Naranja
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnShow
            this.btnShow.Location = new System.Drawing.Point(340, 10);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(100, 30);
            this.btnShow.TabIndex = 3;
            this.btnShow.Text = "Mostrar";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.BackColor = System.Drawing.Color.FromArgb(76, 175, 80); // Verde
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShow.FlatAppearance.BorderSize = 0;
            this.btnShow.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);


            // btnExport
            this.btnExport.Location = new System.Drawing.Point(450, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Exportar";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(156, 39, 176); // Morado
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);


            // btnImport
            this.btnImport.Location = new System.Drawing.Point(560, 10);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(100, 30);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "Importar";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(33, 150, 243); // Azul claro
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

            // btnFilter
            this.btnFilter.Location = new System.Drawing.Point(670, 10);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(60, 30);
            this.btnFilter.TabIndex = 6;
            this.btnFilter.Text = "Filtrar";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(255, 193, 7); // Amarillo
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
        }
        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panelInputFields;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label labelCategoria;
        private System.Windows.Forms.Label labelProveedor;
    }
    #endregion
}
