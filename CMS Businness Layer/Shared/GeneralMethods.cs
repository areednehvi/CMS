using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Businness_Layer.Shared
{
    public class GeneralMethods
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

        public static string Encode(string text)
        {
            byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(text);
            string returntext = System.Convert.ToBase64String(mybyte);
            return returntext;
        }

        public static string Decode(string text)
        {
            byte[] mybyte = System.Convert.FromBase64String(text);
            string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
            return returntext;
        }

        public static string httpGetWebRequest(string URI)
        {
            string returnedJSON;
            try
            {
                if (!URI.StartsWith("http"))
                    URI = "http://" + URI;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(URI);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";

                var response = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    returnedJSON = sr.ReadToEnd();
                }
                return string.IsNullOrEmpty(returnedJSON) ? "[]" : returnedJSON;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
