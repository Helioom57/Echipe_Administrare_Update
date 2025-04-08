using System;
using System.Windows.Forms;
using Echipe_Administrare.Forms;



namespace Echipe_Administrare
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            System.Windows.Forms.Application.Run(new MainForm());
        }
    }
}