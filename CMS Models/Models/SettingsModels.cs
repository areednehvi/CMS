using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class SettingsModel :NotifyPropertyChanged
    {     
        private ObservableCollection<NoOfRowsInGridsModel> _NoOfRowsInGrids;
        private Boolean _AlwaysShowMenuBar;
        private Boolean _DisableSUAdminAccount;
        public ObservableCollection<NoOfRowsInGridsModel> NoOfRowsInGrids
        {
            get
            {
                return _NoOfRowsInGrids;
            }
            set
            {
                _NoOfRowsInGrids = value;
                OnPropertyChanged("NoOfRowsInGrids");
            }
        }

        public Boolean AlwaysShowMenuBar
        {
            get
            {
                return _AlwaysShowMenuBar;
            }
            set
            {
                _AlwaysShowMenuBar = value;
                OnPropertyChanged("AlwaysShowMenuBar");
            }
        }

        public Boolean DisableSUAdminAccount
        {
            get
            {
                return _DisableSUAdminAccount;
            }
            set
            {
                _DisableSUAdminAccount = value;
                OnPropertyChanged("DisableSUAdminAccount");
            }
        }

        
    }
    public class NoOfRowsInGridsModel
    {
        public string name { get; set; }
        public string id { get; set; }
    }
}
