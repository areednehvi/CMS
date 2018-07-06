using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class LicensingModel : NotifyPropertyChanged
    {
        private LicenseModel _License;

        public LicenseModel License
        {
            get
            {
                return _License;
            }
            set
            {
                _License = value;
            }
        }
    }
    public class LicenseModel 
    {     
        public LicenseModel()
        {
            RegistryFolder = @"SOFTWARE\TechnologFireOS\9a01c9fc-5b05-4d70-8ecf-aba9bc694f50";
            SaltValue = "aba9bc694f50";
            AttemptsLeftKey = "9a01c9fc-5b05-4d70-8ecf-aba9bc694f51";
            LicenseKey = "9a01c9fc-5b05-4d70-8ecf-aba9bc694f52";
        }
        public string RegistryFolder { get; set; }
        public string SaltValue { get; set; }
        public string AttemptsLeftKey { get; set; }
        public string LicenseKey { get; set; }
        public Int16 AttemptsLeftValue { get; set; }
        public string LicenseValue { get; set; }
    }
}
