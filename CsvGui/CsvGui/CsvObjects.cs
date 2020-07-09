using System;
using System.Collections;
using System.Collections.Generic;

namespace Csv
{
    public class CsvForm : IEnumerable
    {
        public List<CsvRow> rows = null;
        public CsvRow headRow = null;
        public CsvForm()
        {
            this.rows = new List<CsvRow>();
        }
        public CsvForm(CsvForm form) : this()
        {
            this.rows.AddRange(form.rows);
        }

        public void AddRow(CsvRow row)
        {
            rows.Add(row);
        }
        public void SetHeadRow(CsvRow headRow)
        {
            this.headRow = headRow;
        }
        public bool HasHead()
        {
            return headRow != null;
        }

        public CsvItem this[int rowIndex, int columnIndex]
        {
            get
            {
                return this.rows[rowIndex][columnIndex];
            }
        }    
        public CsvRow this[int rowIndex]
        {
            get
            {
                return this.rows[rowIndex];
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

        public List<CsvItem> GetColumnItems(int columnIndex)
        {
            List<CsvItem> items = new List<CsvItem>();
            foreach (CsvRow row in this.rows)
            {
                if (columnIndex < row.items.Count)
                {
                    items.Add((CsvItem)row[columnIndex].Clone());
                }
                else
                {
                    items.Add(new CsvString(String.Empty));
                }
            }
            return items;
        }

        public IEnumerator GetEnumerator()
        {
            return this.rows.GetEnumerator();
        }
    }

    public class CsvRow : IEnumerable
    {
        public List<CsvItem> items = null;
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

        public CsvItem this[int index]
        {
            get
            {
                return this.items[index];
            }
            set
            {
                this.items[index] = value;
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
                this.items.Add(CsvItem.Null);
            }
            if (value.GetType() == this.items[itemIndex].GetValueType() )
            {
                this.items[itemIndex].UpdateValue(value);
            }
            else
            {
                this.items[itemIndex] = CsvItem.CreateCsvItem(value, this);
            }
                
        }

        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }


    public abstract class CsvItem : ICloneable
    {
        public static readonly CsvItem Null = new CsvItemNull();
        public static CsvItem CreateCsvItem(object value, CsvRow parent)
        {
            return new CsvString((string)value, parent);
        }

        protected CsvRow parent = null;

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

        
    }
}
