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
            CsvForm form = new CsvForm();
            StreamReader stream = new StreamReader(filename);
            if (hasHead && !stream.EndOfStream)
            {
                form.SetHeadRow(ReadRow(stream));
            }
            while (!stream.EndOfStream)
            {
                form.AddRow(ReadRow(stream));
            }
            stream.Close();
            return form;
        }

        public static CsvRow ReadRow(StreamReader stream)
        {
            string line = stream.ReadLine();
            //string line = ",123,";
            CsvRow row = new CsvRow();
            while (line.StartsWith(",")) // regex has a problem with empty items at start
            {
                row.AddItem(new CsvString(String.Empty, row));
                line = line.Remove(0, 1);
            }
            MatchCollection itemMatches = Regex.Matches(line, "(?:^|,)(?=[^\"]|(\")?)\"?(?(1)[^\"]*|[^,\"]*)\"?(?=,|$)"); // Csv regex format            
            foreach (Match match in itemMatches)
            {
                string itemValue = match.Value.StartsWith(",") ? match.Value.Remove(0, 1) : match.Value;
                CsvString item = new CsvString(itemValue, row);
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
