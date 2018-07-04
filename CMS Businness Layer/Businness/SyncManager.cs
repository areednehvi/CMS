using CMS.Models;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMS_Models.Models.DBModels;

namespace SMS_Businness_Layer.Businness
{
    public class SyncManager
    {

        #region Get Data from Online
        public static List<fee_categoriesModel> GetFeeCategoriesFromOnline()
        {
            List<fee_categoriesModel> lstFeeCategories = new List<fee_categoriesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    fee_categoriesModel objfee_categories = new fee_categoriesModel();
                    objfee_categories.id_offline =  (i).ToString();
                    lstFeeCategories.Add(objfee_categories);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstFeeCategories;
        }
        public static List<feesModel> GetFeesFromOnline()
        {
            List<feesModel> lstFees = new List<feesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    feesModel objfees = new feesModel();
                    objfees.id_offline =  (i).ToString();
                    lstFees.Add(objfees);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstFees;
        }
        public static List<filesModel> GetFilesFromOnline()
        {
            List<filesModel> lstFiles = new List<filesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    filesModel objfiles = new filesModel();
                    objfiles.id_offline =  (i).ToString();
                    lstFiles.Add(objfiles);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstFiles;
        }
        public static List<grade_feesModel> GetGradeFeesFromOnline()
        {
            List<grade_feesModel> lstgrade_fees = new List<grade_feesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    grade_feesModel objgrade_fees = new grade_feesModel();
                    objgrade_fees.id_offline =  (i).ToString();
                    lstgrade_fees.Add(objgrade_fees);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstgrade_fees;
        }
        public static List<gradesModel> GetGradesFromOnline()
        {
            List<gradesModel> lstgrades = new List<gradesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    gradesModel objgrades = new gradesModel();
                    objgrades.id_offline =  (i).ToString();
                    lstgrades.Add(objgrades);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstgrades;
        }
        public static List<parentsModel> GetParentsFromOnline()
        {
            List<parentsModel> lstparents = new List<parentsModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    parentsModel objparents = new parentsModel();
                    objparents.id_offline =  (i).ToString();
                    lstparents.Add(objparents);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstparents;
        }
        public static List<route_stopsModel> GetRouteStopsFromOnline()
        {
            List<route_stopsModel> lstroute_stops = new List<route_stopsModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    route_stopsModel objroute_stops = new route_stopsModel();
                    objroute_stops.id_offline =  (i).ToString();
                    lstroute_stops.Add(objroute_stops);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstroute_stops;
        }
        public static List<route_vehicle_stopsModel> GetRouteVehicleStopsFromOnline()
        {
            List<route_vehicle_stopsModel> lstroute_vehicle_stops = new List<route_vehicle_stopsModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    route_vehicle_stopsModel objroute_vehicle_stops = new route_vehicle_stopsModel();
                    objroute_vehicle_stops.id_offline =  (i).ToString();
                    lstroute_vehicle_stops.Add(objroute_vehicle_stops);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstroute_vehicle_stops;
        }
        public static List<route_vehicle_stops_fee_logsModel> GetRouteVehicleStopsFeeLogsFromOnline()
        {
            List<route_vehicle_stops_fee_logsModel> lstroute_vehicle_stops_fee_logs = new List<route_vehicle_stops_fee_logsModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    route_vehicle_stops_fee_logsModel objroute_vehicle_stops_fee_log = new route_vehicle_stops_fee_logsModel();
                    objroute_vehicle_stops_fee_log.id_offline =  (i).ToString();
                    lstroute_vehicle_stops_fee_logs.Add(objroute_vehicle_stops_fee_log);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstroute_vehicle_stops_fee_logs;
        }
        public static List<route_vehiclesModel> GetRouteVehiclesFromOnline()
        {
            List<route_vehiclesModel> lstroute_vehicles = new List<route_vehiclesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    route_vehiclesModel objroute_vehicles = new route_vehiclesModel();
                    objroute_vehicles.id_offline =  (i).ToString();
                    lstroute_vehicles.Add(objroute_vehicles);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstroute_vehicles;
        }
        public static List<routesModel> GetRoutesFromOnline()
        {
            List<routesModel> lstroutes = new List<routesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    routesModel objroutes = new routesModel();
                    objroutes.id_offline =  (i).ToString();
                    lstroutes.Add(objroutes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstroutes;
        }
        public static List<sectionsModel> GetSectionsFromOnline()
        {
            List<sectionsModel> lstsections = new List<sectionsModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    sectionsModel objsections = new sectionsModel();
                    objsections.id_offline =  (i).ToString();
                    lstsections.Add(objsections);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstsections;
        }
        public static List<sessionsModel> GetSessionsFromOnline()
        {
            List<sessionsModel> lstsessions = new List<sessionsModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    sessionsModel objsessions = new sessionsModel();
                    objsessions.id_offline =  (i).ToString();
                    lstsessions.Add(objsessions);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstsessions;
        }
        public static List<student_feesModel> GetStudentFeesFromOnline()
        {
            List<student_feesModel> lststudent_fees = new List<student_feesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    student_feesModel objstudent_fees = new student_feesModel();
                    objstudent_fees.id_offline =  (i).ToString();
                    lststudent_fees.Add(objstudent_fees);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lststudent_fees;
        }
        public static List<student_grade_session_logModel> GetStudentGradeSessionLogFromOnline()
        {
            List<student_grade_session_logModel> lststudent_grade_session_log = new List<student_grade_session_logModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    student_grade_session_logModel objstudent_grade_session_log = new student_grade_session_logModel();
                    objstudent_grade_session_log.id_offline =  (i).ToString();
                    lststudent_grade_session_log.Add(objstudent_grade_session_log);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lststudent_grade_session_log;
        }
        public static List<student_paymentsModel> GetStudentPaymentsFromOnline()
        {
            List<student_paymentsModel> lststudent_payments = new List<student_paymentsModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    student_paymentsModel objstudent_payments = new student_paymentsModel();
                    objstudent_payments.id_offline =  (i).ToString();
                    lststudent_payments.Add(objstudent_payments);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lststudent_payments;
        }
        public static List<studentsModel> GetStudentsFromOnline()
        {
            List<studentsModel> lststudents = new List<studentsModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    studentsModel objstudents = new studentsModel();
                    objstudents.id_offline =  (i).ToString();
                    lststudents.Add(objstudents);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lststudents;
        }
        public static List<trip_stopsModel> GetTripStopsFromOnline()
        {
            List<trip_stopsModel> lsttrip_stops = new List<trip_stopsModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    trip_stopsModel objtrip_stops = new trip_stopsModel();
                    objtrip_stops.id_offline =  (i).ToString();
                    lsttrip_stops.Add(objtrip_stops);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lsttrip_stops;
        }
        public static List<user_avatar_filesModel> GetUserAvatarFilesFromOnline()
        {
            List<user_avatar_filesModel> lstuser_avatar_files = new List<user_avatar_filesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    user_avatar_filesModel objuser_avatar_files = new user_avatar_filesModel();
                    objuser_avatar_files.id_offline =  (i).ToString();
                    lstuser_avatar_files.Add(objuser_avatar_files);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstuser_avatar_files;
        }
        public static List<usersModel> GetUsersFromOnline()
        {
            List<usersModel> lstUsers = new List<usersModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    usersModel objUser = new usersModel();
                    objUser.id_offline =  (i).ToString();
                    lstUsers.Add(objUser);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstUsers;
        }
        public static List<vehiclesModel> GetVehiclesFromOnline()
        {
            List<vehiclesModel> lstvehicles = new List<vehiclesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    vehiclesModel objvehicles = new vehiclesModel();
                    objvehicles.id_offline =  (i).ToString();
                    lstvehicles.Add(objvehicles);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstvehicles;
        }
        #endregion

        #region Get Data from Offline        
        public static List<student_feesModel> GetStudentFeesFromOffline()
        {
            List<student_feesModel> lststudent_fees = new List<student_feesModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    student_feesModel objstudent_fees = new student_feesModel();
                    objstudent_fees.id_offline =  (i).ToString();
                    lststudent_fees.Add(objstudent_fees);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lststudent_fees;
        }
        public static List<student_paymentsModel> GetStudentPaymentsFromOffline()
        {
            List<student_paymentsModel> lststudent_payments = new List<student_paymentsModel>();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    student_paymentsModel objstudent_payments = new student_paymentsModel();
                    objstudent_payments.id_offline =  (i).ToString();
                    lststudent_payments.Add(objstudent_payments);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lststudent_payments;
        }
        #endregion

        #region Sync Data

        #region 1. Users
        public static Boolean SyncUsersFromOnline(usersModel objUsers)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapUsersToDataTable(objUsers);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.users;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncUsers, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }

