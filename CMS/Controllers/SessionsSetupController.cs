using CMS.Models;
using CMS.Shared;
using SMS_Businness_Layer.Businness;
using SMS_Models.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CMS.Controllers
{
    public class SessionsSetupController :NotifyPropertyChanged
    {
        #region Fields
        private SessionsSetupModel _SessionsSetup;      

        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private ICommand _addNewSessionCommand;
        private ICommand _cancelNewSessionCommand;
        private ICommand _saveSessionsCommand;
        #endregion

        #region Constructor
        public SessionsSetupController()
        {

            _SessionsSetup = new SessionsSetupModel()
            {
                CurrentLogin = new LoginModel(),
                SchoolInfo = new SchoolModel()
            };

            //Get Global Objects
            GetGlobalObjects();

            // Get Lists
            //this.GetDropDownLists();

            //Get Settings
            this.GetSettings();
            // Set pagination
            this.ResetPagination();

            //Subscribe to Model's Property changed event
            this.SessionsSetup.PropertyChanged += (s, e) => {
                if (e.PropertyName == "SelectedItemInSessionsList")
                {
                    SessionsSetup.Session = SessionsSetup.SelectedItemInSessionsList;
                    this.ShowForm();
                }
            };

            

            //Get Initial Sessions list
            this.GetSessionsList();

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _addNewSessionCommand = new RelayCommand(AddNewSession, CanAddNewSession);
            _cancelNewSessionCommand = new RelayCommand(CancelNewSession, CanCancelNewSession);
            _saveSessionsCommand = new RelayCommand(SaveSessions, CanSaveSessions);

            this.ShowList();
        }
        
        #endregion

        #region Properties

        public SessionsSetupModel SessionsSetup
        {
            get
            {
                return _SessionsSetup;
            }
            set
            {
                _SessionsSetup = value;
                OnPropertyChanged("SessionsSetup");
            }
        }
       
        #endregion

        #region NextPageCommand
        public ICommand NextPageCommand
        {
            get { return _nextPageCommand; }        
        }

      
        public bool CanMoveToNextPage(object obj)
        {          
            return true;
        }

        public void MoveToNextPage(object obj)
        {
            try
            {
                SessionsSetup.pageNo++;
                SessionsSetup.PageNo = "Page No : " + SessionsSetup.pageNo;
                SessionsSetup.fromRowNo = SessionsSetup.toRowNo + 1;
                SessionsSetup.toRowNo = SessionsSetup.pageNo * SessionsSetup.NoOfRecordsPerPage;
                this.GetSessionsList();
                if (SessionsSetup.pageNo > 1 && SessionsSetup.SessionsList.Count == 0)
                    MoveToPreviousPage(obj);
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }
            
        }

        #endregion

        #region PreviousPageCommand

        public ICommand PreviousPageCommand
        {
            get { return _previousPageCommand; }
        }


        public bool CanMoveToPreviousPage(object obj)
        {
            return true;
        }


        public void MoveToPreviousPage(object obj)
        {
            try
            {             
                if (SessionsSetup.pageNo > 1)
                {
                    SessionsSetup.pageNo--;
                    SessionsSetup.PageNo = "Page No : " + SessionsSetup.pageNo;
                    SessionsSetup.toRowNo = SessionsSetup.fromRowNo - 1;
                    SessionsSetup.fromRowNo = (SessionsSetup.toRowNo + 1) - SessionsSetup.NoOfRecordsPerPage;
                    this.GetSessionsList();
                }

            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }
        }
        #endregion

        #region AddNewSessionCommand

        public ICommand AddNewSessionCommand
        {
            get { return _addNewSessionCommand; }
        }


        public bool CanAddNewSession(object obj)
        {
            return true;
        }


        public void AddNewSession(object obj)
        {
            try
            {
                SessionsSetup.Session = new SessionsListModel()
                {
                    is_active = true,
                    from_date = DateTime.Now,
                    to_date = DateTime.Now.AddYears(1),                   
                    CreatedBy = SessionsSetup.CurrentLogin.User.full_name,
                };
                this.ShowForm();
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }
        }
        #endregion

        #region CancelNewSessionCommand

        public ICommand CancelNewSessionCommand
        {
            get { return _cancelNewSessionCommand; }
        }


        public bool CanCancelNewSession(object obj)
        {
            return true;
        }


        public void CancelNewSession(object obj)
        {
            try
            {
                this.ShowList();
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }
        }
        #endregion

        #region SaveSessionsCommand
        public ICommand SaveSessionsCommand
        {
            get { return _saveSessionsCommand; }
        }


        public bool CanSaveSessions(object obj)
        {
            return SessionsSetup.Session != null && SessionsSetup.Session.name != null && SessionsSetup.Session.from_date != null && SessionsSetup.Session.to_date != null;                
        }

        public void SaveSessions(object obj)
        {
            try
            {
                if (SessionsSetupManager.CreateOrModfiySessions(SessionsSetup.Session, SessionsSetup.CurrentLogin, SessionsSetup.SchoolInfo))
                {
                    CreateSessionGlobalObject();

                    GeneralMethods.ShowNotification("Notification", "Session Saved Successfully");
                    this.GetSessionsList();
                    this.ShowList();
                }

            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }

        }

        #endregion      

        
        private void GetSessionsList()
        {
            try
            {
                SessionsSetup.SessionsList = SessionsSetupManager.GetSessionsList(SessionsSetup.fromRowNo, SessionsSetup.toRowNo);
                SessionsSetup.NoRecordsFound = SessionsSetup.SessionsList.Count > 0 ? "Collapsed" : "Visible";
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }

        }

        private void ShowForm()
        {
            SessionsSetup.ListVisibility = "Collapsed";
            SessionsSetup.FormVisibility = "Visible";
        }

        private void ShowList()
        {
            SessionsSetup.ListVisibility = "Visible";
            SessionsSetup.FormVisibility = "Collapsed";
        }

        private void GetGlobalObjects()
        {
            //Get the Current Login
            SessionsSetup.CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get College Info
            SessionsSetup.SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
        }
        private void CreateSessionGlobalObject()
        {
            //Maintain state of Session Info
            GeneralMethods.CreateGlobalObject(GlobalObjects.CurrentSession, SessionsSetup.Session);
        }

        private void ResetPagination()
        {
            SessionsSetup.fromRowNo = 1;
            SessionsSetup.pageNo = 1;
            SessionsSetup.PageNo = "Page No : " + SessionsSetup.pageNo;
            SessionsSetup.NoOfRecordsPerPage = SessionsSetup.NoOfRecords;
            SessionsSetup.toRowNo = SessionsSetup.pageNo * SessionsSetup.NoOfRecordsPerPage;
        }


        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            SessionsSetup.NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }


    }
}
