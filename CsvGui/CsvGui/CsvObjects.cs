using System;

namespace Csv
{
    public abstract class CsvItem
    {
        protected CsvRow parent = null;

        public abstract bool IsNull();
        public CsvRow GetParent()
        {
            return parent;
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

        public override bool IsNull()
        {
            return String.IsNullOrEmpty(this.value);
        }
    }

}
