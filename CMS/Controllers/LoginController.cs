using CMS.Models;
using CMS.Shared;
using CMS.Views;
using SMS_Businness_Layer.Businness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static SMS_Models.Models.DBModels;

namespace CMS.Controllers
{
    class LoginController
    {
        #region Fields
        private LoginModel _login;
        private SchoolModel _SchoolInfo;
        private SessionsListModel _CurrentSession;
        private Window _window;      
        private ICommand _loginCommand;
        private ICommand _closeCommand;
        private ICommand _minimizeCommand;
        #endregion

        #region Constructor
        public LoginController()
        {
            _login = new LoginModel()
            {
                User = new UsersListModel()
            };

            //Get Settings
            //this.GetSettings();

            //Initialize commands
            _loginCommand = new RelayCommand(AuthenticateUser, CanLogin);
            _closeCommand = new RelayCommand(CloseLogin, CanClose);
            _minimizeCommand = new RelayCommand(MinimizeLogin, CanMinimize);
        }
        #endregion

        #region Properties
        public LoginModel Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
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

        public Window Window
        {
            get
            {
                return _window;
            }
            set
            {
                _window = value;
            }
        }
        #endregion

        #region LoginCommand
        public ICommand LoginCommand
        {
            get { return _loginCommand; }        
        }

      
        public bool CanLogin(object obj)
        {
          
            if (Login.User.username != null)
                return true;
            return false;
        }

       
        public void AuthenticateUser(object obj)
        {
            try
            {
                PasswordBox pwBox = obj as PasswordBox;
                Login.User.password = pwBox.Password;
                if (LoginManager.ValidateUser(Login))
                {

                    CreateLoginGlobalObject();

                    CreateSessionGlobalObject();

                    UpdateLastLoginTime();

                    if (SchoolSetupManager.IsSchoolSetup())
                    {
                        
                        CreateSchoolGlobalObject();

                        //open Main window after authentication
                        Main objMainWindow = new Main(Login);
                        objMainWindow.Show();
                        Window.Close();
                    }
                    else
                    {
                        //open College Setup window after authentication
                        SchoolSetup objSchoolSetupWindow = new SchoolSetup();
                        objSchoolSetupWindow.Show();
                        Window.Close();
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

        #region CloseCommand

        public ICommand CloseCommand
        {
            get { return _closeCommand; }
        }


        public bool CanClose(object obj)
        {
            return true;
        }


        public void CloseLogin(object obj)
        {           
            Window.Close();
        }
        #endregion

        #region MinimizeCommand

        public ICommand MinimizeCommand
        {
            get { return _minimizeCommand; }
        }


        public bool CanMinimize(object obj)
        {
            return true;
        }


        public void MinimizeLogin(object obj)
        {
            Window.WindowState = WindowState.Minimized;
        }
        #endregion

        #region Private Functions
        private void CreateLoginGlobalObject()
        {
            //Maintain state of Login
            GeneralMethods.CreateGlobalObject(GlobalObjects.CurrentLogin, Login);
        }
        private void CreateSchoolGlobalObject()
        {
            //Maintain state of College Info
            SchoolInfo = SchoolSetupManager.GetSchoolInfo();
            GeneralMethods.CreateGlobalObject(GlobalObjects.SchoolInfo, SchoolInfo);
        }
        private void CreateSessionGlobalObject()
        {
            //Maintain state of Session Info
            if (SessionsSetupManager.GetCurrentSession().Count > 0)
            {
                CurrentSession = SessionsSetupManager.GetCurrentSession()[0];
                GeneralMethods.CreateGlobalObject(GlobalObjects.CurrentSession, CurrentSession);
            }
        }
        private void GetSettings()
        {

        }
        private void UpdateLastLoginTime()
        {
            Login.User.last_login_time = DateTime.Now;
            UsersManager.CreateOrModfiyUsers(Login.User, Login, SchoolInfo);
        }
        #endregion  

    }
}
