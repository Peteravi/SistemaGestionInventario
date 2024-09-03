using MySql.Data.MySqlClient;
using System;

namespace InventoryManagement
{
    public class DatabaseConnection
    {
        private MySqlConnection connection;

        // Constructor
        public DatabaseConnection()
        {
            string connectionString = "Server=localhost;Database=InventarioDB;Uid=root;Pwd=piteravi07;";
            connection = new MySqlConnection(connectionString);
        }

        // Abrir conexión
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // Cerrar conexión
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // Obtener conexión
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}