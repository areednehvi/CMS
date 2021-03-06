﻿using MaterialDesignThemes.Wpf;
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
    public class SchoolSetupController :NotifyPropertyChanged
    {
        #region Fields
        private SchoolSetupModel _SchoolSetup;
        private LoginModel _login;
        private ICommand _setupSchoolCommand;
        private ICommand _minimizeCommand;
        private ICommand _closeCommand;
        #endregion

        #region Constructor
        public SchoolSetupController()
        {
            _SchoolSetup = new SchoolSetupModel()
            {
                SchoolInfo = new SchoolModel()
                {
                    EducationKey = "FreeTrial",
                    License = "FreeTrial",
                    id_offline = Guid.NewGuid().ToString(),
                    id_online = Guid.Empty.ToString(),
                    created_on = DateTime.Now,
                    database_id = Guid.Empty.ToString(),
                }
            };
            _login = new LoginModel();

            GetGlobalObjects();

            //Initialize  Commands
            _setupSchoolCommand = new RelayCommand(SetupSchool, CanSetupSchool);
            _closeCommand = new RelayCommand(CloseLogin, CanClose);
            _minimizeCommand = new RelayCommand(MinimizeLogin, CanMinimize);
        }

        #endregion

        #region Properties

        public Window Window
        {
            get; set;   
        }
        public SchoolSetupModel SchoolSetup
        {
            get
            {
                return _SchoolSetup;
            }
            set
            {
                _SchoolSetup = value;
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
        #endregion

        #region SetupSchoolCommand
        public ICommand SetupSchoolCommand
        {
            get { return _setupSchoolCommand; }        
        }

      
        public bool CanSetupSchool(object obj)
        {
            return SchoolSetup.SchoolInfo!= null && 
                SchoolSetup.SchoolInfo.name != null &&
                SchoolSetup.SchoolInfo.phone != null &&
                SchoolSetup.SchoolInfo.address != null;
        }

        public void SetupSchool(object obj)
        {
            try
            {
                if (SchoolSetupManager.SetSchooInfo(SchoolSetup.SchoolInfo))
                {
                    if (LicensingManager.IsCMSInstalledBefore()) //CMS installed before
                    {
                        //prompt to validate license
                        LicenseSetup winLicenseSetup = new LicenseSetup();
                        winLicenseSetup.Show();
                        Window.Close();
                    }
                    else
                    {

                        CreateLicencing();

                        CreateSchoolGlobalObject();

                        GeneralMethods.ShowNotification("Notification", "College Setup Successfully");

                        Main winMain = new Main();
                        winMain.Show();
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

        private void CreateLicencing()
        {
            try
            {
                if (LicensingManager.SetLicense("FreeTrial"))
                {
                    //Free Trial Started
                    GeneralMethods.ShowDialog("Free Trial Started", "Your free trial for using CMS has started! Kindly contact CMS administration Team \n to purchase license.");
                }               
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);

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
            SchoolSetup.SchoolInfo = SchoolSetupManager.GetSchoolInfo();
            GeneralMethods.CreateGlobalObject(GlobalObjects.SchoolInfo, SchoolSetup.SchoolInfo);
        }

        private void GetGlobalObjects()
        {
            //Get the Current Login
            Login = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
        }
        #endregion

    }
}
