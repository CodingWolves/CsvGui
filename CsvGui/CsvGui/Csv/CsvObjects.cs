using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Csv
{
    public class CsvItemCollction:IEnumerable, ICloneable, IComparable<CsvItemCollction>
    {
        public Dictionary<CsvIndex,CsvItem> items = null;
        public CsvIndex index = new CsvIndex();
        public CsvItemCollction()
        {
            this.items = new Dictionary<CsvIndex, CsvItem>();
        }
        public CsvItemCollction(CsvItemCollction collection) : this()
        {
            this.index = (CsvIndex)collection.index.Clone();
            foreach (CsvItem item in collection)
            {
                this.items.Add(item.index,item.Clone(this));
            }
        }
        public CsvItemCollction(CsvIndex rowIndex) : this()
        {
            this.index = rowIndex;
        }

        public CsvItem this[CsvIndex index]
        {
            get
            {
                return this.GetItem(index);
            }
        }
        public void AddItem(CsvItem item)
        {
            this.items.Add(item.index, item);
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

        public void UpdateValue(CsvIndex itemIndex, object value)
        {
            CsvItem item = GetItem(itemIndex);
            if (item!=null && value.GetType() == item.GetValueType())
            {
                item.UpdateValue(value);
            }
            else
            {
                SetItem(itemIndex, CsvItem.CreateCsvItem(value, this, itemIndex));
            }

        }

        public CsvItem GetItem(CsvIndex itemIndex)
        {
            foreach (CsvItem item in this.items.Values)
            {
                if (item.index.Equals(itemIndex))
                {
                    return item;
                }
            }
            return null;
        }

        public void SetItem(CsvIndex itemIndex, CsvItem item)
        {
            this.items[itemIndex] = item;
        }

        public IEnumerator GetEnumerator()
        {
            return this.items.Values.GetEnumerator();
        }

        public void Save(StreamWriter stream)
        {
            bool first = true;
            foreach (CsvItem item in this.items.Values)
            {
                stream.Write(item.ToSaveableString());
                if (first)
                {
                    first = false;
                    continue;
                }
                stream.Write(',');
            }
            stream.WriteLine();
        }

        public object Clone()
        {
            return new CsvRow(this);
        }

        internal bool HasItemIndex(CsvIndex collectionIndex)
        {
            foreach (CsvItem item in this)
            {
                if (item.index.Equals(collectionIndex))
                {
                    return true;
                }
            }
            return false;
        }

        public int CompareTo(CsvItemCollction other)
        {
            return this.index.CompareTo(other.index);
        }
    }

    public class CsvRow : CsvItemCollction
    {
        public CsvRow() : base()
        { }
        public CsvRow(CsvItemCollction row) : base(row)
        { }
        public CsvRow(int rowIndex)
        {
            this.index = new CsvIndex(rowIndex, IndexType.Row);
        }
    }

    public class CsvColumn : CsvItemCollction
    {
        public CsvColumn() : base()
        { }
        public CsvColumn(CsvItemCollction row) : base(row)
        { }
        public CsvColumn(int rowIndex)
        {
            this.index = new CsvIndex(rowIndex, IndexType.Column);
        }
    }


    public abstract class CsvItem
    {
        public static readonly CsvItem Null = new CsvItemNull();
        public static CsvItem CreateCsvItem(object value, CsvItemCollction parent, CsvIndex index)
        {
            if (value is string && string.IsNullOrEmpty((string)value) || string.IsNullOrWhiteSpace((string)value))
            {
                return CreateNullCsvItem(parent, index);
            }
            return new CsvString((string)value, parent, index);
        }
        public static CsvItem CreateNullCsvItem(CsvItemCollction parent, CsvIndex index)
        {
            CsvItemNull item = new CsvItemNull();
            item.index = index;
            item.parent = parent;
            return item;
        }
        public static int GetEmptyItemCount(List<CsvItem> items)
        {
            int count = 0;
            foreach (CsvItem item in items)
            {
                if (item.IsNull())
                {
                    count++;
                }
                if (item is CsvString && (string)item.GetValue() == "")
                {
                    count++;
                }
            }
            return count;
        }
        public static List<CsvItem> GetUniqueItems(CsvItemCollction itemCollction)
        {
            HashSet<CsvItem> uniqueItems = new HashSet<CsvItem>(itemCollction.items.Values, new ValueComparer());
            //uniqueItems.ExceptWith(null);
            return new List<CsvItem>(uniqueItems);
        }

        protected CsvItemCollction parent = null;
        public CsvIndex index;

        public abstract IComparable GetValue(); 
        public abstract Type GetValueType();

        public abstract void UpdateValue(object value);

        public bool IsNull()
        {
            return this is CsvItemNull;
        }
        public CsvItemCollction GetParent()
        {
            return parent;
        }
        public abstract CsvItem Clone(CsvItemCollction parent);
        public override abstract string ToString();
        public abstract string ToSaveableString();

        private class CsvItemNull : CsvItem
        {
            public override CsvItem Clone(CsvItemCollction parent)
            {
                return Null;
            }
            public override IComparable GetValue()
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

        public class ValueComparer : IComparer<CsvItem>, IEqualityComparer<CsvItem>
        {
            public int Compare(CsvItem x, CsvItem y)
            {
                if (x.GetValue() == null)
                {
                    return y.GetValue() == null ? 0 : -1;
                }
                return x.GetValue().CompareTo(y.GetValue());
            }

            public bool Equals(CsvItem x, CsvItem y)
            {
                if (x.GetValue() == null)
                {
                    return y.GetValue() == null;
                }
                return x.GetValue().Equals(y.GetValue());
            }

            public int GetHashCode(CsvItem obj)
            {
                if (obj.GetValue() == null)
                {
                    return 0;
                }
                return obj.GetValue().GetHashCode();
            }
        }
        public class IndexComparer : IComparer<CsvItem>, IEqualityComparer<CsvItem>
        {
            public int Compare(CsvItem x, CsvItem y)
            {
                return x.index.CompareTo(y.index);
            }

            public bool Equals(CsvItem x, CsvItem y)
            {
                return x.index.Equals(y.index);
            }

            public int GetHashCode(CsvItem obj)
            {
                return obj.index.GetHashCode();
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
        public CsvString(string str, CsvItemCollction parent) : this(str)
        {
            this.parent = parent;
        }
        public CsvString(string str, CsvItemCollction parent, CsvIndex itemIndex) : this(str, parent)
        {
            this.index = itemIndex;
        }
        public CsvString(CsvString item, CsvItemCollction parent)
        {
            this.value = item.value;
            this.parent = parent;
            this.index = item.index;
        }
        public override IComparable GetValue()
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

        public override CsvItem Clone(CsvItemCollction parent)
        {
            return new CsvString(this, parent);
        }
    }

    public enum IndexType { Column, Row }

    public class CsvIndex: IComparable<CsvIndex>, IEquatable<CsvIndex>, ICloneable
    {
        private int row = -1;
        private int column = -1;

        public CsvIndex()
        { }
        public CsvIndex(CsvIndex index)
        {
            this.row = index.row;
            this.column = index.column;
        }
        public CsvIndex(int index, IndexType indexType)
        {
            if (indexType == IndexType.Column)
            {
                this.column = index;
            }
            else
            {
                this.row = index;
            }
        }
        public CsvIndex(int rowIndex, int columnIndex)
        {
            this.row = rowIndex;
            this.column = columnIndex;
        }

        public int GetIndex(IndexType indexType)
        {
            if (indexType == IndexType.Column)
            {
                return this.column;
            }
            else
            {
                return this.row;
            }
        }

        public int GetRowIndex()
        {
            return this.row;
        }
        public int GetColumnIndex()
        {
            return this.column;
        }

        public int CompareTo(CsvIndex other)
        {
            return this.row - other.row + this.column - other.column;
        }

        public override string ToString()
        {
            string str = "";
            if (row >= 0)
            {
                str += row;
            }
            if (column >= 0)
            {
                if (!str.Equals(""))
                    str += " ";
                str += column;
            }
            return str;
        }
        public override int GetHashCode()
        {
            return row.GetHashCode() + column.GetHashCode();
        }

        public bool Equals(CsvIndex other)
        {
            bool rowEquals = this.row == other.row;
            bool columnEquals = this.column == other.column;
            return ((this.row > 0 && other.row > 0) ? rowEquals : true) && ((this.column > 0 && other.column > 0) ? columnEquals : true);
        }

        public object Clone()
        {
            return new CsvIndex(this);
        }

        public static bool operator ==(CsvIndex i1, CsvIndex i2)
        {
            return i1.Equals(i2);
        }
        public static bool operator !=(CsvIndex i1, CsvIndex i2)
        {
            return !i1.Equals(i2);
        }
        public static CsvIndex operator -(CsvIndex i1, CsvIndex i2)
        {
            return new CsvIndex(i1.row - i2.row, i1.column - i2.column);
        }
    }
}
