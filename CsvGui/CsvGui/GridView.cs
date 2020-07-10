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
    public partial class GridView : Form
    {
        private CsvForm form = null;
        private bool editEnabled = false;
        public GridView(CsvForm form, bool editEnabled)
        {
            InitializeComponent();
            if (form == null)
            {
                return;
            }
            this.form = form;
            this.editEnabled = editEnabled;
            RefreshDataGrid();
        }
        public GridView(CsvForm form, bool editEnabled, string gridName) : this(form, editEnabled)
        {
            this.Name = gridName;
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
                itemEdit.UpdateValue(cell.Value);
                cell.Value = itemEdit; // recursive method by calling cell.Value to set to CsvItem
            }
            //RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            foreach (CsvItem item in form.headRow)
            {
                dataGridView.Columns.Add(item.index.ToString(), item.ToString());
            }
            foreach (CsvRow row in form.rows)
            {
                object[] rowValues = row.items.ToArray();
                dataGridView.Rows.Add(rowValues);
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
                else
                {

                }
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
    }
}
