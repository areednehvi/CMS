using CMS.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CMS.Shared
{
    public class GeneralMethods
    {
        public static void ShowNotification(string Title, string Message, Boolean IsError = false)
        {
            Notification winNotification = new Notification(Title, Message, IsError);
            winNotification.Show();
        }

        public static void ShowDialog(string Title, string Message, Boolean IsError = false)
        {
            Dialog winDialog = new Dialog(Title, Message, IsError);
            winDialog.Show();
        }

        public static void CreateGlobalObject(string globalObjectName, object objObject)
        {
            Application.Current.Properties[globalObjectName] = objObject;
        }
        public static object GetGlobalObject(string globalObjectName)
        {
            return Application.Current.Properties[globalObjectName];
        }

        public static Boolean IsInternetAvailable()
        {
            try
            {
                IPHostEntry ipHe = Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static Boolean Log(string line,Boolean IsHeading = false, Boolean append = true)
        {
            Boolean IsSuccess = false;
            string fileName = @"Logs\log.txt";
            if (!File.Exists(fileName))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            using (StreamWriter objStreamWriter = new StreamWriter(fileName, append))
            {
                if(IsHeading)
                    objStreamWriter.WriteLine("--------------------------------------------------------------------------------------");
                objStreamWriter.WriteLine(line);
                if (!IsHeading)
                    objStreamWriter.WriteLine();
                if (IsHeading)
                    objStreamWriter.WriteLine("--------------------------------------------------------------------------------------");
                IsSuccess = true;
            }


            // Read and show each line from the file.
            //string line = "";
            //using (StreamReader sr = new StreamReader("names.txt"))
            //{
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        Console.WriteLine(line);
            //    }
            //}

            return IsSuccess;

        }
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
