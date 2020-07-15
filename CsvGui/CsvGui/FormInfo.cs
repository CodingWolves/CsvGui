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
using static System.Windows.Forms.ListView;

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
            RowCountTextBox.Text = form.RowCount.ToString();
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

        private List<CsvItem> uniqueValueItems;
        private const int ColumnValuesLoadLimit = 50;
        private void RefreshColumnUniqueValues(int columnIndex)
        {
            ColumnHeaderValuesListView.Items.Clear();
            CsvColumn column = form.GetColumn(columnIndex);
            uniqueValueItems = CsvItem.GetUniqueItems(column);
            int count = 0;
            foreach (CsvItem item in uniqueValueItems)
            {
                count++;
                ListViewItem viewItem = new ListViewItem(item.ToString());
                viewItem.Tag = item;
                ColumnHeaderValuesListView.Items.Add(viewItem);
                if (count >= ColumnValuesLoadLimit)
                {
                    break;
                }
            }
            ColumnValuesViewMoreButton.Visible = count >= ColumnValuesLoadLimit;
            UniqueValuesLabel.Text = uniqueValueItems.Count.ToString() + Resources.NUMBER_UNIQUE_VALUES_STRING;
            int nullCount = form.RowCount - column.items.Count;
            NullValuesLabel.Text = nullCount.ToString() + Resources.NUMBER_NULL_VALUES_STRING;
            ColumnHeaderValuesPanel.Visible = true;
        }

        private void ColumnValuesViewMoreButton_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i= ColumnHeaderValuesListView.Items.Count+1;i< uniqueValueItems.Count;i++) 
            {
                CsvItem item = uniqueValueItems[i];
                count++;
                ColumnHeaderValuesListView.Items.Add(item.ToString());
                if (count >= ColumnValuesLoadLimit)
                {
                    break;
                }
            }
            ColumnValuesViewMoreButton.Visible = count >= ColumnValuesLoadLimit;
        }

        #region Form Interactions

        private void ColumnHeaderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CsvItem itemColumnHeader = (CsvItem)ColumnHeaderComboBox.SelectedItem;
            if (ColumnIndexComboBox.SelectedItem is null || itemColumnHeader.index != (CsvIndex)ColumnIndexComboBox.SelectedItem)
            {
                ColumnIndexComboBox.SelectedItem = itemColumnHeader.index;
            }
        }

        private void ColumnIndexComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CsvIndex index = (CsvIndex)ColumnIndexComboBox.SelectedItem;
            if (ColumnHeaderComboBox.SelectedItem is null || index != ((CsvItem)ColumnHeaderComboBox.SelectedItem).index)
            {
                ColumnHeaderComboBox.SelectedItem = form.headRow.GetItem(index);
            }
            RefreshColumnUniqueValues(index.GetColumnIndex());
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
            ApplicationQueue.InstanceAddFormQueue(new FormLoader());
        }

        private void OpenGridViewButton_Click(object sender, EventArgs e)
        {
            if (form != null)
            {
                ApplicationQueue.InstanceAddFormQueue(LoadingScreen.ConstructForm<GridView>(this.form));
            }
        }

        private void ColumnValuesOpenSelectedButton_Click(object sender, EventArgs e)
        {
            int columnIndex = ((CsvIndex)ColumnIndexComboBox.SelectedItem).GetColumnIndex();
            List<CsvItem> items = new List<CsvItem>();
            SelectedListViewItemCollection collection = ColumnHeaderValuesListView.SelectedItems;
            foreach (ListViewItem viewItem in collection)
            {
                items.Add(((CsvItem)viewItem.Tag));
            }

            CsvForm queryForm = new CsvForm();
            queryForm.name = form.name + "_SelectedRowsItemValuesByColumn"+ columnIndex;
            foreach (CsvItem item in items)
            {
                queryForm.Append(CsvQuery.GetRowsContainsValue(form, item, false),true);
            }
        }

        #endregion


        #region Query Interactions

        private void OperationComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((string)OperationComboBox1.SelectedItem)
            {
                case "Select":
                case "Remove": break;
            }
        }

        private IEnumerator<CsvItemCollction> OperationItemComboBoxEnumerator = null;
        private const int ItemsLoadLimit = 100;
        private int lastLoadedOperationItemIndex = 0;
        private string ViewMoreString = "[View More]";
        private void OperationOnComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationItemComboBox1.Items.Clear();
            lastLoadedOperationItemIndex = 0;
            List<CsvItemCollction> OperationItemComboBoxList = new List<CsvItemCollction>();
            switch ((string)OperationOnComboBox1.SelectedItem)
            {
                case "Columns":
                    {
                        foreach (CsvColumn column in form.GetColumns())
                        {
                            OperationItemComboBoxList.Add(column);
                        }
                    }
                    break;
                case "Rows":
                    {
                        foreach (CsvRow row in form.GetRows())
                        {
                            OperationItemComboBoxList.Add(row);
                        }
                    }
                    break;
                case "All":
                    {
                        foreach (CsvColumn column in form.GetColumns())
                        {
                            OperationItemComboBoxList.Add(column);
                        }
                        foreach (CsvRow row in form.GetRows())
                        {
                            OperationItemComboBoxList.Add(row);
                        }
                    }
                    break;
            }
            int count = 0;
            OperationItemComboBoxEnumerator = OperationItemComboBoxList.GetEnumerator();
            while (OperationItemComboBoxEnumerator.MoveNext())
            {
                if (count < ItemsLoadLimit)
                {
                    OperationItemComboBox1.Items.Add(OperationItemComboBoxEnumerator.Current);
                    lastLoadedOperationItemIndex++;
                    count++;
                    continue;
                }
                break;
            }
            if (lastLoadedOperationItemIndex < OperationItemComboBoxList.Count)
            {
                OperationItemComboBox1.Items.Add(ViewMoreString);
            }

        }

        private void OperationItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OperationItemComboBox1.SelectedItem.Equals(ViewMoreString))
            {
                OperationItemComboBox1.Items.Remove(ViewMoreString);
                int count = 0;
                do
                {
                    if (count++ >= ItemsLoadLimit)
                    {
                        break;
                    }
                    OperationItemComboBox1.Items.Add(OperationItemComboBoxEnumerator.Current);
                    lastLoadedOperationItemIndex++;
                }
                while (OperationItemComboBoxEnumerator.MoveNext());
                if (OperationItemComboBoxEnumerator.Current != null)
                {
                    OperationItemComboBox1.Items.Add(ViewMoreString);
                }
            }
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