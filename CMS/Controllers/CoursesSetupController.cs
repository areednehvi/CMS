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
    public class CoursesSetupController :NotifyPropertyChanged
    {
        #region Fields
        private CoursesSetupModel _CoursesSetup;      

        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private ICommand _addNewCourseCommand;
        private ICommand _cancelNewCourseCommand;
        private ICommand _saveCoursesCommand;
        #endregion

        #region Constructor
        public CoursesSetupController()
        {

            _CoursesSetup = new CoursesSetupModel()
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
            this.CoursesSetup.PropertyChanged += (s, e) => {
                if (e.PropertyName == "SelectedItemInCoursesList")
                {
                    CoursesSetup.Course = CoursesSetup.SelectedItemInCoursesList;
                    this.ShowForm();
                }
            };

            

            //Get Initial Courses list
            this.GetCoursesList();

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _addNewCourseCommand = new RelayCommand(AddNewCourse, CanAddNewCourse);
            _cancelNewCourseCommand = new RelayCommand(CancelNewCourse, CanCancelNewCourse);
            _saveCoursesCommand = new RelayCommand(SaveCourses, CanSaveCourses);

            this.ShowList();
        }
        
        #endregion

        #region Properties

        public CoursesSetupModel CoursesSetup
        {
            get
            {
                return _CoursesSetup;
            }
            set
            {
                _CoursesSetup = value;
                OnPropertyChanged("CoursesSetup");
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
                CoursesSetup.pageNo++;
                CoursesSetup.PageNo = "Page No : " + CoursesSetup.pageNo;
                CoursesSetup.fromRowNo = CoursesSetup.toRowNo + 1;
                CoursesSetup.toRowNo = CoursesSetup.pageNo * CoursesSetup.NoOfRecordsPerPage;
                this.GetCoursesList();
                if (CoursesSetup.pageNo > 1 && CoursesSetup.CoursesList.Count == 0)
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
                if (CoursesSetup.pageNo > 1)
                {
                    CoursesSetup.pageNo--;
                    CoursesSetup.PageNo = "Page No : " + CoursesSetup.pageNo;
                    CoursesSetup.toRowNo = CoursesSetup.fromRowNo - 1;
                    CoursesSetup.fromRowNo = (CoursesSetup.toRowNo + 1) - CoursesSetup.NoOfRecordsPerPage;
                    this.GetCoursesList();
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

        #region AddNewCourseCommand

        public ICommand AddNewCourseCommand
        {
            get { return _addNewCourseCommand; }
        }


        public bool CanAddNewCourse(object obj)
        {
            return true;
        }


        public void AddNewCourse(object obj)
        {
            try
            {
                CoursesSetup.Course = new CoursesListModel()
                {
                    CreatedBy = CoursesSetup.CurrentLogin.User.full_name
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

        #region CancelNewCourseCommand

        public ICommand CancelNewCourseCommand
        {
            get { return _cancelNewCourseCommand; }
        }


        public bool CanCancelNewCourse(object obj)
        {
            return true;
        }


        public void CancelNewCourse(object obj)
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

        #region SaveCoursesCommand
        public ICommand SaveCoursesCommand
        {
            get { return _saveCoursesCommand; }
        }


        public bool CanSaveCourses(object obj)
        {
            return CoursesSetup.Course != null && CoursesSetup.Course.name != null;                
        }

        public void SaveCourses(object obj)
        {
            try
            {
                if (CoursesSetupManager.CreateOrModfiyCourses(CoursesSetup.Course, CoursesSetup.CurrentLogin, CoursesSetup.SchoolInfo))
                {
                    GeneralMethods.ShowNotification("Notification", "Course Saved Successfully");
                    this.GetCoursesList();
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

        
        private void GetCoursesList()
        {
            try
            {
                CoursesSetup.CoursesList = CoursesSetupManager.GetCoursesList(CoursesSetup.fromRowNo, CoursesSetup.toRowNo);
                CoursesSetup.NoRecordsFound = CoursesSetup.CoursesList.Count > 0 ? "Collapsed" : "Visible";
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
            CoursesSetup.ListVisibility = "Collapsed";
            CoursesSetup.FormVisibility = "Visible";
        }

        private void ShowList()
        {
            CoursesSetup.ListVisibility = "Visible";
            CoursesSetup.FormVisibility = "Collapsed";
        }

        private void GetGlobalObjects()
        {
            //Get the Current Login
            CoursesSetup.CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get College Info
            CoursesSetup.SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
        }

        private void ResetPagination()
        {
            CoursesSetup.fromRowNo = 1;
            CoursesSetup.pageNo = 1;
            CoursesSetup.PageNo = "Page No : " + CoursesSetup.pageNo;
            CoursesSetup.NoOfRecordsPerPage = CoursesSetup.NoOfRecords;
            CoursesSetup.toRowNo = CoursesSetup.pageNo * CoursesSetup.NoOfRecordsPerPage;
        }


        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            CoursesSetup.NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }


    }
}
