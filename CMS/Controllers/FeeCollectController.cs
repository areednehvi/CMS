using MaterialDesignThemes.Wpf;
using CMS.Models;
using CMS.Shared;
using CMS.Views;
using SMS_Businness_Layer.Businness;
using SMS_Businness_Layer.Shared;
using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CMS.Controllers
{
    public class FeeCollectController :NotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<PaymentModel> _PaymentHistoryList;
        private ObservableCollection<FeeDueModel> _FeeDueList;
        private FeeCollectionStudentListModel _FeeCollectionStudent;
        private PaymentModel _selectedItemInPaymentHistoryList;
        private FeeCollectOtherFieldsModel _FeeCollectOtherFields;
        private FeeDueFormFieldsModel _FeeDueFormFields;
        private FeeDueListFiltersModel _FeeDueListFilters;
        private FeeCategoryModel _selectedFeeCategory;
        private ObservableCollection<PaymentModeModel> _PaymentModeList;
        private ObservableCollection<PendingMonthlyFeeModel> _PendingMonthlyFeeList = new ObservableCollection<PendingMonthlyFeeModel>();
        private MakePaymentModel _makePayment;
        private LoginModel _CurrentLogin;
        private SchoolModel _SchoolInfo;
        private Window _window;
        private DataGrid _paymentHistoryDataGrid;
        private DataGrid _feeDueDataGrid;
        private Card _pendingMonthlyFeesMaterialDesignCard;
        private Card _makePaymentScreenMaterialDesignCard;
        private int _SelectedTabItem;
        private int NoOfRecords;
        private int fromRowNo,pageNo, NoOfRecordsPerPage, toRowNo;
        private Double _SumOfAllSelectedFees;
        private string _NoRecordsFound;
        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private ICommand _minimizeCommand;
        private ICommand _closeCommand;
        private ICommand _showHideMakePaymentScreenCommand;
        private ICommand _feeDueApplyCommand;
        private ICommand _feeDueDeleteCommand;
        private ICommand _checkAllFeeDueCommand;
        private ICommand _CheckAllPendingMonthlyFeeCommand;
        private ICommand _paymentHistorySaveCommand;
        private ICommand _makePaymentCommand;
        #endregion

        #region Constructor
        public FeeCollectController()
        {            
            //Get Global Objects
            GetGlobalObjects();
            //Get Settings
            this.GetSettings();

            _PaymentHistoryList = new ObservableCollection<PaymentModel>();
            _FeeDueList = new ObservableCollection<FeeDueModel>();
            _FeeCollectionStudent = new FeeCollectionStudentListModel();
            _FeeCollectOtherFields = new FeeCollectOtherFieldsModel();
            _FeeDueFormFields = new FeeDueFormFieldsModel();
            _FeeDueListFilters = new FeeDueListFiltersModel();
            _PendingMonthlyFeeList= new ObservableCollection<PendingMonthlyFeeModel>();
            _makePayment = new MakePaymentModel() { Payment = new PaymentModel() { payment_date = DateTime.Today } };            

            //Subscribe to Model's Property changed event
            //this.FeeDueListFilters.PropertyChanged += (s, e) => {
            //    this.LoadStudentFeeDueListAsFiltersHaveChanged();
            //};

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _closeCommand = new RelayCommand(CloseLogin, CanClose);
            _minimizeCommand = new RelayCommand(MinimizeLogin, CanMinimize);
            _showHideMakePaymentScreenCommand = new RelayCommand(ShowHideMakePaymentScreen,CanShowHideMakePaymentScreen);
            _feeDueApplyCommand = new RelayCommand(ApplyFeeDue, CanApplyFeeDue);
            _feeDueDeleteCommand = new RelayCommand(DeleteFeeDue, CanDeleteFeeDue);
            _checkAllFeeDueCommand = new RelayCommand(CheckAllFeeDue, CanCheckAllFeeDue);
            _CheckAllPendingMonthlyFeeCommand = new RelayCommand(CheckAllPendingMonthlyFee, CanCheckAllPendingMonthlyFee);
            _paymentHistorySaveCommand = new RelayCommand(SavePaymentHistory,CanSavePaymentHistory);
            _makePaymentCommand = new RelayCommand(MakeThePayment, CanMakePayment);
        }        

        #endregion

        #region Properties
        public LoginModel CurrentLogin
        {
            get
            {
                return _CurrentLogin;
            }
            set
            {
                _CurrentLogin = value;
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
        public ObservableCollection<PendingMonthlyFeeModel> PendingMonthlyFeeList
        {            
            get
            {
                return _PendingMonthlyFeeList;
            }
            set
            {
                _PendingMonthlyFeeList = value;
                OnPropertyChanged("PendingMonthlyFeeList");
            }
        }

        public ObservableCollection<PaymentModeModel> PaymentModeList
        {
            get
            {
                return _PaymentModeList;
            }

            set
            {
                _PaymentModeList = value;
                OnPropertyChanged("PaymentModeList");
            }

        }
        
        public ObservableCollection<PaymentModel> PaymentHistoryList
        {
            get
            {
                return _PaymentHistoryList;
            }

            set
            {
                _PaymentHistoryList = value;
            }

        }

        public FeeCollectOtherFieldsModel FeeCollectOtherFields
        {
            get
            {
                return _FeeCollectOtherFields;
            }
            set
            {
                _FeeCollectOtherFields = value;
            }
        }

        public FeeDueFormFieldsModel FeeDueFormFields
        {
            get
            {
                return _FeeDueFormFields;
            }
            set
            {
                _FeeDueFormFields = value;
            }
        }
        public FeeDueListFiltersModel FeeDueListFilters
        {
            get
            {
                return _FeeDueListFilters;
            }
            set
            {
                _FeeDueListFilters = value;
            }
        }

        public PaymentModel SelectedItemInPaymentHistoryList
        {
            get
            {
                return _selectedItemInPaymentHistoryList;
            }

            set
            {
                _selectedItemInPaymentHistoryList = value;
            }

        }
        public Double SumOfAllSelectedFees
        {
            get
            {
                return _SumOfAllSelectedFees;
            }
            set
            {
                _SumOfAllSelectedFees = value;
                OnPropertyChanged("SumOfAllSelectedFees");
            }
        }

        public MakePaymentModel MakePayment
        {
            get
            {
                return _makePayment;
            }

            set
            {
                _makePayment = value;
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

        public Card PendingMonthlyFeesMaterialDesignCard
        {
            get
            {
                return _pendingMonthlyFeesMaterialDesignCard;
            }
            set
            {
                _pendingMonthlyFeesMaterialDesignCard = value;
            }
        }

        public Card MakePaymentScreenMaterialDesignCard
        {
            get
            {
                return _makePaymentScreenMaterialDesignCard;
            }
            set
            {
                _makePaymentScreenMaterialDesignCard = value;
            }
        }

        public int SelectedTabItem
        {
            get { return _SelectedTabItem; }
            set
            {
                _SelectedTabItem = value;
                OnPropertyChanged("SelectedTabItem");
                ResetPagination();
                switch(_SelectedTabItem)
                {
                    case 0:
                        SumOfAllSelectedFees = 0;
                        ShowHideMakePaymentScreen("Back");
                        // Get Lists
                        this.GetMakePaymentDropDownLists();
                        this.MakePayment.SelectedPaymentMode = new PaymentModeModel() { id = "Cash", name = "Cash" };
                        this.GetStudentFeeBalancesList();
                        break;
                    case 1:
                        this.GetStudentPaymentHistoryList();
                        break;
                    case 2:
                        // Get Lists
                        this.GetFeeDueDropDownLists();
                        SelectedFeeCategory = new FeeCategoryModel() { id_offline = Guid.Empty.ToString(), name ="All" };
                        this.GetStudentFeeDueList();
                        break;
                }
                
            }
        }

        public FeeCategoryModel SelectedFeeCategory
        {
            get { return _selectedFeeCategory; }
            set
            {
                if (_selectedFeeCategory != value)
                {
                    _selectedFeeCategory = value;
                    FeeDueListFilters.FeeCategory = this.SelectedFeeCategory;
                    OnPropertyChanged("SelectedFeeCategory");
                    this.LoadStudentFeeDueListAsFiltersHaveChanged();
                }
            }
        }

        public DataGrid PaymentHistorListDataGrid
        {
            get
            {
                return _paymentHistoryDataGrid;
            }
            set
            {
                _paymentHistoryDataGrid = value;
            }
        }

        public DataGrid FeeDueListDataGrid
        {
            get
            {
                return _feeDueDataGrid;
            }
            set
            {
                _feeDueDataGrid = value;
            }
        }

        public string NoRecordsFound
        {
            get
            {
                return _NoRecordsFound;
            }
            set
            {
                _NoRecordsFound = value;
                OnPropertyChanged("NoRecordsFound");
            }
        }

        public FeeCollectionStudentListModel FeeCollectionStudentList
        {
            get
            {
                return _FeeCollectionStudent;
            }
            set
            {
                _FeeCollectionStudent = value;
                OnPropertyChanged("FeeCollectionStudentList");
            }
        }
        public ObservableCollection<FeeDueModel> FeeDueList
        {
            get
            {
                return _FeeDueList;
            }

            set
            {
                _FeeDueList = value;
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
                PaymentHistorListDataGrid.ItemsSource = null;
                pageNo++;
                FeeCollectOtherFields.PageNo = "Page No : " + pageNo;
                fromRowNo = toRowNo + 1;
                toRowNo = pageNo * NoOfRecordsPerPage;
                this.GetStudentPaymentHistoryList();
                if (pageNo > 1 && PaymentHistoryList.Count == 0)
                    MoveToPreviousPage(obj);
                PaymentHistorListDataGrid.ItemsSource = PaymentHistoryList;
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                Shared.GeneralMethods.ShowDialog("Error", errorMessage, true);
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
                if (pageNo > 1)
                {
                    PaymentHistorListDataGrid.ItemsSource = null;
                    pageNo--;
                    FeeCollectOtherFields.PageNo = "Page No : " + pageNo;
                    toRowNo = fromRowNo - 1;
                    fromRowNo = (toRowNo + 1) - NoOfRecordsPerPage;
                    this.GetStudentPaymentHistoryList();
                    PaymentHistorListDataGrid.ItemsSource = PaymentHistoryList;
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

        #region ShowHideMakePaymentScreenCommand
        public ICommand ShowHideMakePaymentScreenCommand
        {
            get { return _showHideMakePaymentScreenCommand; }
        }


        public bool CanShowHideMakePaymentScreen(object obj)
        {
            if (SumOfAllSelectedFees > 0)
                return true;
            else
                return false;
        }


        public void ShowHideMakePaymentScreen(object obj)
        {
            try
            {
                switch ((string)obj)
                {
                    case "Pay":
                        MakePayment.Payment.recept_no = DateTime.Now.ToString("ddMMyyyyHHmmss");
                        PendingMonthlyFeesMaterialDesignCard.Visibility = Visibility.Collapsed;
                        MakePaymentScreenMaterialDesignCard.Visibility = Visibility.Visible;
                        this.CalculatePaymentTotals();
                        break;
                    case "Back":
                        PendingMonthlyFeesMaterialDesignCard.Visibility = Visibility.Visible;
                        MakePaymentScreenMaterialDesignCard.Visibility = Visibility.Collapsed;
                        break;
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

        #region MakePaymentCommand
        public ICommand MakePaymentCommand
        {
            get { return _makePaymentCommand; }
        }


        public bool CanMakePayment(object obj)
        {
            if (MakePayment.TotalOfSelectedFeeBalancesGrandTotal > 0 && MakePayment.Payment.payment_date != null && MakePayment.Payment.recept_no !=null)
                return true;
            else
                return false;
        }


        public void MakeThePayment(object obj)
        {
            try
            {
                if (FeeCollectManager.MakePayments(MakePayment, CurrentLogin, SchoolInfo))
                {
                    GeneralMethods.ShowNotification("Notification", "Payment Saved Successfully!");
                    if(FeeCollectManager.PrintReceipt(MakePayment, CurrentLogin, SchoolInfo, FeeCollectionStudentList))
                    {

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

        #region FeeDueApplyCommand
        public ICommand FeeDueApplyCommand
        {
            get { return _feeDueApplyCommand; }
        }


        public bool CanApplyFeeDue(object obj)
        {
            foreach (FeeDueModel objFeeDueModel in FeeDueList)
            {
                if (objFeeDueModel.IsSelected)
                    return true;
            }
            return false;
        }


        public void ApplyFeeDue(object obj)
        {
            try
            {
                //MessageBox.Show(FeeDueFormFields.Fine + " " + FeeDueFormFields.Concession + " " + FeeDueFormFields.ConcessionPercentage);
                foreach(FeeDueModel objFeeDueModel in FeeDueList)
                {
                    if (objFeeDueModel.IsSelected)
                    {
                        objFeeDueModel.fine = FeeDueFormFields.Fine;
                        objFeeDueModel.concession_amount = FeeDueFormFields.Concession;
                        FeeCollectManager.UpdateFeeDue(objFeeDueModel,CurrentLogin);
                    }
                        
                }
                GeneralMethods.ShowNotification("Notification", "Fee Due(s) Updated Successfully!");
                this.GetStudentFeeDueList();

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

        #region FeeDueDeleteCommand
        public ICommand FeeDueDeleteCommand
        {
            get { return _feeDueDeleteCommand; }
        }


        public bool CanDeleteFeeDue(object obj)
        {
            foreach (FeeDueModel objFeeDueModel in FeeDueList)
            {
                if (objFeeDueModel.IsSelected)
                    return true;
            }
            return false;
        }


        public void DeleteFeeDue(object obj)
        {
            try
            {
                Boolean showNotification = false;
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.OKCancel);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    foreach (FeeDueModel objFeeDueModel in FeeDueList)
                    {
                        if (objFeeDueModel.IsSelected)
                        {
                            if (FeeCollectManager.DeleteFeeDue(objFeeDueModel))
                                showNotification = true;
                        }

                    }
                    if(showNotification)
                        GeneralMethods.ShowNotification("Notification", "Fee Due(s) Deleted Successfully!");
                    this.GetStudentFeeDueList();
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

        #region PaymentHistorySaveCommand
        public ICommand PaymentHistorySaveCommand
        {
            get { return _paymentHistorySaveCommand; }
        }


        public bool CanSavePaymentHistory(object obj)
        {
            if (SelectedItemInPaymentHistoryList != null && SelectedItemInPaymentHistoryList.recept_no != null && SelectedItemInPaymentHistoryList.recept_no != string.Empty)
                return true;
            else
                return false;
        }


        public void SavePaymentHistory(object obj)
        {
            try
            {
                if(FeeCollectManager.UpdatePaymentHistory(SelectedItemInPaymentHistoryList,CurrentLogin))
                    GeneralMethods.ShowNotification("Notification", "Payment Saved Successfully!");                

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

        #region CheckAllFeeDueCommand
        public ICommand CheckAllFeeDueCommand
        {
            get { return _checkAllFeeDueCommand; }
        }


        public bool CanCheckAllFeeDue(object obj)
        {
            return true;
        }


        public void CheckAllFeeDue(object obj)
        {
            try
            {
                Boolean IsChecked = (Boolean)obj;
                if (IsChecked)
                {
                    for (int count = 0; count < FeeDueList.Count; count++)
                        FeeDueList[count].IsSelected = true;
                }
                else
                {
                    for (int count = 0; count < FeeDueList.Count; count++)
                        FeeDueList[count].IsSelected = false;
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

        #region CheckAllPendingMonthlyFeeCommand
        public ICommand CheckAllPendingMonthlyFeeCommand
        {
            get { return _CheckAllPendingMonthlyFeeCommand; }
        }


        public bool CanCheckAllPendingMonthlyFee(object obj)
        {
            return true;
        }


        public void CheckAllPendingMonthlyFee(object obj)
        {
            try
            {
                PendingMonthlyFeeModel objSelectedPendingMonthlyFee = (PendingMonthlyFeeModel)obj;
                if (objSelectedPendingMonthlyFee.IsSelected)
                {
                    for (int count = 0; count < objSelectedPendingMonthlyFee.FeeBalancesList.Count; count++)
                        objSelectedPendingMonthlyFee.FeeBalancesList[count].IsSelected = true;
                }
                else
                {
                    for (int count = 0; count < objSelectedPendingMonthlyFee.FeeBalancesList.Count; count++)
                        objSelectedPendingMonthlyFee.FeeBalancesList[count].IsSelected = false;
                }
                this.CalculateSumOfSelectedFeesAndPopulateSelectedFeeListForMakePayment();

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

        #region Collections Changed
        public void SelectedFeeListForMakePayment_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {            
            if (e.NewItems != null)
                foreach (FeeBalancesModel item in e.NewItems)
                    item.PropertyChanged += FeeBalancesModel_PropertyChanged;

            if (e.OldItems != null)
                foreach (FeeBalancesModel item in e.OldItems)
                    item.PropertyChanged -= FeeBalancesModel_PropertyChanged;
        }

        public void FeeBalancesModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "fine" || e.PropertyName == "balance_amount")
                this.CalculatePaymentTotals();
        }
        #endregion

        
        #region Private Methods

        private void GetStudentPaymentHistoryList()
        {
            try
            {
                PaymentHistorListDataGrid.ItemsSource = null;
                PaymentHistoryList = FeeCollectManager.GetStudentPaymentHistory(fromRowNo, toRowNo, FeeCollectionStudentList.id_offline.ToString());
                PaymentHistorListDataGrid.ItemsSource = PaymentHistoryList;
                NoRecordsFound = PaymentHistoryList.Count > 0 ? "Collapsed" : "Visible";
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

        private void GetStudentFeeBalancesList()
        {
            try
            {
                if (FeeCollectionStudentList != null)
                    PendingMonthlyFeeList = FeeCollectManager.GetStudentFeeBalances(FeeCollectionStudentList.id_offline.ToString());
                //NoRecordsFound = PaymentHistoryList.Count > 0 ? "Collapsed" : "Visible";
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

        private void GetStudentFeeDueList()
        {
            try
            {
                FeeDueListDataGrid.ItemsSource = null;
                FeeDueList = FeeCollectManager.GetStudentFeeDue(fromRowNo, toRowNo, FeeCollectionStudentList.id_offline.ToString(),FeeDueListFilters);
                FeeDueListDataGrid.ItemsSource = FeeDueList;
                NoRecordsFound = FeeDueList.Count > 0 ? "Collapsed" : "Visible";
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

        private void ResetPagination()
        {
            fromRowNo = 1;
            pageNo = 1;
            FeeCollectOtherFields.PageNo = "Page No : " + pageNo;
            NoOfRecordsPerPage = NoOfRecords;
            toRowNo = pageNo * NoOfRecordsPerPage;
        }

        private void GetFeeDueDropDownLists()
        {

            FeeDueListFilters.FeeCategoryList = GetListManager.GetFeeCategories();            
        }

        private void GetMakePaymentDropDownLists()
        {

            PaymentModeList = GetListManager.GetPaymentModes();
        }

        private void LoadStudentFeeDueListAsFiltersHaveChanged()
        {
            ResetPagination();
            this.GetStudentFeeDueList();
            if (FeeDueListDataGrid != null)
            {
                FeeDueListDataGrid.ItemsSource = null;
                FeeDueListDataGrid.ItemsSource = FeeDueList;
            }
        }

        private void CalculateSumOfSelectedFees(ObservableCollection<FeeBalancesModel> objFeeBalancesModelList )
        {
            for (int countFeeBalances = 0; countFeeBalances < objFeeBalancesModelList.Count; countFeeBalances++)
            {
                if (objFeeBalancesModelList[countFeeBalances].IsSelected)
                    SumOfAllSelectedFees += objFeeBalancesModelList[countFeeBalances].balance_amount;
                else
                    SumOfAllSelectedFees -= objFeeBalancesModelList[countFeeBalances].balance_amount;
            }

        }

        private void CalculatePaymentTotals()
        {
            MakePayment.TotalOfSelectedFeeBalancesFee = 0;
            MakePayment.TotalOfSelectedFeeBalancesFine = 0;
            MakePayment.TotalOfSelectedFeeBalancesConcession = 0;
            MakePayment.TotalOfAlreadyPaidAmount = 0;
            MakePayment.TotalOfSelectedFeeBalancesGrandTotal = 0;
            MakePayment.TotalOfSelectedFeeBalancesAmount = 0;
            MakePayment.TotalBalance = 0;
            foreach (FeeBalancesModel objFeeBalance in MakePayment.SelectedFeeBalances)
            {
                MakePayment.TotalOfSelectedFeeBalancesFee += objFeeBalance.fee_amount;
                MakePayment.TotalOfSelectedFeeBalancesFine += objFeeBalance.fine;
                MakePayment.TotalOfSelectedFeeBalancesConcession += objFeeBalance.concession_amount;
                MakePayment.TotalOfAlreadyPaidAmount += objFeeBalance.paid_amount;
                MakePayment.TotalOfSelectedFeeBalancesGrandTotal += objFeeBalance.balance_amount;
                MakePayment.TotalOfSelectedFeeBalancesAmount += objFeeBalance.amount_to_pay;               
            }
            MakePayment.TotalBalance += MakePayment.TotalOfSelectedFeeBalancesFee - MakePayment.TotalOfAlreadyPaidAmount - MakePayment.TotalOfSelectedFeeBalancesConcession;
        }

        public void CalculateSumOfSelectedFeesAndPopulateSelectedFeeListForMakePayment()
        {
            this.SumOfAllSelectedFees = 0;
            this.MakePayment.SelectedFeeBalances = new ObservableCollection<FeeBalancesModel>();
            this.MakePayment.SelectedFeeBalances.CollectionChanged += SelectedFeeListForMakePayment_CollectionChanged;
            for (int count = 0; count < PendingMonthlyFeeList.Count; count++)
            {
                for (int countFeeBalances = 0; countFeeBalances < PendingMonthlyFeeList[count].FeeBalancesList.Count; countFeeBalances++)
                {
                    if (PendingMonthlyFeeList[count].FeeBalancesList[countFeeBalances].IsSelected)
                    {
                        MakePayment.SelectedFeeBalances.Add(PendingMonthlyFeeList[count].FeeBalancesList[countFeeBalances]);
                        SumOfAllSelectedFees += PendingMonthlyFeeList[count].FeeBalancesList[countFeeBalances].balance_amount;
                    }
                }
            }

        }        

        private void GetGlobalObjects()
        {
            //Get the Current Login
            _CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get College Info
            _SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
        }

        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }
        #endregion
    }
}
