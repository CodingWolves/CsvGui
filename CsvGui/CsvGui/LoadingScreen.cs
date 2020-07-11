using Csv;
using CsvGui.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvGui
{
    public partial class LoadingScreen : Form
    {
        public static Class ConstructForm<Class>(CsvForm formArg) where Class : Csv.ICsvFormable, new()
        {
            return ConstructForm<Class>(formArg, formArg.name);
        }
        public static Class ConstructForm<Class>(CsvForm formArg, string loadingName) where Class : Csv.ICsvFormable , new()
        {
            LoadingScreen loadingScreen = new LoadingScreen(loadingName);
            Thread thread = new Thread(OpenLoadingScreen);
            thread.Start(loadingScreen);

            Class classForm = new Class();
            classForm.SetForm(formArg);

            loadingScreen.Stop();
            return classForm;
        }

        private static void OpenLoadingScreen(object arg)
        {
            Application.Run((LoadingScreen)arg);
        }

        private bool isLoading = true;

        public LoadingScreen(string loadingName)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = Resources.LOADING_STRING + loadingName;
            this.LoadingNameLabel.Text = loadingName;
        }

        private void LoadingScreen_Shown(object sender, EventArgs e)
        {
            int count = 0;
            while (isLoading)
            {
                this.ProgressLabel.Text = String.Concat(Enumerable.Repeat(".",(count++/10)%4));
                Application.DoEvents();
                Thread.Sleep(50);
            }
            this.Close();
        }

        public void Stop()
        {
            lock (this)
            {
                this.isLoading = false;
            }
        }

    }
}
