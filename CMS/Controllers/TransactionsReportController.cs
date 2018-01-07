using CMS.Models;
using CMS.Shared;
using SMS_Businness_Layer.Businness;
using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static SMS_Models.Models.DBModels;

namespace CMS.Controllers
{
    public class TransactionsReportController :NotifyPropertyChanged
    {
        #region Fields
        private TransactionsReportModel _TransactionsReport;

        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private ICommand _exportToExcelCommand;
        #endregion

        #region Constructor
        public TransactionsReportController()
        {

            _TransactionsReport = new TransactionsReportModel()
            {
                CurrentLogin = new LoginModel(),
                SchoolInfo = new SchoolModel(),             
            };

            //Get Global Objects
            //GetGlobalObjects();

            // Get drop down Lists
            this.GetDropDownLists();

            //Get Settings
            this.GetSettings();

            // Set pagination
            this.ResetPagination();

            //Subscribe to Model's Property changed event
            this.TransactionsReport.TransactionsReportFilters.PropertyChanged += (s, e) =>
            {
                this.GetTransactionsReportList();
            };

            //Get Initial TransactionsReport list
            this.GetTransactionsReportList();

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _exportToExcelCommand = new RelayCommand(ExportToExcel, CanExportToExcel);
        }

        #endregion

        #region Properties

        public TransactionsReportModel TransactionsReport
        {
            get
            {
                return _TransactionsReport;
            }
            set
            {
                _TransactionsReport = value;
                OnPropertyChanged("TransactionsReport");
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
                TransactionsReport.pageNo++;
                TransactionsReport.PageNo = "Page No : " + TransactionsReport.pageNo;
                TransactionsReport.fromRowNo = TransactionsReport.toRowNo + 1;
                TransactionsReport.toRowNo = TransactionsReport.pageNo * TransactionsReport.NoOfRecordsPerPage;
                this.GetTransactionsReportList();
                if (TransactionsReport.pageNo > 1 && TransactionsReport.TransactionsReportList.Count == 0)
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
                if (TransactionsReport.pageNo > 1)
                {
                    TransactionsReport.pageNo--;
                    TransactionsReport.PageNo = "Page No : " + TransactionsReport.pageNo;
                    TransactionsReport.toRowNo = TransactionsReport.fromRowNo - 1;
                    TransactionsReport.fromRowNo = (TransactionsReport.toRowNo + 1) - TransactionsReport.NoOfRecordsPerPage;
                    this.GetTransactionsReportList();
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
        #endregion\

        #region ExportToExcelCommand

        public ICommand ExportToExcelCommand
        {
            get { return _exportToExcelCommand; }
        }


        public bool CanExportToExcel(object obj)
        {
            return true;
        }


        public void ExportToExcel(object obj)
        {
            try
            {
                TransactionsReportManager.ExportTransactionsReportToExcel(TransactionsReport.TransactionsReportFilters);
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

        private void GetTransactionsReportList()
        {
            try
            {
                TransactionsReport.TransactionsReportList = TransactionsReportManager.GetTransactionsReportList(TransactionsReport.fromRowNo, TransactionsReport.toRowNo,TransactionsReport.TransactionsReportFilters);
                TransactionsReport.NoRecordsFound = TransactionsReport.TransactionsReportList.Count > 0 ? "Collapsed" : "Visible";
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

        private void GetGlobalObjects()
        {
            //Get the Current Login
            TransactionsReport.CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get College Info
            TransactionsReport.SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
            //Get Current Session
            TransactionsReport.CurrentSession = (SessionsListModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentSession);
        }

        private void ResetPagination()
        {
            TransactionsReport.fromRowNo = 1;
            TransactionsReport.pageNo = 1;
            TransactionsReport.PageNo = "Page No : " + TransactionsReport.pageNo;
            TransactionsReport.NoOfRecordsPerPage = TransactionsReport.NoOfRecords;
            TransactionsReport.toRowNo = TransactionsReport.pageNo * TransactionsReport.NoOfRecordsPerPage;
        }


        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            TransactionsReport.NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }

        private void GetDropDownLists()
        {
            TransactionsReport.TransactionsReportFilters = new TransactionsReporFiltersModel()
            {
                GradesList = GradesSetupManager.GetAllGrades(IncludeAllOption : true),
                SectionsList = SectionsSetupManager.GetAllSections(IncludeAllOption: true),
                FromDate = new DateTime(DateTime.Today.Year,DateTime.Today.Month, 1),
                ToDate = DateTime.Today,
            };
        }


    }
}
