using System;
using System.Windows.Forms;

namespace Sistemadegestiondeinventario
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicaci�n.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configuraci�n de la aplicaci�n
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ejecuta el formulario principal
            Application.Run(new Form1());
        }
    }
}