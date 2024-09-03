using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

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

        // M�todo para abrir la conexi�n
        private void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // M�todo para cerrar la conexi�n
        private void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // M�todo para cargar categor�as en el ComboBox
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
                MessageBox.Show("Error al cargar las categor�as: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // M�todo para cargar proveedores en el ComboBox
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

        // M�todo para agregar un producto
        private void AddProduct(string name, int quantity, double price, int categoriaId, int proveedorId)
        {
            string query = "INSERT INTO Productos (Nombre, Cantidad, Precio, CategoriaID, ProveedorID) VALUES (@name, @quantity, @price, @categoriaId, @proveedorId)";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
            cmd.Parameters.AddWithValue("@proveedorId", proveedorId);

            try
            {
                OpenConnection();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Producto a�adido correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al a�adir el producto: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // M�todo para mostrar los productos con informaci�n de categor�as y proveedores
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

        // M�todo para actualizar un producto
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
                MessageBox.Show("Producto actualizado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el producto: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // M�todo para eliminar un producto
        private void DeleteProduct(int id)
        {
            string query = "DELETE FROM Productos WHERE ProductoID = @id";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                OpenConnection();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Producto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el producto: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Eventos de bot�n para operaciones CRUD
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || !double.TryParse(txtPrice.Text, out double price)
                || cmbCategoria.SelectedValue == null || cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Por favor, ingrese valores num�ricos v�lidos para la cantidad y precio.");
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
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            GetProducts();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            if (!int.TryParse(txtId.Text, out int id) || !int.TryParse(txtQuantity.Text, out int quantity)
                || !double.TryParse(txtPrice.Text, out double price) || cmbCategoria.SelectedValue == null || cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Por favor, ingrese valores num�ricos v�lidos.");
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
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID.");
                return;
            }

            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Por favor, ingrese un valor num�rico v�lido para el ID.");
                return;
            }

            DeleteProduct(id);
            GetProducts();

            // Limpiar los campos
            txtId.Clear();
            txtName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
        }
    }
}