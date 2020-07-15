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
        public Dictionary<int, CsvRow> rowDictionary = null;
        public CsvRow headRow = null;
        public string name = null;
        public string filePath = null;
        public CsvForm()
        {
            this.headRow = new CsvRow();
            this.rowDictionary = new Dictionary<int, CsvRow>();
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

        public void Append(CsvItem item, bool clone)
        {
            CsvRow row = GetRow(item.index.GetRowIndex());
            if (row != null)
            {
                row.Append(item, clone);
            }
            else
            {
                row = new CsvRow(item.index.GetRowIndex());
                row.Append(item, clone);
                this.rowDictionary.Add(row.index, row);
            }
        }
        public void Append(CsvRow row, bool clone)
        {
            CsvRow formRow = GetRow(row.index);
            if (formRow != null)
            {
                formRow.Append(row, clone);
            }
            else
            {
                if (clone)
                {
                    formRow = row.Clone();
                }
                else
                {
                    formRow = row;
                }
                this.AddRow(row);
            }
            ExpandHeadRow(formRow);
        }
        public void Append(CsvColumn column, bool clone)
        {
            foreach (CsvItem item in column)
            {
                Append(item, clone);
            }
        }
        public void Append(CsvForm form,bool clone)
        {
            if (form.headRow != null)
            {
                this.headRow.Append(form.headRow, clone);
            }
            foreach(CsvRow row in form)
            {
                this.Append(row, clone);
            }
        }

        private void ExpandHeadRow(CsvRow row)
        {
            foreach (CsvItem item in row)
            {
                if (headRow.HasIndex(item.index.GetColumnIndex()) == false)
                {
                    headRow.Append(CsvItem.CreateNullCsvItem(headRow, item.index), false);
                }
            }
        }

        // need to find if fast or not
        public void ExpandHeadRow()
        {
            HashSet<int> columnIndexes = new HashSet<int>();
            foreach (CsvRow row in this)
            {
                foreach (int index in row.items.Keys)
                {
                    columnIndexes.Add(index);
                }
            }
            foreach (int columnIndex in columnIndexes) // start to here - humongus.csv 23ms
            {
                if (headRow.HasIndex(columnIndex) == false)
                {
                    headRow.Append(CsvItem.CreateNullCsvItem(headRow, columnIndex, IndexType.Column),false);
                }
            }
        }

        public bool HasRow(int rowIndex)
        {
            return rowDictionary.ContainsKey(rowIndex);
        }
        public CsvRow GetRow(int rowIndex)
        {
            if (HasRow(rowIndex))
            {
                return this.rowDictionary[rowIndex];
            }
            return null;
        }
        public List<CsvRow> GetRows()
        {
            return rowDictionary.Values.ToList();
        }

        public CsvColumn GetColumn(int columnIndex)
        {
            CsvColumn column = new CsvColumn(columnIndex);
            foreach (CsvRow row in this)
            {
                List<CsvItem> itemList = row.Where((CsvItem item) => item.index.GetColumnIndex() == columnIndex);
                foreach (CsvItem item in itemList)
                {
                    column.Append(item,false);
                }
            }
            return column;
        }
        public List<CsvColumn> GetColumns()
        {
            List<CsvIndex> columnIndexes = GetColumnIndexes();
            Dictionary<CsvIndex, CsvColumn> columns = new Dictionary<CsvIndex, CsvColumn>();
            List<CsvItem> itemsCompared = new List<CsvItem>();
            foreach (CsvIndex index in columnIndexes)
            {
                itemsCompared.Add(CsvItem.CreateCsvItemForIndex(index));
            }
            foreach (CsvRow row in this) // long operation
            {
                foreach (CsvItem item in row)
                {
                    CsvIndex itemColumnIndex = new CsvIndex(item.index.GetColumnIndex(), IndexType.Column);
                    if (columns.ContainsKey(itemColumnIndex))
                    {
                        columns[itemColumnIndex].Append(item,false);
                    }
                    else
                    {
                        columns.Add(itemColumnIndex, new CsvColumn(itemColumnIndex.GetColumnIndex()));
                        columns[itemColumnIndex].Append(item,false);
                    }
                }
            }
            return columns.Values.ToList();
        }

        public List<CsvIndex> GetRowIndexes()
        {
            List<CsvIndex> indexes = new List<CsvIndex>();

            foreach (CsvRow row in this)
            {
                indexes.Add(new CsvIndex(row.index,IndexType.Row));
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
            rowDictionary.Remove(rowIndex);
        }

        public void RemoveColumn(int columnIndex)
        {
            foreach (CsvRow row in this)
            {
                foreach (CsvItem item in row)
                {
                    row.RemoveAt(item.index.GetColumnIndex());
                }
            }
        }
        public void RemoveColumn(CsvIndex index)
        {
            RemoveColumn(index.GetColumnIndex());
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

        public CsvForm WhereColumn(IEqualityComparer<CsvColumn> comparer, CsvColumn compared)
        {
            CsvForm newForm = new CsvForm();
            List<CsvColumn> columns = GetColumns();
            foreach (CsvColumn column in columns)
            {
                if (comparer.Equals(column, compared))
                {
                    newForm.Append(column,false);
                }
            }
            return newForm;
        }
        public CsvForm WhereRow(IEqualityComparer<CsvRow> comparer, CsvRow compared)
        {
            CsvForm newForm = new CsvForm();
            List<CsvRow> rows = new List<CsvRow>(rowDictionary.Values);
            foreach (CsvRow row in rows)
            {
                if (comparer.Equals(row, compared))
                {
                    newForm.Append(row,true);
                }
            }
            return newForm;
        }

        public object Clone()
        {
            return new CsvForm(this);
        }


        public List<CsvRow> Where(Func<CsvRow, bool> func)
        {
            List<CsvRow> rows = new List<CsvRow>();
            foreach (CsvRow row in this)
            {
                if (func(row))
                {
                    rows.Add(row);
                }
            }
            return rows;
        }

        public List<CsvColumn> Where(Func<CsvColumn, bool> func)
        {
            List<CsvColumn> columns = new List<CsvColumn>();
            foreach (CsvColumn column in this.GetColumns())
            {
                if (func(column))
                {
                    columns.Add(column);
                }
            }
            return columns;
        }
    }
}
