using Csv;
using CsvGui.Properties;
using System;
using System.Collections.Generic;
using System.IO;
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
            Application.Run(new GridView());

            string filename = Resources.BIG_CSV_FILENAME;

            CsvForm form = CsvReader.ReadFile(filename, true);
            GridView gridView = LoadingScreen.ConstructForm<GridView>(form);

            Application.Run(gridView);
            //form.Save(Resources.TEST_SAVE_CSV_FILENAME);
        }
    }
}
