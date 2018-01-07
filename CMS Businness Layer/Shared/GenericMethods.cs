using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Businness_Layer.Shared
{
    public class GenericMethods
    {
        public static Boolean ExportDatatableToExcel(DataTable dataTable, string pathAndName)
        {
            var lines = new List<string>();

            string[] columnNames = dataTable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();

            var header = string.Join(",", columnNames);
            lines.Add(header);

            var valueLines = dataTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);

            File.WriteAllLines(pathAndName, lines);
            return true;
        }
    }
}
