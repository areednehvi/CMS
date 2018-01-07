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

namespace SMS_Businness_Layer.Businness
{
    public class FeeCollectionListManager
    {        
        public static ObservableCollection<FeeCollectionStudentListModel> GetFeeCollectionStudentList(Int64 fromRowNo, Int64 toRowNo, FeeCollectionListFiltersModel objFeeCollectionListFilters)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo},
                    new SqlParameter() {ParameterName = "@RollNumber",  SqlDbType = SqlDbType.NVarChar, Value = objFeeCollectionListFilters.RollNumber == "" ? null : objFeeCollectionListFilters.RollNumber},
                    new SqlParameter() {ParameterName = "@GradeID",  SqlDbType = SqlDbType.NVarChar, Value = (objFeeCollectionListFilters.Grade != null && objFeeCollectionListFilters.Grade.name ==  "All") ? null : (objFeeCollectionListFilters.Grade != null ? objFeeCollectionListFilters.Grade.id_offline : null)},
                    new SqlParameter() {ParameterName = "@SectionID",  SqlDbType = SqlDbType.NVarChar, Value = (objFeeCollectionListFilters.Section != null && objFeeCollectionListFilters.Section.name == "All") ? null : (objFeeCollectionListFilters.Section != null ? objFeeCollectionListFilters.Section.id_offline : null)},
                    new SqlParameter() {ParameterName = "@RegistrationID",  SqlDbType = SqlDbType.NVarChar, Value = objFeeCollectionListFilters.RegistrationID == "" ? null : objFeeCollectionListFilters.RegistrationID},
                    new SqlParameter() {ParameterName = "@ConcessionAmount",  SqlDbType = SqlDbType.NVarChar, Value = objFeeCollectionListFilters.ConcessionAmount == "" ? null : objFeeCollectionListFilters.ConcessionAmount}
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentFeeAllocatedList,lstSqlParameters);
                // The below method autopopulates properties but it is slow, so manually copy the properties as done below
                //return objDatable.DataTableToList<FeeCollectionStudentList>();
                return MapDatatableToFeeCollectionStudentListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }

        private static ObservableCollection<FeeCollectionStudentListModel> MapDatatableToFeeCollectionStudentListObject(DataTable objDatatable)
        {
            ObservableCollection<FeeCollectionStudentListModel> objFeeCollectionStudentList = new ObservableCollection<FeeCollectionStudentListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    FeeCollectionStudentListModel objFeeCollectionStudent = new FeeCollectionStudentListModel();
                    objFeeCollectionStudent.id_offline =  row["id_offline"] != DBNull.Value ?  Convert.ToString(row["id_offline"]) : string.Empty;
                    objFeeCollectionStudent.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty; ;
                    objFeeCollectionStudent.registration_id = row["registration_id"] != DBNull.Value ? Convert.ToString(row["registration_id"]) : string.Empty; ;
                    objFeeCollectionStudent.status = row["status"] != DBNull.Value ? row["status"].ToString() : string.Empty;
                    objFeeCollectionStudent.file_id = row["file_id"] != DBNull.Value ? row["file_id"].ToString() : string.Empty;
                    objFeeCollectionStudent.session_id = row["session_id"] != DBNull.Value ? Convert.ToString(row["session_id"]) : string.Empty; ;
                    objFeeCollectionStudent.fees_id = row["fees_id"] != DBNull.Value ? Convert.ToString(row["fees_id"]) : string.Empty; ;
                    objFeeCollectionStudent.grade_fees_id = row["grade_fees_id"] != DBNull.Value ? Convert.ToString(row["grade_fees_id"]) : string.Empty; ;
                    objFeeCollectionStudent.apply_from = row["apply_from"] != DBNull.Value ? Convert.ToDateTime(row["apply_from"]) : (DateTime?)null;
                    objFeeCollectionStudent.apply_to = row["apply_to"] != DBNull.Value ? Convert.ToDateTime(row["apply_to"]) : (DateTime?)null;
                    objFeeCollectionStudent.concession_amount = row["concession_amount"] != DBNull.Value ? Convert.ToDouble(row["concession_amount"]) : 0; ;
                    objFeeCollectionStudent.fine = row["fine"] != DBNull.Value ? Convert.ToDouble(row["fine"]) : 0 ;
                    objFeeCollectionStudent.grade_id = row["grade_id"] != DBNull.Value ? Convert.ToString(row["grade_id"]) : string.Empty; ;
                    objFeeCollectionStudent.section_id = row["section_id"] != DBNull.Value ? Convert.ToString(row["section_id"]) : string.Empty; ;
                    objFeeCollectionStudent.full_name = row["full_name"] != DBNull.Value ?  row["full_name"].ToString() : string.Empty;
                    objFeeCollectionStudent.roll_number = row["roll_number"] != DBNull.Value ? row["roll_number"].ToString() : string.Empty;
                    objFeeCollectionStudent.section_name = row["section_name"] != DBNull.Value ? row["section_name"].ToString() : string.Empty;
                    objFeeCollectionStudent.order = row["order"] != DBNull.Value ? Convert.ToString(row["order"]) : string.Empty; ;
                    objFeeCollectionStudent.grade_section = row["grade_section"] != DBNull.Value ? row["grade_section"].ToString() : string.Empty;
                    objFeeCollectionStudent.allocated_fee_cource_count = row["allocated_fee_cource_count"] != DBNull.Value ? Convert.ToInt64(row["allocated_fee_cource_count"]) : 0; 
                    objFeeCollectionStudent.parentage = row["parentage"] != DBNull.Value ? row["parentage"].ToString() : string.Empty;

                    objFeeCollectionStudentList.Add(objFeeCollectionStudent);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objFeeCollectionStudentList;
        }

        // The below method autopopulates properties but it is slow
        /*public static ObservableCollection<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                ObservableCollection<T> list = new ObservableCollection<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }*/
    }
}
