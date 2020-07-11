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
            //Application.Run(new GridView());

            string filePath = Resources.SMALL_CSV_FILENAME;

            DateTime start = DateTime.Now;
            CsvForm form = Csv.Csv.ReadFile(filePath, true);
            //CsvForm form = CsvReader.ReadFile(filePath, true, 32438);
            //CsvForm form = CsvReader.ReadFile(filePath, true, new TimeSpan(0,0,0,0,10));
            //MessageBox.Show(DateTime.Now.Subtract(start).Milliseconds + " milisecs to read");

            //GridView gridView = LoadingScreen.ConstructForm<GridView>(form);
            //Application.Run(gridView);

            Application.Run(new FormInfo(form));

            //form.Save(Resources.TEST_SAVE_CSV_FILENAME);
        }
    }
}
