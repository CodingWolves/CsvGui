using Csv;
using CsvGui.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvGui
{
    public partial class FormInfo : Form, ICsvFormable
    {
        private CsvForm form = null;
        public FormInfo()
        {
            InitializeComponent();
        }

        private void RefreshInfo()
        {
            FilenameTextBox.Text = form.name;
            RowCountTextBox.Text = form.rows.Count.ToString();
            ColumnHeaderComboBox.Items.Clear();
            ColumnIndexComboBox.Items.Clear();
            if (form.headRow != null)
            {
                foreach (CsvItem item in form.headRow)
                {
                    ColumnHeaderComboBox.Items.Add(item);
                    ColumnIndexComboBox.Items.Add(item.index);
                }
            }
        }

        private void RefreshColumnUniqueValues(int columnIndex)
        {
            ColumnHeaderValuesListView.Items.Clear();
            List<CsvItem> columnItems = form.GetColumnItems(columnIndex);
            List<CsvItem> uniqueValueItems = CsvItem.GetUniqueItems(columnItems);
            foreach (CsvItem item in uniqueValueItems)
            {
                ColumnHeaderValuesListView.Items.Add(item.ToString());
            }
            UniqueValuesLabel.Text = uniqueValueItems.Count.ToString() + Resources.NUMBER_UNIQUE_VALUES_STRING;

            int nullCount = form.rows.Count - columnItems.Count;
            NullValuesLabel.Text = nullCount.ToString() + Resources.NUMBER_NULL_VALUES_STRING;

            ColumnHeaderValuesPanel.Visible = true;
        }

        #region Form Interactions

        private void ColumnHeaderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CsvItem itemColumnHeader = (CsvItem)ColumnHeaderComboBox.SelectedItem;
            if (ColumnIndexComboBox.SelectedItem is null || itemColumnHeader.index != (int)ColumnIndexComboBox.SelectedItem)
            {
                ColumnIndexComboBox.SelectedItem = itemColumnHeader.index;
            }
        }

        private void ColumnIndexComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int columnIndex = (int)ColumnIndexComboBox.SelectedItem;
            if (ColumnHeaderComboBox.SelectedItem is null || columnIndex != ((CsvItem)ColumnHeaderComboBox.SelectedItem).index)
            {
                ColumnHeaderComboBox.SelectedItem = form.headRow.GetItem(columnIndex);
            }
            RefreshColumnUniqueValues(columnIndex);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.form.filePath))
            {
                saveFileDialog.FileName = this.form.filePath;
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                this.form.Save(filePath);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                CsvForm newForm = Csv.Csv.LoadCsv(filePath, false);
                ApplicationQueue.GetInstance().AddFormQueue(LoadingScreen.ConstructForm<FormInfo>(newForm));
            }
        }

        private void OpenGridViewButton_Click(object sender, EventArgs e)
        {
            ApplicationQueue.GetInstance().AddFormQueue(LoadingScreen.ConstructForm<GridView>(this.form));
        }

        #endregion

        public void SetForm(CsvForm form)
        {
            this.form = form;
            RefreshInfo();
        }

        public CsvForm GetForm()
        {
            return this.form;
        }

        
    }
}