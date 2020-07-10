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

        public static Class ConstructForm<Class>(params object[] args)
        {
            return ConstructForm<Class>("", args);
        }
        public static Class ConstructForm<Class>(string loadingName, params object[] args)
        {
            LoadingScreen loadingScreen = new LoadingScreen(loadingName);
            Thread thread = new Thread(OpenLoadingScreen);
            thread.Start(loadingScreen);

            Class form = ConstructClass<Class>(args);

            loadingScreen.Stop();
            return form;
        }

        public static Class ConstructClass<Class>(params object[] args)
        {
            IEnumerable<Type> argTypes = args.Select(arg => arg.GetType());
            ConstructorInfo ctor = typeof(Class).GetConstructor(argTypes.ToArray());
            if (ctor == null)
            {
                throw new ArgumentException();
            }
            Class obj = (Class)ctor.Invoke(args);
            return obj;
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
            this.Text = "Loading " + loadingName;
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
