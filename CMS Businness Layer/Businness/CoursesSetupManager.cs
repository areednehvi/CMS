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
    public class CoursesSetupManager
    {

        #region List      
        public static ObservableCollection<CoursesListModel> GetCoursesList(Int64 fromRowNo, Int64 toRowNo)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo}                   
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetCoursesList, lstSqlParameters);
                return MapDatatableToCoursesListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }
        public static List<coursesModel> GetAllCourses(Boolean IncludeAllOption = false)
        {
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = 1},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = Int64.MaxValue}
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetCoursesList, lstSqlParameters);
                return MapDatatableToCoursesObject(objDatable, IncludeAllOption);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        private static List<coursesModel> MapDatatableToCoursesObject(DataTable objDatatable, Boolean IncludeAllOption = false)
        {
            List<coursesModel> objCoursesList = new List<coursesModel>();
            try
            {
                if (IncludeAllOption)
                    objCoursesList.Add(new coursesModel() { id_offline = Guid.Empty.ToString(), name = "All" });
                foreach (DataRow row in objDatatable.Rows)
                {
                    coursesModel obj = new coursesModel();
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    objCoursesList.Add(obj);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objCoursesList;
        }

        private static ObservableCollection<CoursesListModel> MapDatatableToCoursesListObject(DataTable objDatatable)
        {
            ObservableCollection<CoursesListModel> objCoursesList = new ObservableCollection<CoursesListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    CoursesListModel obj = new CoursesListModel();
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;                    
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;                   
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    obj.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToString(row["CreatedBy"]) : string.Empty;
                    objCoursesList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objCoursesList;
        }

        #endregion

        #region view
        public static Boolean CreateOrModfiyCourses(CoursesListModel objCourse, LoginModel CurrentLogin, SchoolModel SchoolInfo)
        {
            Boolean IsSuccess = false;
            try
            {
                if (objCourse.id_offline == null) // New Course
                {
                    objCourse.id_offline = Guid.NewGuid().ToString();
                    objCourse.id_online = Guid.Empty.ToString();
                    objCourse.created_by = CurrentLogin.User.id_offline;
                    objCourse.created_on = DateTime.Now;
                    objCourse.school_id = SchoolInfo.id_offline;
                }
                objCourse.updated_by = CurrentLogin.User.id_offline;
                objCourse.updated_on = DateTime.Now;

                DataTable objDatatable = MapCourseListObjectToDataTable(objCourse);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.courses;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.CreateOrModifyCourses, objSqlParameter);
                
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

        private static DataTable MapCourseListObjectToDataTable(CoursesListModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));                
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online, 
                                obj.school_id,                                
                                obj.name,
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
        public static DataTable MapCourseObjectToDataTable(coursesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.name,
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
        public static DataTable MapCoursesObjectToDataTable(List<coursesModel> objList)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                foreach(coursesModel obj in objList)
                {
                    table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.name,
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
