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
    public class SessionsSetupManager
    {

        #region List      
        public static ObservableCollection<SessionsListModel> GetSessionsList(Int64 fromRowNo, Int64 toRowNo)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo}                   
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetSessionsList, lstSqlParameters);
                return MapDatatableToSessionsListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }

        public static ObservableCollection<SessionsListModel> GetCurrentSession()
        {
            try
            {
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetCurrentSession);
                return MapDatatableToSessionsListObject(objDatable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        public static List<ListModel> GetFeeMonthsOfCurrentSession()
        {
            List<ListModel> FeeMonthList = new List<ListModel>();
            try
            {
                ObservableCollection<SessionsListModel> CurrentSession = GetCurrentSession();
                for (DateTime date = Convert.ToDateTime(CurrentSession[0].from_date); date.Date <= Convert.ToDateTime(CurrentSession[0].to_date).Date; date = date.AddMonths(1))
                {
                    FeeMonthList.Add(new ListModel() { id = new DateTime(date.Year, date.Month, 1).ToString(), name = date.ToString("MMMM yyyy") });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return FeeMonthList;

        }

        private static ObservableCollection<SessionsListModel> MapDatatableToSessionsListObject(DataTable objDatatable)
        {
            ObservableCollection<SessionsListModel> objSessionsList = new ObservableCollection<SessionsListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    SessionsListModel obj = new SessionsListModel();
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;
                    obj.from_date = row["from_date"] != DBNull.Value ? Convert.ToDateTime(row["from_date"]) : (DateTime?)null;
                    obj.to_date = row["to_date"] != DBNull.Value ? Convert.ToDateTime(row["to_date"]) : (DateTime?)null;
                    obj.is_active = row["is_active"] != DBNull.Value ? Convert.ToBoolean(row["is_active"]) : false;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    obj.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToString(row["CreatedBy"]) : string.Empty;
                    objSessionsList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objSessionsList;
        }

        #endregion

        #region view
        public static Boolean CreateOrModfiySessions(SessionsListModel objSession, LoginModel CurrentLogin, SchoolModel SchoolInfo)
        {
            Boolean IsSuccess = false;
            try
            {
                if (objSession.id_offline == null) // new Session
                {
                    objSession.id_offline = Guid.NewGuid().ToString();
                    objSession.id_online = Guid.Empty.ToString();
                    objSession.created_by = CurrentLogin.User.id_offline;
                    objSession.created_on = DateTime.Now;
                    objSession.school_id = SchoolInfo.id_offline;                    
                }
                objSession.order = "0";
                objSession.updated_by = CurrentLogin.User.id_offline;
                objSession.updated_on = DateTime.Now;

                DataTable objDatatable = MapSessionListObjectToDataTable(objSession);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.sessions;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.CreateOrModifySessions, objSqlParameter);
                
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

        private static DataTable MapSessionListObjectToDataTable(SessionsListModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));                
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("order", typeof(int));
                table.Columns.Add("from_date", typeof(DateTime));
                table.Columns.Add("to_date", typeof(DateTime));
                table.Columns.Add("is_active", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online, 
                                obj.school_id,                                
                                obj.name,
                                obj.order,
                                obj.from_date,
                                obj.to_date,
                                obj.is_active,
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
