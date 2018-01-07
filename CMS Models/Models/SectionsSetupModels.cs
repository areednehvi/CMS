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
    public class SectionsSetupModel :NotifyPropertyChanged
    {
        private SectionsListModel _SelectedItemInSectionsList;
        private SectionsListModel _Section;
        private ObservableCollection<SectionsListModel> _SectionsList;
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
        public ObservableCollection<SectionsListModel> SectionsList
        {
            get
            {
                return _SectionsList;
            }
            set
            {
                _SectionsList = value;
                OnPropertyChanged("SectionsList");
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

        public SectionsListModel SelectedItemInSectionsList
        {
            get
            {
                return _SelectedItemInSectionsList;
            }
            set
            {
                _SelectedItemInSectionsList = value;
                OnPropertyChanged("SelectedItemInSectionsList");
            }
        }
        public SectionsListModel Section
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

    public class SectionsListModel :sectionsModel
    {
        public string CreatedBy { get; set; }
    }


}
