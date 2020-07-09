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
    public partial class Form1 : Form
    {
        private CsvForm form = null;
        public Form1()
        {
            InitializeComponent();
            form = CsvReader.ReadFile("C:\\Users\\IDO\\Documents\\GitHub\\CsvGui\\Tests\\small.csv", true);
            RefreshDataGrid();
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;
            form.UpdateValue(e.RowIndex, e.ColumnIndex, dataGridView[e.ColumnIndex,e.RowIndex].Value);
            //RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            foreach (CsvItem item in form.headRow)
            {
                dataGridView.Columns.Add((string)item.GetValue(), (string)item.GetValue());
            }
            foreach (CsvRow row in form.rows)
            {
                object[] rowValues = row.GetAllValues().ToArray();
                dataGridView.Rows.Add(rowValues);
            }
        }

        private void dataGridView_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                StripCellPositionLabel.Text = string.Format("row {0}, column {1}", e.Cell.RowIndex, e.Cell.ColumnIndex);
            }
        }
    }
}
