using CMS.Models;
using Microsoft.Win32;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SMS_Models.Models.DBModels;

namespace SMS_Businness_Layer.Businness
{
    public class LicensingManager
    {
        public static Boolean SetLicense(string license)
        {
            Boolean isSuccess = false;
            try
            {
                LicenseModel objLicense = new LicenseModel()
                {
                     AttemptsLeftValue = 100,
                     LicenseValue = license
                };     
                RegistryKey key = Registry.CurrentUser.CreateSubKey(objLicense.RegistryFolder);
                string attemptsLeftEncodedValue = GeneralMethods.Encode(objLicense.SaltValue + objLicense.AttemptsLeftValue + objLicense.SaltValue);
                string licenseEncodedValue = GeneralMethods.Encode(objLicense.SaltValue + objLicense.LicenseValue + objLicense.SaltValue);

                key.SetValue(objLicense.AttemptsLeftKey, attemptsLeftEncodedValue);
                key.SetValue(objLicense.LicenseKey, licenseEncodedValue);
                key.Close();

                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return isSuccess;
        }
        public static Boolean IsCMSInstalledBefore()
        {
            Boolean isInstalled = false;
            try
            {
                LicenseModel objLicense = new LicenseModel();
                RegistryKey key = Registry.CurrentUser.OpenSubKey(objLicense.RegistryFolder,true);
                if (key != null)
                {
                    isInstalled = true;
                    key.Close();
                }         
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return isInstalled;
        }
        public static LicenseModel GetLicense()
        {
            LicenseModel objLicense = new LicenseModel();
            try
            {              
                RegistryKey key = Registry.CurrentUser.OpenSubKey(objLicense.RegistryFolder,true);
                if (key != null)
                {
                    //Get No of attempts Left
                    string attemptsLeftEncodedValue = key.GetValue(objLicense.AttemptsLeftKey).ToString();
                    objLicense.AttemptsLeftValue = Convert.ToInt16(GeneralMethods.Decode(attemptsLeftEncodedValue).Replace(objLicense.SaltValue, string.Empty));

                    //Get license key
                    string licenseKeyEncodedValue = key.GetValue(objLicense.LicenseKey).ToString();
                    objLicense.LicenseValue = GeneralMethods.Decode(licenseKeyEncodedValue).Replace(objLicense.SaltValue, string.Empty);

                    key.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objLicense;
        }
        public static Boolean DecreaseNoOfAttemptsLeft()
        {
            Boolean isSuccess = false;
            LicenseModel objLicense = new LicenseModel();
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(objLicense.RegistryFolder,true);
                if (key != null)
                {
                    //Get No of attempts Left
                    string attemptsLeftEncodedValue = key.GetValue(objLicense.AttemptsLeftKey).ToString();
                    objLicense.AttemptsLeftValue = Convert.ToInt16(GeneralMethods.Decode(attemptsLeftEncodedValue).Replace(objLicense.SaltValue, string.Empty));

                    objLicense.AttemptsLeftValue--;

                    //Write Back
                    attemptsLeftEncodedValue = GeneralMethods.Encode(objLicense.SaltValue + objLicense.AttemptsLeftValue + objLicense.SaltValue);
                    key.SetValue(objLicense.AttemptsLeftKey, attemptsLeftEncodedValue);
                    

                    key.Close();
                    isSuccess = true;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return isSuccess;
        }
        public static string ValidateLicense(LicenseModel objLicense)
        {
            string responseString = string.Empty;
            try
            {

                string url = "http://kashmirhunt.com/areed/EducationCRM/index.php?EducationKey=" + objLicense.EducationKey + "&License=" + objLicense.LicenseValue;
                WebRequest request = WebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "GET";                
                var response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    responseString = sr.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return responseString;
        }

    }


}
