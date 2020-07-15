using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Csv
{
    public abstract class CsvItemCollction:IEnumerable
    {
        public Dictionary<int,CsvItem> items = null;
        public int index;
        public CsvItemCollction()
        {
            this.items = new Dictionary<int, CsvItem>();
            this.index = -1;
        }

        public CsvItem this[CsvIndex index]
        {
            get
            {
                return this.GetItem(index);
            }
        }

        public bool HasIndex(int index)
        {
            return this.items.ContainsKey(index);
        }

        public List<CsvItem> GetAllItems()
        {
            return new List<CsvItem>(items.Values);
        }

        /// <summary>
        /// Clones the item and adds it to the approptiate index
        /// </summary>
        /// <param name="item"></param>
        public abstract void Append(CsvItem item, bool clone);
        public void Append(List<CsvItem> items, bool clone)
        {
            foreach (CsvItem item in items)
            {
                this.Append(item, clone);
            }
        }
        public void Append(CsvItemCollction collction, bool clone)
        {
            foreach (CsvItem item in collction)
            {
                this.Append(item, clone);
            }
        }

        public void RemoveAt(int index)
        {
            this.items.Remove(index);
        }

        public abstract CsvItem GetItem(CsvIndex itemIndex);

        public IEnumerator GetEnumerator()
        {
            return this.items.Values.GetEnumerator();
        }

        //public abstract CsvItemCollction Clone();
        public abstract override string ToString();

        public List<CsvItem> Where(Func<CsvItem,bool> func)
        {
            List<CsvItem> itemList = new List<CsvItem>();
            foreach (CsvItem item in this)
            {
                if (func(item))
                {
                    itemList.Add(item);
                }
            }
            return itemList;
        }       
    }

    public class CsvRow : CsvItemCollction, IComparable<CsvRow>
    {
        public CsvRow() : base()
        { }
        public CsvRow(CsvItemCollction coll)
        {
            this.items = new Dictionary<int, CsvItem>();
            this.index = coll.index;
            foreach (CsvItem item in coll)
            {
                this.items.Add(item.index.GetColumnIndex(), item.Clone(this));
            }
        }
        public CsvRow(int rowIndex)
        {
            this.items = new Dictionary<int, CsvItem>();
            this.index = rowIndex;
        }

        public override void Append(CsvItem item, bool clone)
        {
            if (item == null)
            {
                return;
            }
            int index = item.index.GetColumnIndex();
            if (clone)
            {
                item = item.Clone(this);
            }
            if (items.ContainsKey(index))
            {
                this.items[index] = item;
            }
            else
            {
                this.items.Add(index, item);
            }
        }

        public override CsvItem GetItem(CsvIndex itemIndex)
        {
            int index = itemIndex.GetColumnIndex();
            if (this.items.ContainsKey(index))
            {
                return this.items[index];
            }
            return null;
        }

        public int CompareTo(CsvRow other)
        {
            return this.index - other.index;
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

        public override string ToString()
        {
            return "Row "+index.ToString();
        }

        public CsvRow Clone()
        {
            return new CsvRow(this);
        }
    }

    public class CsvColumn : CsvItemCollction, IComparable<CsvColumn>
    {
        public CsvColumn() :base()
        { }
        public CsvColumn(CsvItemCollction coll)
        {
            this.items = new Dictionary<int, CsvItem>();
            this.index = coll.index;
            foreach (CsvItem item in coll)
            {
                this.items.Add(item.index.GetRowIndex(), item.Clone(this));
            }
        }
        public CsvColumn(int columnIndex)
        {
            this.items = new Dictionary<int, CsvItem>();
            this.index = columnIndex;
        }

        public override void Append(CsvItem item, bool clone)
        {
            if (item == null)
            {
                return;
            }
            int index = item.index.GetColumnIndex();
            if (clone)
            {
                item = item.Clone(this);
            }
            if (items.ContainsKey(index))
            {
                this.items[index] = item;
            }
            else
            {
                this.items.Add(index, item);
            }
        }

        public override CsvItem GetItem(CsvIndex itemIndex)
        {
            int index = itemIndex.GetRowIndex();
            if (this.items.ContainsKey(index))
            {
                return this.items[index];
            }
            return null;
        }

        public int CompareTo(CsvColumn other)
        {
            return this.index - other.index;
        }

        public override string ToString()
        {
            return "Column " + index.ToString();
        }

        public CsvColumn Clone()
        {
            return new CsvColumn(this);
        }
    }


    public abstract class CsvItem
    {
        public static readonly CsvItem Null = new CsvItemNull();
        public static CsvItem CreateCsvItem(object value, CsvItemCollction parent, CsvIndex index)
        {
            if (value is string && (string.IsNullOrEmpty((string)value) || string.IsNullOrWhiteSpace((string)value)))
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
        public static CsvItem CreateNullCsvItem(CsvItemCollction parent, int index, IndexType indexType)
        {
            return CreateNullCsvItem(parent, new CsvIndex(index, indexType));
        }
        public static CsvItem CreateNullCsvItem(CsvItemCollction parent, int rowIndex, int columnIndex)
        {
            return CreateNullCsvItem(parent, new CsvIndex(rowIndex, columnIndex));
        }
        public static CsvItem CreateCsvItemForIndex(CsvIndex index)
        {
            return new CsvString("", null, index);
        }
        public static CsvItem CreateCsvItemForIndex(int index, IndexType indexType)
        {
            return new CsvString("", null, new CsvIndex(index, indexType));
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
        public bool Equals(IEqualityComparer<CsvItem> comparer, CsvItem compared)
        {
            return comparer.Equals(this, compared);
        }

        

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

        #region Comparers

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
        public class IndexRowComparer : IComparer<CsvItem>, IEqualityComparer<CsvItem>
        {
            public int Compare(CsvItem x, CsvItem y)
            {
                return x.index.GetRowIndex().CompareTo(y.index.GetRowIndex());
            }

            public bool Equals(CsvItem x, CsvItem y)
            {
                return x.index.GetRowIndex().Equals(y.index.GetRowIndex());
            }

            public int GetHashCode(CsvItem obj)
            {
                return obj.index.GetRowIndex().GetHashCode();
            }
        }
        public class IndexColumnComparer : IComparer<CsvItem>, IEqualityComparer<CsvItem>
        {
            public int Compare(CsvItem x, CsvItem y)
            {
                return x.index.GetColumnIndex().CompareTo(y.index.GetColumnIndex());
            }

            public bool Equals(CsvItem x, CsvItem y)
            {
                return x.index.GetColumnIndex().Equals(y.index.GetColumnIndex());
            }

            public int GetHashCode(CsvItem obj)
            {
                return obj.index.GetColumnIndex().GetHashCode();
            }
        }

        #endregion
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
        private int row;
        private int column;

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
                this.row = -1;
            }
            else
            {
                this.row = index;
                this.column = -1;
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
        public bool Equals(IEqualityComparer<CsvIndex> comparer, CsvIndex other)
        {
            return comparer.Equals(other);
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

        public class RowEquility : IEqualityComparer<CsvIndex>
        {
            public bool Equals(CsvIndex x, CsvIndex y)
            {
                return x.row.Equals(y.row);
            }

            public int GetHashCode(CsvIndex obj)
            {
                return obj.row.GetHashCode();
            }
        }
        public class ColumnEquility : IEqualityComparer<CsvIndex>
        {
            public bool Equals(CsvIndex x, CsvIndex y)
            {
                return x.column.Equals(y.column);
            }

            public int GetHashCode(CsvIndex obj)
            {
                return obj.column.GetHashCode();
            }
        }
    }
}
