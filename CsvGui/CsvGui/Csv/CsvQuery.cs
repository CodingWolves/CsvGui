using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Csv
{
    class CsvQuery
    {
        public static CsvForm GetRows(CsvForm form, List<int> rowIndexes, bool connected)
        {
            CsvForm newForm = new CsvForm();
            newForm.name = form.name + " RowsQuery";

            foreach (int rowIndex in rowIndexes)
            {
                CsvRow row = form.GetRow(rowIndex);
                if (row!=null && connected == false)
                {
                    row = (CsvRow)row.Clone();
                }
                newForm.AddRow(row);
            }
            return newForm;
        }
        public static CsvForm GetColumns(CsvForm form, List<int> columnIndexes, bool connected)
        {
            CsvForm newForm = new CsvForm();
            newForm.name = form.name + " ColumnsQuery";

            foreach (int columnIndex in columnIndexes)
            {
                CsvColumn column = form.GetColumn(columnIndex);
                foreach (CsvItem item in column)
                {
                    newForm.Append(column, connected);
                }
            }
            return newForm;
        }
        public static CsvForm GetRowsContainsValue(CsvForm form, CsvItem item, bool connected)
        {
            CsvForm newForm = (CsvForm)form.Clone();
            newForm.name = form.name + "_RowsContainsValueQuery";

            //TODO 

            return newForm;
        }
    }
}
