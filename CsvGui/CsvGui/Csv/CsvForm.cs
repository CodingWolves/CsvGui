using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Csv
{
    public class CsvForm : IEnumerable, ICloneable
    {
        public Dictionary<CsvIndex, CsvRow> rowDictionary = null;
        public CsvRow headRow = null;
        public string name = null;
        public string filePath = null;
        public CsvForm()
        {
            this.headRow = new CsvRow();
            this.rowDictionary = new Dictionary<CsvIndex, CsvRow>();
        }
        public CsvForm(CsvForm form) : this()
        {
            foreach (CsvRow row in form.rowDictionary.Values)
            {
                this.rowDictionary.Add(row.index, (CsvRow)row.Clone());
            }
            this.headRow = (CsvRow)form.headRow.Clone();
            this.name = form.name;
            this.filePath = form.filePath;
        }

        public CsvItem this[CsvIndex index]
        {
            get
            {
                return this.GetRow(index.GetRowIndex()).GetItem(index);
            }
        }
        public CsvItem this[int rowIndex, int columnIndex]
        {
            get
            {
                return this.GetRow(rowIndex).GetItem(new CsvIndex(rowIndex, columnIndex));
            }
        }
        public CsvRow this[int rowIndex]
        {
            get
            {
                return this.GetRow(rowIndex);
            }
        }

        public int RowCount
        {
            get
            {
                return this.rowDictionary.Count;
            }
        }
        public int ColumnCount
        {
            get
            {
                return this.headRow.items.Count;
            }
        }

        public void AddRow(CsvRow row)
        {
            if (row == null)
            {
                return;
            }
            rowDictionary.Add(row.index,row);
        }
        public void SetHeadRow(CsvRow headRow)
        {
            this.headRow = headRow;
        }

        public void AppendItem(CsvItem item)
        {
            CsvRow row = GetRow(item.index.GetRowIndex());
            if (row != null)
            {
                row.UpdateValue(item.index, item.GetValue());
            }
            else
            {
                row = new CsvRow(item.index.GetRowIndex());
                row.AddItem(item);
                this.rowDictionary.Add(row.index, row);
            }
        }
        public void AppendRow(CsvRow row)
        {
            CsvRow formRow = GetRow(row.index);
            if (formRow != null)
            {
                foreach (CsvItem newItem in row) // checking every item in row
                {
                    if (formRow.HasItemIndex(newItem.index))
                    {
                        formRow.UpdateValue(newItem.index, newItem.GetValue());
                    }
                    else
                    {
                        formRow.AddItem(newItem);
                    }
                }
            }
            else
            {
                formRow = row;
                this.AddRow(row);
            }
            ExpandHeadRow(formRow);
        }
        public void AppendColumn(CsvColumn column)
        {
            foreach (CsvItem item in column)
            {
                AppendItem(item);
            }
        }
        public void Append(CsvForm form)
        {
            if (form.headRow != null)
            {
                foreach (CsvItem item in form.headRow)
                {
                    this.headRow.UpdateValue(item.index, item.GetValue());
                }
            }
            foreach(CsvRow row in form)
            {
                this.AppendRow(row);
            }
        }

        private void ExpandHeadRow(CsvRow row)
        {
            foreach (CsvItem item in row)
            {
                if (headRow.HasItemIndex(item.index) == false)
                {
                    headRow.AddItem(CsvItem.CreateNullCsvItem(headRow, item.index));
                }
            }
        }
        /// <summary>
        /// Searches in all items an item out of column, long procedure, not recommended
        /// </summary>
        public void ExpandHeadRow()
        {
            HashSet<int> columnIndexes = new HashSet<int>();
            foreach (CsvRow row in this)
            {
                foreach (CsvItem item in row)
                {
                    columnIndexes.Add(item.index.GetColumnIndex());
                }
            }
            foreach (int columnIndex in columnIndexes)
            {
                CsvIndex index = new CsvIndex(columnIndex, IndexType.Column);
                if (headRow.HasItemIndex(index) == false)
                {
                    headRow.AddItem(CsvItem.CreateNullCsvItem(headRow, index));
                }
            }
        }

        public CsvRow GetRow(int rowIndex)
        {
            CsvIndex index = new CsvIndex(rowIndex, IndexType.Row);
            return this.rowDictionary[index];
        }
        public CsvRow GetRow(CsvIndex index)
        {
            if (this.rowDictionary.ContainsKey(index) == false)
                return null;
            return this.rowDictionary[index];
        }

        public CsvColumn GetColumn(int columnIndex)
        {
            CsvColumn column = new CsvColumn(this.rowDictionary.Count);
            foreach (CsvRow row in this)
            {
                CsvItem rowItem = row.GetItem(new CsvIndex(columnIndex, IndexType.Column));
                if (rowItem != null)
                {
                    column.AddItem(rowItem);
                }
            }
            return column;
        }

        public List<CsvIndex> GetRowIndexes()
        {
            List<CsvIndex> indexes = new List<CsvIndex>();

            foreach (CsvRow row in this)
            {
                indexes.Add(row.index);
            }

            return indexes;
        }
        public List<CsvIndex> GetColumnIndexes()
        {
            List<CsvIndex> indexes = new List<CsvIndex>();

            foreach (CsvItem item in this.headRow)
            {
                indexes.Add(item.index);
            }

            return indexes;
        }

        public List<CsvItem> GetUniqueValueColumnItems(int columnIndex)
        {
            CsvColumn items = GetColumn(columnIndex);
            return CsvItem.GetUniqueItems(items);
        }

        public void RemoveRow(int rowIndex)
        {
            RemoveRow(new CsvIndex(rowIndex, IndexType.Row));
        }
        public void RemoveRow(CsvIndex index)
        {
            this.rowDictionary.Remove(index);
        }

        public IEnumerator GetEnumerator()
        {
            return this.rowDictionary.Values.GetEnumerator();
        }

        public void Save(string filePath)
        {
            StreamWriter stream = new StreamWriter(filePath, false);
            foreach (CsvRow row in this)
            {
                row.Save(stream);
            }
            stream.Close();
            this.filePath = filePath;
        }

        public object Clone()
        {
            return new CsvForm(this);
        }

        /// <summary>
        /// do rowProgressionFunc as compared with comparedItem and returns the progress in rows (+1/-1/0/...)
        /// </summary>
        /// <param name="rowProgressionFunc">does things and returns the progress in rows</param>
        /// <param name="comparedItem"></param>
        public void RowProgressionComparer(Func<CsvForm, CsvRow, CsvItem, int> rowProgressionFunc, CsvItem comparedItem)
        {
            int i = 0;
            List<CsvIndex> rowIndexes = this.GetRowIndexes();
            while (i < rowIndexes.Count)
            {
                CsvRow row = this.GetRow(rowIndexes[i].GetRowIndex());
                i += rowProgressionFunc(this, row, comparedItem);
            }
        }

        /// <summary>
        /// do rowProgressionFunc as compared with comparedItem and returns the progress in rows (+1/-1/0/...)
        /// </summary>
        /// <param name="rowProgressionFunc">does things and returns the progress in rows</param>
        /// <param name="comparedItem"></param>
        public void ColumnProgressionComparer(Func<CsvForm, CsvColumn, CsvItem, int> rowProgressionFunc, CsvItem comparedItem)
        {
            int i = 0;
            this.ExpandHeadRow();
            List<CsvIndex> columnIndexes = this.GetColumnIndexes();
            while (i < columnIndexes.Count)
            {
                CsvColumn column = this.GetColumn(columnIndexes[i].GetColumnIndex());
                i += rowProgressionFunc(this, column, comparedItem);
            }
        }
    }
}
