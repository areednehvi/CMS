using CMS.Models;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SMS_Models.Models.DBModels;

namespace SMS_Businness_Layer.Businness
{
    public class TransactionsReportManager
    {
  
        public static ObservableCollection<TransactionsReportListModel> GetTransactionsReportList(Int64 fromRowNo, Int64 toRowNo,TransactionsReporFiltersModel TransactionsReportListFilters)
        {
            try
            {
                DataTable objGradesDatatable, objSectionsDatatable;
                if (TransactionsReportListFilters.Grade == null || (TransactionsReportListFilters.Grade != null && TransactionsReportListFilters.Grade.id_offline == Guid.Empty.ToString()))
                {
                    List<GradesListModel> gradesList = new List<GradesListModel>();
                    gradesList = TransactionsReportListFilters.GradesList.FindAll(x=>x.id_offline != Guid.Empty.ToString());
                    objGradesDatatable = GradesSetupManager.MapGradesObjectToDataTable(gradesList);
                }
                else
                    objGradesDatatable = GradesSetupManager.MapGradeObjectToDataTable(TransactionsReportListFilters.Grade);
                if (TransactionsReportListFilters.Section == null || (TransactionsReportListFilters.Section != null && TransactionsReportListFilters.Section.id_offline == Guid.Empty.ToString()))
                {
                    List<sectionsModel> sectionsList = new List<sectionsModel>();
                    sectionsList = TransactionsReportListFilters.SectionsList.FindAll(x => x.id_offline != Guid.Empty.ToString());
                    objSectionsDatatable = SectionsSetupManager.MapSectionsObjectToDataTable(sectionsList);
                }
                else
                    objSectionsDatatable = SectionsSetupManager.MapSectionObjectToDataTable(TransactionsReportListFilters.Section);
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo},
                    new SqlParameter() {ParameterName = "@GradesModel",  TypeName = DBTableTypes.grades, Value = objGradesDatatable},
                    new SqlParameter() {ParameterName = "@SectionsModel",  TypeName = DBTableTypes.sections, Value = objSectionsDatatable},
                    new SqlParameter() {ParameterName = "@RollNumber",  SqlDbType = SqlDbType.NVarChar, Value = (TransactionsReportListFilters.RollNumber == "" ? null :TransactionsReportListFilters.RollNumber)},
                    new SqlParameter() {ParameterName = "@ReceiptNumber",  SqlDbType = SqlDbType.NVarChar, Value = (TransactionsReportListFilters.ReceiptNumber == "" ? null :TransactionsReportListFilters.ReceiptNumber)},
                    new SqlParameter() {ParameterName = "@PaymentFromDate",  SqlDbType = SqlDbType.Date, Value = (TransactionsReportListFilters.FromDate == (DateTime?)null ? (DateTime?)null :TransactionsReportListFilters.FromDate)},
                    new SqlParameter() {ParameterName = "@PaymentToDate",  SqlDbType = SqlDbType.Date, Value = (TransactionsReportListFilters.ToDate == (DateTime?)null ? (DateTime?)null :TransactionsReportListFilters.ToDate)},
                    new SqlParameter() {ParameterName = "@RegistrationID",  SqlDbType = SqlDbType.NVarChar, Value = (TransactionsReportListFilters.RegistrationID == "" ? null :TransactionsReportListFilters.RegistrationID)},
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetTransactionsReport, lstSqlParameters);
                return MapDatatableToTransactionsReportObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }

