using MaterialDesignThemes.Wpf;
using CMS.Models;
using CMS.Shared;
using CMS.Views;
using SMS_Businness_Layer.Businness;
using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace CMS.Controllers
{
    public class LicensingController :NotifyPropertyChanged
    {
        #region Fields
        private LoginModel _login;
        private SchoolModel _SchoolInfo;
        private LicensingModel _Licensing;
        private ICommand _validateLicenseCommand;
        private ICommand _minimizeCommand;
        private ICommand _closeCommand;
        #endregion

        #region Constructor
        public LicensingController()
        {
            _Licensing = new LicensingModel()
            {
                License = new LicenseModel()
            };
            _SchoolInfo = new SchoolModel();
            _login = new LoginModel();

            GetGlobalObjects();

            GetLicenseDetailsFromDB();

            //Initialize  Commands
            _validateLicenseCommand = new RelayCommand(ValidateLicense, CanValidateLicense);
            _closeCommand = new RelayCommand(CloseLogin, CanClose);
            _minimizeCommand = new RelayCommand(MinimizeLogin, CanMinimize);
        }        

        #endregion

        #region Properties

        public Window Window
        {
            get; set;   
        }
        public LicensingModel Licensing
        {
            get
            {
                return _Licensing;
            }
            set
            {
                _Licensing = value;
            }
        }
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
        #endregion

        #region ValidateLicenseCommand
        public ICommand ValidateLicenseCommand
        {
            get { return _validateLicenseCommand; }        
        }

      
        public bool CanValidateLicense(object obj)
        {
            return Licensing.License.LicenseValue != null && Licensing.License.EducationKey != null;                
        }

        public void ValidateLicense(object obj)
        {
            Licensing.Status = LicensingDefinitions.Validating;
            LicenseOnline objLicenseOnline = new LicenseOnline();
            try
            {
                if (GeneralMethods.IsInternetAvailable())
                {
                    objLicenseOnline = LicensingManager.ValidateLicense(Licensing.License);

                    if (objLicenseOnline.Status == LicensingDefinitions.LicenseValid)
                    {
                        if (LicensingManager.SetLicense(Licensing.License.LicenseValue))
                        {

                            GeneralMethods.ShowNotification("Notification", "License Validated Successfully");

                            SchoolInfo.EducationKey = Licensing.License.EducationKey;
                            SchoolInfo.License = Licensing.License.LicenseValue;
                            SchoolInfo.LicenseStart = objLicenseOnline.LicenseStart;
                            SchoolInfo.LicenseEnd = objLicenseOnline.LicenseEnd ;
                            if (SchoolSetupManager.SetSchooInfo(SchoolInfo))
                            {

                                CreateSchoolGlobalObject();

                                //open Main window after authentication
                                Main objMainWindow = new Main(Login);
                                objMainWindow.Show();
                                Window.Close();

                                Main winMain = new Main();
                                winMain.Show();
                                Window.Close();
                            }
                        }
                    }
                    else if (objLicenseOnline.Status == LicensingDefinitions.LicenseInValid)
                        Licensing.Status = LicensingDefinitions.LicenseInValidMessage;
                    else if (objLicenseOnline.Status == LicensingDefinitions.EducationKeyInvalid)
                        Licensing.Status = LicensingDefinitions.EducationKeyInvalidMessage;
                    else if (objLicenseOnline.Status == LicensingDefinitions.LicenseExpired)
                        Licensing.Status = LicensingDefinitions.LicenseExpiredMessage;
                }
                else
                    Licensing.Status = LicensingDefinitions.InternetNotAvailable;
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
        private void CreateSchoolGlobalObject()
        {
            //Maintain state of College Info
            //SchoolInfo = SchoolSetupManager.GetSchoolInfo();
            GeneralMethods.CreateGlobalObject(GlobalObjects.SchoolInfo, SchoolInfo);
        }

        private void GetGlobalObjects()
        {
            //Get the Current Login
            Login = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
        }

        private void GetLicenseDetailsFromDB()
        {
            try
            {                
                SchoolInfo = SchoolSetupManager.GetSchoolInfo();
                Licensing.License.EducationKey = SchoolInfo.EducationKey;
                Licensing.License.LicenseValue = SchoolInfo.License;
            }
            catch(Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
        }
        #endregion

    }
}
