using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvGui
{
    public partial class FormLoader : Form
    {
        public FormLoader()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            bool hasHead = HeadCheckBox.Checked;
            ApplicationQueue.InstanceAddFormQueue(LoadingScreen.ConstructForm<FormInfo>(filePath,hasHead));
        }

        private void FilePathOpenDialog_Click(object sender, EventArgs e)
        {
            if (FilePathDialog.ShowDialog() == DialogResult.OK)
            {
                FilePathTextBox.Text = FilePathDialog.FileName;
            }
        }

        private void FilePathOpenFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "/select, \""+FilePathTextBox.Text+"\"");
        }
    }
}
