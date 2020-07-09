using Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvGui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CsvForm form = CsvReader.ReadFile("C:\\Users\\IDO\\Documents\\GitHub\\CsvGui\\Tests\\big.csv", true);
            Application.Run(new GridView(form));          
        }
    }
}
