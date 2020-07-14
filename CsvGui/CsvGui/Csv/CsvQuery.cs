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
                    if (connected == false)
                    {
                        newForm.AppendColumn((CsvColumn)column.Clone());
                    }
                    else
                    {
                        newForm.AppendColumn(column);
                    }
                }
            }
            return newForm;
        }
        public static CsvForm GetRowsContainsValue(CsvForm form, CsvItem item, bool connected)
        {
            CsvForm newForm = (CsvForm)form.Clone();
            newForm.name = form.name + "_RowsContainsValueQuery";

            newForm.RowProgressionComparer(KeepRowsContainsItemValue, item);

            return newForm;
        }

        private static int KeepRowsContainsItemValue(CsvForm form,CsvRow row, CsvItem comparedItem)
        {
            if (row.GetAllValues().Contains(comparedItem.GetValue()))
            {
                return 1;
            }
            else
            {
                form.RemoveRow(row.index.GetRowIndex());
                return 1;
            }
        }

        //private static int KeepColumnIndex(CsvForm form, CsvColumn column, CsvItem comparedItem)
        //{
        //    if (column.index.GetColumnIndex() == comparedItem.index.GetColumnIndex())
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        form.RemoveColumn(column.index.GetColumnIndex());
        //        return 0;
        //    }
        //}

    }
}
