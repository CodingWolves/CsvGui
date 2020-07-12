using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Csv
{
    public class Csv
    {
        public static CsvForm LoadCsv(string filePath, bool hasHead)
        {
            return LoadCsv(filePath, hasHead, 0, int.MaxValue, TimeSpan.MaxValue);
        }
        public static CsvForm LoadCsv(string filePath, bool hasHead, int rowOffset)
        {
            return LoadCsv(filePath, hasHead, rowOffset, int.MaxValue, TimeSpan.MaxValue);
        }
        public static CsvForm LoadCsv(string filePath, bool hasHead, int rowOffset, int rows)
        {
            return LoadCsv(filePath, hasHead, rowOffset, rows, TimeSpan.MaxValue);
        }    
        public static CsvForm LoadCsv(string filePath, bool hasHead, TimeSpan readTimeSpan)
        {
            return LoadCsv(filePath, hasHead, 0, int.MaxValue, readTimeSpan);
        }

        public static CsvForm LoadCsv(string filePath, bool hasHead, int rowOffset, int rows, TimeSpan readTimeSpan)
        {
            DateTime startTime = DateTime.Now;
            CsvForm form = new CsvForm();
            form.name = Path.GetFileName(filePath);
            form.filePath = filePath;

            StreamReader stream = new StreamReader(filePath);
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
                row.AddItem(CsvItem.CreateCsvItem(null,row,++itemCount));
                line = line.Remove(0, 1);
            }
            MatchCollection itemMatches = Regex.Matches(line, "(?:^|,)(?=[^\"]|(\")?)\"?(?(1)[^\"]*|[^,\"]*)\"?(?=,|$)"); // Csv regex format, no normal parethesses allowed    
            foreach (Match match in itemMatches)
            {
                string itemValue = match.Value.StartsWith(",") ? match.Value.Remove(0, 1) : match.Value;
                if (itemValue.StartsWith("\"") && itemValue.EndsWith("\""))
                {
                    itemValue = itemValue.Substring(1, itemValue.Length - 2);
                }
                row.AddItem(CsvItem.CreateCsvItem(itemValue, row, ++itemCount));
            }
            return row;
        }
    }
}