        private static ObservableCollection<TransactionsReportListModel> MapDatatableToTransactionsReportObject(DataTable objDatatable)
        {
            ObservableCollection<TransactionsReportListModel> objTransactionsReportList = new ObservableCollection<TransactionsReportListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    TransactionsReportListModel obj = new TransactionsReportListModel()
                    {
                        User = new usersModel(),
                        Grade = new gradesModel(),
                        Section = new sectionsModel(),
                        Student = new studentsModel(),
                        Student_fees = new student_feesModel(),
                        Fee_categories = new fee_categoriesModel(),
                        Student_payment = new student_paymentsModel(),
                        Student_grade_session_log = new student_grade_session_logModel()
                    };
                    //Student
                    obj.Student.id_offline = row["Students.id_offline"] != DBNull.Value ? Convert.ToString(row["students.id_offline"]) : string.Empty;
                    obj.Student.id_online = row["students.id_online"] != DBNull.Value ? Convert.ToString(row["students.id_online"]) : string.Empty;
                    obj.Student.school_id = row["students.school_id"] != DBNull.Value ? Convert.ToString(row["students.school_id"]) : string.Empty;
                    obj.Student.user_id = row["students.user_id"] != DBNull.Value ? Convert.ToString(row["students.user_id"]) : string.Empty;
                    obj.Student.parent_id = row["students.parent_id"] != DBNull.Value ? Convert.ToString(row["students.parent_id"]) : string.Empty;
                    obj.Student.enrollment_date = row["students.enrollment_date"] != DBNull.Value ? Convert.ToDateTime(row["students.enrollment_date"]) : (DateTime?)null;
                    obj.Student.status = row["students.status"] != DBNull.Value ? Convert.ToString(row["students.status"]) : string.Empty;
                    obj.Student.dc_number = row["students.dc_number"] != DBNull.Value ? Convert.ToString(row["students.dc_number"]) : string.Empty;
                    obj.Student.dc_date_of_issue = row["students.dc_date_of_issue"] != DBNull.Value ? Convert.ToDateTime(row["students.dc_date_of_issue"]) : (DateTime?)null;
                    obj.Student.created_by = row["students.created_by"] != DBNull.Value ? Convert.ToString(row["students.created_by"]) : string.Empty;
                    obj.Student.created_on = row["students.created_on"] != DBNull.Value ? Convert.ToDateTime(row["students.created_on"]) : (DateTime?)null;
                    obj.Student.updated_by = row["students.updated_by"] != DBNull.Value ? Convert.ToString(row["students.updated_by"]) : string.Empty;
                    obj.Student.updated_on = row["students.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["students.updated_on"]) : (DateTime?)null;

                    //User
                    obj.User.id_offline = row["users.id_offline"] != DBNull.Value ? Convert.ToString(row["users.id_offline"]) : string.Empty;
                    obj.User.id_online = row["users.id_online"] != DBNull.Value ? Convert.ToString(row["users.id_online"]) : string.Empty;
                    obj.User.school_id = row["users.school_id"] != DBNull.Value ? Convert.ToString(row["users.school_id"]) : string.Empty;
                    obj.User.student_id = row["users.student_id"] != DBNull.Value ? Convert.ToString(row["users.student_id"]) : string.Empty;
                    obj.User.role_id = row["users.role_id"] != DBNull.Value ? Convert.ToString(row["users.role_id"]) : string.Empty;
                    obj.User.user_type = row["users.user_type"] != DBNull.Value ? Convert.ToString(row["users.user_type"]) : string.Empty;
                    obj.User.username = row["users.username"] != DBNull.Value ? Convert.ToString(row["users.username"]) : string.Empty;
                    obj.User.email = row["users.email"] != DBNull.Value ? Convert.ToString(row["users.email"]) : string.Empty;
                    obj.User.phone = row["users.phone"] != DBNull.Value ? Convert.ToString(row["users.phone"]) : string.Empty;
                    obj.User.address_line_one = row["users.address_line_one"] != DBNull.Value ? Convert.ToString(row["users.address_line_one"]) : string.Empty;
                    obj.User.address_line_two = row["users.address_line_two"] != DBNull.Value ? Convert.ToString(row["users.address_line_two"]) : string.Empty;
                    obj.User.area = row["users.area"] != DBNull.Value ? Convert.ToString(row["users.area"]) : string.Empty;
                    obj.User.first_name = row["users.first_name"] != DBNull.Value ? Convert.ToString(row["users.first_name"]) : string.Empty;
                    obj.User.middle_name = row["users.middle_name"] != DBNull.Value ? Convert.ToString(row["users.middle_name"]) : string.Empty;
                    obj.User.last_name = row["users.last_name"] != DBNull.Value ? Convert.ToString(row["users.last_name"]) : string.Empty;
                    obj.User.full_name = row["users.full_name"] != DBNull.Value ? Convert.ToString(row["users.full_name"]) : string.Empty;
                    obj.User.gender = row["users.gender"] != DBNull.Value ? Convert.ToString(row["users.gender"]) : string.Empty;
                    obj.User.blood_group = row["users.blood_group"] != DBNull.Value ? Convert.ToString(row["users.blood_group"]) : string.Empty;
                    obj.User.password = row["users.password"] != DBNull.Value ? Convert.ToString(row["users.password"]) : string.Empty;
                    obj.User.birth_date = row["users.birth_date"] != DBNull.Value ? Convert.ToDateTime(row["users.birth_date"]) : (DateTime?)null;
                    obj.User.other_phones = row["users.other_phones"] != DBNull.Value ? Convert.ToString(row["users.other_phones"]) : string.Empty;
                    obj.User.default_phone_number_id = row["users.default_phone_number_id"] != DBNull.Value ? Convert.ToString(row["users.default_phone_number_id"]) : string.Empty;
                    obj.User.adhaar_number = row["users.adhaar_number"] != DBNull.Value ? Convert.ToString(row["users.adhaar_number"]) : string.Empty;
                    obj.User.bank_name = row["users.bank_name"] != DBNull.Value ? Convert.ToString(row["users.bank_name"]) : string.Empty;
                    obj.User.bank_branch = row["users.bank_branch"] != DBNull.Value ? Convert.ToString(row["users.bank_branch"]) : string.Empty;
                    obj.User.bank_account_number = row["users.bank_account_number"] != DBNull.Value ? Convert.ToString(row["users.bank_account_number"]) : string.Empty;
                    obj.User.bank_ifsc_code = row["users.bank_ifsc_code"] != DBNull.Value ? Convert.ToString(row["users.bank_ifsc_code"]) : string.Empty;
                    obj.User.flags = row["users.flags"] != DBNull.Value ? Convert.ToString(row["users.flags"]) : string.Empty;
                    obj.User.last_login_time = row["users.last_login_time"] != DBNull.Value ? Convert.ToDateTime(row["users.last_login_time"]) : (DateTime?)null;
                    obj.User.user_avatar_file_id = row["users.user_avatar_file_id"] != DBNull.Value ? Convert.ToString(row["users.user_avatar_file_id"]) : string.Empty;
                    obj.User.is_active = row["users.is_active"] != DBNull.Value ? Convert.ToBoolean(row["users.is_active"]) : false;
                    obj.User.created_on = row["users.created_on"] != DBNull.Value ? Convert.ToDateTime(row["users.created_on"]) : (DateTime?)null;
                    obj.User.created_by = row["users.created_by"] != DBNull.Value ? Convert.ToString(row["users.created_by"]) : string.Empty;
                    obj.User.updated_on = row["users.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["users.updated_on"]) : (DateTime?)null;
                    obj.User.updated_by = row["users.updated_by"] != DBNull.Value ? Convert.ToString(row["users.updated_by"]) : string.Empty;
                    //Grade
                    obj.Grade.id_offline = row["grades.id_offline"] != DBNull.Value ? Convert.ToString(row["grades.id_offline"]) : string.Empty;
                    obj.Grade.id_online = row["grades.id_online"] != DBNull.Value ? Convert.ToString(row["grades.id_online"]) : string.Empty;
                    obj.Grade.school_id = row["grades.school_id"] != DBNull.Value ? Convert.ToString(row["grades.school_id"]) : string.Empty;
                    obj.Grade.course_id = row["grades.course_id"] != DBNull.Value ? Convert.ToString(row["grades.course_id"]) : string.Empty;
                    obj.Grade.name = row["grades.name"] != DBNull.Value ? Convert.ToString(row["grades.name"]) : string.Empty;
                    obj.Grade.order = row["grades.order"] != DBNull.Value ? Convert.ToString(row["grades.order"]) : string.Empty;
                    obj.Grade.created_by = row["grades.created_by"] != DBNull.Value ? Convert.ToString(row["grades.created_by"]) : string.Empty;
                    obj.Grade.created_on = row["grades.created_on"] != DBNull.Value ? Convert.ToDateTime(row["grades.created_on"]) : (DateTime?)null;
                    obj.Grade.updated_by = row["grades.updated_by"] != DBNull.Value ? Convert.ToString(row["grades.updated_by"]) : string.Empty;
                    obj.Grade.updated_on = row["grades.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["grades.updated_on"]) : (DateTime?)null;
                    //Section
                    obj.Section.id_offline = row["sections.id_offline"] != DBNull.Value ? Convert.ToString(row["sections.id_offline"]) : string.Empty;
                    obj.Section.id_online = row["sections.id_online"] != DBNull.Value ? Convert.ToString(row["sections.id_online"]) : string.Empty;
                    obj.Section.school_id = row["sections.school_id"] != DBNull.Value ? Convert.ToString(row["sections.school_id"]) : string.Empty;
                    obj.Section.name = row["sections.name"] != DBNull.Value ? Convert.ToString(row["sections.name"]) : string.Empty;
                    obj.Section.capacity = row["sections.capacity"] != DBNull.Value ? Convert.ToInt16(row["sections.capacity"]) : 0;
                    obj.Section.created_by = row["sections.created_by"] != DBNull.Value ? Convert.ToString(row["sections.created_by"]) : string.Empty;
                    obj.Section.created_on = row["sections.created_on"] != DBNull.Value ? Convert.ToDateTime(row["sections.created_on"]) : (DateTime?)null;
                    obj.Section.updated_by = row["sections.updated_by"] != DBNull.Value ? Convert.ToString(row["sections.updated_by"]) : string.Empty;
                    obj.Section.updated_on = row["sections.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["sections.updated_on"]) : (DateTime?)null;
                    //Student_payments
                    obj.Student_payment.id_offline = row["student_payments.id_offline"] != DBNull.Value ? Convert.ToString(row["student_payments.id_offline"]) : string.Empty;
                    obj.Student_payment.id_online = row["student_payments.id_online"] != DBNull.Value ? Convert.ToString(row["student_payments.id_online"]) : string.Empty;
                    obj.Student_payment.school_id = row["student_payments.school_id"] != DBNull.Value ? Convert.ToString(row["student_payments.school_id"]) : string.Empty;
                    obj.Student_payment.student_fees_id = row["student_payments.student_fees_id"] != DBNull.Value ? Convert.ToString(row["student_payments.student_fees_id"]) : string.Empty;
                    obj.Student_payment.amount = row["student_payments.amount"] != DBNull.Value ? Convert.ToDouble(row["student_payments.amount"]) : 0;
                    obj.Student_payment.fine = row["student_payments.fine"] != DBNull.Value ? Convert.ToDouble(row["student_payments.fine"]) : 0;
                    obj.Student_payment.comment = row["student_payments.comment"] != DBNull.Value ? Convert.ToString(row["student_payments.comment"]) : string.Empty;
                    obj.Student_payment.recept_no = row["student_payments.recept_no"] != DBNull.Value ? Convert.ToString(row["student_payments.recept_no"]) : string.Empty;
                    obj.Student_payment.payment_mode = row["student_payments.payment_mode"] != DBNull.Value ? Convert.ToString(row["student_payments.payment_mode"]) : string.Empty;
                    obj.Student_payment.payment_date = row["student_payments.payment_date"] != DBNull.Value ? Convert.ToDateTime(row["student_payments.payment_date"]) : (DateTime?)null;
                    obj.Student_payment.ip = row["student_payments.ip"] != DBNull.Value ? Convert.ToString(row["student_payments.ip"]) : string.Empty;
                    obj.Student_payment.created_by = row["student_payments.created_by"] != DBNull.Value ? Convert.ToString(row["student_payments.created_by"]) : string.Empty;
                    obj.Student_payment.created_on = row["student_payments.created_on"] != DBNull.Value ? Convert.ToDateTime(row["student_payments.created_on"]) : (DateTime?)null;
                    obj.Student_payment.updated_by = row["student_payments.updated_by"] != DBNull.Value ? Convert.ToString(row["student_payments.updated_by"]) : string.Empty;
                    obj.Student_payment.updated_on = row["student_payments.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["student_payments.updated_on"]) : (DateTime?)null;
                    //Student_fees
                    obj.Student_fees.id_offline = row["Student_fees.id_offline"] != DBNull.Value ? Convert.ToString(row["Student_fees.id_offline"]) : string.Empty;
                    obj.Student_fees.id_online = row["Student_fees.id_online"] != DBNull.Value ? Convert.ToString(row["Student_fees.id_online"]) : string.Empty;
                    obj.Student_fees.school_id = row["Student_fees.school_id"] != DBNull.Value ? Convert.ToString(row["Student_fees.school_id"]) : string.Empty;
                    obj.Student_fees.grade_fees_id = row["Student_fees.grade_fees_id"] != DBNull.Value ? Convert.ToString(row["Student_fees.grade_fees_id"]) : string.Empty;
                    obj.Student_fees.student_id = row["Student_fees.student_id"] != DBNull.Value ? Convert.ToString(row["Student_fees.student_id"]) : string.Empty;
                    obj.Student_fees.route_vehicle_stops_fee_log_id = row["Student_fees.route_vehicle_stops_fee_log_id"] != DBNull.Value ? Convert.ToString(row["Student_fees.route_vehicle_stops_fee_log_id"]) : string.Empty;
                    obj.Student_fees.apply_from = row["Student_fees.apply_from"] != DBNull.Value ? Convert.ToDateTime(row["Student_fees.apply_from"]) : (DateTime?)null;
                    obj.Student_fees.apply_to = row["Student_fees.apply_to"] != DBNull.Value ? Convert.ToDateTime(row["Student_fees.apply_to"]) : (DateTime?)null;
                    obj.Student_fees.fine = row["Student_fees.fine"] != DBNull.Value ? Convert.ToDouble(row["Student_fees.fine"]) : 0;
                    obj.Student_fees.concession_amount = row["Student_fees.concession_amount"] != DBNull.Value ? Convert.ToDouble(row["Student_fees.concession_amount"]) : 0;
                    obj.Student_fees.no_fine = row["Student_fees.no_fine"] != DBNull.Value ? Convert.ToString(row["Student_fees.no_fine"]) : string.Empty;
                    obj.Student_fees.created_by = row["Student_fees.created_by"] != DBNull.Value ? Convert.ToString(row["Student_fees.created_by"]) : string.Empty;
                    obj.Student_fees.created_on = row["Student_fees.created_on"] != DBNull.Value ? Convert.ToDateTime(row["Student_fees.created_on"]) : (DateTime?)null;
                    obj.Student_fees.updated_by = row["Student_fees.updated_by"] != DBNull.Value ? Convert.ToString(row["Student_fees.updated_by"]) : string.Empty;
                    obj.Student_fees.updated_on = row["Student_fees.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["Student_fees.updated_on"]) : (DateTime?)null;
                    //fee_categories
                    obj.Fee_categories.id_offline = row["fee_categories.id_offline"] != DBNull.Value ? Convert.ToString(row["fee_categories.id_offline"]) : string.Empty;
                    obj.Fee_categories.id_online = row["fee_categories.id_online"] != DBNull.Value ? Convert.ToString(row["fee_categories.id_online"]) : string.Empty;
                    obj.Fee_categories.school_id = row["fee_categories.school_id"] != DBNull.Value ? Convert.ToString(row["fee_categories.school_id"]) : string.Empty;
                    obj.Fee_categories.name = row["fee_categories.name"] != DBNull.Value ? Convert.ToString(row["fee_categories.name"]) : string.Empty;
                    obj.Fee_categories.recur = row["fee_categories.recur"] != DBNull.Value ? Convert.ToString(row["fee_categories.recur"]) : string.Empty;
                    obj.Fee_categories.is_transport = row["fee_categories.is_transport"] != DBNull.Value ? Convert.ToString(row["fee_categories.is_transport"]) : string.Empty;
                    obj.Fee_categories.order = row["fee_categories.order"] != DBNull.Value ? Convert.ToString(row["fee_categories.order"]) : string.Empty;
                    obj.Fee_categories.created_by = row["fee_categories.created_by"] != DBNull.Value ? Convert.ToString(row["fee_categories.created_by"]) : string.Empty;
                    obj.Fee_categories.created_on = row["fee_categories.created_on"] != DBNull.Value ? Convert.ToDateTime(row["fee_categories.created_on"]) : (DateTime?)null;
                    obj.Fee_categories.updated_by = row["fee_categories.updated_by"] != DBNull.Value ? Convert.ToString(row["fee_categories.updated_by"]) : string.Empty;
                    obj.Fee_categories.updated_on = row["fee_categories.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["fee_categories.updated_on"]) : (DateTime?)null;
                    //Student_grade_session_log
                    obj.Student_grade_session_log.id_offline = row["Student_grade_session_log.id_offline"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.id_offline"]) : string.Empty;
                    obj.Student_grade_session_log.id_online = row["Student_grade_session_log.id_online"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.id_online"]) : string.Empty;
                    obj.Student_grade_session_log.school_id = row["Student_grade_session_log.school_id"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.school_id"]) : string.Empty;
                    obj.Student_grade_session_log.student_id = row["Student_grade_session_log.student_id"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.student_id"]) : string.Empty;
                    obj.Student_grade_session_log.registration_id = row["Student_grade_session_log.registration_id"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.registration_id"]) : string.Empty;
                    obj.Student_grade_session_log.grade_id = row["Student_grade_session_log.grade_id"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.grade_id"]) : string.Empty;
                    obj.Student_grade_session_log.section_id = row["Student_grade_session_log.section_id"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.section_id"]) : string.Empty;
                    obj.Student_grade_session_log.roll_number = row["Student_grade_session_log.roll_number"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.roll_number"]) : string.Empty;
                    obj.Student_grade_session_log.exam_roll_number = row["Student_grade_session_log.exam_roll_number"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.exam_roll_number"]) : string.Empty;
                    obj.Student_grade_session_log.session_id = row["Student_grade_session_log.session_id"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.session_id"]) : string.Empty;
                    obj.Student_grade_session_log.sgsl_status = row["Student_grade_session_log.sgsl_status"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.sgsl_status"]) : string.Empty;
                    obj.Student_grade_session_log.created_by = row["Student_grade_session_log.created_by"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.created_by"]) : string.Empty;
                    obj.Student_grade_session_log.created_on = row["Student_grade_session_log.created_on"] != DBNull.Value ? Convert.ToDateTime(row["Student_grade_session_log.created_on"]) : (DateTime?)null;
                    obj.Student_grade_session_log.updated_by = row["Student_grade_session_log.updated_by"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.updated_by"]) : string.Empty;
                    obj.Student_grade_session_log.updated_on = row["Student_grade_session_log.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["Student_grade_session_log.updated_on"]) : (DateTime?)null;

                    objTransactionsReportList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objTransactionsReportList;
        }

        private static DataTable GetTransactionsReportListDatatable(TransactionsReporFiltersModel TransactionsReportListFilters)
        {
            try
            {
                DataTable objGradesDatatable, objSectionsDatatable;
                if (TransactionsReportListFilters.Grade == null || (TransactionsReportListFilters.Grade != null && TransactionsReportListFilters.Grade.id_offline == Guid.Empty.ToString()))
                {
                    List<GradesListModel> gradesList = new List<GradesListModel>();
                    gradesList = TransactionsReportListFilters.GradesList.FindAll(x => x.id_offline != Guid.Empty.ToString());
                    objGradesDatatable = GradesSetupManager.MapGradesObjectToDataTable(gradesList);
                }
                else
                    objGradesDatatable = GradesSetupManager.MapGradeObjectToDataTable(TransactionsReportListFilters.Grade);
                if (TransactionsReportListFilters.Section == null || (TransactionsReportListFilters.Section != null && TransactionsReportListFilters.Section.id_offline == Guid.Empty.ToString()))
                {
                    List<sectionsModel> sectionsList = new List<sectionsModel>();
                    sectionsList = TransactionsReportListFilters.SectionsList.FindAll(x => x.id_offline != Guid.Empty.ToString());
                    objSectionsDatatable = SectionsSetupManager.MapSectionsObjectToDataTable(sectionsList);
                }
                else
                    objSectionsDatatable = SectionsSetupManager.MapSectionObjectToDataTable(TransactionsReportListFilters.Section);
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = 1},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = Int64.MaxValue},
                    new SqlParameter() {ParameterName = "@GradesModel",  TypeName = DBTableTypes.grades, Value = objGradesDatatable},
                    new SqlParameter() {ParameterName = "@SectionsModel",  TypeName = DBTableTypes.sections, Value = objSectionsDatatable},
                    new SqlParameter() {ParameterName = "@RollNumber",  SqlDbType = SqlDbType.NVarChar, Value = (TransactionsReportListFilters.RollNumber == "" ? null :TransactionsReportListFilters.RollNumber)},
                    new SqlParameter() {ParameterName = "@ReceiptNumber",  SqlDbType = SqlDbType.NVarChar, Value = (TransactionsReportListFilters.ReceiptNumber == "" ? null :TransactionsReportListFilters.ReceiptNumber)},
                    new SqlParameter() {ParameterName = "@PaymentFromDate",  SqlDbType = SqlDbType.Date, Value = (TransactionsReportListFilters.FromDate == (DateTime?)null ? (DateTime?)null :TransactionsReportListFilters.FromDate)},
                    new SqlParameter() {ParameterName = "@PaymentToDate",  SqlDbType = SqlDbType.Date, Value = (TransactionsReportListFilters.ToDate == (DateTime?)null ? (DateTime?)null :TransactionsReportListFilters.ToDate)},
                    new SqlParameter() {ParameterName = "@RegistrationID",  SqlDbType = SqlDbType.NVarChar, Value = (TransactionsReportListFilters.RegistrationID == "" ? null :TransactionsReportListFilters.RegistrationID)},
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetTransactionsReport, lstSqlParameters);
                return objDatable;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        public static Boolean ExportTransactionsReportToExcel(TransactionsReporFiltersModel TransactionsReportListFilters)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("Registration #", typeof(string));
                table.Columns.Add("Student", typeof(string));
                table.Columns.Add("Grade", typeof(string));
                table.Columns.Add("Roll Number", typeof(string));
                table.Columns.Add("Receipt Number", typeof(string));
                table.Columns.Add("Fee", typeof(string));
                table.Columns.Add("Payment Date", typeof(string));
                table.Columns.Add("Payment", typeof(string));

                DataTable objDatatable = GetTransactionsReportListDatatable(TransactionsReportListFilters);
                foreach(DataRow row in objDatatable.Rows)
                {
                    table.Rows.Add(
                        row["Student_grade_session_log.registration_id"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.registration_id"]) : string.Empty,
                        row["users.full_name"] != DBNull.Value ? Convert.ToString(row["users.full_name"]) : string.Empty,
                        (row["grades.name"] != DBNull.Value ? Convert.ToString(row["grades.name"]) : string.Empty) + " - " + (row["sections.name"] != DBNull.Value ? Convert.ToString(row["sections.name"]) : string.Empty),
                        row["Student_grade_session_log.roll_number"] != DBNull.Value ? Convert.ToString(row["Student_grade_session_log.roll_number"]) : string.Empty,
                        row["Student_payments.recept_no"] != DBNull.Value ? Convert.ToString(row["Student_payments.recept_no"]) : string.Empty,
                        (row["fee_categories.name"] != DBNull.Value ? Convert.ToString(row["fee_categories.name"]) : string.Empty) + " - " + (row["student_fees.apply_from"] != DBNull.Value ? Convert.ToDateTime(row["student_fees.apply_from"]).ToString("MMM yyyy") : string.Empty),
                        row["Student_payments.payment_date"] != DBNull.Value ? Convert.ToDateTime(row["Student_payments.payment_date"]).ToString("dd-MMM-yyyy") : string.Empty,
                        row["Student_payments.amount"] != DBNull.Value ? Convert.ToString(row["Student_payments.amount"]) : string.Empty
                    );
                }

                String pathAndName = "Reports/TransactionReport_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv";
                GeneralMethods.ExportDatatableToExcel(table, pathAndName);
                if (File.Exists(pathAndName))
                {
                    Process process = new Process();
                    process.StartInfo.FileName = Path.GetFullPath(pathAndName);
                    process.Start();
                }
            }
            catch(Exception ex)
            {
                throw ex;

            }
            return IsSuccess;
        }
    }
}
