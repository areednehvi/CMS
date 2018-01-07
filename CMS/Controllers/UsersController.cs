using CMS.Models;
using CMS.Shared;
using SMS_Businness_Layer.Businness;
using SMS_Models.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CMS.Controllers
{
    public class UsersController :NotifyPropertyChanged
    {
        #region Fields
        private UsersModel _Users;      

        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private ICommand _addNewUserCommand;
        private ICommand _cancelNewUserCommand;
        private ICommand _saveUsersCommand;
        #endregion

        #region Constructor
        public UsersController()
        {

            _Users = new UsersModel()
            {
                CurrentLogin = new LoginModel(),
                SchoolInfo = new SchoolModel()
            };

            //Get Global Objects
            GetGlobalObjects();

            //Get Settings
            this.GetSettings();
            // Set pagination
            this.ResetPagination();

            //Subscribe to Model's Property changed event
            this.Users.PropertyChanged += (s, e) => {
                if (e.PropertyName == "SelectedItemInUsersList")
                {
                    Users.User = Users.SelectedItemInUsersList;
                    Users.PasswordBox.Password = Users.User != null ? Users.User.password : string.Empty;
                    this.ShowForm();
                }
            };

            

            //Get Initial Users list
            this.GetUsersList();

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _addNewUserCommand = new RelayCommand(AddNewUser, CanAddNewUser);
            _cancelNewUserCommand = new RelayCommand(CancelNewUser, CanCancelNewUser);
            _saveUsersCommand = new RelayCommand(SaveUsers, CanSaveUsers);

            this.ShowList();
        }
        
        #endregion

        #region Properties

        public UsersModel Users
        {
            get
            {
                return _Users;
            }
            set
            {
                _Users = value;
                OnPropertyChanged("Users");
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
                Users.pageNo++;
                Users.PageNo = "Page No : " + Users.pageNo;
                Users.fromRowNo = Users.toRowNo + 1;
                Users.toRowNo = Users.pageNo * Users.NoOfRecordsPerPage;
                this.GetUsersList();
                if (Users.pageNo > 1 && Users.UsersList.Count == 0)
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
                if (Users.pageNo > 1)
                {
                    Users.pageNo--;
                    Users.PageNo = "Page No : " + Users.pageNo;
                    Users.toRowNo = Users.fromRowNo - 1;
                    Users.fromRowNo = (Users.toRowNo + 1) - Users.NoOfRecordsPerPage;
                    this.GetUsersList();
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

        #region AddNewUserCommand

        public ICommand AddNewUserCommand
        {
            get { return _addNewUserCommand; }
        }


        public bool CanAddNewUser(object obj)
        {
            return true;
        }


        public void AddNewUser(object obj)
        {
            try
            {
                Users.User = new UsersListModel()
                {
                    is_active = true,
                    CreatedBy = Users.CurrentLogin.User.username
                };
                Users.PasswordBox.Password = null;
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

        #region CancelNewUserCommand

        public ICommand CancelNewUserCommand
        {
            get { return _cancelNewUserCommand; }
        }


        public bool CanCancelNewUser(object obj)
        {
            return true;
        }


        public void CancelNewUser(object obj)
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

        #region SaveUsersCommand
        public ICommand SaveUsersCommand
        {
            get { return _saveUsersCommand; }
        }


        public bool CanSaveUsers(object obj)
        {
            return Users.User != null &&
                Users.User.full_name != null &&
                Users.User.username != null;

        }

        public void SaveUsers(object obj)
        {
            try
            {
                Users.User.password = Users.PasswordBox.Password;
                if (Users.User.id_offline == null && UsersManager.IsExistingUser(Users.User))
                {
                    GeneralMethods.ShowDialog("User Cannot be Created", "An account with same username exists. Kindly choose some other username", true);
                   
                }
                else
                {
                    if (UsersManager.CreateOrModfiyUsers(Users.User, Users.CurrentLogin, Users.SchoolInfo))
                    {
                        GeneralMethods.ShowNotification("Notification", "User Saved Successfully");
                        this.GetUsersList();
                        this.ShowList();
                    }
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

        
        private void GetUsersList()
        {
            try
            {
                Users.UsersList = UsersManager.GetUsersList(Users.fromRowNo, Users.toRowNo);
                Users.NoRecordsFound = Users.UsersList.Count > 0 ? "Collapsed" : "Visible";
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
            Users.ListVisibility = "Collapsed";
            Users.FormVisibility = "Visible";
        }

        private void ShowList()
        {
            Users.ListVisibility = "Visible";
            Users.FormVisibility = "Collapsed";
        }

        private void GetGlobalObjects()
        {
            //Get the Current Login
            Users.CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get College Info
            Users.SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
        }

        private void ResetPagination()
        {
            Users.fromRowNo = 1;
            Users.pageNo = 1;
            Users.PageNo = "Page No : " + Users.pageNo;
            Users.NoOfRecordsPerPage = Users.NoOfRecords;
            Users.toRowNo = Users.pageNo * Users.NoOfRecordsPerPage;
        }


        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            Users.NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }


    }
}
