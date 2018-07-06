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
        #endregion

        #region ValidateLicenseCommand
        public ICommand ValidateLicenseCommand
        {
            get { return _validateLicenseCommand; }        
        }

      
        public bool CanValidateLicense(object obj)
        {
            return Licensing.License.LicenseValue != null;
                
        }

        public void ValidateLicense(object obj)
        {
            try
            {
                if (LicensingManager.ValidateLicense(Licensing.License))
                {
                    LicensingManager.SetLicense(Licensing.License.LicenseValue);

                    GeneralMethods.ShowNotification("Notification", "License Validated Successfully");

                    Main winMain = new Main();
                    winMain.Show();
                    Window.Close();
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

        
        #endregion

    }
}
