using Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            Application.Run(new GridView(null, false));

            CsvForm form = CsvReader.ReadFile("C:\\Users\\IDO\\Documents\\GitHub\\CsvGui\\Tests\\big.csv", true);
            GridView gridView = LoadingScreen.ConstructForm<GridView>("GridView54326543765", form, true);

            Application.Run(gridView);
        }
    }
}
