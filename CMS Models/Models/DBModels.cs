using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models.Models
{
    public class DBModels
    {

        public class fee_categoriesModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string name { get; set; }
            public string recur { get; set; }
            public string is_transport { get; set; }
            public string order { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }
        }
        public class feesModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string fee_category_id { get; set; }
            public string session_id { get; set; }
            public Double amount { get; set; }
            public string fee_cources { get; set; }
            public string last_date { get; set; }
            public string last_day { get; set; }
            public Double fine_per_day { get; set; }
            public string is_allocated { get; set; }
            public string remarks { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }
        public class filesModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string name { get; set; }
            public string extension { get; set; }
            public string size { get; set; }
            public string school_id { get; set; }
            public string owner_user_id { get; set; }
            public DateTime? created { get; set; }

        }
        public class grade_feesModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string fees_id { get; set; }
            public string grade_id { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }
        }
        public class gradesModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string course_id { get; set; }
            public string name { get; set; }
            public string order { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }
        public class parentsModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string f_first_name { get; set; }
            public string f_middle_name { get; set; }
            public string f_last_name { get; set; }
            public string f_full_name { get; set; }
            public string f_mobile { get; set; }
            public string f_phone { get; set; }
            public string f_office { get; set; }
            public string f_email { get; set; }
            public string m_first_name { get; set; }
            public string m_middle_name { get; set; }
            public string m_last_name { get; set; }
            public string m_full_name { get; set; }
            public string m_mobile { get; set; }
            public string m_phone { get; set; }
            public string m_office { get; set; }
            public string m_email { get; set; }
            public string g_fullname { get; set; }
            public string g_mobile { get; set; }
            public string g_email { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }
        public class route_stopsModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string route_id { get; set; }
            public string name { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }
        public class route_vehicle_stopsModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string route_vehicle_id { get; set; }
            public string route_stop_id { get; set; }

        }
        public class route_vehicle_stops_fee_logsModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string route_vehicle_stop_id { get; set; }
            public string fees_id { get; set; }

        }
        public class route_vehiclesModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string vehicle_id { get; set; }
            public string route_id { get; set; }

        }
        public class routesModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string name { get; set; }
            public string start_location { get; set; }
            public string end_location { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }
        public class schoolsModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string database_id { get; set; }
            public string subdomain { get; set; }
            public string domain { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string theme { get; set; }
            public DateTime? created_on { get; set; }

        }
        public class sectionsModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string name { get; set; }
            public int capacity { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }
        public class sessionsModel :NotifyPropertyChanged
        {
            private Boolean _is_active;

            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string name { get; set; }
            public string order { get; set; }
            public DateTime? from_date { get; set; }
            public DateTime? to_date { get; set; }
            public Boolean is_active
            {
                get
                {
                    return _is_active;
                }
                set
                {
                    _is_active = value;
                    OnPropertyChanged("is_active");
                }
            }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

            

        }
        public class coursesModel : NotifyPropertyChanged
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string name { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }



        }
        public class student_feesModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string grade_fees_id { get; set; }
            public string student_id { get; set; }
            public string route_vehicle_stops_fee_log_id { get; set; }
            public DateTime? apply_from { get; set; }
            public DateTime? apply_to { get; set; }
            public Double fine { get; set; }
            public Double concession_amount { get; set; }
            public string no_fine { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }
        public class student_grade_session_logModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string student_id { get; set; }
            public string registration_id { get; set; }
            public string grade_id { get; set; }
            public string section_id { get; set; }
            public string roll_number { get; set; }
            public string exam_roll_number { get; set; }
            public string session_id { get; set; }
            public string sgsl_status { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }
        public class student_paymentsModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string student_fees_id { get; set; }
            public Double amount { get; set; }
            public Double fine { get; set; }
            public string comment { get; set; }
            public string recept_no { get; set; }
            public string payment_mode { get; set; }
            public DateTime? payment_date { get; set; }
            public string ip { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }
        public class studentsModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string user_id { get; set; }
            public string parent_id { get; set; }
            public string trip_stop_id { get; set; }
            public DateTime? enrollment_date { get; set; }
            public string status { get; set; }
            public string dc_number { get; set; }
            public DateTime? dc_date_of_issue { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }
        public class trip_stopsModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string route_vehicle_stop_id { get; set; }
            public string trip { get; set; }
            public string pick { get; set; }
            public string drop { get; set; }

        }
        public class SyncTableInfoModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string TableName { get; set; }
            public DateTime? LastSyncedOn { get; set; }
        }
        public class user_avatar_filesModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string user_id { get; set; }
            public string applicant_id { get; set; }
            public string file_id { get; set; }

        }
        public class usersModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string student_id { get; set; }
            public string role_id { get; set; }
            public string user_type { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string address_line_one { get; set; }
            public string address_line_two { get; set; }
            public string area { get; set; }
            public string first_name { get; set; }
            public string middle_name { get; set; }
            public string last_name { get; set; }
            public string full_name { get; set; }
            public string gender { get; set; }
            public string blood_group { get; set; }
            public string password { get; set; }
            public DateTime? birth_date { get; set; }
            public string other_phones { get; set; }
            public string default_phone_number_id { get; set; }
            public string adhaar_number { get; set; }
            public string bank_name { get; set; }
            public string bank_branch { get; set; }
            public string bank_account_number { get; set; }
            public string bank_ifsc_code { get; set; }
            public string flags { get; set; }
            public DateTime? last_login_time { get; set; }
            public string user_avatar_file_id { get; set; }
            public Boolean is_active { get; set; }
            public DateTime? created_on { get; set; }
            public string created_by { get; set; }
            public DateTime? updated_on { get; set; }
            public string updated_by { get; set; }

        }
        public class vehiclesModel
        {
            public string id_offline { get; set; }
            public string id_online { get; set; }
            public string school_id { get; set; }
            public string tracker_id { get; set; }
            public string registration_number { get; set; }
            public string bus_number { get; set; }
            public string insurance_renew_date { get; set; }
            public string staff_id { get; set; }
            public string created_by { get; set; }
            public DateTime? created_on { get; set; }
            public string updated_by { get; set; }
            public DateTime? updated_on { get; set; }

        }

    }
}
