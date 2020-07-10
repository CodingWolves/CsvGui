using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Csv
{
    public class CsvReader
    {
        public static CsvForm ReadFile(string filename, bool hasHead)
        {
            return ReadFile(filename, hasHead, 0, int.MaxValue, TimeSpan.MaxValue);
        }
        public static CsvForm ReadFile(string filename, bool hasHead, int rowOffset)
        {
            return ReadFile(filename, hasHead, rowOffset, int.MaxValue, TimeSpan.MaxValue);
        }
        public static CsvForm ReadFile(string filename, bool hasHead, int rowOffset, int rows)
        {
            return ReadFile(filename, hasHead, rowOffset, rows, TimeSpan.MaxValue);
        }    
        public static CsvForm ReadFile(string filename, bool hasHead, TimeSpan readTimeSpan)
        {
            return ReadFile(filename, hasHead, 0, int.MaxValue, readTimeSpan);
        }

        public static CsvForm ReadFile(string filename, bool hasHead, int rowOffset, int rows, TimeSpan readTimeSpan)
        {
            DateTime startTime = DateTime.Now;
            CsvForm form = new CsvForm();
            StreamReader stream = new StreamReader(filename);
            int rowCount = 0;
            if (hasHead && !stream.EndOfStream)
            {
                form.SetHeadRow(ReadRow(stream, ++rowCount));
                rowOffset--;
            }
            while (rowOffset > 0 && !stream.EndOfStream) // skip lines by rowOffset
            {
                stream.ReadLine();
                rowOffset--;
                rowCount++;
            }
            while (DateTime.Now.Subtract(startTime) < readTimeSpan && rows > rowCount && !stream.EndOfStream)
            {
                form.AddRow(ReadRow(stream, ++rowCount));
            }
            stream.Close();
            return form;
        }

        public static CsvRow ReadRow(StreamReader stream, int rowIndex)
        {
            string line = stream.ReadLine();
            CsvRow row = new CsvRow(rowIndex);
            int itemCount = 0;
            while (line.StartsWith(",")) // regex has a problem with empty items at start
            {
                row.AddItem(new CsvString(String.Empty, row, ++itemCount));
                line = line.Remove(0, 1);
            }
            MatchCollection itemMatches = Regex.Matches(line, "(?:^|,)(?=[^\"]|(\")?)\"?(?(1)[^\"]*|[^,\"]*)\"?(?=,|$)"); // Csv regex format            
            foreach (Match match in itemMatches)
            {
                string itemValue = match.Value.StartsWith(",") ? match.Value.Remove(0, 1) : match.Value;
                CsvString item = new CsvString(itemValue, row, ++itemCount);
                row.AddItem(item);
            }
            return row;
        }

        public static CsvItem GetCsvItem(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
