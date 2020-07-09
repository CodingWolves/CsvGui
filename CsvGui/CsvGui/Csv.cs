using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Csv
{
    public class CsvForm:IEnumerable
    {
        public List<CsvRow> rows = null;
        public CsvForm()
        {
            this.rows = new List<CsvRow>();
        }
        public CsvForm(CsvForm form):this()
        {
            this.rows.AddRange(form.rows);
        }

        public IEnumerator GetEnumerator()
        {
            return this.rows.GetEnumerator();
        }
    }

    public class CsvRow:IEnumerable
    {
        protected bool isHead = false;
        public List<CsvItem> items = null;
        public CsvRow()
        {
            this.items = new List<CsvItem>();
        }
        public CsvRow(CsvRow row):this()
        {
            this.isHead = row.isHead;
            this.items.AddRange(row.items);
        }

        public bool IsHead
        {
            get
            {
                return this.isHead;
            }
        }
        public void SetAsHead()
        {
            this.isHead = true;
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

        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }

    

    public class CsvReader
    {
        public static CsvForm ReadFile(string filename)
        {
            return null;
        }

        public static CsvRow ReadLine()//StreamReader stream)
        {
            //string line = stream.ReadLine();
            string line = "123,443,6547,8678,,,,,,,7657,,657,";

            MatchCollection itemSplit = Regex.Matches(line, "(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)");

            string[] itemsValue = new string[14];
            foreach (Match match in itemSplit)
            {

            }

            return null;
        }

        public static Csv.CsvItem GetCsvItem(object obj)
        {
            if (obj is string)
            {
                return new Csv.CsvString((string)obj);
            }

            return null;
        }
    }
}
