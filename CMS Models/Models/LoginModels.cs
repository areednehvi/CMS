using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMS_Models.Models.DBModels;

namespace CMS.Models
{
    public class LoginModel :NotifyPropertyChanged
    {
        private UsersListModel _User;
        private string _message;

        public UsersListModel User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                OnPropertyChanged("User");
            }

        }

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }

        }

        
    }
}
