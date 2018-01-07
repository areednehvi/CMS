using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using static SMS_Models.Models.DBModels;

namespace CMS.Models
{
    public class TransactionsReportModel : NotifyPropertyChanged
    {
        private TransactionsReporFiltersModel _TransactionsReportFilters;
        private ObservableCollection<TransactionsReportListModel> _TransactionsReportList;
        private LoginModel _CurrentLogin;
        private SchoolModel _SchoolInfo;
        private SessionsListModel _CurrentSession;
        private string _PageNo;
        private string _NoRecordsFound;

        public int NoOfRecords{get; set;}
        public int fromRowNo { get; set; }
        public int pageNo { get; set; }
        public int NoOfRecordsPerPage { get; set; }
        public int toRowNo { get; set; }

        public TransactionsReporFiltersModel TransactionsReportFilters
        {
            get { return _TransactionsReportFilters; }
            set
            {
                _TransactionsReportFilters = value;
                OnPropertyChanged("TransactionsReportFilters");
            }
        }
        public ObservableCollection<TransactionsReportListModel> TransactionsReportList
        {
            get
            {
                return _TransactionsReportList;
            }
            set
            {
                _TransactionsReportList = value;
                OnPropertyChanged("TransactionsReportList");
            }
        }
        public string PageNo
        {
            get
            {
                return _PageNo;
            }
            set
            {
                _PageNo = value;
                OnPropertyChanged("PageNo");
            }
        }
        public string NoRecordsFound
        {
            get
            {
                return _NoRecordsFound;
            }
            set
            {
                _NoRecordsFound = value;
                OnPropertyChanged("NoRecordsFound");
            }
        }

        public LoginModel CurrentLogin
        {
            get
            {
                return _CurrentLogin;
            }
            set
            {
                _CurrentLogin = value;
            }
        }
        public SchoolModel SchoolInfo
        {
            get
            {
                return _SchoolInfo;
            }
            set
            {
                _SchoolInfo = value;
            }
        }
        public SessionsListModel CurrentSession
        {
            get
            {
                return _CurrentSession;
            }
            set
            {
                _CurrentSession = value;
            }
        }

        
    }

    public class TransactionsReportListModel : NotifyPropertyChanged 
    {
        private usersModel _User;
        private studentsModel _Student;
        private sectionsModel _Section;
        private gradesModel _Grade;
        private student_paymentsModel _Student_payment;
        private student_feesModel _Student_fees;
        private fee_categoriesModel _Fee_categories;
        private student_grade_session_logModel _Student_grade_session_log;

        public usersModel User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                OnPropertyChanged("User");
            }
        }
        public student_grade_session_logModel Student_grade_session_log
        {
            get
            {
                return _Student_grade_session_log;
            }
            set
            {
                _Student_grade_session_log = value;
                OnPropertyChanged("Student_grade_session_log");
            }
        }
        public fee_categoriesModel Fee_categories
        {
            get { return _Fee_categories; }
            set
            {
                _Fee_categories = value;
                OnPropertyChanged("Fee_categories");
            }
        }
        public gradesModel Grade
        {
            get
            {
                return _Grade;
            }
            set
            {
                _Grade = value;
                OnPropertyChanged("Grade");
            }
        }
        public sectionsModel Section
        {
            get
            {
                return _Section;
            }
            set
            {
                _Section = value;
                OnPropertyChanged("Section");
            }
        }
        public student_feesModel Student_fees
        {
            get
            {
                return _Student_fees;
            }
            set
            {
                _Student_fees = value;
                OnPropertyChanged("Student_fees");
            }
        }
        public student_paymentsModel Student_payment
        {
            get
            {
                return _Student_payment;
            }
            set
            {
                _Student_payment = value;
                OnPropertyChanged("Student_payments");
            }
        }
        public studentsModel Student
        {
            get
            {
                return _Student;
            }
            set
            {
                _Student = value;
                OnPropertyChanged("Student");
            }
        }

    }

    public class TransactionsReporFiltersModel: NotifyPropertyChanged
    {
        private gradesModel _Grade;
        private sectionsModel _Section;
        private string _rollNumber;
        private string _receiptNumber;
        private string _registrationID;
        private DateTime _fromDate;
        private DateTime _toDate;
        private List<gradesModel> _Grades;
        private List<sectionsModel> _Sections;
        private List<gradesModel> _objGradesModelList;
        private List<sectionsModel> _objSectionModelList;

        public List<gradesModel> Grades
        {
            get
            {
                return _Grades;
            }
            set
            {
                _Grades = value;
                OnPropertyChanged("Grades");
            }
        }
        public List<sectionsModel> Sections
        {
            get
            {
                return _Sections;
            }
            set
            {
                _Sections = value;
                OnPropertyChanged("Sections");
            }

        }
        public string RollNumber
        {
            get
            {
                return _rollNumber;
            }
            set
            {
                _rollNumber = value;
                OnPropertyChanged("RollNumber");
            }

        }
        public string ReceiptNumber
        {
            get
            {
                return _receiptNumber;
            }
            set
            {
                _receiptNumber = value;
                OnPropertyChanged("ReceiptNumber");
            }

        }
        public string RegistrationID
        {
            get
            {
                return _registrationID;
            }
            set
            {
                _registrationID = value;
                OnPropertyChanged("RegistrationID");
            }

        }
        public DateTime FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
                OnPropertyChanged("FromDate");
            }

        }
        public DateTime ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
                OnPropertyChanged("ToDate");
            }
        }
        public gradesModel Grade
        {
            get
            {
                return _Grade;
            }
            set
            {
                _Grade = value;
                OnPropertyChanged("Grade");
            }
        }
        public sectionsModel Section
        {
            get
            {
                return _Section;
            }
            set
            {
                _Section = value;
                OnPropertyChanged("Section");
            }

        }
        public List<gradesModel> GradesList
        {
            get
            {
                return _objGradesModelList;
            }
            set
            {
                _objGradesModelList = value;
                OnPropertyChanged("GradesList");
            }
        }
        public List<sectionsModel> SectionsList
        {
            get
            {
                return _objSectionModelList;
            }
            set
            {
                _objSectionModelList = value;
                OnPropertyChanged("SectionsList");
            }
        }
    }



}
