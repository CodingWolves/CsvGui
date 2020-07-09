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

        public void UpdateValue(int rowIndex, int itemIndex, object value)
        {
            this.rows[rowIndex].UpdateValue(itemIndex, value);
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
            if (value is string && this.items[itemIndex] is CsvString)
            {
                this.items[itemIndex].UpdateValue(value);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }


    public abstract class CsvItem : ICloneable
    {
        protected CsvRow parent = null;

        public abstract object GetValue();

        public abstract void UpdateValue(object value);

        public abstract bool IsNull();
        public CsvRow GetParent()
        {
            return parent;
        }
        public abstract object Clone();
        
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
        public override void UpdateValue(object value)
        {
            this.value = (string)value;
        }

        public override bool IsNull()
        {
            return String.IsNullOrEmpty(this.value);
        }

        public override object Clone()
        {
            return new CsvString(this);
        }

        
    }
}
