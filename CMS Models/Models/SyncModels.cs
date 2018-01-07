using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMS_Models.Models.DBModels;

namespace CMS.Models
{
    public class SyncModel :NotifyPropertyChanged
    {
        private List<SyncModule> _SyncModuleList;
        public SyncProgressModel SyncAllProgress { get; set; }
        private string _CurrentSyncModule;
        private string _SyncStatus;
        private bool _IsSyncInProgress;
        private SyncDBmodels _SyncDBModels;
        private List<SyncTableInfoModel> _SyncTableInfoList;

        public bool IsSyncInProgress
        {
            get { return _IsSyncInProgress; }
            set
            {
                _IsSyncInProgress = value;
                OnPropertyChanged("IsSyncInProgress");
                OnPropertyChanged("IsSyncNotInProgress");
            }
        }
        public string SyncStatus
        {
            get
            {
                return _SyncStatus;
            }
            set
            {
                _SyncStatus = value;
                OnPropertyChanged("SyncStatus");
            }
        }
        public string CurrentSyncModule
        {
            get
            {
                return _CurrentSyncModule;
            }
            set
            {
                _CurrentSyncModule = value;
                OnPropertyChanged("CurrentSyncModule");
            }
        }

        public bool IsSyncNotInProgress
        {
            get
            {
                return !IsSyncInProgress;
            }
        }
        public List<SyncModule> SyncModuleList
        {
            get { return _SyncModuleList; }
            set
            {
                _SyncModuleList = value;
                OnPropertyChanged("SyncModule");
            }
        }

        public SyncDBmodels SyncDBmodels
        {
            get { return _SyncDBModels; }
            set
            {
                _SyncDBModels = value;
            }
        }

        public List<SyncTableInfoModel> SyncTableInfoList
        {
            get { return _SyncTableInfoList; }
            set
            {
                _SyncTableInfoList = value;
            }
        }

        
    }

    public class SyncProgressModel :NotifyPropertyChanged
    {
        int _Progress;
        Decimal _ProgressPercentage;
        int _Minimum = 0, _Maximum = 1;

        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                _Maximum = value;
                OnPropertyChanged("Maximum");
            }
        }

        public int Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                _Minimum = value;
                OnPropertyChanged("Minimum");
            }
        }

        public Decimal ProgressPercentage
        {
            get
            {
                return _ProgressPercentage;
            }
            set
            {
                _ProgressPercentage = value;
                OnPropertyChanged("ProgressPercentage");
            }
        }

        public int Progress
        {
            get { return _Progress; }
            set
            {
                if (value <= _Maximum)
                {
                    if (value >= _Minimum)
                    {
                        _Progress = value;
                    }
                    else
                    {
                        _Progress = _Minimum;
                    }
                }
                else
                {
                    _Progress = _Maximum;
                }
                ProgressPercentage = Decimal.Round(((Decimal)_Progress / (Decimal)Maximum) * 100, 2);
                OnPropertyChanged("Progress");
            }
        }

        

    }

    public class SyncModule :NotifyPropertyChanged
    {
        private string _SyncModuleStatus;
        public string Module { get; set; }     
        public string SyncModuleStatus
        {
            get
            {
                return _SyncModuleStatus;
            }
            set
            {
                _SyncModuleStatus = value;
                OnPropertyChanged("SyncModuleStatus");
            }
        }   
        public SyncProgressModel SyncModuleProgress { get; set; }

        

    }

    public class SyncDBmodels
    {
        public List<fee_categoriesModel> fee_categoriesList { get; set; }
        public List<feesModel> feesList { get; set; }
        public List<filesModel> filesList { get; set; }
        public List<grade_feesModel> grade_feesList { get; set; }
        public List<gradesModel> gradesList { get; set; }
        public List<parentsModel> parentsList { get; set; }
        public List<route_stopsModel> route_stopsList { get; set; }
        public List<route_vehicle_stopsModel> route_vehicle_stopsList { get; set; }
        public List<route_vehicle_stops_fee_logsModel> route_vehicle_stops_fee_logsList { get; set; }
        public List<route_vehiclesModel> route_vehiclesList { get; set; }
        public List<routesModel> routesList { get; set; }
        public List<sectionsModel> sectionsList { get; set; }
        public List<sessionsModel> sessionsList { get; set; }
        public List<student_feesModel> student_feesList { get; set; }
        public List<student_grade_session_logModel> student_grade_session_logList { get; set; }
        public List<student_paymentsModel> student_paymentsList { get; set; }
        public List<studentsModel> studentsList { get; set; }
        public List<trip_stopsModel> trip_stopsList { get; set; }
        public List<user_avatar_filesModel> user_avatar_filesList { get; set; }
        public List<usersModel> usersList { get; set; }
        public List<vehiclesModel> vehiclesList { get; set; }
    }

    


}
