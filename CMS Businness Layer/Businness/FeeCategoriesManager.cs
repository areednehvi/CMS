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
    public class FeeCategoriesManager
    {

        #region List      
        public static ObservableCollection<FeeCategoriesListModel> GetFeeCategoriesList(Int64 fromRowNo, Int64 toRowNo)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo}                   
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetFeeCategoriesList, lstSqlParameters);
                return MapDatatableToFeeCategoriesListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }
        public static List<fee_categoriesModel> GetAllFeeCategories()
        {
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = 1},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = Int64.MaxValue}
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetFeeCategoriesList, lstSqlParameters);
                return MapDatatableToFeeCategoriesObject(objDatable);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        public static ObservableCollection<FeeCategoriesListModel> MapDatatableToFeeCategoriesListObject(DataTable objDatatable)
        {
            ObservableCollection<FeeCategoriesListModel> objFeeCategoriesList = new ObservableCollection<FeeCategoriesListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    FeeCategoriesListModel obj = new FeeCategoriesListModel();
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.id_online = row["id_online"] != DBNull.Value ? Convert.ToString(row["id_online"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;
                    obj.recur = row["recur"] != DBNull.Value ? Convert.ToString(row["recur"]) : string.Empty;
                    obj.is_transport = row["is_transport"] != DBNull.Value ? Convert.ToString(row["is_transport"]) : string.Empty;
                    obj.order = row["order"] != DBNull.Value ? Convert.ToString(row["order"]) : string.Empty;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    obj.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToString(row["CreatedBy"]) : string.Empty;
                    objFeeCategoriesList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objFeeCategoriesList;
        }

        public static List<fee_categoriesModel> MapDatatableToFeeCategoriesObject(DataTable objDatatable)
        {
            List<fee_categoriesModel> objFeeCategoriesList = new List<fee_categoriesModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    fee_categoriesModel obj = new fee_categoriesModel();
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.id_online = row["id_online"] != DBNull.Value ? Convert.ToString(row["id_online"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;
                    obj.recur = row["recur"] != DBNull.Value ? Convert.ToString(row["recur"]) : string.Empty;
                    obj.is_transport = row["is_transport"] != DBNull.Value ? Convert.ToString(row["is_transport"]) : string.Empty;
                    obj.order = row["order"] != DBNull.Value ? Convert.ToString(row["order"]) : string.Empty;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    objFeeCategoriesList.Add(obj);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objFeeCategoriesList;
        }

        #endregion

        #region view
        public static Boolean CreateOrModfiyFeeCategories(FeeCategoriesListModel objFeeCategories, LoginModel CurrentLogin, SchoolModel SchoolInfo)
        {
            Boolean IsSuccess = false;
            try
            {
                if (objFeeCategories.id_offline == null) // New FeeCategory
                {
                    objFeeCategories.id_offline = Guid.NewGuid().ToString();
                    objFeeCategories.id_online = Guid.Empty.ToString();
                    objFeeCategories.created_by = CurrentLogin.User.id_offline;
                    objFeeCategories.created_on = DateTime.Now;
                    objFeeCategories.school_id = SchoolInfo.id_offline;
                    objFeeCategories.order = "0";
                    objFeeCategories.recur = "0";
                }
                objFeeCategories.updated_by = CurrentLogin.User.id_offline;
                objFeeCategories.updated_on = DateTime.Now;
                                                
                DataTable objDatatable = MapFeeCategoriesListObjectToDataTable(objFeeCategories);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.fee_categories;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.CreateOrModifyFeeCategories, objSqlParameter);
                
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

        private static DataTable MapFeeCategoriesListObjectToDataTable(FeeCategoriesListModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("recur", typeof(string));
                table.Columns.Add("is_transport", typeof(string));
                table.Columns.Add("order", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(string));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.name,
                                obj.recur,
                                obj.is_transport,
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
        public static DataTable MapFeeCategoriesObjectToDataTable(fee_categoriesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("recur", typeof(string));
                table.Columns.Add("is_transport", typeof(string));
                table.Columns.Add("order", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(string));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.name,
                                obj.recur,
                                obj.is_transport,
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
        #endregion

    }
}
