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
    public class ParentsManager
    {
        public static DataTable MapParentsToDataTable(parentsModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("f_first_name", typeof(string));
                table.Columns.Add("f_middle_name", typeof(string));
                table.Columns.Add("f_last_name", typeof(string));
                table.Columns.Add("f_full_name", typeof(string));
                table.Columns.Add("f_mobile", typeof(string));
                table.Columns.Add("f_phone", typeof(string));
                table.Columns.Add("f_office", typeof(string));
                table.Columns.Add("f_email", typeof(string));
                table.Columns.Add("m_first_name", typeof(string));
                table.Columns.Add("m_middle_name", typeof(string));
                table.Columns.Add("m_last_name", typeof(string));
                table.Columns.Add("m_full_name", typeof(string));
                table.Columns.Add("m_mobile", typeof(string));
                table.Columns.Add("m_phone", typeof(string));
                table.Columns.Add("m_office", typeof(string));
                table.Columns.Add("m_email", typeof(string));
                table.Columns.Add("g_fullname", typeof(string));
                table.Columns.Add("g_mobile", typeof(string));
                table.Columns.Add("g_email", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.f_first_name,
                                obj.f_middle_name,
                                obj.f_last_name,
                                obj.f_full_name,
                                obj.f_mobile,
                                obj.f_phone,
                                obj.f_office,
                                obj.f_email,
                                obj.m_first_name,
                                obj.m_middle_name,
                                obj.m_last_name,
                                obj.m_full_name,
                                obj.m_mobile,
                                obj.m_phone,
                                obj.m_office,
                                obj.m_email,
                                obj.g_fullname,
                                obj.g_mobile,
                                obj.g_email,
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


    }
}
