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
    public class StudentsManager
    {

        #region List      
        public static ObservableCollection<StudentsListModel> GetStudentsList(Int64 fromRowNo, Int64 toRowNo,StudentsListFiltersModel StudentsListFilters)
        {
            try
            {
                DataTable objGradesDatatable, objSectionsDatatable;
                if (StudentsListFilters.Grade == null || (StudentsListFilters.Grade != null && StudentsListFilters.Grade.id_offline == Guid.Empty.ToString()))
                {
                    List<GradesListModel> gradesList = new List<GradesListModel>();
                    gradesList = StudentsListFilters.GradesList.FindAll(x=>x.id_offline != Guid.Empty.ToString());
                    objGradesDatatable = GradesSetupManager.MapGradesObjectToDataTable(gradesList);
                }
                else
                    objGradesDatatable = GradesSetupManager.MapGradeObjectToDataTable(StudentsListFilters.Grade);
                if (StudentsListFilters.Section == null || (StudentsListFilters.Section != null && StudentsListFilters.Section.id_offline == Guid.Empty.ToString()))
                {
                    List<sectionsModel> sectionsList = new List<sectionsModel>();
                    sectionsList = StudentsListFilters.SectionsList.FindAll(x => x.id_offline != Guid.Empty.ToString());
                    objSectionsDatatable = SectionsSetupManager.MapSectionsObjectToDataTable(sectionsList);
                }
                else
                    objSectionsDatatable = SectionsSetupManager.MapSectionObjectToDataTable(StudentsListFilters.Section);
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo},
                    new SqlParameter() {ParameterName = "@GradesModel",  TypeName = DBTableTypes.grades, Value = objGradesDatatable},
                    new SqlParameter() {ParameterName = "@SectionsModel",  TypeName = DBTableTypes.sections, Value = objSectionsDatatable},
                    new SqlParameter() {ParameterName = "@RollNumber",  SqlDbType = SqlDbType.NVarChar, Value = (StudentsListFilters.RollNumber == "" ? null :StudentsListFilters.RollNumber)},
                    new SqlParameter() {ParameterName = "@RegistrationID",  SqlDbType = SqlDbType.NVarChar, Value = (StudentsListFilters.RegistrationID == "" ? null :StudentsListFilters.RegistrationID)},
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentsList, lstSqlParameters);
                return MapDatatableToStudentsListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }

        private static ObservableCollection<StudentsListModel> MapDatatableToStudentsListObject(DataTable objDatatable)
        {
            ObservableCollection<StudentsListModel> objStudentsList = new ObservableCollection<StudentsListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    StudentsListModel obj = new StudentsListModel()
                    {
                        User = new usersModel(),
                        Grade = new GradesListModel() { Course = new coursesModel() },
                        Section = new sectionsModel(),
                        Parents = new parentsModel(),
                        Session = new sessionsModel(),
                        Student_grade_session_log = new student_grade_session_logModel()
                    };
                    //Student
                    obj.id_offline = row["students.id_offline"] != DBNull.Value ? Convert.ToString(row["students.id_offline"]) : string.Empty;
                    obj.id_online = row["students.id_online"] != DBNull.Value ? Convert.ToString(row["students.id_online"]) : string.Empty;
                    obj.school_id = row["students.school_id"] != DBNull.Value ? Convert.ToString(row["students.school_id"]) : string.Empty;
                    obj.user_id = row["students.user_id"] != DBNull.Value ? Convert.ToString(row["students.user_id"]) : string.Empty;
                    obj.parent_id = row["students.parent_id"] != DBNull.Value ? Convert.ToString(row["students.parent_id"]) : string.Empty;
                    obj.enrollment_date = row["students.enrollment_date"] != DBNull.Value ? Convert.ToDateTime(row["students.enrollment_date"]) : (DateTime?)null;
                    obj.status = row["students.status"] != DBNull.Value ? Convert.ToString(row["students.status"]) : string.Empty;
                    obj.dc_number = row["students.dc_number"] != DBNull.Value ? Convert.ToString(row["students.dc_number"]) : string.Empty;
                    obj.dc_date_of_issue = row["students.dc_date_of_issue"] != DBNull.Value ? Convert.ToDateTime(row["students.dc_date_of_issue"]) : (DateTime?)null;
                    obj.created_by = row["students.created_by"] != DBNull.Value ? Convert.ToString(row["students.created_by"]) : string.Empty;
                    obj.created_on = row["students.created_on"] != DBNull.Value ? Convert.ToDateTime(row["students.created_on"]) : (DateTime?)null;
                    obj.updated_by = row["students.updated_by"] != DBNull.Value ? Convert.ToString(row["students.updated_by"]) : string.Empty;
                    obj.updated_on = row["students.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["students.updated_on"]) : (DateTime?)null;
                    obj.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToString(row["CreatedBy"]) : string.Empty;
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

                    obj.Grade.Course.id_offline = row["courses.id_offline"] != DBNull.Value ? Convert.ToString(row["courses.id_offline"]) : string.Empty;
                    obj.Grade.Course.id_online = row["courses.id_online"] != DBNull.Value ? Convert.ToString(row["courses.id_online"]) : string.Empty;
                    obj.Grade.Course.school_id = row["courses.school_id"] != DBNull.Value ? Convert.ToString(row["courses.school_id"]) : string.Empty;
                    obj.Grade.Course.name = row["courses.name"] != DBNull.Value ? Convert.ToString(row["courses.name"]) : string.Empty;
                    obj.Grade.Course.created_by = row["courses.created_by"] != DBNull.Value ? Convert.ToString(row["courses.created_by"]) : string.Empty;
                    obj.Grade.Course.created_on = row["courses.created_on"] != DBNull.Value ? Convert.ToDateTime(row["courses.created_on"]) : (DateTime?)null;
                    obj.Grade.Course.updated_by = row["courses.updated_by"] != DBNull.Value ? Convert.ToString(row["courses.updated_by"]) : string.Empty;
                    obj.Grade.Course.updated_on = row["courses.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["courses.updated_on"]) : (DateTime?)null;
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
                    //Parents
                    obj.Parents.id_offline = row["parents.id_offline"] != DBNull.Value ? Convert.ToString(row["parents.id_offline"]) : string.Empty;
                    obj.Parents.id_online = row["parents.id_online"] != DBNull.Value ? Convert.ToString(row["parents.id_online"]) : string.Empty;
                    obj.Parents.school_id = row["parents.school_id"] != DBNull.Value ? Convert.ToString(row["parents.school_id"]) : string.Empty;
                    obj.Parents.f_first_name = row["parents.f_first_name"] != DBNull.Value ? Convert.ToString(row["parents.f_first_name"]) : string.Empty;
                    obj.Parents.f_middle_name = row["parents.f_middle_name"] != DBNull.Value ? Convert.ToString(row["parents.f_middle_name"]) : string.Empty;
                    obj.Parents.f_last_name = row["parents.f_last_name"] != DBNull.Value ? Convert.ToString(row["parents.f_last_name"]) : string.Empty;
                    obj.Parents.f_full_name = row["parents.f_full_name"] != DBNull.Value ? Convert.ToString(row["parents.f_full_name"]) : string.Empty;
                    obj.Parents.f_mobile = row["parents.f_mobile"] != DBNull.Value ? Convert.ToString(row["parents.f_mobile"]) : string.Empty;
                    obj.Parents.f_phone = row["parents.f_phone"] != DBNull.Value ? Convert.ToString(row["parents.f_phone"]) : string.Empty;
                    obj.Parents.f_office = row["parents.f_office"] != DBNull.Value ? Convert.ToString(row["parents.f_office"]) : string.Empty;
                    obj.Parents.f_email = row["parents.f_email"] != DBNull.Value ? Convert.ToString(row["parents.f_email"]) : string.Empty;
                    obj.Parents.m_first_name = row["parents.m_first_name"] != DBNull.Value ? Convert.ToString(row["parents.m_first_name"]) : string.Empty;
                    obj.Parents.m_middle_name = row["parents.m_middle_name"] != DBNull.Value ? Convert.ToString(row["parents.m_middle_name"]) : string.Empty;
                    obj.Parents.m_last_name = row["parents.m_last_name"] != DBNull.Value ? Convert.ToString(row["parents.m_last_name"]) : string.Empty;
                    obj.Parents.m_full_name = row["parents.m_full_name"] != DBNull.Value ? Convert.ToString(row["parents.m_full_name"]) : string.Empty;
                    obj.Parents.m_mobile = row["parents.m_mobile"] != DBNull.Value ? Convert.ToString(row["parents.m_mobile"]) : string.Empty;
                    obj.Parents.m_phone = row["parents.m_phone"] != DBNull.Value ? Convert.ToString(row["parents.m_phone"]) : string.Empty;
                    obj.Parents.m_office = row["parents.m_office"] != DBNull.Value ? Convert.ToString(row["parents.m_office"]) : string.Empty;
                    obj.Parents.m_email = row["parents.m_email"] != DBNull.Value ? Convert.ToString(row["parents.m_email"]) : string.Empty;
                    obj.Parents.g_fullname = row["parents.g_fullname"] != DBNull.Value ? Convert.ToString(row["parents.g_fullname"]) : string.Empty;
                    obj.Parents.g_mobile = row["parents.g_mobile"] != DBNull.Value ? Convert.ToString(row["parents.g_mobile"]) : string.Empty;
                    obj.Parents.g_email = row["parents.g_email"] != DBNull.Value ? Convert.ToString(row["parents.g_email"]) : string.Empty;
                    obj.Parents.created_by = row["parents.created_by"] != DBNull.Value ? Convert.ToString(row["parents.created_by"]) : string.Empty;
                    obj.Parents.created_on = row["parents.created_on"] != DBNull.Value ? Convert.ToDateTime(row["parents.created_on"]) : (DateTime?)null;
                    obj.Parents.updated_by = row["parents.updated_by"] != DBNull.Value ? Convert.ToString(row["parents.updated_by"]) : string.Empty;
                    obj.Parents.updated_on = row["parents.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["parents.updated_on"]) : (DateTime?)null;
                    //Sessions
                    obj.Session.id_offline = row["sessions.id_offline"] != DBNull.Value ? Convert.ToString(row["sessions.id_offline"]) : string.Empty;
                    obj.Session.id_online = row["sessions.id_online"] != DBNull.Value ? Convert.ToString(row["sessions.id_online"]) : string.Empty;
                    obj.Session.school_id = row["sessions.school_id"] != DBNull.Value ? Convert.ToString(row["sessions.school_id"]) : string.Empty;
                    obj.Session.name = row["sessions.name"] != DBNull.Value ? Convert.ToString(row["sessions.name"]) : string.Empty;
                    obj.Session.order = row["sessions.order"] != DBNull.Value ? Convert.ToString(row["sessions.order"]) : string.Empty;
                    obj.Session.from_date = row["sessions.from_date"] != DBNull.Value ? Convert.ToDateTime(row["sessions.from_date"]) : (DateTime?)null;
                    obj.Session.to_date = row["sessions.to_date"] != DBNull.Value ? Convert.ToDateTime(row["sessions.to_date"]) : (DateTime?)null;
                    obj.Session.is_active = row["sessions.is_active"] != DBNull.Value ? Convert.ToBoolean(row["sessions.is_active"]) : false;
                    obj.Session.created_by = row["sessions.created_by"] != DBNull.Value ? Convert.ToString(row["sessions.created_by"]) : string.Empty;
                    obj.Session.created_on = row["sessions.created_on"] != DBNull.Value ? Convert.ToDateTime(row["sessions.created_on"]) : (DateTime?)null;
                    obj.Session.updated_by = row["sessions.updated_by"] != DBNull.Value ? Convert.ToString(row["sessions.updated_by"]) : string.Empty;
                    obj.Session.updated_on = row["sessions.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["sessions.updated_on"]) : (DateTime?)null;
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

                    objStudentsList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objStudentsList;
        }

        public static List<StudentsListModel> GetStudentsList(StudentsListFiltersModel StudentsListFilters)
        {
            try
            {
                DataTable objGradesDatatable = GradesSetupManager.MapGradesObjectToDataTable(StudentsListFilters.Grades);
                DataTable objSectionsDatatable = SectionsSetupManager.MapSectionsObjectToDataTable(StudentsListFilters.Sections);
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo", SqlDbType = SqlDbType.NVarChar, Value = 1},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = Int64.MaxValue},
                    new SqlParameter() {ParameterName = "@GradesModel",  TypeName = DBTableTypes.grades, Value = objGradesDatatable},
                    new SqlParameter() {ParameterName = "@SectionsModel",  TypeName = DBTableTypes.sections, Value = objSectionsDatatable},
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentsList, lstSqlParameters);
                return MapDatatableToStudentsList(objDatable);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        private static List<StudentsListModel> MapDatatableToStudentsList(DataTable objDatatable)
        {
            List<StudentsListModel> objStudentsList = new List<StudentsListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    StudentsListModel obj = new StudentsListModel()
                    {
                        User = new usersModel(),
                        Grade = new GradesListModel() { Course = new coursesModel() },
                        Section = new sectionsModel(),
                        Parents = new parentsModel(),
                        Session = new sessionsModel(),
                        Student_grade_session_log = new student_grade_session_logModel()
                    };
                    //Student
                    obj.id_offline = row["students.id_offline"] != DBNull.Value ? Convert.ToString(row["students.id_offline"]) : string.Empty;
                    obj.id_online = row["students.id_online"] != DBNull.Value ? Convert.ToString(row["students.id_online"]) : string.Empty;
                    obj.school_id = row["students.school_id"] != DBNull.Value ? Convert.ToString(row["students.school_id"]) : string.Empty;
                    obj.user_id = row["students.user_id"] != DBNull.Value ? Convert.ToString(row["students.user_id"]) : string.Empty;
                    obj.parent_id = row["students.parent_id"] != DBNull.Value ? Convert.ToString(row["students.parent_id"]) : string.Empty;
                    obj.enrollment_date = row["students.enrollment_date"] != DBNull.Value ? Convert.ToDateTime(row["students.enrollment_date"]) : (DateTime?)null;
                    obj.status = row["students.status"] != DBNull.Value ? Convert.ToString(row["students.status"]) : string.Empty;
                    obj.dc_number = row["students.dc_number"] != DBNull.Value ? Convert.ToString(row["students.dc_number"]) : string.Empty;
                    obj.dc_date_of_issue = row["students.dc_date_of_issue"] != DBNull.Value ? Convert.ToDateTime(row["students.dc_date_of_issue"]) : (DateTime?)null;
                    obj.created_by = row["students.created_by"] != DBNull.Value ? Convert.ToString(row["students.created_by"]) : string.Empty;
                    obj.created_on = row["students.created_on"] != DBNull.Value ? Convert.ToDateTime(row["students.created_on"]) : (DateTime?)null;
                    obj.updated_by = row["students.updated_by"] != DBNull.Value ? Convert.ToString(row["students.updated_by"]) : string.Empty;
                    obj.updated_on = row["students.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["students.updated_on"]) : (DateTime?)null;
                    obj.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToString(row["CreatedBy"]) : string.Empty;
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

                    obj.Grade.Course.id_offline = row["courses.id_offline"] != DBNull.Value ? Convert.ToString(row["courses.id_offline"]) : string.Empty;
                    obj.Grade.Course.id_online = row["courses.id_online"] != DBNull.Value ? Convert.ToString(row["courses.id_online"]) : string.Empty;
                    obj.Grade.Course.school_id = row["courses.school_id"] != DBNull.Value ? Convert.ToString(row["courses.school_id"]) : string.Empty;                    
                    obj.Grade.Course.name = row["courses.name"] != DBNull.Value ? Convert.ToString(row["courses.name"]) : string.Empty;                    
                    obj.Grade.Course.created_by = row["courses.created_by"] != DBNull.Value ? Convert.ToString(row["courses.created_by"]) : string.Empty;
                    obj.Grade.Course.created_on = row["courses.created_on"] != DBNull.Value ? Convert.ToDateTime(row["courses.created_on"]) : (DateTime?)null;
                    obj.Grade.Course.updated_by = row["courses.updated_by"] != DBNull.Value ? Convert.ToString(row["courses.updated_by"]) : string.Empty;
                    obj.Grade.Course.updated_on = row["courses.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["courses.updated_on"]) : (DateTime?)null;
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
                    //Parents
                    obj.Parents.id_offline = row["parents.id_offline"] != DBNull.Value ? Convert.ToString(row["parents.id_offline"]) : string.Empty;
                    obj.Parents.id_online = row["parents.id_online"] != DBNull.Value ? Convert.ToString(row["parents.id_online"]) : string.Empty;
                    obj.Parents.school_id = row["parents.school_id"] != DBNull.Value ? Convert.ToString(row["parents.school_id"]) : string.Empty;
                    obj.Parents.f_first_name = row["parents.f_first_name"] != DBNull.Value ? Convert.ToString(row["parents.f_first_name"]) : string.Empty;
                    obj.Parents.f_middle_name = row["parents.f_middle_name"] != DBNull.Value ? Convert.ToString(row["parents.f_middle_name"]) : string.Empty;
                    obj.Parents.f_last_name = row["parents.f_last_name"] != DBNull.Value ? Convert.ToString(row["parents.f_last_name"]) : string.Empty;
                    obj.Parents.f_full_name = row["parents.f_full_name"] != DBNull.Value ? Convert.ToString(row["parents.f_full_name"]) : string.Empty;
                    obj.Parents.f_mobile = row["parents.f_mobile"] != DBNull.Value ? Convert.ToString(row["parents.f_mobile"]) : string.Empty;
                    obj.Parents.f_phone = row["parents.f_phone"] != DBNull.Value ? Convert.ToString(row["parents.f_phone"]) : string.Empty;
                    obj.Parents.f_office = row["parents.f_office"] != DBNull.Value ? Convert.ToString(row["parents.f_office"]) : string.Empty;
                    obj.Parents.f_email = row["parents.f_email"] != DBNull.Value ? Convert.ToString(row["parents.f_email"]) : string.Empty;
                    obj.Parents.m_first_name = row["parents.m_first_name"] != DBNull.Value ? Convert.ToString(row["parents.m_first_name"]) : string.Empty;
                    obj.Parents.m_middle_name = row["parents.m_middle_name"] != DBNull.Value ? Convert.ToString(row["parents.m_middle_name"]) : string.Empty;
                    obj.Parents.m_last_name = row["parents.m_last_name"] != DBNull.Value ? Convert.ToString(row["parents.m_last_name"]) : string.Empty;
                    obj.Parents.m_full_name = row["parents.m_full_name"] != DBNull.Value ? Convert.ToString(row["parents.m_full_name"]) : string.Empty;
                    obj.Parents.m_mobile = row["parents.m_mobile"] != DBNull.Value ? Convert.ToString(row["parents.m_mobile"]) : string.Empty;
                    obj.Parents.m_phone = row["parents.m_phone"] != DBNull.Value ? Convert.ToString(row["parents.m_phone"]) : string.Empty;
                    obj.Parents.m_office = row["parents.m_office"] != DBNull.Value ? Convert.ToString(row["parents.m_office"]) : string.Empty;
                    obj.Parents.m_email = row["parents.m_email"] != DBNull.Value ? Convert.ToString(row["parents.m_email"]) : string.Empty;
                    obj.Parents.g_fullname = row["parents.g_fullname"] != DBNull.Value ? Convert.ToString(row["parents.g_fullname"]) : string.Empty;
                    obj.Parents.g_mobile = row["parents.g_mobile"] != DBNull.Value ? Convert.ToString(row["parents.g_mobile"]) : string.Empty;
                    obj.Parents.g_email = row["parents.g_email"] != DBNull.Value ? Convert.ToString(row["parents.g_email"]) : string.Empty;
                    obj.Parents.created_by = row["parents.created_by"] != DBNull.Value ? Convert.ToString(row["parents.created_by"]) : string.Empty;
                    obj.Parents.created_on = row["parents.created_on"] != DBNull.Value ? Convert.ToDateTime(row["parents.created_on"]) : (DateTime?)null;
                    obj.Parents.updated_by = row["parents.updated_by"] != DBNull.Value ? Convert.ToString(row["parents.updated_by"]) : string.Empty;
                    obj.Parents.updated_on = row["parents.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["parents.updated_on"]) : (DateTime?)null;
                    //Sessions
                    obj.Session.id_offline = row["sessions.id_offline"] != DBNull.Value ? Convert.ToString(row["sessions.id_offline"]) : string.Empty;
                    obj.Session.id_online = row["sessions.id_online"] != DBNull.Value ? Convert.ToString(row["sessions.id_online"]) : string.Empty;
                    obj.Session.school_id = row["sessions.school_id"] != DBNull.Value ? Convert.ToString(row["sessions.school_id"]) : string.Empty;
                    obj.Session.name = row["sessions.name"] != DBNull.Value ? Convert.ToString(row["sessions.name"]) : string.Empty;
                    obj.Session.order = row["sessions.order"] != DBNull.Value ? Convert.ToString(row["sessions.order"]) : string.Empty;
                    obj.Session.from_date = row["sessions.from_date"] != DBNull.Value ? Convert.ToDateTime(row["sessions.from_date"]) : (DateTime?)null;
                    obj.Session.to_date = row["sessions.to_date"] != DBNull.Value ? Convert.ToDateTime(row["sessions.to_date"]) : (DateTime?)null;
                    obj.Session.is_active = row["sessions.is_active"] != DBNull.Value ? Convert.ToBoolean(row["sessions.is_active"]) : false;
                    obj.Session.created_by = row["sessions.created_by"] != DBNull.Value ? Convert.ToString(row["sessions.created_by"]) : string.Empty;
                    obj.Session.created_on = row["sessions.created_on"] != DBNull.Value ? Convert.ToDateTime(row["sessions.created_on"]) : (DateTime?)null;
                    obj.Session.updated_by = row["sessions.updated_by"] != DBNull.Value ? Convert.ToString(row["sessions.updated_by"]) : string.Empty;
                    obj.Session.updated_on = row["sessions.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["sessions.updated_on"]) : (DateTime?)null;
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

                    objStudentsList.Add(obj);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objStudentsList;
        }

        #endregion

        #region view
        public static Boolean CreateOrModfiyStudents(StudentsListModel objStudents, LoginModel CurrentLogin, SchoolModel SchoolInfo, SessionsListModel CurrentSession)
        {
            Boolean IsSuccess = false;
            try
            {
                if (objStudents.id_offline == null) // new student
                {
                    objStudents.id_offline = Guid.NewGuid().ToString();
                    objStudents.id_online = Guid.Empty.ToString();
                    objStudents.created_by = CurrentLogin.User.id_offline;                    
                    objStudents.created_on = DateTime.Now;                                       
                    objStudents.trip_stop_id = Guid.Empty.ToString();
                    objStudents.school_id = SchoolInfo.id_offline;
                    objStudents.user_id = Guid.Empty.ToString();
                    objStudents.parent_id = Guid.Empty.ToString();
                    if (objStudents.User.full_name != null)
                    {
                        objStudents.User.first_name = objStudents.User.full_name.Split(' ')[0];
                        objStudents.User.last_name = objStudents.User.full_name.Split(' ').Length > 1 ? objStudents.User.full_name.Split(' ')[1] : string.Empty;
                    }
                }                
                objStudents.updated_by = CurrentLogin.User.id_offline;
                objStudents.updated_on = DateTime.Now;
                objStudents.status = objStudents.Status.id;

                if (objStudents.Parents.f_full_name != null)
                {
                    objStudents.Parents.f_first_name = objStudents.Parents.f_full_name.Split(' ')[0] ;
                    objStudents.Parents.f_last_name = objStudents.Parents.f_full_name.Split(' ').Length > 1 ? objStudents.Parents.f_full_name.Split(' ')[1] : string.Empty;
                }
                if (objStudents.Parents.m_full_name != null)
                {
                    objStudents.Parents.m_first_name = objStudents.Parents.m_full_name.Split(' ')[0];
                    objStudents.Parents.m_last_name = objStudents.Parents.m_full_name.Split(' ').Length > 1 ? objStudents.Parents.m_full_name.Split(' ')[1] : string.Empty;
                }
                objStudents.Parents.updated_on = DateTime.Now;
                objStudents.Parents.updated_by = CurrentLogin.User.id_offline;

                objStudents.User.blood_group = objStudents.BloodGroup.id;
                objStudents.User.gender = objStudents.Gender.id;
                objStudents.User.updated_by = CurrentLogin.User.id_offline;
                objStudents.User.updated_on = DateTime.Now;

                objStudents.Student_grade_session_log.student_id = objStudents.id_offline;
                objStudents.Student_grade_session_log.sgsl_status = "active";
                objStudents.Student_grade_session_log.updated_by = CurrentLogin.User.id_offline;
                objStudents.Student_grade_session_log.updated_on = DateTime.Now;
                objStudents.Student_grade_session_log.section_id = objStudents.Section.id_offline;
                objStudents.Student_grade_session_log.grade_id = objStudents.Grade.id_offline;      

                DataTable objStudentDatatable = MapStudentsListObjectToDataTable(objStudents);
                DataTable objUserDatatable = UsersManager.MapUsersObjectToDataTable(objStudents.User);
                DataTable objStudent_grade_session_logDatatable = MapStudent_grade_session_logToDataTable(objStudents.Student_grade_session_log);
                DataTable objParentsDatatable = ParentsManager.MapParentsToDataTable(objStudents.Parents);

                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@StudentModel", SqlDbType = SqlDbType.Structured, TypeName = DBTableTypes.students, Value = objStudentDatatable},
                    new SqlParameter() {ParameterName = "@UserModel",  SqlDbType = SqlDbType.Structured, TypeName = DBTableTypes.users, Value = objUserDatatable},
                    new SqlParameter() {ParameterName = "@ParentModel",  SqlDbType = SqlDbType.Structured, TypeName = DBTableTypes.parents, Value = objParentsDatatable},
                    new SqlParameter() {ParameterName = "@Student_grade_session_logModel",  SqlDbType = SqlDbType.Structured, TypeName = DBTableTypes.student_grade_session_log, Value = objStudent_grade_session_logDatatable},
                };
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.CreateOrModifyStudents, lstSqlParameters);
                
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

        private static DataTable MapStudentsListObjectToDataTable(StudentsListModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("user_id", typeof(string));
                table.Columns.Add("parent_id", typeof(string));
                table.Columns.Add("trip_stop_id", typeof(string));
                table.Columns.Add("enrollment_date", typeof(DateTime));
                table.Columns.Add("status", typeof(string));
                table.Columns.Add("dc_number", typeof(string));
                table.Columns.Add("dc_date_of_issue", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.user_id,
                                obj.parent_id,
                                obj.trip_stop_id,
                                obj.enrollment_date,
                                obj.status,
                                obj.dc_number,
                                obj.dc_date_of_issue,
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

        private static DataTable MapStudent_grade_session_logToDataTable(student_grade_session_logModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("student_id", typeof(string));
                table.Columns.Add("registration_id", typeof(string));
                table.Columns.Add("grade_id", typeof(string));
                table.Columns.Add("section_id", typeof(string));
                table.Columns.Add("roll_number", typeof(string));
                table.Columns.Add("exam_roll_number", typeof(string));
                table.Columns.Add("session_id", typeof(string));
                table.Columns.Add("sgsl_status", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.student_id,
                                obj.registration_id,
                                obj.grade_id,
                                obj.section_id,
                                obj.roll_number,
                                obj.exam_roll_number,
                                obj.session_id,
                                obj.sgsl_status,
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
