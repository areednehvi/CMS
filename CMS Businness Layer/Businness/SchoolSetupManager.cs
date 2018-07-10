using CMS.Models;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Businness_Layer.Businness
{
    public class SchoolSetupManager
    {
        public static Boolean IsSchoolSetup()
        {
            Boolean isSchoolSetup = false;

            try
            {
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.IsSchoolSetup, null);
                if (objDatable.Rows.Count > 0)
                    isSchoolSetup = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return isSchoolSetup;

        }

        public static SchoolModel GetSchoolInfo()
        {
            try
            {
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetSchoolInfo);
                return MapDatatableToSchoolObject(objDatable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        private static SchoolModel MapDatatableToSchoolObject(DataTable objDatatable)
        {
            SchoolModel objSchoolInfo = new SchoolModel();
            try
            {
                objSchoolInfo.id_offline =  objDatatable.Rows[0]["id_offline"] != DBNull.Value ? objDatatable.Rows[0]["id_offline"].ToString() : string.Empty;
                objSchoolInfo.id_online = objDatatable.Rows[0]["id_online"] != DBNull.Value ? objDatatable.Rows[0]["id_online"].ToString() : string.Empty;
                objSchoolInfo.EducationKey = objDatatable.Rows[0]["EducationKey"] != DBNull.Value ? objDatatable.Rows[0]["EducationKey"].ToString() : string.Empty;
                objSchoolInfo.License = objDatatable.Rows[0]["License"] != DBNull.Value ? objDatatable.Rows[0]["License"].ToString() : string.Empty;
                objSchoolInfo.database_id = objDatatable.Rows[0]["database_id"] != DBNull.Value ? objDatatable.Rows[0]["database_id"].ToString() : string.Empty;
                objSchoolInfo.subdomain = objDatatable.Rows[0]["subdomain"] != DBNull.Value ? objDatatable.Rows[0]["subdomain"].ToString() : string.Empty;
                objSchoolInfo.domain = objDatatable.Rows[0]["domain"] != DBNull.Value ? objDatatable.Rows[0]["domain"].ToString() : string.Empty;
                objSchoolInfo.name = objDatatable.Rows[0]["name"] != DBNull.Value ? objDatatable.Rows[0]["name"].ToString() : string.Empty;
                objSchoolInfo.website = objDatatable.Rows[0]["website"] != DBNull.Value ? objDatatable.Rows[0]["website"].ToString() : string.Empty;
                objSchoolInfo.phone = objDatatable.Rows[0]["phone"] != DBNull.Value ? objDatatable.Rows[0]["phone"].ToString() : string.Empty;
                objSchoolInfo.email = objDatatable.Rows[0]["email"] != DBNull.Value ? objDatatable.Rows[0]["email"].ToString() : string.Empty;
                objSchoolInfo.address = objDatatable.Rows[0]["address"] != DBNull.Value ? objDatatable.Rows[0]["address"].ToString() : string.Empty;
                objSchoolInfo.theme = objDatatable.Rows[0]["theme"] != DBNull.Value ? objDatatable.Rows[0]["theme"].ToString() : string.Empty;
                objSchoolInfo.created_on = objDatatable.Rows[0]["created_on"] != DBNull.Value ? Convert.ToDateTime(objDatatable.Rows[0]["created_on"].ToString()) : (DateTime?)null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objSchoolInfo;
        }

        public static Boolean SetSchooInfo(SchoolModel objSchoolInfo)
        {
            Boolean IsSuccess = false;
            try
            {                
                DataTable objDatatable = MapSchoolInfoToDataTable(objSchoolInfo);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.schools;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SetSchoolInfo, objSqlParameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return IsSuccess;

        }

        private static DataTable MapSchoolInfoToDataTable(SchoolModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("EducationKey", typeof(string));
                table.Columns.Add("License", typeof(string));
                table.Columns.Add("database_id", typeof(string));
                table.Columns.Add("subdomain", typeof(string));
                table.Columns.Add("domain", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("address", typeof(string));
                table.Columns.Add("website", typeof(string));
                table.Columns.Add("phone", typeof(string));
                table.Columns.Add("email", typeof(string));
                table.Columns.Add("theme", typeof(string));
                table.Columns.Add("created_on", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.EducationKey,
                                obj.License,
                                obj.database_id,
                                obj.subdomain,
                                obj.domain,
                                obj.name,
                                obj.address,
                                obj.website,
                                obj.phone,
                                obj.email,
                                obj.theme,
                                obj.created_on
                              );
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
    }

}