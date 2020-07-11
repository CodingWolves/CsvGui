using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Csv
{
    public class CsvForm : IEnumerable
    {
        public List<CsvRow> rows = null;
        public CsvRow headRow = null;
        public bool editable = true;
        public string name = null;
        public CsvForm()
        {
            this.rows = new List<CsvRow>();
            this.headRow = new CsvRow();
        }
        public CsvForm(CsvForm form) : this()
        {
            this.rows.AddRange(form.rows);
        }

        public void AddRow(CsvRow row)
        {
            rows.Add(row);
            this.ExpandHeadRow(row);
        }
        public void SetHeadRow(CsvRow headRow)
        {
            this.headRow = headRow;
        }

        public CsvItem this[int rowIndex, int columnIndex]
        {
            get
            {
                return this.GetRow(rowIndex).GetItem(columnIndex);
            }
        }    
        public CsvRow this[int rowIndex]
        {
            get
            {
                return this.GetRow(rowIndex);
            }
        }

        public void UpdateValue(int rowIndex, int columnIndex, object value)
        {
            while (rowIndex >= this.rows.Count)
            {
                this.rows.Add(new CsvRow());
            }
            this.rows[rowIndex].UpdateValue(columnIndex, value);
        }
        private void ExpandHeadRow(CsvRow row)
        {
            while (row.items.Count > headRow.items.Count)
            {
                headRow.AddItem(CsvItem.CreateNullCsvItem(headRow, headRow.items[headRow.items.Count-1].index+1));
            }
        }

        public CsvRow GetRow(int rowIndex)
        {
            foreach (CsvRow row in this.rows)
            {
                if (row.index == rowIndex)
                {
                    return row;
                }
            }
            return null;
        }

        public List<CsvItem> GetColumnItems(int columnIndex)
        {
            List<CsvItem> items = new List<CsvItem>();
            foreach (CsvRow row in this.rows)
            {
                if (columnIndex <= row.items.Count)
                {
                    items.Add((CsvItem)row.GetItem(columnIndex).Clone());
                }
                else
                {
                    items.Add(CsvItem.CreateNullCsvItem(row, row.items.Count));
                }
            }
            return items;
        }

        public List<CsvItem> GetUniqueValueColumnItems(int columnIndex)
        {
            List<CsvItem> items = GetColumnItems(columnIndex);

            for (int i = 0; i < items.Count; i++)
            {
                object iValue = items[i].GetValue();
                if (iValue == null)
                {
                    items.RemoveAt(i);
                    i--;
                    continue;
                }
                for (int j = i + 1; j < items.Count; j++)
                {
                    object jValue = items[j].GetValue();
                    if (iValue.Equals(jValue))
                    {
                        items.RemoveAt(j);
                        j--;
                    }
                }
            }

            return items;
        }

        public IEnumerator GetEnumerator()
        {
            return this.rows.GetEnumerator();
        }

        public void Save(string filePath)
        {
            StreamWriter stream = new StreamWriter(filePath, false);
            foreach(CsvRow row in this.rows)
            {
                row.Save(stream);
            }
            stream.Close();
        }
    }

    public class CsvRow : IEnumerable
    {
        public List<CsvItem> items = null;
        public int index = -1;
        public CsvRow()
        {
            this.items = new List<CsvItem>();
        }
        public CsvRow(CsvRow row) : this()
        {
            foreach (CsvItem item in row.items)
            {
                this.items.Add((CsvItem)item.Clone());
            }
        }
        public CsvRow(int rowIndex):this()
        {
            this.index = rowIndex;
        }

        public CsvItem this[int index]
        {
            get
            {
                return this.GetItem(index);
            }
        }
        public void AddItem(CsvItem item)
        {
            this.items.Add(item);
        }

        public List<Object> GetAllValues()
        {
            List<Object> objs = new List<Object>();
            foreach (CsvItem item in this)
            {
                objs.Add(item.GetValue());
            }
            return objs;
        }

        public void UpdateValue(int itemIndex, object value)
        {
            while (itemIndex >= this.items.Count)
            {
                this.items.Add(CsvItem.CreateNullCsvItem(this, this.items.Count));
            }
            if (value.GetType() == this.items[itemIndex].GetValueType())
            {
                this.items[itemIndex].UpdateValue(value);
            }
            else
            {
                this.items[itemIndex] = CsvItem.CreateCsvItem(value, this, this.items[itemIndex].index);
            }
                
        }

        public CsvItem GetItem(int itemIndex)
        {
            foreach (CsvItem item in this.items)
            {
                if (item.index == itemIndex)
                {
                    return item;
                }
            }
            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        public void Save(StreamWriter stream)
        {
            if (this.items.Count > 0)
                stream.Write(this.items[0].ToSaveableString());
            for (int i=1;i<this.items.Count;i++)
            {
                stream.Write(',');
                stream.Write(this.items[i].ToSaveableString());
            }
            stream.WriteLine();
        }
    }


    public abstract class CsvItem : ICloneable
    {
        public static readonly CsvItem Null = new CsvItemNull();
        public static CsvItem CreateCsvItem(object value, CsvRow parent, int index)
        {
            if (value is null || (value is string && (string)value == ""))
            {
                return CreateNullCsvItem(parent, index);
            }
            return new CsvString((string)value, parent, index);
        }
        public static CsvItem CreateNullCsvItem(CsvRow parent, int index)
        {
            CsvItemNull item = new CsvItemNull();
            item.index = index;
            item.parent = parent;
            return item;
        }

        protected CsvRow parent = null;
        public int index = -1;

        public abstract object GetValue();
        public abstract Type GetValueType();

        public abstract void UpdateValue(object value);

        public bool IsNull()
        {
            return this is CsvItemNull;
        }
        public CsvRow GetParent()
        {
            return parent;
        }
        public abstract object Clone();
        public override abstract string ToString();
        public abstract string ToSaveableString();

        private class CsvItemNull : CsvItem
        {
            public override object Clone()
            {
                return this;
            }

            public override object GetValue()
            {
                return null;
            }

            public override Type GetValueType()
            {
                return null;
            }

            public override string ToSaveableString()
            {
                return "";
            }

            public override string ToString()
            {
                return "";
            }

            public override void UpdateValue(object value)
            {
                return;
            }
        }
    }

    public class CsvString : CsvItem
    {
        protected string value = null;

        public CsvString(string str)
        {
            this.value = str;
        }
        public CsvString(string str, CsvRow parent) : this(str)
        {
            this.parent = parent;
        }
        public CsvString(string str, CsvRow parent, int itemIndex) : this(str, parent)
        {
            this.index = itemIndex;
        }
        public CsvString(CsvString item) : this(item.value, item.parent)
        {
        }
        public override object GetValue()
        {
            return this.value;
        }
        public override Type GetValueType()
        {
            return value.GetType();
        }
        public override void UpdateValue(object value)
        {
            this.value = (string)value;
        }

        public override object Clone()
        {
            return new CsvString(this);
        }

        public override string ToString()
        {
            return this.value.ToString();
        }

        public override string ToSaveableString()
        {
            if (this.value.IndexOf(",") >= 0)
            {
                return '"' + this.value + '"';
            }
            return this.value;
        }
    }
}
