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
    public class FeeAllocationManager
    {

        #region List      
        public static ObservableCollection<FeeAllocationListModel> GetFeeAllocationList(Int64 fromRowNo, Int64 toRowNo)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo}                   
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetFeeAllocationList, lstSqlParameters);
                return MapDatatableToFeeAllocationListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }

        public static ObservableCollection<FeeAllocationListModel> MapDatatableToFeeAllocationListObject(DataTable objDatatable)
        {
            ObservableCollection<FeeAllocationListModel> objFeeAllocationList = new ObservableCollection<FeeAllocationListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    FeeAllocationListModel obj = new FeeAllocationListModel()
                    {
                        FeeCategory = new fee_categoriesModel()
                    };
                    //fees
                    obj.id_offline = row["fees.id_offline"] != DBNull.Value ? Convert.ToString(row["fees.id_offline"]) : string.Empty;
                    obj.id_online = row["fees.id_online"] != DBNull.Value ? Convert.ToString(row["fees.id_online"]) : string.Empty;
                    obj.school_id = row["fees.school_id"] != DBNull.Value ? Convert.ToString(row["fees.school_id"]) : string.Empty;
                    obj.fee_category_id = row["fees.fee_category_id"] != DBNull.Value ? Convert.ToString(row["fees.fee_category_id"]) : string.Empty;
                    obj.session_id = row["fees.session_id"] != DBNull.Value ? Convert.ToString(row["fees.session_id"]) : string.Empty;
                    obj.amount = row["fees.amount"] != DBNull.Value ? Convert.ToDouble(row["fees.amount"]) : 0;
                    obj.fee_cources = row["fees.fee_cources"] != DBNull.Value ? Convert.ToString(row["fees.fee_cources"]) : string.Empty;
                    obj.last_date = row["fees.last_date"] != DBNull.Value ? Convert.ToString(row["fees.last_date"]) : string.Empty;
                    obj.last_day = row["fees.last_day"] != DBNull.Value ? Convert.ToString(row["fees.last_day"]) : string.Empty;
                    obj.fine_per_day = row["fees.fine_per_day"] != DBNull.Value ? Convert.ToDouble(row["fees.fine_per_day"]) : 0;
                    obj.is_allocated = row["fees.is_allocated"] != DBNull.Value ? Convert.ToString(row["fees.is_allocated"]) : string.Empty;
                    obj.remarks = row["fees.remarks"] != DBNull.Value ? Convert.ToString(row["fees.remarks"]) : string.Empty;
                    obj.created_by = row["fees.created_by"] != DBNull.Value ? Convert.ToString(row["fees.created_by"]) : string.Empty;
                    obj.created_on = row["fees.created_on"] != DBNull.Value ? Convert.ToDateTime(row["fees.created_on"]) : (DateTime?)null;
                    obj.updated_by = row["fees.updated_by"] != DBNull.Value ? Convert.ToString(row["fees.updated_by"]) : string.Empty;
                    obj.updated_on = row["fees.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["fees.updated_on"]) : (DateTime?)null;
                    //fee_categories
                    obj.FeeCategory.id_offline = row["fee_categories.id_offline"] != DBNull.Value ? Convert.ToString(row["fee_categories.id_offline"]) : string.Empty;
                    obj.FeeCategory.id_online = row["fee_categories.id_online"] != DBNull.Value ? Convert.ToString(row["fee_categories.id_online"]) : string.Empty;
                    obj.FeeCategory.school_id = row["fee_categories.school_id"] != DBNull.Value ? Convert.ToString(row["fee_categories.school_id"]) : string.Empty;
                    obj.FeeCategory.name = row["fee_categories.name"] != DBNull.Value ? Convert.ToString(row["fee_categories.name"]) : string.Empty;
                    obj.FeeCategory.recur = row["fee_categories.recur"] != DBNull.Value ? Convert.ToString(row["fee_categories.recur"]) : string.Empty;
                    obj.FeeCategory.is_transport = row["fee_categories.is_transport"] != DBNull.Value ? Convert.ToString(row["fee_categories.is_transport"]) : string.Empty;
                    obj.FeeCategory.order = row["fee_categories.order"] != DBNull.Value ? Convert.ToString(row["fee_categories.order"]) : string.Empty;
                    obj.FeeCategory.created_by = row["fee_categories.created_by"] != DBNull.Value ? Convert.ToString(row["fee_categories.created_by"]) : string.Empty;
                    obj.FeeCategory.created_on = row["fee_categories.created_on"] != DBNull.Value ? Convert.ToDateTime(row["fee_categories.created_on"]) : (DateTime?)null;
                    obj.FeeCategory.updated_by = row["fee_categories.updated_by"] != DBNull.Value ? Convert.ToString(row["fee_categories.updated_by"]) : string.Empty;
                    obj.FeeCategory.updated_on = row["fee_categories.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["fee_categories.updated_on"]) : (DateTime?)null;
                    //grades applied to
                    obj.GradesAppliedTo = row["GradesAppliedTo"] != DBNull.Value ? Convert.ToString(row["GradesAppliedTo"]) : string.Empty;
                    //applied to Grade IDs
                    obj.AppliedToGradeIDs = row["AppliedToGradeIDs"] != DBNull.Value ? Convert.ToString(row["AppliedToGradeIDs"]) : string.Empty;
                    //student count
                    obj.StudentCount = row["StudentCount"] != DBNull.Value ? Convert.ToInt64(row["StudentCount"]) : 0;
                    //Created By
                    obj.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToString(row["CreatedBy"]) : string.Empty;
                    objFeeAllocationList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objFeeAllocationList;
        }

        #endregion

        #region view
        public static Boolean CreateOrModfiyFeeAllocation(
            FeeAllocationListModel objFees, 
            ObservableCollection<FeeMonthsMultiComboBoxItem> FeeMonthsMultiComboBoxCheckedItems, 
            ObservableCollection<GradesMultiComboBoxItem> GradesMultiComboBoxCheckedItems,
            ObservableCollection<StudentsMultiComboBoxItem> StudentsMultiComboBoxCheckedItems,
            List<GradesListModel> AllGradesList,
            List<sectionsModel> AllSectionsList,
            LoginModel CurrentLogin, SchoolModel SchoolInfo, sessionsModel CurrentSession,
            ListModel AllocateFeeTo
        )
        {
            Boolean IsSuccess = false;
            try
            {
                if (objFees.id_offline == null) // New FeeCategory
                {
                    objFees.id_offline = Guid.NewGuid().ToString();
                    objFees.id_online = Guid.Empty.ToString();
                    objFees.created_by = CurrentLogin.User.id_offline;
                    objFees.created_on = DateTime.Now;
                    objFees.school_id = SchoolInfo.id_offline;
                    objFees.session_id = CurrentSession.id_offline;
                    objFees.is_allocated = "true";
                }
                objFees.fee_cources = string.Empty;
                objFees.fee_category_id = objFees.FeeCategory.id_offline;
                foreach (FeeMonthsMultiComboBoxItem feeMonthsMultiComboBoxItem in FeeMonthsMultiComboBoxCheckedItems)
                {
                    if (feeMonthsMultiComboBoxItem.FeeMonth.id != "All")
                    {
                        objFees.fee_cources += Convert.ToDateTime(feeMonthsMultiComboBoxItem.FeeMonth.id).ToString("yyyy-MM-dd") + ",";
                    }
                }
                objFees.fee_cources = objFees.fee_cources.Substring(0, objFees.fee_cources.Length - 1);
                objFees.updated_by = CurrentLogin.User.id_offline;
                objFees.updated_on = DateTime.Now;

                List<grade_feesModel> objExistingGradeFeesList = GetAllGradeFeesList();
                List<grade_feesModel> objGradeFeesList = new List<grade_feesModel>();
                List<student_feesModel> objStudentFeesList = new List<student_feesModel>();

                foreach (GradesMultiComboBoxItem gradesMultiComboBoxItem in GradesMultiComboBoxCheckedItems)
                {
                    grade_feesModel objgrade_fees = new grade_feesModel()
                    {
                        id_offline = Guid.NewGuid().ToString(),
                        id_online = Guid.Empty.ToString(),
                        created_by = CurrentLogin.User.id_offline,
                        created_on = DateTime.Now,
                        school_id = SchoolInfo.id_offline,
                        fees_id = objFees.id_offline,
                        grade_id = gradesMultiComboBoxItem.Grade.id_offline,
                        updated_by = CurrentLogin.User.id_offline,
                        updated_on = DateTime.Now
                    };

                    grade_feesModel objExistingGradeFees = objExistingGradeFeesList.Find(x => x.fees_id == objFees.id_offline && x.grade_id == gradesMultiComboBoxItem.Grade.id_offline);
                    if (objExistingGradeFees != null && objExistingGradeFees.id_offline != null)
                        objgrade_fees.id_offline = objExistingGradeFees.id_offline;
                    else
                        objgrade_fees.id_offline = Guid.NewGuid().ToString();
                    objGradeFeesList.Add(objgrade_fees);
                }; 
                switch(AllocateFeeTo.id)
                {
                    case "All students of selected grades":
                        
                        foreach (grade_feesModel objGradeFees in objGradeFeesList)
                        {
                            GradesListModel grade = new GradesListModel();
                            grade = AllGradesList.Find(x => x.id_offline == objGradeFees.grade_id);
                            StudentsListFiltersModel StudentsListFilters = new StudentsListFiltersModel()
                            { 
                                Grade  = grade,
                                SectionsList = AllSectionsList,
                                Sections = AllSectionsList,
                            };
                            
                            ObservableCollection<StudentsListModel> StudentList = StudentsManager.GetStudentsList(1, Int64.MaxValue, StudentsListFilters);
                            foreach (StudentsListModel Student in StudentList)
                            {
                                foreach (FeeMonthsMultiComboBoxItem feeMonthsMultiComboBoxItem in FeeMonthsMultiComboBoxCheckedItems)
                                {
                                    if (feeMonthsMultiComboBoxItem.FeeMonth.id != "All")
                                    {
                                        student_feesModel objStudentFees = new student_feesModel()
                                        {
                                            id_offline = Guid.NewGuid().ToString(),
                                            id_online = Guid.Empty.ToString(),
                                            created_by = CurrentLogin.User.id_offline,
                                            created_on = DateTime.Now,
                                            school_id = SchoolInfo.id_offline,
                                            grade_fees_id = objGradeFees.id_offline,
                                            apply_from = Convert.ToDateTime(feeMonthsMultiComboBoxItem.FeeMonth.id),
                                            apply_to = Convert.ToDateTime(feeMonthsMultiComboBoxItem.FeeMonth.id).AddMonths(1).AddDays(-1),
                                            concession_amount = 0,
                                            fine = 0,
                                            no_fine = "0",
                                            route_vehicle_stops_fee_log_id = Guid.Empty.ToString(),
                                            student_id = Student.id_offline,
                                            updated_by = CurrentLogin.User.id_offline,
                                            updated_on = DateTime.Now

                                        };
                                        objStudentFeesList.Add(objStudentFees);
                                    }
                                }
                                
                            }
                        }
                        break;
                    case "Chosen students from a list":
                        foreach (grade_feesModel objGradeFees in objGradeFeesList)
                        {
                            foreach (StudentsMultiComboBoxItem studentMultiComboBoxItem in StudentsMultiComboBoxCheckedItems)
                            {
                                StudentsListModel Student = studentMultiComboBoxItem.Student;
                                if (Student.Grade.id_offline == objGradeFees.grade_id)
                                {
                                    foreach (FeeMonthsMultiComboBoxItem feeMonthsMultiComboBoxItem in FeeMonthsMultiComboBoxCheckedItems)
                                    {
                                        if (feeMonthsMultiComboBoxItem.FeeMonth.id != "All")
                                        {
                                            student_feesModel objStudentFees = new student_feesModel()
                                            {
                                                id_offline = Guid.NewGuid().ToString(),
                                                id_online = Guid.Empty.ToString(),
                                                created_by = CurrentLogin.User.id_offline,
                                                created_on = DateTime.Now,
                                                school_id = SchoolInfo.id_offline,
                                                grade_fees_id = objGradeFees.id_offline,
                                                apply_from = Convert.ToDateTime(feeMonthsMultiComboBoxItem.FeeMonth.id),
                                                apply_to = Convert.ToDateTime(feeMonthsMultiComboBoxItem.FeeMonth.id).AddMonths(1).AddDays(-1),
                                                concession_amount = 0,
                                                fine = 0,
                                                no_fine = "0",
                                                route_vehicle_stops_fee_log_id = Guid.Empty.ToString(),
                                                student_id = Student.id_offline,
                                                updated_by = CurrentLogin.User.id_offline,
                                                updated_on = DateTime.Now

                                            };
                                            objStudentFeesList.Add(objStudentFees);
                                        }
                                    }
                                }

                            }
                        }
                        break;
                    case "No one - Will allocate later":
                        objStudentFeesList = null;
                        break;
                }
                DataTable objFeesDatatable = MapFeeAllocationListObjectToDataTable(objFees);
                DataTable objGradeFeesDatatable = MapGradeFeesObjectsToDataTable(objGradeFeesList); 
                DataTable objStudentFeesDatatable = MapStudentFeesObjectsToDataTable(objStudentFeesList);

                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FeesModel", SqlDbType = SqlDbType.Structured, TypeName = DBTableTypes.fees, Value = objFeesDatatable},
                    new SqlParameter() {ParameterName = "@GradeFeesModel",  SqlDbType = SqlDbType.Structured, TypeName = DBTableTypes.grade_fees, Value = objGradeFeesDatatable},
                    new SqlParameter() {ParameterName = "@StudentFeesModel",  SqlDbType = SqlDbType.Structured, TypeName = DBTableTypes.student_fees, Value = objStudentFeesDatatable},
                };
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.CreateOrModifyFeeAllocation, lstSqlParameters);
                
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

        private static DataTable MapFeeAllocationListObjectToDataTable(FeeAllocationListModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("fee_category_id", typeof(string));
                table.Columns.Add("session_id", typeof(string));
                table.Columns.Add("amount", typeof(string));
                table.Columns.Add("fee_cources", typeof(string));
                table.Columns.Add("last_date", typeof(string));
                table.Columns.Add("last_day", typeof(string));
                table.Columns.Add("fine_per_day", typeof(string));
                table.Columns.Add("is_allocated", typeof(string));
                table.Columns.Add("remarks", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(string));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.fee_category_id,
                                obj.session_id,
                                obj.amount,
                                obj.fee_cources,
                                obj.last_date,
                                obj.last_day,
                                obj.fine_per_day,
                                obj.is_allocated,
                                obj.remarks,
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
        public static DataTable MapGradeFeesObjectsToDataTable(List<grade_feesModel> objList)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("fees_id", typeof(string));
                table.Columns.Add("grade_id", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(string));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(string));

                foreach(grade_feesModel obj in objList)
                {
                    table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.fees_id,
                                obj.grade_id,
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
        public static DataTable MapStudentFeesObjectsToDataTable(List<student_feesModel> objList)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("grade_fees_id", typeof(string));
                table.Columns.Add("student_id", typeof(string));
                table.Columns.Add("route_vehicle_stops_fee_log_id", typeof(string));
                table.Columns.Add("apply_from", typeof(DateTime));
                table.Columns.Add("apply_to", typeof(DateTime));
                table.Columns.Add("fine", typeof(string));
                table.Columns.Add("concession_amount", typeof(string));
                table.Columns.Add("no_fine", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                foreach (student_feesModel obj in objList)
                {
                    table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.grade_fees_id,
                                obj.student_id,
                                obj.route_vehicle_stops_fee_log_id,
                                obj.apply_from,
                                obj.apply_to,
                                obj.fine,
                                obj.concession_amount,
                                obj.no_fine,
                                obj.created_by,
                                obj.created_on,
                                obj.updated_by,
                                obj.updated_on
                              );
                };
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
        public static List<grade_feesModel> GetAllGradeFeesList()
        {
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = 1},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = Int64.MaxValue}
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetGradeFeesList, lstSqlParameters);
                return MapDatatableToGradeFeesListObject(objDatable);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        public static List<grade_feesModel> MapDatatableToGradeFeesListObject(DataTable objDatatable)
        {
            List<grade_feesModel> objGradeFeesList = new List<grade_feesModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    grade_feesModel obj = new grade_feesModel();

                    obj.id_offline = row["grade_fees.id_offline"] != DBNull.Value ? Convert.ToString(row["grade_fees.id_offline"]) : string.Empty;
                    obj.id_online = row["grade_fees.id_online"] != DBNull.Value ? Convert.ToString(row["grade_fees.id_online"]) : string.Empty;
                    obj.school_id = row["grade_fees.school_id"] != DBNull.Value ? Convert.ToString(row["grade_fees.school_id"]) : string.Empty;
                    obj.fees_id = row["grade_fees.fees_id"] != DBNull.Value ? Convert.ToString(row["grade_fees.fees_id"]) : string.Empty;
                    obj.grade_id = row["grade_fees.grade_id"] != DBNull.Value ? Convert.ToString(row["grade_fees.grade_id"]) : string.Empty;
                    obj.created_by = row["grade_fees.created_by"] != DBNull.Value ? Convert.ToString(row["grade_fees.created_by"]) : string.Empty;
                    obj.created_on = row["grade_fees.created_on"] != DBNull.Value ? Convert.ToDateTime(row["grade_fees.created_on"]) : (DateTime?)null;
                    obj.updated_by = row["grade_fees.updated_by"] != DBNull.Value ? Convert.ToString(row["grade_fees.updated_by"]) : string.Empty;
                    obj.updated_on = row["grade_fees.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["grade_fees.updated_on"]) : (DateTime?)null;

                    objGradeFeesList.Add(obj);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objGradeFeesList;
        }
        #endregion

    }
}