        private static DataTable MapUsersToDataTable(usersModel obj)
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
                table.Columns.Add("is_active", typeof(Boolean));
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

        public static Boolean SyncUser_avatar_filesFromOnline(user_avatar_filesModel objUser_avatar_files)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapUser_avatar_filesToDataTable(objUser_avatar_files);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.user_avatar_files;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncUser_avatar_files, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }

        private static DataTable MapUser_avatar_filesToDataTable(user_avatar_filesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("user_id", typeof(string));
                table.Columns.Add("applicant_id", typeof(string));
                table.Columns.Add("file_id", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.user_id,
                                obj.applicant_id,
                                obj.file_id
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

        public static Boolean SyncFilesFromOnline(filesModel objFiles)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapFilesToDataTable(objFiles);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.files;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncFiles, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }

        private static DataTable MapFilesToDataTable(filesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("extension", typeof(string));
                table.Columns.Add("size", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("owner_user_id", typeof(string));
                table.Columns.Add("created", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.name,
                                obj.extension,
                                obj.size,
                                obj.school_id,
                                obj.owner_user_id,
                                obj.created                             );
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

        #region 2. Students
        public static Boolean SyncStudentsFromOnline(studentsModel objStudents)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapStudentsToDataTable(objStudents);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.students;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncStudents, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapStudentsToDataTable(studentsModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
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

        public static Boolean SyncParentsFromOnline(parentsModel objParents)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapParentsToDataTable(objParents);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.parents;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncParents, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapParentsToDataTable(parentsModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("f_first_name", typeof(string));
                table.Columns.Add("f_middle_name", typeof(string));
                table.Columns.Add("f_last_name", typeof(string));
                table.Columns.Add("f_mobile", typeof(string));
                table.Columns.Add("f_phone", typeof(string));
                table.Columns.Add("f_office", typeof(string));
                table.Columns.Add("f_email", typeof(string));
                table.Columns.Add("m_first_name", typeof(string));
                table.Columns.Add("m_middle_name", typeof(string));
                table.Columns.Add("m_last_name", typeof(string));
                table.Columns.Add("m_mobile", typeof(string));
                table.Columns.Add("m_phone", typeof(string));
                table.Columns.Add("m_office", typeof(string));
                table.Columns.Add("m_email", typeof(string));
                table.Columns.Add("g_fullname", typeof(string));
                table.Columns.Add("g_mobile", typeof(string));
                table.Columns.Add("g_email", typeof(string));

                table.Rows.Add(
								obj.id_offline,
								obj.school_id,
								obj.f_first_name,
								obj.f_middle_name,
								obj.f_last_name,
								obj.f_mobile,
								obj.f_phone,
								obj.f_office,
								obj.f_email,
								obj.m_first_name,
								obj.m_middle_name,
								obj.m_last_name,
								obj.m_mobile,
								obj.m_phone,
								obj.m_office,
								obj.m_email,
								obj.g_fullname,
								obj.g_mobile,
								obj.g_email
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

        #region 3. Grades
        public static Boolean SyncGradesFromOnline(gradesModel objGrades)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapGradesToDataTable(objGrades);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.grades;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncGrades, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapGradesToDataTable(gradesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("course_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("order", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.course_id,
                                obj.name,
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

        public static Boolean SyncSectionsFromOnline(sectionsModel objSections)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapSectionsToDataTable(objSections);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.sections;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncSections, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapSectionsToDataTable(sectionsModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("capacity", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.name,
                                obj.capacity,
                                obj.created_by,
                                obj.created_on

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

        public static Boolean SyncSessionsFromOnline(sessionsModel objSessions)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapSessionsToDataTable(objSessions);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.sessions;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncSessions, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapSessionsToDataTable(sessionsModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("order", typeof(string));
                table.Columns.Add("from_date", typeof(DateTime));
                table.Columns.Add("to_date", typeof(DateTime));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.name,
                                obj.order,
                                obj.from_date,
                                obj.to_date,
                                obj.created_by,
                                obj.created_on

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
     
        public static Boolean SyncStudent_grade_session_logFromOnline(student_grade_session_logModel objStudent_grade_session_log)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapStudent_grade_session_logToDataTable(objStudent_grade_session_log);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.student_grade_session_log;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncStudent_grade_session_log, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapStudent_grade_session_logToDataTable(student_grade_session_logModel  obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
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

        #region 4. Transportation
        public static Boolean SyncVehiclesFromOnline(vehiclesModel objVehicles)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapVehiclesToDataTable(objVehicles);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.vehicles;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncVehicles, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapVehiclesToDataTable(vehiclesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("tracker_id", typeof(string));
                table.Columns.Add("registration_number", typeof(string));
                table.Columns.Add("bus_number", typeof(string));
                table.Columns.Add("insurance_renew_date", typeof(DateTime));
                table.Columns.Add("staff_id", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.tracker_id,
                                obj.registration_number,
                                obj.bus_number,
                                obj.insurance_renew_date,
                                obj.staff_id,
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

        public static Boolean SyncRoutesFromOnline(routesModel objRoutes)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapRoutesToDataTable(objRoutes);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.routes;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncRoutes, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapRoutesToDataTable(routesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("start_location", typeof(string));
                table.Columns.Add("end_location", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.name,
                                obj.start_location,
                                obj.end_location,
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

        public static Boolean SyncRoute_stopsFromOnline(route_stopsModel objRoute_stops)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapRoute_stopsToDataTable(objRoute_stops);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.route_stops;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncRoute_stops, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapRoute_stopsToDataTable(route_stopsModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("route_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("latitude", typeof(string));
                table.Columns.Add("longitude", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.route_id,
                                obj.name,
                                obj.latitude,
                                obj.longitude,
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

        public static Boolean SyncRoute_vehiclesFromOnline(route_vehiclesModel objRoute_vehicles)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapRoute_vehiclesToDataTable(objRoute_vehicles);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.route_vehicles;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncRoute_vehicles, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapRoute_vehiclesToDataTable(route_vehiclesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("vehicle_id", typeof(string));
                table.Columns.Add("route_id", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.vehicle_id,
                                obj.route_id

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

        public static Boolean SyncRoute_vehicle_stopsFromOnline(route_vehicle_stopsModel objRoute_vehicle_stops)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapRoute_vehicle_stopsToDataTable(objRoute_vehicle_stops);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.route_vehicle_stops;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncRoute_vehicle_stops, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapRoute_vehicle_stopsToDataTable(route_vehicle_stopsModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("route_vehicle_id", typeof(string));
                table.Columns.Add("route_stop_id", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.route_vehicle_id,
                                obj.route_stop_id
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

        public static Boolean SyncTrip_stopsFromOnline(trip_stopsModel objTrip_stops)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapTrip_stopsToDataTable(objTrip_stops);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.trip_stops;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncTrip_stops, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapTrip_stopsToDataTable(trip_stopsModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("route_vehicle_stop_id", typeof(string));
                table.Columns.Add("trip", typeof(string));
                table.Columns.Add("pick", typeof(string));
                table.Columns.Add("drop", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.route_vehicle_stop_id,
                                obj.trip,
                                obj.pick,
                                obj.drop
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

        #region 5. Fees

        public static Boolean SyncFee_categoriesFromOnline(fee_categoriesModel objFee_categories)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapFee_categoriesToDataTable(objFee_categories);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.fee_categories;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncFee_categories, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapFee_categoriesToDataTable(fee_categoriesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("recur", typeof(string));
                table.Columns.Add("is_transport", typeof(string));
                table.Columns.Add("order", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
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

        public static Boolean SyncFeesFromOnline(feesModel objFees)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapFeesToDataTable(objFees);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.fees;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncFees, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapFeesToDataTable(feesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("fee_category_id", typeof(string));
                table.Columns.Add("session_id", typeof(string));
                table.Columns.Add("amount", typeof(Double));
                table.Columns.Add("fee_cources", typeof(string));
                table.Columns.Add("last_date", typeof(DateTime));
                table.Columns.Add("last_day", typeof(string));
                table.Columns.Add("fine_per_day", typeof(Double));
                table.Columns.Add("is_allocated", typeof(string));
                table.Columns.Add("remarks", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
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

        public static Boolean SyncGrade_feesFromOnline(grade_feesModel objGrade_fees)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapGrade_feesToDataTable(objGrade_fees);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.grade_fees;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncGrade_fees, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapGrade_feesToDataTable(grade_feesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("fees_id", typeof(string));
                table.Columns.Add("grade_id", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.school_id,
                                obj.fees_id,
                                obj.grade_id,
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

        public static Boolean SyncStudent_feesFromOnline(student_feesModel objStudent_fees)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapStudent_feesToDataTable(objStudent_fees);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.student_fees;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncStudent_fees, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapStudent_feesToDataTable(student_feesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("grade_fees_id", typeof(string));
                table.Columns.Add("student_id", typeof(string));
                table.Columns.Add("route_vehicle_stops_fee_log_id", typeof(string));
                table.Columns.Add("apply_from", typeof(DateTime));
                table.Columns.Add("apply_to", typeof(DateTime));
                table.Columns.Add("fine", typeof(Double));
                table.Columns.Add("concession_amount", typeof(Double));
                table.Columns.Add("no_fine", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
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

        public static Boolean SyncRoute_vehicle_stops_fee_logsFromOnline(route_vehicle_stops_fee_logsModel objRoute_vehicle_stops_fee_logs)
        {
            Boolean IsSuccess = false;
            try
            {
                DataTable objDatatable = MapRoute_vehicle_stops_fee_logsToDataTable(objRoute_vehicle_stops_fee_logs);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.route_vehicle_stops_fee_logs;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncRoute_vehicle_stops_fee_logs, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }
        private static DataTable MapRoute_vehicle_stops_fee_logsToDataTable(route_vehicle_stops_fee_logsModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(string));
                table.Columns.Add("route_vehicle_stop_id", typeof(string));
                table.Columns.Add("fees_id", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.route_vehicle_stop_id,
                                obj.fees_id
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

        #region 6. Payments
        public static Boolean SyncStudent_paymentsFromOnline(student_paymentsModel objStudent_payments)
        {
            Boolean IsSuccess = false;
            try
            {
                objStudent_payments.id_offline = new Guid().ToString();
                objStudent_payments.school_id = "18";
                objStudent_payments.student_fees_id = "1121";
                objStudent_payments.recept_no = "ssss";
                objStudent_payments.payment_mode = "Cash";
                objStudent_payments.updated_by = "1";
                objStudent_payments.created_by = "1";
                DataTable objDatatable = MapStudent_paymentsToDataTable(objStudent_payments);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.student_payments;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.SyncStudent_payments, objSqlParameter);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IsSuccess;
        }

        private static DataTable MapStudent_paymentsToDataTable(student_paymentsModel objPaymentHistoryModel)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(Guid));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("student_fees_id", typeof(string));
                table.Columns.Add("amount", typeof(Double));
                table.Columns.Add("fine", typeof(Double));
                table.Columns.Add("comment", typeof(string));
                table.Columns.Add("recept_no", typeof(string));
                table.Columns.Add("payment_mode", typeof(string));
                table.Columns.Add("payment_date", typeof(DateTime));
                table.Columns.Add("ip", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                objPaymentHistoryModel.id_offline,
                                objPaymentHistoryModel.id_online,
                                objPaymentHistoryModel.school_id,
                                objPaymentHistoryModel.student_fees_id,
                                objPaymentHistoryModel.amount,
                                objPaymentHistoryModel.fine,
                                objPaymentHistoryModel.comment,
                                objPaymentHistoryModel.recept_no,
                                objPaymentHistoryModel.payment_mode,
                                objPaymentHistoryModel.payment_date,
                                objPaymentHistoryModel.ip,
                                objPaymentHistoryModel.created_by,
                                objPaymentHistoryModel.created_on,
                                objPaymentHistoryModel.updated_by,
                                objPaymentHistoryModel.updated_on
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

        #endregion

        #region Last Sync Info from SyncTableInfo
        public static List<SyncTableInfoModel> GetSyncTableInfo()
        {            
            try
            {
                DataTable objDataTable = DataAccess.GetDataTable(StoredProcedures.GetSyncTableInfo);
                return MapDatatableToSyncTableInfoList(objDataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        private static List<SyncTableInfoModel> MapDatatableToSyncTableInfoList(DataTable objDatatable)
        {
            List<SyncTableInfoModel> lstSyncTableInfoList = new List<SyncTableInfoModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    SyncTableInfoModel objSyncTableInfo = new SyncTableInfoModel();

                    objSyncTableInfo.id_offline = row["id_offline"] != DBNull.Value ? row["id_offline"].ToString() : string.Empty;
                    objSyncTableInfo.TableName = row["TableName"] != DBNull.Value ? row["TableName"].ToString() : string.Empty;
                    objSyncTableInfo.LastSyncedOn = row["LastSyncedOn"] != DBNull.Value ? Convert.ToDateTime(row["LastSyncedOn"]) : (DateTime?)null;

                    lstSyncTableInfoList.Add(objSyncTableInfo);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstSyncTableInfoList;
        }

        public static Boolean UpdateSyncTableInfo(List<SyncTableInfoModel> objSyncTableInfoList)
        {
            Boolean IsSuccess = false;
            try
            {
                foreach(SyncTableInfoModel objSyncTableInfo in objSyncTableInfoList)
                {
                    List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@ID",     SqlDbType = SqlDbType.NVarChar, Value = objSyncTableInfo.id_offline},
                        new SqlParameter() {ParameterName = "@LastSyncedOn",  SqlDbType = SqlDbType.NVarChar, Value = objSyncTableInfo.LastSyncedOn}
                    };
                    IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.UpdateSyncTableInfo, lstSqlParameters);
                }                
                

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


        #endregion
    }

}