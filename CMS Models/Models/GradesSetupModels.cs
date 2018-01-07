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
    public class GradesSetupModel :NotifyPropertyChanged
    {
        private GradesListModel _SelectedItemInGradesList;
        private GradesListModel _Grade;
        private ObservableCollection<GradesListModel> _GradesList;
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
        public ObservableCollection<GradesListModel> GradesList
        {
            get
            {
                return _GradesList;
            }
            set
            {
                _GradesList = value;
                OnPropertyChanged("GradesList");
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

        public GradesListModel SelectedItemInGradesList
        {
            get
            {
                return _SelectedItemInGradesList;
            }
            set
            {
                _SelectedItemInGradesList = value;
                OnPropertyChanged("SelectedItemInGradesList");
            }
        }
        public GradesListModel Grade
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

    public class GradesListModel : gradesModel
    {        
        public string CreatedBy { get; set; }
    }


}
