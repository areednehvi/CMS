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
    public class FeeCategoriesController :NotifyPropertyChanged
    {
        #region Fields
        private FeeCategoriesModel _FeeCategories;

        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private ICommand _addNewFeeCategoryCommand;
        private ICommand _cancelNewFeeCategoryCommand;
        private ICommand _saveFeeCategoriesCommand;
        #endregion

        #region Constructor
        public FeeCategoriesController()
        {

            _FeeCategories = new FeeCategoriesModel()
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
            this.FeeCategories.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedItemInFeeCategoriesList")
                {
                    FeeCategories.FeeCategory = FeeCategories.SelectedItemInFeeCategoriesList;
                    this.ShowForm();
                }
            };



            //Get Initial FeeCategories list
            this.GetFeeCategoriesList();

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _addNewFeeCategoryCommand = new RelayCommand(AddNewFeeCategory, CanAddNewFeeCategory);
            _cancelNewFeeCategoryCommand = new RelayCommand(CancelNewFeeCategory, CanCancelNewFeeCategory);
            _saveFeeCategoriesCommand = new RelayCommand(SaveFeeCategories, CanSaveFeeCategories);

            this.ShowList();
        }

        #endregion

        #region Properties

        public FeeCategoriesModel FeeCategories
        {
            get
            {
                return _FeeCategories;
            }
            set
            {
                _FeeCategories = value;
                OnPropertyChanged("FeeCategories");
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
                FeeCategories.pageNo++;
                FeeCategories.PageNo = "Page No : " + FeeCategories.pageNo;
                FeeCategories.fromRowNo = FeeCategories.toRowNo + 1;
                FeeCategories.toRowNo = FeeCategories.pageNo * FeeCategories.NoOfRecordsPerPage;
                this.GetFeeCategoriesList();
                if (FeeCategories.pageNo > 1 && FeeCategories.FeeCategoriesList.Count == 0)
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
                if (FeeCategories.pageNo > 1)
                {
                    FeeCategories.pageNo--;
                    FeeCategories.PageNo = "Page No : " + FeeCategories.pageNo;
                    FeeCategories.toRowNo = FeeCategories.fromRowNo - 1;
                    FeeCategories.fromRowNo = (FeeCategories.toRowNo + 1) - FeeCategories.NoOfRecordsPerPage;
                    this.GetFeeCategoriesList();
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

        #region AddNewFeeCategoryCommand

        public ICommand AddNewFeeCategoryCommand
        {
            get { return _addNewFeeCategoryCommand; }
        }


        public bool CanAddNewFeeCategory(object obj)
        {
            return true;
        }


        public void AddNewFeeCategory(object obj)
        {
            try
            {
                FeeCategories.FeeCategory = new FeeCategoriesListModel()
                {
                    CreatedBy = FeeCategories.CurrentLogin.User.full_name
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

        #region CancelNewFeeCategoryCommand

        public ICommand CancelNewFeeCategoryCommand
        {
            get { return _cancelNewFeeCategoryCommand; }
        }


        public bool CanCancelNewFeeCategory(object obj)
        {
            return true;
        }


        public void CancelNewFeeCategory(object obj)
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

        #region SaveFeeCategoriesCommand
        public ICommand SaveFeeCategoriesCommand
        {
            get { return _saveFeeCategoriesCommand; }
        }


        public bool CanSaveFeeCategories(object obj)
        {
            return FeeCategories.FeeCategory != null;

        }

        public void SaveFeeCategories(object obj)
        {
            try
            {

                if (FeeCategoriesManager.CreateOrModfiyFeeCategories(FeeCategories.FeeCategory, FeeCategories.CurrentLogin, FeeCategories.SchoolInfo))
                {
                    GeneralMethods.ShowNotification("Notification", "Fee Category Saved Successfully");
                    this.GetFeeCategoriesList();
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

        
        private void GetFeeCategoriesList()
        {
            try
            {
                FeeCategories.FeeCategoriesList = FeeCategoriesManager.GetFeeCategoriesList(FeeCategories.fromRowNo, FeeCategories.toRowNo);
                FeeCategories.NoRecordsFound = FeeCategories.FeeCategoriesList.Count > 0 ? "Collapsed" : "Visible";
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
            FeeCategories.ListVisibility = "Collapsed";
            FeeCategories.FormVisibility = "Visible";
        }

        private void ShowList()
        {
            FeeCategories.ListVisibility = "Visible";
            FeeCategories.FormVisibility = "Collapsed";
        }

        private void GetGlobalObjects()
        {
            //Get the Current Login
            FeeCategories.CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get College Info
            FeeCategories.SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
        }

        private void ResetPagination()
        {
            FeeCategories.fromRowNo = 1;
            FeeCategories.pageNo = 1;
            FeeCategories.PageNo = "Page No : " + FeeCategories.pageNo;
            FeeCategories.NoOfRecordsPerPage = FeeCategories.NoOfRecords;
            FeeCategories.toRowNo = FeeCategories.pageNo * FeeCategories.NoOfRecordsPerPage;
        }


        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            FeeCategories.NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }


    }
}
