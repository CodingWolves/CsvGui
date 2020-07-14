using Csv;
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
    public partial class GridView : Form , ICsvFormable
    {
        private CsvForm form = null;
        public GridView()
        {
            InitializeComponent();
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) // row name change caught
            {
                return;
            }
            DataGridViewCell cell = dataGridView[e.ColumnIndex, e.RowIndex];
            if (cell.Value is string)
            {
                if (cell.Value != itemEdit.GetValue())
                {
                    itemEdit.UpdateValue(cell.Value);
                    cell.Value = itemEdit; // recursive method by calling cell.Value to set to CsvItem
                }
            }
            //RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            Dictionary<int, int> columnToGridIndex = new Dictionary<int, int>();
            int gridIndexCount = 0;
            foreach (CsvItem item in form.headRow)
            {
                dataGridView.Columns.Add(item.index.ToString(), item.ToString());
                columnToGridIndex.Add(item.index.GetColumnIndex(), gridIndexCount++);
            }
            foreach (CsvRow row in form)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                int ColumnIndex = 0;
                List<object> objs = new List<object>();
                foreach(CsvItem item in row)
                {
                    while (columnToGridIndex[item.index.GetColumnIndex()] > ColumnIndex)
                    {
                        objs.Add(null);
                        ColumnIndex++;
                    }
                    objs.Add(item);
                    ColumnIndex++;
                }
                dataGridView.Rows.Add(objs.ToArray());
            }
        }

        private void dataGridView_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (e.Cell.Value is CsvItem)
                {
                    CsvItem item = (CsvItem)e.Cell.Value;
                    StripCellPositionLabel.Text = string.Format("row {0}, column {1}", item.GetParent().index, item.index);
                }
                else if (e.Cell.Value is CsvItem)
                {
                    
                }
                itemEdit = (CsvItem)e.Cell.Value;
            }
        }

        private CsvItem itemEdit = null;
        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell cell = dataGridView[e.ColumnIndex, e.RowIndex];
            if (cell.Value is CsvItem)
            {
                itemEdit = (CsvItem)cell.Value;
            }
        }

        private void dataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                DataGridViewCell firstCell = e.Row.Cells[0];
                if (firstCell.Value is CsvItem)
                {
                    CsvItem item = (CsvItem)firstCell.Value;
                    StripCellPositionLabel.Text = string.Format("row {0}", item.GetParent().index);
                }
            }
        }

        private void dataGridView_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                int columnIndex = int.Parse(e.Column.Name);
                StripCellPositionLabel.Text = string.Format("column {0}", columnIndex);

            }
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        public void SetForm(CsvForm form)
        {
            if (form == null)
            {
                return;
            }
            this.form = form;
            RefreshDataGrid();
        }

        public CsvForm GetForm()
        {
            return this.form;
        }
    }
}
