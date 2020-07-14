using Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvGui
{
    class ApplicationQueue
    {
        private static ApplicationQueue instance = new ApplicationQueue();
        public static ApplicationQueue GetInstance()
        {
            return instance;
        }

        private Queue<Form> formQueue = new Queue<Form>();
        private List<Form> openForms = new List<Form>();
        public void AddFormQueue(Form form)
        {
            lock (this)
            {
                formQueue.Enqueue(form);
            }
        }
        public void RemoveOpenForm(object sender)
        {
            if (sender is Form)
            {
                Form form = (Form)sender;
                form.Dispose();
                lock (this)
                {
                    this.openForms.Remove(form);
                }
            }
        }

        public static void InstanceAddFormQueue(Form form)
        {
            instance.AddFormQueue(form);
        }

        public static void RunApplicationQueue()
        {
            bool stilRunning = true;
            while (stilRunning)
            {
                lock (instance)
                {
                    if (instance.formQueue.Count > 0)
                    {
                        Form form = instance.formQueue.Dequeue();
                        form.FormClosed += FormClosed;
                        Thread th = new Thread(Run);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start(form);
                        instance.openForms.Add(form);
                    }
                }
                Thread.Sleep(10);
                lock (instance)
                {
                    stilRunning = instance.openForms.Count>0;
                }
            }
        }

        private static void FormClosed(object sender, FormClosedEventArgs e)
        {
            lock (instance)
            {
                instance.RemoveOpenForm(sender);
            }
        }
        private static void Run(object form)
        {
            Application.Run((Form)form);
        }
    }
}
