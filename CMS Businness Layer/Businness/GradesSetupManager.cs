﻿using CMS.Models;
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
    public class GradesSetupManager
    {

        #region List      
        public static ObservableCollection<GradesListModel> GetGradesList(Int64 fromRowNo, Int64 toRowNo)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo}                   
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetGradesList, lstSqlParameters);
                return MapDatatableTogradesListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }
        public static List<GradesListModel> GetAllGrades(Boolean IncludeAllOption = false)
        {
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = 1},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = Int64.MaxValue}
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetGradesList, lstSqlParameters);
                return MapDatatableTogradesObject(objDatable,IncludeAllOption);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        private static ObservableCollection<GradesListModel> MapDatatableTogradesListObject(DataTable objDatatable)
        {
            ObservableCollection<GradesListModel> objGradesList = new ObservableCollection<GradesListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    GradesListModel obj = new GradesListModel()
                    {
                        Course = new coursesModel()
                    };
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.course_id = row["course_id"] != DBNull.Value ? Convert.ToString(row["course_id"]) : string.Empty;
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;
                    obj.order = row["order"] != DBNull.Value ? Convert.ToString(row["order"]) : string.Empty;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    obj.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToString(row["CreatedBy"]) : string.Empty;
                    //Courses                  
                    obj.Course.id_offline = row["courses.id_offline"] != DBNull.Value ? Convert.ToString(row["courses.id_offline"]) : string.Empty;
                    obj.Course.school_id = row["courses.school_id"] != DBNull.Value ? Convert.ToString(row["courses.school_id"]) : string.Empty;
                    obj.Course.name = row["courses.name"] != DBNull.Value ? Convert.ToString(row["courses.name"]) : string.Empty;
                    obj.Course.created_by = row["courses.created_by"] != DBNull.Value ? Convert.ToString(row["courses.created_by"]) : string.Empty;
                    obj.Course.created_on = row["courses.created_on"] != DBNull.Value ? Convert.ToDateTime(row["courses.created_on"]) : (DateTime?)null;
                    obj.Course.updated_by = row["courses.updated_by"] != DBNull.Value ? Convert.ToString(row["courses.updated_by"]) : string.Empty;
                    obj.Course.updated_on = row["courses.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["courses.updated_on"]) : (DateTime?)null;

                    objGradesList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objGradesList;
        }
        private static List<GradesListModel> MapDatatableTogradesObject(DataTable objDatatable,Boolean IncludeAllOption = false)
        {
            List<GradesListModel> objGradesList = new List<GradesListModel>();
            try
            {
                if (IncludeAllOption)
                    objGradesList.Add(new GradesListModel() { id_offline = Guid.Empty.ToString(), name = "All", Course = new coursesModel() { id_offline = Guid.Empty.ToString(), name = "All" } });
                foreach (DataRow row in objDatatable.Rows)
                {
                    GradesListModel obj = new GradesListModel()
                    {
                         Course = new coursesModel()
                    };
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.course_id = row["course_id"] != DBNull.Value ? Convert.ToString(row["course_id"]) : string.Empty;
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;
                    obj.order = row["order"] != DBNull.Value ? Convert.ToString(row["order"]) : string.Empty;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;

                    //Courses                  
                    obj.Course.id_offline = row["courses.id_offline"] != DBNull.Value ? Convert.ToString(row["courses.id_offline"]) : string.Empty;
                    obj.Course.school_id = row["courses.school_id"] != DBNull.Value ? Convert.ToString(row["courses.school_id"]) : string.Empty;
                    obj.Course.name = row["courses.name"] != DBNull.Value ? Convert.ToString(row["courses.name"]) : string.Empty;
                    obj.Course.created_by = row["courses.created_by"] != DBNull.Value ? Convert.ToString(row["courses.created_by"]) : string.Empty;
                    obj.Course.created_on = row["courses.created_on"] != DBNull.Value ? Convert.ToDateTime(row["courses.created_on"]) : (DateTime?)null;
                    obj.Course.updated_by = row["courses.updated_by"] != DBNull.Value ? Convert.ToString(row["courses.updated_by"]) : string.Empty;
                    obj.Course.updated_on = row["courses.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["courses.updated_on"]) : (DateTime?)null;

                    objGradesList.Add(obj);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objGradesList;
        }

        #endregion

        #region view
        public static Boolean CreateOrModfiyGrades(GradesListModel objGrade, LoginModel CurrentLogin, SchoolModel SchoolInfo)
        {
            Boolean IsSuccess = false;
            try
            {
                if(objGrade.id_offline == null) // New Grade
                {
                    objGrade.id_offline = Guid.NewGuid().ToString();
                    objGrade.id_online = Guid.Empty.ToString();
                    objGrade.created_by = CurrentLogin.User.id_offline;
                    objGrade.created_on = DateTime.Now;
                    objGrade.school_id = SchoolInfo.id_offline;
                    objGrade.order = string.Empty;
                }
                objGrade.course_id = objGrade.Course.id_offline;            
                objGrade.updated_by = CurrentLogin.User.id_offline;
                objGrade.updated_on = DateTime.Now;
                                
                DataTable objDatatable = MapGradeListObjectToDataTable(objGrade);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.grades;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.CreateOrModifyGrades, objSqlParameter);
                
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

        private static DataTable MapGradeListObjectToDataTable(GradesListModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("course_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("order", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online, 
                                obj.school_id,
                                obj.course_id,
                                obj.name,
                                obj.order,
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

        public static DataTable MapGradeObjectToDataTable(gradesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("course_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("order", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.course_id,
                                obj.name,
                                obj.order,
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
        public static DataTable MapGradesObjectToDataTable(List<GradesListModel> objGrades)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("course_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("order", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                foreach (gradesModel obj in objGrades)
                {
                    table.Rows.Add(
                                    obj.id_offline,
                                    obj.id_online,
                                    obj.school_id,
                                    obj.course_id,
                                    obj.name,
                                    obj.order,
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
