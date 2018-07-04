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
    public class FeeAllocationModel :NotifyPropertyChanged
    {
        private FeeAllocationListModel _SelectedItemInFeeAllocationList;
        private FeeAllocationListModel _Fees;
        private ObservableCollection<FeeAllocationListModel> _FeeAllocationList;
        private List<fee_categoriesModel> _FeeCategoriesList;
        private List<GradesListModel> _GradesList;
        private List<sectionsModel> _SectionsList;
        private List<StudentsListModel> _StudentsList;
        private List<ListModel> _FeeMonthsList;
        private List<ListModel> _AllocateFeeToList;
        private LoginModel _CurrentLogin;
        private sessionsModel _CurrentSession;
        private SchoolModel _SchoolInfo;
        private string _ListVisibility;
        private string _FormVisibility;
        private string _PageNo;
        private string _NoRecordsFound;
        private Boolean _IsStudentListEnabled;

        public int NoOfRecords{get; set;}
        public int fromRowNo { get; set; }
        public int pageNo { get; set; }
        public int NoOfRecordsPerPage { get; set; }
        public int toRowNo { get; set; } 
        public Boolean IsStudentListEnabled
        {
            get { return _IsStudentListEnabled; }
            set
            {
                _IsStudentListEnabled = value;
                OnPropertyChanged("IsStudentListEnabled");
            }
        }
        

        public ObservableCollection<FeeAllocationListModel> FeeAllocationList
        {
            get
            {
                return _FeeAllocationList;
            }
            set
            {
                _FeeAllocationList = value;
                OnPropertyChanged("FeeAllocationList");
            }
        }
        public List<fee_categoriesModel> FeeCategoriesList
        {
            get
            {
                return _FeeCategoriesList;
            }
            set
            {
                _FeeCategoriesList = value;
                OnPropertyChanged("FeeCategoriesList");
            }
        }
        public List<ListModel> AllocateFeeToList
        {
            get
            {
                return _AllocateFeeToList;
            }
            set
            {
                _AllocateFeeToList = value;
                OnPropertyChanged("AllocateFeeToList");
            }
        }
        public List<GradesListModel> GradesList
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
        public List<sectionsModel> SectionsList
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
        public List<StudentsListModel> StudentsList
        {
            get
            {
                return _StudentsList;
            }
            set
            {
                _StudentsList = value;
                OnPropertyChanged("StudentsList");
            }
        }
        public List<ListModel> FeeMonthsList
        {
            get
            {
                return _FeeMonthsList;
            }
            set
            {
                _FeeMonthsList = value;
                OnPropertyChanged("FeeMonthsList");
            }
        }
        public GradesMultiComboBox GradesMultiComboBox
        {
            get;set;
        }
        public FeeMonthsMultiComboBox FeeMonthsMultiComboBox
        {
            get; set;
        }
        public StudentsMultiComboBox StudentsMultiComboBox
        {
            get; set;
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

        public FeeAllocationListModel SelectedItemInFeeAllocationList
        {
            get
            {
                return _SelectedItemInFeeAllocationList;
            }
            set
            {
                _SelectedItemInFeeAllocationList = value;
                OnPropertyChanged("SelectedItemInFeeAllocationList");
            }
        }
        public FeeAllocationListModel Fees
        {
            get
            {
                return _Fees;
            }
            set
            {
                _Fees = value;
                OnPropertyChanged("Fees");
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
        public sessionsModel CurrentSession
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

    public class FeeAllocationListModel : feesModel , INotifyPropertyChanged
    {
        private fee_categoriesModel _FeeCategory;
        private ListModel _AllocateFeeTo;
        public fee_categoriesModel FeeCategory
        {
            get
            {
                return _FeeCategory;
            }
            set
            {
                _FeeCategory = value;
                OnPropertyChanged("FeeCategory");
            }
        }
        public ListModel AllocateFeeTo
        {
            get
            {
                return _AllocateFeeTo;
            }
            set
            {
                _AllocateFeeTo = value;
                OnPropertyChanged("AllocateFeeTo");
            }
        }
        public string GradesAppliedTo { get; set; }
        public string AppliedToGradeIDs { get; set; }
        public string CreatedBy { get; set; }
        public Int64 StudentCount { get; set; }

        #region INotify Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

    public class GradesMultiComboBoxItem : NotifyPropertyChanged
    {
        public GradesListModel Grade { get; set; }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
                    
            }
        }
        

        public GradesMultiComboBoxItem(GradesListModel _grade)
        {
            Grade = _grade;
        }

        public override string ToString()
        {
            return Grade.name;
        }
    }

    public class GradesMultiComboBox :NotifyPropertyChanged
    {
        private ObservableCollection<GradesMultiComboBoxItem> _GradesMultiComboBoxItems;
        private string _text;

        public ObservableCollection<GradesMultiComboBoxItem> GradesMultiComboBoxItems
        {
            get { return _GradesMultiComboBoxItems; }
            set
            {
                _GradesMultiComboBoxItems = value;
                OnPropertyChanged("GradesMultiComboBoxItems");
            }
        }
        public ObservableCollection<GradesMultiComboBoxItem> GradesMultiComboBoxCheckedItems
        {
            get; set;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
    }

    public class FeeMonthsMultiComboBoxItem : NotifyPropertyChanged
    {
        public ListModel FeeMonth { get; set; }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");

            }
        }


        public FeeMonthsMultiComboBoxItem(ListModel _FeeMonth)
        {
            FeeMonth = _FeeMonth;
        }

        public override string ToString()
        {
            return FeeMonth.ToString();
        }
    }

    public class FeeMonthsMultiComboBox : NotifyPropertyChanged
    {
        private ObservableCollection<FeeMonthsMultiComboBoxItem> _FeeMonthsMultiComboBoxItems;
        private ObservableCollection<FeeMonthsMultiComboBoxItem> _FeeMonthsMultiComboBoxCheckedItems;
        private string _text;

        public ObservableCollection<FeeMonthsMultiComboBoxItem> FeeMonthsMultiComboBoxItems
        {
            get { return _FeeMonthsMultiComboBoxItems; }
            set
            {
                _FeeMonthsMultiComboBoxItems = value;
                OnPropertyChanged("FeeMonthsMultiComboBoxItems");
            }
        }
        public ObservableCollection<FeeMonthsMultiComboBoxItem> FeeMonthsMultiComboBoxCheckedItems
        {
            get { return _FeeMonthsMultiComboBoxCheckedItems; }
            set
            {
                _FeeMonthsMultiComboBoxCheckedItems = value;
                OnPropertyChanged("FeeMonthsMultiComboBoxCheckedItems");
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
    }
    public class StudentsMultiComboBoxItem : NotifyPropertyChanged
    {
        public StudentsListModel Student { get; set; }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");

            }
        }

        public StudentsMultiComboBoxItem(StudentsListModel _student)
        {
            Student = _student;
        }

        public override string ToString()
        {
            return Student.User.full_name;
        }
    }
    public class StudentsMultiComboBox : NotifyPropertyChanged
    {
        private ObservableCollection<StudentsMultiComboBoxItem> _StudentsMultiComboBoxItems;
        private string _text;

        public ObservableCollection<StudentsMultiComboBoxItem> StudentsMultiComboBoxItems
        {
            get { return _StudentsMultiComboBoxItems; }
            set
            {
                _StudentsMultiComboBoxItems = value;
                OnPropertyChanged("StudentsMultiComboBoxItems");
            }
        }
        public ObservableCollection<StudentsMultiComboBoxItem> StudentsMultiComboBoxCheckedItems
        {
            get; set;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
    }



}
