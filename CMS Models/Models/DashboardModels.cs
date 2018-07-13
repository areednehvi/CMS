using CMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SMS_Models.Models
{
    public class DashboardModel : NotifyPropertyChanged
    {
        public StudentGenderRatioWidgetModel StudentGenderRatioWidget;
        public StudentPaymentAsPerMonthWidgetModel StudentPaymentAsPerMonthWidget;
        private GeneralInfoWidgetModel _GeneralInfoWidget;
        private ObservableCollection<StudentCountAsPerCourseModel> _StudentCountAsPerCourseList;
        private StudentCountAsPerCourseModel _SelectedItemInStudentCountAsPerCourseList;
        private View _View;
        private string _WidgetsVisibility;
        private string _DrillDownViewVisibility;
        public GeneralInfoWidgetModel GeneralInfoWidget
        {
            get { return _GeneralInfoWidget; }
            set
            {
                _GeneralInfoWidget = value;
                OnPropertyChanged("GeneralInfoWidget");
            }
        }
        public ObservableCollection<StudentCountAsPerCourseModel> StudentCountAsPerCourseList
        {
            get
            {
                return _StudentCountAsPerCourseList;
            }
            set
            {
                _StudentCountAsPerCourseList = value;
                OnPropertyChanged("StudentCountAsPerCourseList");
            }
        }
        public StudentCountAsPerCourseModel SelectedItemInStudentCountAsPerCourseList
        {
            get
            {
                return _SelectedItemInStudentCountAsPerCourseList;
            }
            set
            {
                _SelectedItemInStudentCountAsPerCourseList = value;
                OnPropertyChanged("SelectedItemInStudentCountAsPerCourseList");
            }
        }

        public View SelectedView
        {
            get
            {
                return _View;
            }
            set
            {
                _View = value;
                OnPropertyChanged("SelectedView");
            }
        }
        public string WidgetsVisibility
        {
            get
            {
                return _WidgetsVisibility;
            }
            set
            {
                _WidgetsVisibility = value;
                OnPropertyChanged("WidgetsVisibility");
            }
        }
        public string DrillDownViewVisibility
        {
            get
            {
                return _DrillDownViewVisibility;
            }
            set
            {
                _DrillDownViewVisibility = value;
                OnPropertyChanged("DrillDownViewVisibility");
            }
        }
    }

    
    public class StudentGenderRatioWidgetModel : NotifyPropertyChanged
    {
        private WidgetModel _Widget;
        public WidgetModel Widget
        {
            get
            {
                return _Widget;
            }
            set
            {
                _Widget = value;
                OnPropertyChanged("Widget");
            }
        }
        public GroupBox GBStudentGenderRatioWidget;
    }
    public class StudentPaymentAsPerMonthWidgetModel : NotifyPropertyChanged
    {
        private WidgetModel _Widget;
        public WidgetModel Widget
        {
            get
            {
                return _Widget;
            }
            set
            {
                _Widget = value;
                OnPropertyChanged("Widget");
            }
        }
        public GroupBox GBStudentPaymentAsPerMonthWidget;
    }
    public class GeneralInfoWidgetModel : NotifyPropertyChanged
    {
        private Int32 _StudentCount;
        private Double _TodaysRevenue;
        public Int32 StudentCount
        {
            get
            { return _StudentCount; }
            set
            {
                _StudentCount = value;
                OnPropertyChanged("StudentCount");
            }
        }
        public Double TodaysRevenue
        {
            get
            {
                return _TodaysRevenue;
            }
            set
            {
                _TodaysRevenue = value;
                OnPropertyChanged("TodaysRevenue");
            }
        }
    }
    public class Keyvalue : NotifyPropertyChanged
    {
        private string _Key;
        public string Key
        {
            get { return _Key; }
            set
            {
                _Key = value;
                OnPropertyChanged("Key");
            }
        }

        private Int64 _Value;
        public Int64 Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged("Value");
            }
        }
    }
    public class WidgetModel : NotifyPropertyChanged
    {
        private List<Keyvalue> _DataList;
        public List<Keyvalue> DataList
        {
            get { return _DataList; }
            set
            {
                _DataList = value;
                OnPropertyChanged("DataList");
            }
        }
    }

    public class StudentCountAsPerCourseModel : NotifyPropertyChanged
    {       
        public string Course { get; set; }
        public Int64 StudentCount { get; set; }
        public Guid GradeID { get; set; }
    }
}
