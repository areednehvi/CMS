using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static SMS_Models.Models.DBModels;

namespace CMS.Models
{
    public class CoursesSetupModel :NotifyPropertyChanged
    {
        private CoursesListModel _SelectedItemInCoursesList;
        private CoursesListModel _Course;
        private ObservableCollection<CoursesListModel> _CoursesList;
        private LoginModel _CurrentLogin;
        private SchoolModel _SchoolInfo;
        private string _ListVisibility;
        private string _FormVisibility;
        private string _PageNo;
        private string _NoRecordsFound;


        public int NoOfRecords{get; set;}
        public int fromRowNo { get; set; }
        public int pageNo { get; set; }
        public int NoOfRecordsPerPage { get; set; }
        public int toRowNo { get; set; }
        public ObservableCollection<CoursesListModel> CoursesList
        {
            get
            {
                return _CoursesList;
            }
            set
            {
                _CoursesList = value;
                OnPropertyChanged("CoursesList");
            }
        }
        public string ListVisibility
        {
            get
            {
                return _ListVisibility;
            }
            set
            {
                _ListVisibility = value;
                OnPropertyChanged("ListVisibility");
            }
        }
        public string FormVisibility
        {
            get
            {
                return _FormVisibility;
            }
            set
            {
                _FormVisibility = value;
                OnPropertyChanged("FormVisibility");
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

        public CoursesListModel SelectedItemInCoursesList
        {
            get
            {
                return _SelectedItemInCoursesList;
            }
            set
            {
                _SelectedItemInCoursesList = value;
                OnPropertyChanged("SelectedItemInCoursesList");
            }
        }
        public CoursesListModel Course
        {
            get
            {
                return _Course;
            }
            set
            {
                _Course = value;
                OnPropertyChanged("Course");
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

        
    }

    public class CoursesListModel : coursesModel
    {
        public string CreatedBy { get; set; }
    }


}
