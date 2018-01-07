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
    public class LoginManager
    {
        public static Boolean ValidateUser(LoginModel objLogin)
        {
            Boolean isValidUser = false;


            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {

                    new SqlParameter() {ParameterName = "@Username",     SqlDbType = SqlDbType.NVarChar, Value = objLogin.User.username},
                    new SqlParameter() {ParameterName = "@Password",  SqlDbType = SqlDbType.NVarChar, Value = objLogin.User.password}
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.AuthenticateUser, lstSqlParameters);
                if (objDatable.Rows.Count > 0)
                {
                    if (objDatable.Rows[0][0].ToString() == LoginDefinitions.AccountIsNotActive)
                        objLogin.Message = LoginDefinitions.AccountIsNotActive;
                    else if (objDatable.Rows[0][0].ToString() == LoginDefinitions.PasswordIsIncorrect)
                        objLogin.Message = LoginDefinitions.PasswordIsIncorrect;
                    else if (objDatable.Rows[0][0].ToString() == LoginDefinitions.UsernameIsIncorrect)
                        objLogin.Message = LoginDefinitions.UsernameIsIncorrect;
                    else if (objDatable.Rows[0][0].ToString() == LoginDefinitions.AccountDoesntExist)
                        objLogin.Message = LoginDefinitions.AccountDoesntExist;
                    else
                    {
                        objLogin.User = UsersManager.MapDatatableToUsersListObject(objDatable)[0];                  
                        isValidUser = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return isValidUser;

        }
    }

}