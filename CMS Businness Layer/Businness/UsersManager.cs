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
    public class UsersManager
    {

        #region List      
        public static ObservableCollection<UsersListModel> GetUsersList(Int64 fromRowNo, Int64 toRowNo)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo}                   
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetUsersList, lstSqlParameters);
                return MapDatatableToUsersListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }

        public static ObservableCollection<UsersListModel> MapDatatableToUsersListObject(DataTable objDatatable)
        {
            ObservableCollection<UsersListModel> objUsersList = new ObservableCollection<UsersListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    UsersListModel obj = new UsersListModel();
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.id_online = row["id_online"] != DBNull.Value ? Convert.ToString(row["id_online"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.student_id = row["student_id"] != DBNull.Value ? Convert.ToString(row["student_id"]) : string.Empty;
                    obj.role_id = row["role_id"] != DBNull.Value ? Convert.ToString(row["role_id"]) : string.Empty;
                    obj.user_type = row["user_type"] != DBNull.Value ? Convert.ToString(row["user_type"]) : string.Empty;
                    obj.username = row["username"] != DBNull.Value ? Convert.ToString(row["username"]) : string.Empty;
                    obj.email = row["email"] != DBNull.Value ? Convert.ToString(row["email"]) : string.Empty;
                    obj.phone = row["phone"] != DBNull.Value ? Convert.ToString(row["phone"]) : string.Empty;
                    obj.address_line_one = row["address_line_one"] != DBNull.Value ? Convert.ToString(row["address_line_one"]) : string.Empty;
                    obj.address_line_two = row["address_line_two"] != DBNull.Value ? Convert.ToString(row["address_line_two"]) : string.Empty;
                    obj.area = row["area"] != DBNull.Value ? Convert.ToString(row["area"]) : string.Empty;
                    obj.first_name = row["first_name"] != DBNull.Value ? Convert.ToString(row["first_name"]) : string.Empty;
                    obj.middle_name = row["middle_name"] != DBNull.Value ? Convert.ToString(row["middle_name"]) : string.Empty;
                    obj.last_name = row["last_name"] != DBNull.Value ? Convert.ToString(row["last_name"]) : string.Empty;
                    obj.full_name = row["full_name"] != DBNull.Value ? Convert.ToString(row["full_name"]) : string.Empty;
                    obj.gender = row["gender"] != DBNull.Value ? Convert.ToString(row["gender"]) : string.Empty;
                    obj.blood_group = row["blood_group"] != DBNull.Value ? Convert.ToString(row["blood_group"]) : string.Empty;
                    obj.password = row["password"] != DBNull.Value ? Convert.ToString(row["password"]) : string.Empty;
                    obj.birth_date = row["birth_date"] != DBNull.Value ? Convert.ToDateTime(row["birth_date"]) : (DateTime?)null;
                    obj.other_phones = row["other_phones"] != DBNull.Value ? Convert.ToString(row["other_phones"]) : string.Empty;
                    obj.default_phone_number_id = row["default_phone_number_id"] != DBNull.Value ? Convert.ToString(row["default_phone_number_id"]) : string.Empty;
                    obj.adhaar_number = row["adhaar_number"] != DBNull.Value ? Convert.ToString(row["adhaar_number"]) : string.Empty;
                    obj.bank_name = row["bank_name"] != DBNull.Value ? Convert.ToString(row["bank_name"]) : string.Empty;
                    obj.bank_branch = row["bank_branch"] != DBNull.Value ? Convert.ToString(row["bank_branch"]) : string.Empty;
                    obj.bank_account_number = row["bank_account_number"] != DBNull.Value ? Convert.ToString(row["bank_account_number"]) : string.Empty;
                    obj.bank_ifsc_code = row["bank_ifsc_code"] != DBNull.Value ? Convert.ToString(row["bank_ifsc_code"]) : string.Empty;
                    obj.flags = row["flags"] != DBNull.Value ? Convert.ToString(row["flags"]) : string.Empty;
                    obj.last_login_time = row["last_login_time"] != DBNull.Value ? Convert.ToDateTime(row["last_login_time"]) : (DateTime?)null;
                    obj.user_avatar_file_id = row["user_avatar_file_id"] != DBNull.Value ? Convert.ToString(row["user_avatar_file_id"]) : string.Empty;
                    obj.is_active = row["is_active"] != DBNull.Value ? Convert.ToBoolean(row["is_active"]) : false;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToString(row["CreatedBy"]) : string.Empty;
                    objUsersList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objUsersList;
        }

        #endregion

        #region view
        public static Boolean IsExistingUser(UsersListModel objUser)
        {
            Boolean isExistingUser = false;
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@Username", SqlDbType = SqlDbType.NVarChar, Value = objUser.username}
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.IsExistingUser, lstSqlParameters);
                isExistingUser = objDatable.Rows.Count > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return isExistingUser;
        }
        public static Boolean CreateOrModfiyUsers(UsersListModel objUsers, LoginModel CurrentLogin, SchoolModel SchoolInfo)
        {
            Boolean IsSuccess = false;
            try
            {
                if (objUsers.id_offline == null) // New User
                {
                    objUsers.id_offline = Guid.NewGuid().ToString();
                    objUsers.id_online = Guid.Empty.ToString();
                    objUsers.created_by = CurrentLogin.User.id_offline;
                    objUsers.created_on = DateTime.Now;
                    objUsers.school_id = SchoolInfo.id_offline;
                    objUsers.user_type = "staff";
                    objUsers.student_id = Guid.Empty.ToString();
                    objUsers.role_id = Guid.Empty.ToString();
                    objUsers.user_avatar_file_id = Guid.Empty.ToString();
                    objUsers.default_phone_number_id = Guid.Empty.ToString();
                }
                objUsers.updated_by = CurrentLogin.User.id_offline;
                objUsers.updated_on = DateTime.Now;
                                                
                objUsers.first_name = objUsers.full_name.Split(' ')[0];
                objUsers.last_name = objUsers.full_name.Split(' ').Length > 1 ? objUsers.full_name.Split(' ')[1] : string.Empty;
                DataTable objDatatable = MapUsersListObjectToDataTable(objUsers);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.users;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.CreateOrModifyUsers, objSqlParameter);
                
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

        private static DataTable MapUsersListObjectToDataTable(UsersListModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("student_id", typeof(string));
                table.Columns.Add("role_id", typeof(string));
                table.Columns.Add("user_type", typeof(string));
                table.Columns.Add("username", typeof(string));
                table.Columns.Add("email", typeof(string));
                table.Columns.Add("phone", typeof(string));
                table.Columns.Add("address_line_one", typeof(string));
                table.Columns.Add("address_line_two", typeof(string));
                table.Columns.Add("area", typeof(string));
                table.Columns.Add("first_name", typeof(string));
                table.Columns.Add("middle_name", typeof(string));
                table.Columns.Add("last_name", typeof(string));
                table.Columns.Add("full_name", typeof(string));
                table.Columns.Add("gender", typeof(string));
                table.Columns.Add("blood_group", typeof(string));
                table.Columns.Add("password", typeof(string));
                table.Columns.Add("birth_date", typeof(DateTime));
                table.Columns.Add("other_phones", typeof(string));
                table.Columns.Add("default_phone_number_id", typeof(string));
                table.Columns.Add("adhaar_number", typeof(string));
                table.Columns.Add("bank_name", typeof(string));
                table.Columns.Add("bank_branch", typeof(string));
                table.Columns.Add("bank_account_number", typeof(string));
                table.Columns.Add("bank_ifsc_code", typeof(string));
                table.Columns.Add("flags", typeof(string));
                table.Columns.Add("last_login_time", typeof(DateTime));
                table.Columns.Add("user_avatar_file_id", typeof(string));
                table.Columns.Add("is_active", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.student_id,
                                obj.role_id,
                                obj.user_type,
                                obj.username,
                                obj.email,
                                obj.phone,
                                obj.address_line_one,
                                obj.address_line_two,
                                obj.area,
                                obj.first_name,
                                obj.middle_name,
                                obj.last_name,
                                obj.full_name,
                                obj.gender,
                                obj.blood_group,
                                obj.password,
                                obj.birth_date,
                                obj.other_phones,
                                obj.default_phone_number_id,
                                obj.adhaar_number,
                                obj.bank_name,
                                obj.bank_branch,
                                obj.bank_account_number,
                                obj.bank_ifsc_code,
                                obj.flags,
                                obj.last_login_time,
                                obj.user_avatar_file_id,
                                obj.is_active,
                                obj.created_on,
                                obj.created_by,
                                obj.updated_on,
                                obj.updated_by
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
        public static DataTable MapUsersObjectToDataTable(usersModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("student_id", typeof(string));
                table.Columns.Add("role_id", typeof(string));
                table.Columns.Add("user_type", typeof(string));
                table.Columns.Add("username", typeof(string));
                table.Columns.Add("email", typeof(string));
                table.Columns.Add("phone", typeof(string));
                table.Columns.Add("address_line_one", typeof(string));
                table.Columns.Add("address_line_two", typeof(string));
                table.Columns.Add("area", typeof(string));
                table.Columns.Add("first_name", typeof(string));
                table.Columns.Add("middle_name", typeof(string));
                table.Columns.Add("last_name", typeof(string));
                table.Columns.Add("full_name", typeof(string));
                table.Columns.Add("gender", typeof(string));
                table.Columns.Add("blood_group", typeof(string));
                table.Columns.Add("password", typeof(string));
                table.Columns.Add("birth_date", typeof(DateTime));
                table.Columns.Add("other_phones", typeof(string));
                table.Columns.Add("default_phone_number_id", typeof(string));
                table.Columns.Add("adhaar_number", typeof(string));
                table.Columns.Add("bank_name", typeof(string));
                table.Columns.Add("bank_branch", typeof(string));
                table.Columns.Add("bank_account_number", typeof(string));
                table.Columns.Add("bank_ifsc_code", typeof(string));
                table.Columns.Add("flags", typeof(string));
                table.Columns.Add("last_login_time", typeof(DateTime));
                table.Columns.Add("user_avatar_file_id", typeof(string));
                table.Columns.Add("is_active", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.student_id,
                                obj.role_id,
                                obj.user_type,
                                obj.username,
                                obj.email,
                                obj.phone,
                                obj.address_line_one,
                                obj.address_line_two,
                                obj.area,
                                obj.first_name,
                                obj.middle_name,
                                obj.last_name,
                                obj.full_name,
                                obj.gender,
                                obj.blood_group,
                                obj.password,
                                obj.birth_date,
                                obj.other_phones,
                                obj.default_phone_number_id,
                                obj.adhaar_number,
                                obj.bank_name,
                                obj.bank_branch,
                                obj.bank_account_number,
                                obj.bank_ifsc_code,
                                obj.flags,
                                obj.last_login_time,
                                obj.user_avatar_file_id,
                                obj.is_active,
                                obj.created_on,
                                obj.created_by,
                                obj.updated_on,
                                obj.updated_by
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
