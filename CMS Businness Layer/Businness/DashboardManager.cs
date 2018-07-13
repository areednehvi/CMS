using CMS.Models;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using SMS_Models.Models;
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
    public class DashboardManager
    {
    
        public static List<Keyvalue> GetStudentGenderRatio()
        {
            List<Keyvalue> lstKeyValues = new List<Keyvalue>();
            try
            {   
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentGenderRatio);
                foreach(DataRow row in objDatable.Rows)
                {
                    lstKeyValues.Add(new Keyvalue() { Key = row["Gender"].ToString(), Value  = Convert.ToInt64(row["Count"]) });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstKeyValues;

        }
        public static List<Keyvalue> GetStudentPaymentsAsPerMonthRatio()
        {
            List<Keyvalue> lstKeyValues = new List<Keyvalue>();
            try
            {
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentPaymentsSumAsPerMonth);
                foreach (DataRow row in objDatable.Rows)
                {
                    lstKeyValues.Add(new Keyvalue() { Key = Convert.ToDateTime(row["Month"]).ToString("MMM yyyy") + " ", Value = Convert.ToInt64(row["Amount"]) });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstKeyValues;

        }
        public static GeneralInfoWidgetModel GetDashboardGeneralInfoWidgetDetails()
        {
            try
            {
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetDashboardGeneralInfoWidgetDetails);
                return MapDatatableToGeneralInfoWidgetObject(objDatable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        private static GeneralInfoWidgetModel MapDatatableToGeneralInfoWidgetObject(DataTable objDatatable)
        {
            GeneralInfoWidgetModel obj = new GeneralInfoWidgetModel();
            try
            {                
                obj.StudentCount = objDatatable.Rows[0]["StudentCount"] != DBNull.Value ? Convert.ToInt32(objDatatable.Rows[0]["StudentCount"]) : 0;
                obj.TodaysRevenue = objDatatable.Rows[0]["TodaysRevenue"] != DBNull.Value ? Convert.ToDouble(objDatatable.Rows[0]["TodaysRevenue"]) : 0;               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return obj;
        }

        public static ObservableCollection<StudentCountAsPerCourseModel> GetStudentCountAsPerCourseList()
        {
            try
            {
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentCountAsPerCourseList);
                return MapDatatableToStudentCountAsPerCourseObject(objDatable);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        private static ObservableCollection<StudentCountAsPerCourseModel> MapDatatableToStudentCountAsPerCourseObject(DataTable objDatatable)
        {
            ObservableCollection<StudentCountAsPerCourseModel> objStudentCountAsPerCourseList = new ObservableCollection<StudentCountAsPerCourseModel>();
            try
            {               
                foreach (DataRow row in objDatatable.Rows)
                {
                    StudentCountAsPerCourseModel obj = new StudentCountAsPerCourseModel();         
                    obj.Course = row["Course"] != DBNull.Value ? Convert.ToString(row["Course"]) : string.Empty;
                    obj.StudentCount = row["StudentCount"] != DBNull.Value ? Convert.ToInt64(row["StudentCount"]) : 0;                

                    objStudentCountAsPerCourseList.Add(obj);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objStudentCountAsPerCourseList;
        }
    }
}
