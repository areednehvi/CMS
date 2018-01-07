using CMS.Models;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SMS_Models.Models.DBModels;

namespace SMS_Businness_Layer.Businness
{
    public class SectionsSetupManager
    {

        #region List      
        public static ObservableCollection<SectionsListModel> GetSectionsList(Int64 fromRowNo, Int64 toRowNo)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo}                   
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetSectionsList, lstSqlParameters);
                return MapDatatableToSectionsListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }
        public static List<sectionsModel> GetAllSections(Boolean IncludeAllOption = false)
        {
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = 1},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = Int64.MaxValue}
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetSectionsList, lstSqlParameters);
                return MapDatatableToSectionsObject(objDatable, IncludeAllOption);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        private static List<sectionsModel> MapDatatableToSectionsObject(DataTable objDatatable, Boolean IncludeAllOption = false)
        {
            List<sectionsModel> objSectionsList = new List<sectionsModel>();
            try
            {
                if (IncludeAllOption)
                    objSectionsList.Add(new sectionsModel() { id_offline = Guid.Empty.ToString(), name = "All" });
                foreach (DataRow row in objDatatable.Rows)
                {
                    sectionsModel obj = new sectionsModel();
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.capacity = row["capacity"] != DBNull.Value ? Convert.ToInt32(row["capacity"]) : 0;
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    objSectionsList.Add(obj);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objSectionsList;
        }

        private static ObservableCollection<SectionsListModel> MapDatatableToSectionsListObject(DataTable objDatatable)
        {
            ObservableCollection<SectionsListModel> objSectionsList = new ObservableCollection<SectionsListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    SectionsListModel obj = new SectionsListModel();
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.capacity = row["capacity"] != DBNull.Value ? Convert.ToInt32(row["capacity"]) : 0;
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;                   
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    obj.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToString(row["CreatedBy"]) : string.Empty;
                    objSectionsList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objSectionsList;
        }

        #endregion

        #region view
        public static Boolean CreateOrModfiySections(SectionsListModel objSection, LoginModel CurrentLogin, SchoolModel SchoolInfo)
        {
            Boolean IsSuccess = false;
            try
            {
                if (objSection.id_offline == null) // New Session
                {
                    objSection.id_offline = Guid.NewGuid().ToString();
                    objSection.id_online = Guid.Empty.ToString();
                    objSection.created_by = CurrentLogin.User.id_offline;
                    objSection.created_on = DateTime.Now;
                    objSection.school_id = SchoolInfo.id_offline;
                }
                objSection.updated_by = CurrentLogin.User.id_offline;
                objSection.updated_on = DateTime.Now;

                DataTable objDatatable = MapSectionListObjectToDataTable(objSection);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.sections;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.CreateOrModifySections, objSqlParameter);
                
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

        private static DataTable MapSectionListObjectToDataTable(SectionsListModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));                
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("capacity", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online, 
                                obj.school_id,                                
                                obj.name,
                                obj.capacity,
                                obj.created_by,
                                obj.created_on,
                                obj.updated_by,
                                obj.updated_on                                
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
        public static DataTable MapSectionObjectToDataTable(sectionsModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("capacity", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.name,
                                obj.capacity,
                                obj.created_by,
                                obj.created_on,
                                obj.updated_by,
                                obj.updated_on
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
        public static DataTable MapSectionsObjectToDataTable(List<sectionsModel> objList)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("capacity", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                foreach(sectionsModel obj in objList)
                {
                    table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.name,
                                obj.capacity,
                                obj.created_by,
                                obj.created_on,
                                obj.updated_by,
                                obj.updated_on
                              );
                }
                
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
        #endregion

    }
}
