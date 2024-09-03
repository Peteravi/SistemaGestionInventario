using System;
using System.Windows.Forms;

namespace Sistemadegestiondeinventario
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configuración de la aplicación
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ejecuta el formulario principal
            Application.Run(new Form1());
        }
    }
}