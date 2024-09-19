using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Sistemadegestiondeinventario
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;

        public Form1()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Database=InventarioDB;Uid=root;Pwd=piteravi07;";
            connection = new MySqlConnection(connectionString);

            LoadCategorias();
            LoadProveedores();
        }

        // Método para abrir la conexión
        private void OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al abrir la conexión: " + ex.Message);
            }
        }

        // Método para cerrar la conexión
        private void CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cerrar la conexión: " + ex.Message);
            }
        }

        // Método para cargar categorías en el ComboBox
        private void LoadCategorias()
        {
            string query = "SELECT CategoriaID, Nombre FROM Categorias";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                cmbCategoria.DisplayMember = "Nombre";
                cmbCategoria.ValueMember = "CategoriaID";
                cmbCategoria.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Método para cargar proveedores en el ComboBox
        private void LoadProveedores()
        {
            string query = "SELECT ProveedorID, Nombre FROM Proveedores";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                cmbProveedor.DisplayMember = "Nombre";
                cmbProveedor.ValueMember = "ProveedorID";
                cmbProveedor.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los proveedores: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }


        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Obtén los valores de los controles
            string nameFilter = txtName.Text.Trim();
            string selectedCategoria = cmbCategoria.SelectedItem?.ToString().Trim() ?? "";
            string selectedProveedor = cmbProveedor.SelectedItem?.ToString().Trim() ?? "";

            // Imprime los valores de entrada para depuración
            Console.WriteLine($"Filtro Nombre: '{nameFilter}'");
            Console.WriteLine($"Filtro Categoría: '{selectedCategoria}'");
            Console.WriteLine($"Filtro Proveedor: '{selectedProveedor}'");

            // Construye la consulta SQL con parámetros
            string query = "SELECT p.ProductoID, p.Nombre, p.Cantidad, p.Precio, c.Nombre AS Categoria, pr.Nombre AS Proveedor " +
                           "FROM Productos p " +
                           "LEFT JOIN Categorias c ON p.CategoriaID = c.CategoriaID " +
                           "LEFT JOIN Proveedores pr ON p.ProveedorID = pr.ProveedorID " +
                           "WHERE 1=1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query += " AND p.Nombre LIKE @nameFilter";
                parameters.Add(new MySqlParameter("@nameFilter", $"%{nameFilter}%"));
            }

            if (!string.IsNullOrEmpty(selectedCategoria))
            {
                query += " AND c.Nombre = @categoriaFilter";
                parameters.Add(new MySqlParameter("@categoriaFilter", selectedCategoria));
            }

            if (!string.IsNullOrEmpty(selectedProveedor))
            {
                query += " AND pr.Nombre = @proveedorFilter";
                parameters.Add(new MySqlParameter("@proveedorFilter", selectedProveedor));
            }

            // Ejecuta la consulta con parámetros
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    // Asigna el DataTable al DataGridView
                    dataGridView1.DataSource = dt;
                }
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show($"Error al filtrar los productos (SQL): {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }
            // Limpiar los campos
            txtName.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
        }

        // Método para agregar un producto
        private void AddProduct(string name, int quantity, double price, int categoriaId, int proveedorId)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("El nombre del producto no puede estar vacío.");
                return;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.");
                return;
            }

            if (price <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a 0.");
                return;
            }

            if (categoriaId <= 0)
            {
                MessageBox.Show("ID de categoría inválido.");
                return;
            }

            if (proveedorId <= 0)
            {
                MessageBox.Show("ID de proveedor inválido.");
                return;
            }

            string query = "INSERT INTO Productos (Nombre, Cantidad, Precio, CategoriaID, ProveedorID) VALUES (@name, @quantity, @price, @categoriaId, @proveedorId)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                cmd.Parameters.AddWithValue("@proveedorId", proveedorId);

                try
                {
                    OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    // Manejo específico de errores de MySQL
                    switch (ex.Number)
                    {
                        case 1062: // Código de error para entrada duplicada
                            MessageBox.Show("Ya existe un producto con el mismo nombre.");
                            break;
                        case 1452: // Código de error para clave externa
                            MessageBox.Show("El ID de categoría o proveedor no existe.");
                            break;
                        default:
                            MessageBox.Show("Error al añadir el producto: " + ex.Message);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores generales
                    MessageBox.Show("Se produjo un error inesperado: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        // Método para mostrar los productos con información de categorías y proveedores
        private void GetProducts()
        {
            string query = "SELECT p.ProductoID, p.Nombre, p.Cantidad, p.Precio, c.Nombre AS Categoria, pr.Nombre AS Proveedor " +
                           "FROM Productos p " +
                           "LEFT JOIN Categorias c ON p.CategoriaID = c.CategoriaID " +
                           "LEFT JOIN Proveedores pr ON p.ProveedorID = pr.ProveedorID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                // Asegurarse de que el encabezado del DataGridView esté visible
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ColumnHeadersVisible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los productos: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Método para actualizar un producto
        private void UpdateProduct(int id, string name, int quantity, double price, int categoriaId, int proveedorId)
        {
            string query = "UPDATE Productos SET Nombre = @name, Cantidad = @quantity, Precio = @price, CategoriaID = @categoriaId, ProveedorID = @proveedorId WHERE ProductoID = @id";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
            cmd.Parameters.AddWithValue("@proveedorId", proveedorId);

            try
            {
                OpenConnection();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al actualizar el producto: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Método para eliminar un producto
        private void DeleteProduct(int id)
        {
            string query = "DELETE FROM Productos WHERE ProductoID = @id";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                OpenConnection();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar el producto: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Eventos de botón para operaciones CRUD
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, complete todos los campos.", "Campo(s) Vacío(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("La cantidad debe ser un número válido mayor o igual a 0. Por favor, ingrese una cantidad válida.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(txtPrice.Text, out double price) || price < 0)
            {
                MessageBox.Show("El precio debe ser un número válido mayor o igual a 0. Por favor, ingrese un precio válido.", "Precio Inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbCategoria.SelectedValue == null || cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una categoría y un proveedor. Por favor, seleccione ambas opciones.", "Selección Incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int categoriaId = Convert.ToInt32(cmbCategoria.SelectedValue);
            int proveedorId = Convert.ToInt32(cmbProveedor.SelectedValue);

            AddProduct(txtName.Text, quantity, price, categoriaId, proveedorId);
            GetProducts(); // Actualizar la vista de productos

            // Limpiar los campos
            txtId.Clear();
            txtName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;

            MessageBox.Show("Producto agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } 


        private void btnShow_Click(object sender, EventArgs e)
        {
            GetProducts();
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, complete todos los campos.", "Campo(s) Vacío(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("El ID debe ser un número válido mayor a 0. Por favor, ingrese un ID válido.", "ID Inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("La cantidad debe ser un número válido mayor o igual a 0. Por favor, ingrese una cantidad válida.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(txtPrice.Text, out double price) || price < 0)
            {
                MessageBox.Show("El precio debe ser un número válido mayor o igual a 0. Por favor, ingrese un precio válido.", "Precio Inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbCategoria.SelectedValue == null || cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una categoría y un proveedor. Por favor, seleccione ambas opciones.", "Selección Incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int categoriaId = Convert.ToInt32(cmbCategoria.SelectedValue);
            int proveedorId = Convert.ToInt32(cmbProveedor.SelectedValue);

            UpdateProduct(id, txtName.Text, quantity, price, categoriaId, proveedorId);
            GetProducts();

            // Limpiar los campos
            txtId.Clear();
            txtName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;

            MessageBox.Show("Producto actualizado exitosamente.", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del producto que desea eliminar.", "ID Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("El ID debe ser un número válido mayor a 0. Por favor, ingrese un ID válido.", "ID Inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirmar eliminación
            var result = MessageBox.Show("¿Está seguro de que desea eliminar el producto con ID " + id + "?", "Confirmación de Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DeleteProduct(id);
                GetProducts();

                // Limpiar los campos
                txtId.Clear();
                txtName.Clear();
                txtQuantity.Clear();
                txtPrice.Clear();
                cmbCategoria.SelectedIndex = -1;
                cmbProveedor.SelectedIndex = -1;

                MessageBox.Show("Producto eliminado exitosamente.", "Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // Método para exportar datos a Excel
        private void btnExport_Click(object sender, EventArgs e)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Inventario");

                // Agregar encabezados de columna
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dataGridView1.Columns[i].HeaderText;
                }

                // Agregar datos de las filas
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        // Convertir el valor de la celda a string
                        worksheet.Cell(i + 2, j + 1).Value = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivos Excel|*.xlsx",
                    Title = "Guardar archivo Excel"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Los datos se han exportado exitosamente a Excel.", "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hubo un problema al guardar el archivo: {ex.Message}", "Error de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos Excel|*.xlsx;*.xls",
                Title = "Seleccionar archivo Excel"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string connectionString = "Server=localhost;Database=InventarioDB;Uid=root;Pwd=piteravi07;";

                    // Abrir el archivo Excel seleccionado
                    using (var workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        var worksheet = workbook.Worksheet(1); // Supongamos que los datos están en la primera hoja
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Omitir la fila de encabezados

                        bool hasInvalidData = false;

                        foreach (var row in rows)
                        {
                            // Leer los valores de cada celda en la fila
                            int productoId = row.Cell(1).GetValue<int>();
                            string nombre = row.Cell(2).GetValue<string>();
                            int cantidad = row.Cell(3).GetValue<int>();
                            string precioTexto = row.Cell(4).GetValue<string>();
                            string categoria = row.Cell(5).GetValue<string>();
                            string proveedor = row.Cell(6).GetValue<string>();

                            // Convertir precio a formato numérico
                            double precio = 0;
                            if (!string.IsNullOrEmpty(precioTexto))
                            {
                                precioTexto = precioTexto.Replace("$", "").Replace(",", "").Trim();
                                if (!double.TryParse(precioTexto, out precio))
                                {
                                    hasInvalidData = true;
                                    MessageBox.Show("Error al convertir el precio. Por favor, verifique los valores.", "Datos Inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Validar los datos antes de insertarlos
                            if (!string.IsNullOrWhiteSpace(nombre) && cantidad >= 0 && precio >= 0)
                            {
                                // Asignar IDs de categoría y proveedor según las cadenas proporcionadas
                                int categoriaId = GetCategoryId(categoria, connectionString);
                                int proveedorId = GetProviderId(proveedor, connectionString);

                                // Insertar los datos en la base de datos
                                AddProduct(productoId, nombre, cantidad, precio, categoriaId, proveedorId, connectionString);
                            }
                            else
                            {
                                hasInvalidData = true;
                                // Se puede optar por registrar en un log o mostrar detalles específicos del error
                                // en este caso simplemente mostramos un mensaje y detenemos la importación.
                                MessageBox.Show("Datos inválidos en una de las filas del archivo Excel. Por favor, verifique los valores.", "Datos Inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        if (!hasInvalidData)
                        {
                            MessageBox.Show("Datos importados exitosamente.", "Importación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GetProducts(connectionString); // Actualizar la vista de productos
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al importar los datos: {ex.Message}", "Error de Importación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Método para obtener el ID de la categoría
        private int GetCategoryId(string categoria, string connectionString)
        {
            int categoriaId = -1; // Valor predeterminado para indicar que no se encontró la categoría
            string query = "SELECT CategoriaID FROM Categorias WHERE Nombre = @Nombre";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", categoria);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out categoriaId))
                    {
                        return categoriaId;
                    }
                    else
                    {
                        // Opcional: Agregar categoría si no existe
                        // Puedes implementar la lógica para agregar la categoría aquí
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener el ID de la categoría: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return categoriaId;
        }

        // Método para obtener el ID del proveedor
        private int GetProviderId(string proveedor, string connectionString)
        {
            int proveedorId = -1; // Valor predeterminado para indicar que no se encontró el proveedor
            string query = "SELECT ProveedorID FROM Proveedores WHERE Nombre = @Nombre";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", proveedor);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out proveedorId))
                    {
                        return proveedorId;
                    }
                    else
                    {
                        // Opcional: Agregar proveedor si no existe
                        // Puedes implementar la lógica para agregar el proveedor aquí
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener el ID del proveedor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return proveedorId;
        }

        // Método para añadir un producto a la base de datos
        private void AddProduct(int productoId, string nombre, int cantidad, double precio, int categoriaId, int proveedorId, string connectionString)
        {
            string query = "INSERT INTO Productos (ProductoID, Nombre, Cantidad, Precio, CategoriaID, ProveedorID) VALUES (@ProductoID, @Nombre, @Cantidad, @Precio, @CategoriaID, @ProveedorID)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductoID", productoId);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Cantidad", cantidad);
                    command.Parameters.AddWithValue("@Precio", precio);
                    command.Parameters.AddWithValue("@CategoriaID", categoriaId);
                    command.Parameters.AddWithValue("@ProveedorID", proveedorId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para obtener productos (Asegúrate de que coincida con la firma correcta)
        private void GetProducts(string connectionString)
        {
            // Implementa la lógica para obtener los productos y actualizar la vista aquí
            // Asegúrate de que el método no reciba un argumento si no está definido para hacerlo
        } 


    }
}

