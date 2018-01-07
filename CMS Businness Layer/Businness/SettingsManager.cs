using CMS.Models;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Businness_Layer.Businness
{
    public class SettingsManager
    {
        public static Boolean SaveSetting(string key,string value)
        {
            Boolean isSuccess = false;
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {

                    new SqlParameter() {ParameterName = "@Key",     SqlDbType = SqlDbType.NVarChar, Value = key},
                    new SqlParameter() {ParameterName = "@Value",  SqlDbType = SqlDbType.NVarChar, Value = value}
                };
                isSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SaveSettings, lstSqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return isSuccess;
        }

        public static string GetSetting(string key)
        {
            string value = null;
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@Key",     SqlDbType = SqlDbType.NVarChar, Value = key},
                };
                //return Convert.ToString(DataAccess.GetScalar(StoredProcedures.GetSettings,lstSqlParameters));
                DataTable objDataTable = DataAccess.GetDataTable(StoredProcedures.GetSettings, lstSqlParameters);
                if (objDataTable.Rows.Count > 0)
                    value = objDataTable.Rows[0]["value"].ToString();
                }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return value;
        }

    }

}