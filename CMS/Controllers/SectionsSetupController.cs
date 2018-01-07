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
    public class SectionsSetupController :NotifyPropertyChanged
    {
        #region Fields
        private SectionsSetupModel _SectionsSetup;      

        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private ICommand _addNewSectionCommand;
        private ICommand _cancelNewSectionCommand;
        private ICommand _saveSectionsCommand;
        #endregion

        #region Constructor
        public SectionsSetupController()
        {

            _SectionsSetup = new SectionsSetupModel()
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
            this.SectionsSetup.PropertyChanged += (s, e) => {
                if (e.PropertyName == "SelectedItemInSectionsList")
                {
                    SectionsSetup.Section = SectionsSetup.SelectedItemInSectionsList;
                    this.ShowForm();
                }
            };

            

            //Get Initial Sections list
            this.GetSectionsList();

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _addNewSectionCommand = new RelayCommand(AddNewSection, CanAddNewSection);
            _cancelNewSectionCommand = new RelayCommand(CancelNewSection, CanCancelNewSection);
            _saveSectionsCommand = new RelayCommand(SaveSections, CanSaveSections);

            this.ShowList();
        }
        
        #endregion

        #region Properties

        public SectionsSetupModel SectionsSetup
        {
            get
            {
                return _SectionsSetup;
            }
            set
            {
                _SectionsSetup = value;
                OnPropertyChanged("SectionsSetup");
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
                SectionsSetup.pageNo++;
                SectionsSetup.PageNo = "Page No : " + SectionsSetup.pageNo;
                SectionsSetup.fromRowNo = SectionsSetup.toRowNo + 1;
                SectionsSetup.toRowNo = SectionsSetup.pageNo * SectionsSetup.NoOfRecordsPerPage;
                this.GetSectionsList();
                if (SectionsSetup.pageNo > 1 && SectionsSetup.SectionsList.Count == 0)
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
                if (SectionsSetup.pageNo > 1)
                {
                    SectionsSetup.pageNo--;
                    SectionsSetup.PageNo = "Page No : " + SectionsSetup.pageNo;
                    SectionsSetup.toRowNo = SectionsSetup.fromRowNo - 1;
                    SectionsSetup.fromRowNo = (SectionsSetup.toRowNo + 1) - SectionsSetup.NoOfRecordsPerPage;
                    this.GetSectionsList();
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

        #region AddNewSectionCommand

        public ICommand AddNewSectionCommand
        {
            get { return _addNewSectionCommand; }
        }


        public bool CanAddNewSection(object obj)
        {
            return true;
        }


        public void AddNewSection(object obj)
        {
            try
            {
                SectionsSetup.Section = new SectionsListModel()
                {
                    CreatedBy = SectionsSetup.CurrentLogin.User.full_name
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

        #region CancelNewSectionCommand

        public ICommand CancelNewSectionCommand
        {
            get { return _cancelNewSectionCommand; }
        }


        public bool CanCancelNewSection(object obj)
        {
            return true;
        }


        public void CancelNewSection(object obj)
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

        #region SaveSectionsCommand
        public ICommand SaveSectionsCommand
        {
            get { return _saveSectionsCommand; }
        }


        public bool CanSaveSections(object obj)
        {
            return SectionsSetup.Section != null && SectionsSetup.Section.name != null && SectionsSetup.Section.capacity != 0;                
        }

        public void SaveSections(object obj)
        {
            try
            {
                if (SectionsSetupManager.CreateOrModfiySections(SectionsSetup.Section, SectionsSetup.CurrentLogin, SectionsSetup.SchoolInfo))
                {
                    GeneralMethods.ShowNotification("Notification", "Section Saved Successfully");
                    this.GetSectionsList();
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

        
        private void GetSectionsList()
        {
            try
            {
                SectionsSetup.SectionsList = SectionsSetupManager.GetSectionsList(SectionsSetup.fromRowNo, SectionsSetup.toRowNo);
                SectionsSetup.NoRecordsFound = SectionsSetup.SectionsList.Count > 0 ? "Collapsed" : "Visible";
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
            SectionsSetup.ListVisibility = "Collapsed";
            SectionsSetup.FormVisibility = "Visible";
        }

        private void ShowList()
        {
            SectionsSetup.ListVisibility = "Visible";
            SectionsSetup.FormVisibility = "Collapsed";
        }

        private void GetGlobalObjects()
        {
            //Get the Current Login
            SectionsSetup.CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get College Info
            SectionsSetup.SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
        }

        private void ResetPagination()
        {
            SectionsSetup.fromRowNo = 1;
            SectionsSetup.pageNo = 1;
            SectionsSetup.PageNo = "Page No : " + SectionsSetup.pageNo;
            SectionsSetup.NoOfRecordsPerPage = SectionsSetup.NoOfRecords;
            SectionsSetup.toRowNo = SectionsSetup.pageNo * SectionsSetup.NoOfRecordsPerPage;
        }


        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            SectionsSetup.NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }


    }
}
