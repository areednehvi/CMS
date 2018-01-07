using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CMS.Models;
using CMS.Shared;
using CMS.Views;
using SMS_Businness_Layer.Businness;
using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace CMS.Controllers
{
    public class SyncController :NotifyPropertyChanged
    {
        #region Fields
        private ICommand _SyncCommand;
        private ICommand _SyncAllCommand;
        private SyncModel _Sync;

        #endregion

        #region Constructor
        public SyncController()
        {
            _Sync = new SyncModel() {
                SyncModuleList = new List<SyncModule>() {
                    new SyncModule(){ Module = SyncModules.Users, SyncModuleProgress = new SyncProgressModel() },
                    new SyncModule(){ Module = SyncModules.Students, SyncModuleProgress = new SyncProgressModel() },
                    new SyncModule(){ Module = SyncModules.Grades, SyncModuleProgress = new SyncProgressModel() },
                    new SyncModule(){ Module = SyncModules.Transportation, SyncModuleProgress = new SyncProgressModel() },
                    new SyncModule(){ Module = SyncModules.Fees, SyncModuleProgress = new SyncProgressModel() },
                    new SyncModule(){ Module = SyncModules.Payments, SyncModuleProgress = new SyncProgressModel() }
                },
                SyncTableInfoList = SyncManager.GetSyncTableInfo(),
                SyncAllProgress = new SyncProgressModel()
            };
            //Initialise Commands
            _SyncCommand = new RelayCommand(StartSync, CanSync);
            _SyncAllCommand = new RelayCommand(StartSyncAll, CanSyncAll);
        }
        #endregion

        #region Properties
        
        public SyncModel Sync
        {
            get
            {
                return _Sync;
            }
            set
            {
                _Sync = value;
                OnPropertyChanged("SyncModel");
            }
        }
               
        #endregion

        #region SyncCommand
        public ICommand SyncCommand
        {
            get { return _SyncCommand; }
        }


        public bool CanSync(object obj)
        {
            bool IsEnabled = false;
            string SyncModuleName = (string)obj;

            if (SyncModules.Users == SyncModuleName)
                IsEnabled = true;
            else if (SyncModules.Students == SyncModuleName)
                IsEnabled = Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleStatus == SyncNotifications.SyncCompleted ? true : false;
            else if (SyncModules.Grades == SyncModuleName)
                IsEnabled = Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleStatus == SyncNotifications.SyncCompleted ? true : false;
            else if (SyncModules.Transportation == SyncModuleName)
                IsEnabled = Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus == SyncNotifications.SyncCompleted ? true : false;
            else if (SyncModules.Fees == SyncModuleName)
                IsEnabled = Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus == SyncNotifications.SyncCompleted ? true : false;
            else if (SyncModules.Payments == SyncModuleName)
                IsEnabled = Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus == SyncNotifications.SyncCompleted ? true : false;
            return IsEnabled;
        }


        public void StartSync(object obj)
        {
            try
            {
                Sync.CurrentSyncModule = (string)obj;

                ResetProgress();
                Sync.IsSyncInProgress = true;
                BackgroundWorker objBackgroundWorker = new BackgroundWorker();

                // Configure the function that will run when started
                objBackgroundWorker.DoWork += new DoWorkEventHandler(SyncRecords);

                /*The progress reporting is not needed with this implementation and is therefore
                commented out.  However, if our College App Grows into a more complex application, we may have a use for
                for this.

                //Enable progress and configure the progress function
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                */

                // Configure the function to run when completed
                objBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SyncCompleted);

                // Launch the sync               
                objBackgroundWorker.RunWorkerAsync();
                
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.Log(errorMessage);
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }

        }
        #endregion

        #region SyncAllCommand
        public ICommand SyncAllCommand
        {
            get { return _SyncAllCommand; }
        }


        public bool CanSyncAll(object obj)
        {
            if (Sync.IsSyncNotInProgress)
                return true;
            else
                return false;

        }


        public void StartSyncAll(object obj)
        {
            try
            {

                Sync.CurrentSyncModule = (string)obj;

                ResetAllProgress();
                Sync.IsSyncInProgress = true;
                BackgroundWorker objBackgroundWorker = new BackgroundWorker();

                // Configure the function that will run when started
                objBackgroundWorker.DoWork += new DoWorkEventHandler(SyncRecords);

                /*The progress reporting is not needed with this implementation and is therefore
                commented out.  However, if our College App Grows into a more complex application, we may have a use for
                for this.

                //Enable progress and configure the progress function
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                */

                // Configure the function to run when completed
                objBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SyncAllCompleted);

                // Launch the sync               
                objBackgroundWorker.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.Log(errorMessage);
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }

        }
        #endregion

        #region Private Functions
        private void CloseOtherWindows()
        {
            for (int intCounter = Application.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
            {
                if (Application.Current.Windows[intCounter].Name == WindowDefnitions.FeeCollectWindow)
                    Application.Current.Windows[intCounter].Close();
            }
        }

        private void ResetProgress()
        {
            Sync.SyncModuleList[SyncModules.GetSyncModuleID(Sync.CurrentSyncModule)].SyncModuleProgress.Progress = Sync.SyncModuleList[SyncModules.GetSyncModuleID(Sync.CurrentSyncModule)].SyncModuleProgress.Minimum;
        }
        private void ResetAllProgress()
        {
            for(int count = 0; count < Sync.SyncModuleList.Count; count++)
                Sync.SyncModuleList[count].SyncModuleProgress.Progress = Sync.SyncModuleList[count].SyncModuleProgress.Minimum;
            Sync.SyncAllProgress.Progress = Sync.SyncAllProgress.Minimum;
        }

        private void SyncRecords(object sender, DoWorkEventArgs e)
        {
            try
            {
                //BackgroundWorker worker = sender as BackgroundWorker;
                Sync.SyncStatus = SyncNotifications.CheckingInternetConnection;        
                if (!GeneralMethods.IsInternetAvailable())
                {
                    Sync.SyncStatus = SyncNotifications.InternetNotAvailable;
                    return;
                }
                Sync.SyncStatus = SyncNotifications.SyncStarted;
                GeneralMethods.Log(Sync.SyncStatus, true, SyncModules.All == Sync.CurrentSyncModule || SyncModules.Users == Sync.CurrentSyncModule ? false : true);

                Sync.SyncDBmodels = new SyncDBmodels();

                Sync.SyncAllProgress.Maximum = Sync.SyncModuleList.Count;

                if (SyncModules.Users == Sync.CurrentSyncModule)
                    SyncUsers();
                else if (SyncModules.Students == Sync.CurrentSyncModule)
                    SyncStudents();
                else if (SyncModules.Grades == Sync.CurrentSyncModule)
                    SyncGrades();
                else if (SyncModules.Transportation == Sync.CurrentSyncModule)
                    SyncTransportation();
                else if (SyncModules.Fees == Sync.CurrentSyncModule)
                    SyncFees();
                else if (SyncModules.Payments == Sync.CurrentSyncModule)
                    SyncPayments();
                else if (SyncModules.All == Sync.CurrentSyncModule)
                {
                    SyncUsers();
                    SyncStudents();
                    SyncGrades();
                    SyncTransportation();
                    SyncFees();
                    SyncPayments();
                }


                Sync.SyncStatus = SyncNotifications.SyncCompleted;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Does nothing yet
            throw new NotImplementedException();
        }

        void SyncCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Sync.IsSyncInProgress = false;
            if (Sync.SyncStatus == SyncNotifications.InternetNotAvailable)
                GeneralMethods.ShowNotification("Internet Not Available!", "Please check your Internet Connection!", true);
            else if (Sync.SyncStatus == SyncNotifications.SyncCompleted)
            {
                UpdateLastSyncDateForModules();

                GeneralMethods.ShowNotification("Sync Completed!", "Sync finished Successfully!");
                GeneralMethods.Log("Sync finished Successfully!",true);
            }

        }
        void SyncAllCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Sync.IsSyncInProgress = false;
                if (Sync.SyncStatus == SyncNotifications.InternetNotAvailable)
                    GeneralMethods.ShowNotification("Internet Not Available!", "Please check your Internet Connection!", true);
                else if (Sync.SyncStatus == SyncNotifications.SyncCompleted)
                {
                    UpdateLastSyncDateForModules();

                    GeneralMethods.ShowNotification("Sync Completed!", "Syncing all Modules finished Successfully!");
                    GeneralMethods.Log("Syncing all Modules finished Successfully!", true);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }           
        }
        #endregion

        
        #region Sync Functions

        #region 1. Users
        private Boolean SyncUsers()
        {
            Boolean IsSuccess = false;
            try
            {
                
                Sync.SyncStatus = SyncNotifications.SyncingModule + SyncModules.Users;
                GeneralMethods.Log(Sync.SyncStatus, true);

                GetUsersDataFromOnline();

                SyncUsersOnlineData();
                                                                
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleStatus = SyncNotifications.SyncCompleted;
                GeneralMethods.Log(SyncNotifications.SyncCompleted, true);

                Sync.SyncAllProgress.Progress++;
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return IsSuccess;         
        }

        private void GetUsersDataFromOnline()
        {
            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleStatus = SyncNotifications.GettingDataFromOnline;
            GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleStatus);

            Sync.SyncDBmodels.usersList = SyncManager.GetUsersFromOnline();
            Sync.SyncDBmodels.user_avatar_filesList = SyncManager.GetUserAvatarFilesFromOnline();
            Sync.SyncDBmodels.filesList = SyncManager.GetFilesFromOnline();

            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleProgress.Maximum = 
                Sync.SyncDBmodels.usersList.Count + 
                Sync.SyncDBmodels.user_avatar_filesList.Count + 
                Sync.SyncDBmodels.filesList.Count;
        }

        private void SyncUsersOnlineData()
        {
            //Get API Request
            /*string url = "http://arifnazir.000webhostapp.com/api/api.php";
            WebRequest request = WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";
            string responseString;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                responseString = sr.ReadToEnd();
            }

            var obj = JObject.Parse(responseString);
            var data = obj["data"];
            SchoolClass objSchoolClass = JsonConvert.DeserializeObject<SchoolClass>(data.ToString());*/

            //POST API Request
            /*var values = new Dictionary<string, string>
            {
               { "thing1", "hello" },
               { "thing2", "world" }
            };*/


            //var json = JsonConvert.SerializeObject(values);

            string json = "{\"user\":\"test\"," +
                  "\"password\":\"bla\"}";

            var url = "http://arifnazir.000webhostapp.com/api/api.php";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            //request.ContentLength = json.Length;
            httpWebRequest.Method = "POST";

            using (var stream = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                stream.Write(json);
            }

            var response = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }
            var obj = JObject.Parse(json);
            var data = obj["data"];
            SchoolClass objSchoolClass = new SchoolClass();
            objSchoolClass = JsonConvert.DeserializeObject<SchoolClass>(data.ToString());

            //users
            for (int count = 0; count < Sync.SyncDBmodels.usersList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.users + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleStatus);                

                string i = Sync.SyncDBmodels.usersList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.users)].LastSyncedOn = DateTime.Now;

            //user_avatar_files
            for (int count = 0; count < Sync.SyncDBmodels.user_avatar_filesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.user_avatar_files + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.user_avatar_filesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.user_avatar_files)].LastSyncedOn = DateTime.Now;

            //files
            for (int count = 0; count < Sync.SyncDBmodels.filesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.files + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.filesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Users)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.files)].LastSyncedOn = DateTime.Now;
        }        

        #endregion

        #region 2. Students

        private Boolean SyncStudents()
        {
            Boolean IsSuccess = false;
            try
            {
                Sync.SyncStatus = SyncNotifications.SyncingModule + SyncModules.Students;
                GeneralMethods.Log(Sync.SyncStatus,true);

                GetStudentsDataFromOnline();

                SyncStudentsOnlineData();
                                              
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleStatus = SyncNotifications.SyncCompleted;
                GeneralMethods.Log(SyncNotifications.SyncCompleted, true);

                Sync.SyncAllProgress.Progress++;
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return IsSuccess;
        }

        private void GetStudentsDataFromOnline()
        {
            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleStatus = SyncNotifications.GettingDataFromOnline;
            GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleStatus);

            Sync.SyncDBmodels.studentsList = SyncManager.GetStudentsFromOnline();
            Sync.SyncDBmodels.parentsList = SyncManager.GetParentsFromOnline();

            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleProgress.Maximum = 
                Sync.SyncDBmodels.studentsList.Count + 
                Sync.SyncDBmodels.parentsList.Count;
        }

        private void  SyncStudentsOnlineData()
        {
            //students
            for (int count = 0; count < Sync.SyncDBmodels.studentsList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.students + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.studentsList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.students)].LastSyncedOn = DateTime.Now;


            //parents
            Sync.SyncDBmodels.parentsList = SyncManager.GetParentsFromOnline();
            for (int count = 0; count < Sync.SyncDBmodels.parentsList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.parents + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.parentsList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.parents)].LastSyncedOn = DateTime.Now;
        }

        #endregion

        #region 3. Grades

        private Boolean SyncGrades()
        {
            Boolean IsSuccess = false;
            try
            {
                Sync.SyncStatus = SyncNotifications.SyncingModule + SyncModules.Grades;
                GeneralMethods.Log(Sync.SyncStatus, true);

                GetGradesDataFromOnline();

                SyncGradesOnlineData();
                                               
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus = SyncNotifications.SyncCompleted;
                GeneralMethods.Log(SyncNotifications.SyncCompleted, true);

                Sync.SyncAllProgress.Progress++;
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return IsSuccess;
        }

        private void GetGradesDataFromOnline()
        {
            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus = SyncNotifications.GettingDataFromOnline;
            GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus);

            Sync.SyncDBmodels.gradesList = SyncManager.GetGradesFromOnline();
            Sync.SyncDBmodels.sectionsList = SyncManager.GetSectionsFromOnline();
            Sync.SyncDBmodels.sessionsList = SyncManager.GetSessionsFromOnline();
            Sync.SyncDBmodels.student_grade_session_logList = SyncManager.GetStudentGradeSessionLogFromOnline();

            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleProgress.Maximum = 
                Sync.SyncDBmodels.gradesList.Count + 
                Sync.SyncDBmodels.sectionsList.Count + 
                Sync.SyncDBmodels.sessionsList.Count + 
                Sync.SyncDBmodels.student_grade_session_logList.Count;
        }

        private void SyncGradesOnlineData()
        {
            //grades
            for (int count = 0; count < Sync.SyncDBmodels.gradesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.grades + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.gradesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.grades)].LastSyncedOn = DateTime.Now;

            //sections                
            for (int count = 0; count < Sync.SyncDBmodels.sectionsList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.sections + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.sectionsList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.sections)].LastSyncedOn = DateTime.Now;

            //sessions                
            for (int count = 0; count < Sync.SyncDBmodels.sessionsList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.sessions + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.sessionsList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.sessions)].LastSyncedOn = DateTime.Now;

            //student_grade_session_log                
            for (int count = 0; count < Sync.SyncDBmodels.student_grade_session_logList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.student_grade_session_log + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.student_grade_session_logList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Grades)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.student_grade_session_log)].LastSyncedOn = DateTime.Now;
        }

        #endregion

        #region 4. Transportation
        private Boolean SyncTransportation()
        {
            Boolean IsSuccess = false;
            try
            {
                Sync.SyncStatus = SyncNotifications.SyncingModule + SyncModules.Transportation;
                GeneralMethods.Log(Sync.SyncStatus, true);

                GetTransportationDataFromOnline();

                SyncTransportationOnlineData();                

                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus = SyncNotifications.SyncCompleted;
                GeneralMethods.Log(SyncNotifications.SyncCompleted, true);

                Sync.SyncAllProgress.Progress++;
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return IsSuccess;
        }

        private void GetTransportationDataFromOnline()
        {
            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus = SyncNotifications.GettingDataFromOnline;
            GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus);

            Sync.SyncDBmodels.vehiclesList = SyncManager.GetVehiclesFromOnline();
            Sync.SyncDBmodels.routesList = SyncManager.GetRoutesFromOnline();
            Sync.SyncDBmodels.route_stopsList = SyncManager.GetRouteStopsFromOnline();
            Sync.SyncDBmodels.route_vehiclesList = SyncManager.GetRouteVehiclesFromOnline();
            Sync.SyncDBmodels.route_vehicle_stopsList = SyncManager.GetRouteVehicleStopsFromOnline();         
            Sync.SyncDBmodels.trip_stopsList = SyncManager.GetTripStopsFromOnline();

            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleProgress.Maximum =
                Sync.SyncDBmodels.vehiclesList.Count +
                Sync.SyncDBmodels.routesList.Count +
                Sync.SyncDBmodels.route_stopsList.Count +
                Sync.SyncDBmodels.route_vehiclesList.Count +
                Sync.SyncDBmodels.route_vehicle_stopsList.Count +
                Sync.SyncDBmodels.trip_stopsList.Count;
        }

        private void SyncTransportationOnlineData()
        {
            //vehicles
            for (int count = 0; count < Sync.SyncDBmodels.vehiclesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.vehicles + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.vehiclesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.vehicles)].LastSyncedOn = DateTime.Now;

            //routes
            for (int count = 0; count < Sync.SyncDBmodels.routesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.routes + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.routesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.routes)].LastSyncedOn = DateTime.Now;

            //route_stops
            for (int count = 0; count < Sync.SyncDBmodels.route_stopsList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.route_stops + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.route_stopsList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.route_stops)].LastSyncedOn = DateTime.Now;

            //route_vehicles
            for (int count = 0; count < Sync.SyncDBmodels.route_vehiclesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.route_vehicles + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.route_vehiclesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.route_vehicles)].LastSyncedOn = DateTime.Now;

            //route_vehicle_stops
            for (int count = 0; count < Sync.SyncDBmodels.route_vehicle_stopsList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.route_vehicle_stops + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.route_vehicle_stopsList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.route_vehicle_stops)].LastSyncedOn = DateTime.Now;

            //trip_stops
            for (int count = 0; count < Sync.SyncDBmodels.trip_stopsList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.trip_stops + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.trip_stopsList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Transportation)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.trip_stops)].LastSyncedOn = DateTime.Now;
        }
        #endregion

        #region 5. Fees
        private Boolean SyncFees()
        {
            Boolean IsSuccess = false;
            try
            {
                Sync.SyncStatus = SyncNotifications.SyncingModule + SyncModules.Fees;
                GeneralMethods.Log(Sync.SyncStatus, true);

                GetFeesDataFromOnline();

                SyncFeesOnlineData();

                GetFeesDataFromOffline();

                SyncFeesOfflineData();

                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus = SyncNotifications.SyncCompleted;
                GeneralMethods.Log(SyncNotifications.SyncCompleted, true);

                Sync.SyncAllProgress.Progress++;
                IsSuccess = true;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return IsSuccess;
        }        

        private void GetFeesDataFromOnline()
        {
            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus = SyncNotifications.GettingDataFromOnline;
            GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus);

            Sync.SyncDBmodels.fee_categoriesList = SyncManager.GetFeeCategoriesFromOnline();
            Sync.SyncDBmodels.feesList = SyncManager.GetFeesFromOnline();
            Sync.SyncDBmodels.grade_feesList = SyncManager.GetGradeFeesFromOnline();
            Sync.SyncDBmodels.student_feesList = SyncManager.GetStudentFeesFromOnline();
            Sync.SyncDBmodels.route_vehicle_stops_fee_logsList = SyncManager.GetRouteVehicleStopsFeeLogsFromOnline();

            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleProgress.Maximum =
                Sync.SyncDBmodels.fee_categoriesList.Count +
                Sync.SyncDBmodels.feesList.Count +
                Sync.SyncDBmodels.grade_feesList.Count +
                Sync.SyncDBmodels.student_feesList.Count +
                Sync.SyncDBmodels.route_vehicle_stops_fee_logsList.Count;
        }

        private void SyncFeesOnlineData()
        {
            //fee_categories
            for (int count = 0; count < Sync.SyncDBmodels.fee_categoriesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.fee_categories + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.fee_categoriesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.fee_categories)].LastSyncedOn = DateTime.Now;

            //fees               
            for (int count = 0; count < Sync.SyncDBmodels.feesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.fees + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.feesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.fees)].LastSyncedOn = DateTime.Now;

            //grade_fees               
            for (int count = 0; count < Sync.SyncDBmodels.grade_feesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.grade_fees + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.grade_feesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.grade_fees)].LastSyncedOn = DateTime.Now;

            //student_fees               
            for (int count = 0; count < Sync.SyncDBmodels.student_feesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.student_fees + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Students)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.student_feesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            //Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.student_fees)].LastSyncedOn = DateTime.Now;
            //Update Last Sync when syn is done both online and offline

            //route_vehicle_stops_fee_logs               
            for (int count = 0; count < Sync.SyncDBmodels.route_vehicle_stops_fee_logsList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.route_vehicle_stops_fee_logs + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.route_vehicle_stops_fee_logsList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.route_vehicle_stops)].LastSyncedOn = DateTime.Now;
        }
        private void GetFeesDataFromOffline()
        {
            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus = SyncNotifications.SendingDataToOnline;
            GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus);

            Sync.SyncDBmodels.student_feesList = SyncManager.GetStudentFeesFromOffline();

            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleProgress.Maximum =
               Sync.SyncDBmodels.student_feesList.Count;
        }
        private void SyncFeesOfflineData()
        {
            //student_fees
            for (int count = 0; count < Sync.SyncDBmodels.student_feesList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.student_fees + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.student_feesList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Fees)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.student_fees)].LastSyncedOn = DateTime.Now;
        }

        
        #endregion

        #region 6. Payments
        private Boolean SyncPayments()
        {
            Boolean IsSuccess = false;
            try
            {
                Sync.SyncStatus = SyncNotifications.SyncingModule + SyncModules.Payments;
                GeneralMethods.Log(Sync.SyncStatus, true);

                GetPaymentsDataFromOnline();

                SyncPaymentsOnlineData();

                GetPaymentsDataFromOffline();

                SyncPaymentsOfflineData();

                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleStatus = SyncNotifications.SyncCompleted;
                GeneralMethods.Log(SyncNotifications.SyncCompleted, true);

                Sync.SyncAllProgress.Progress++;
                IsSuccess = true;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return IsSuccess;
        }        

        private void GetPaymentsDataFromOnline()
        {
            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleStatus = SyncNotifications.GettingDataFromOnline;
            GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleStatus);

            Sync.SyncDBmodels.student_paymentsList = SyncManager.GetStudentPaymentsFromOnline();

            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleProgress.Maximum =
                Sync.SyncDBmodels.student_paymentsList.Count;
        }

        private void SyncPaymentsOnlineData()
        {
            //student_payments
            for (int count = 0; count < Sync.SyncDBmodels.student_paymentsList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.student_payments + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.student_paymentsList[count].id_offline;
                SyncManager.SyncStudent_paymentsFromOnline(Sync.SyncDBmodels.student_paymentsList[count]);
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleProgress.Progress++;
            }
            
        }
        private void GetPaymentsDataFromOffline()
        {
            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleStatus = SyncNotifications.SendingDataToOnline;
            GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleStatus);

            Sync.SyncDBmodels.student_paymentsList = SyncManager.GetStudentPaymentsFromOffline();

            Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleProgress.Maximum =
                Sync.SyncDBmodels.student_paymentsList.Count;
        }

        private void SyncPaymentsOfflineData()
        {
            //student_payments
            for (int count = 0; count < Sync.SyncDBmodels.student_paymentsList.Count; count++)
            {
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleStatus = SyncNotifications.SyncInProgress + " Table: " + Tables.student_payments + " Record: " + count;
                GeneralMethods.Log(Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleStatus);

                string i = Sync.SyncDBmodels.student_paymentsList[count].id_offline;
                //Thread.Sleep(1);
                Sync.SyncModuleList[SyncModules.GetSyncModuleID(SyncModules.Payments)].SyncModuleProgress.Progress++;
            }
            //update LastSyncDate
            Sync.SyncTableInfoList[Sync.SyncTableInfoList.FindIndex(r => r.TableName == Tables.student_payments)].LastSyncedOn = DateTime.Now;
        }
        #endregion

        private Boolean UpdateLastSyncDateForModules()
        {
            try
            {
                return SyncManager.UpdateSyncTableInfo(Sync.SyncTableInfoList);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            
        }

        #endregion

    }

    public class SchoolClass {
        public string java { get; set; }
        public string php { get; set; }
    }
}
