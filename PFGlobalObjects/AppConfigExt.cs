//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace AppGlobals
{
    /// <summary>
    /// Routines for retrieving values for an application's configuration file.
    /// If configuration is for an application different from the calling application, 
    /// then the calling application must supply the path to the executable for which the configuration items will be read or modified.
    /// If no file name is supplied, the configuration for the calling application will be managed.
    /// </summary>
    public class AppConfigExt
    {
        private string _pathToExecutable = string.Empty;
        private Configuration _config = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AppConfigExt()
        {
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pathToExecutable">Path to executable for which the configuration items will be read or modified.</param>
        public AppConfigExt(string pathToExecutable)
        {
            _pathToExecutable = pathToExecutable;
            _config = ConfigurationManager.OpenExeConfiguration(pathToExecutable);
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <returns>String value if key found; otherwise an empty string is returned.</returns>
        public string GetConfigValue(string psKey)
        {
            string sConfigValue = string.Empty;
            try
            {
                sConfigValue = string.Empty;
                sConfigValue = _config.AppSettings.Settings[psKey].Value;
            }
            catch
            {
                sConfigValue = string.Empty;
            }

            if (sConfigValue == null)
                sConfigValue = string.Empty;

            return sConfigValue;

        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <returns>String value if key found; otherwise an empty string is returned.</returns>
        public string GetStringValueFromConfigFile(string psKey)
        {
            return GetStringValueFromConfigFile(psKey, string.Empty);
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <param name="psDefaultValue">Value to return if key not found.</param>
        /// <returns>String value if key found; otherwise an psDefaultValue is returned.</returns>
        public string GetStringValueFromConfigFile(string psKey, string psDefaultValue)
        {
            string sKey = psKey;
            if (sKey == null)
                sKey = string.Empty;
            string sConfigValue = GetConfigValue(sKey);
            if (sConfigValue.Length == 0)
                sConfigValue = psDefaultValue;
            return sConfigValue;
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <returns>Boolean value defined by the key if key is found; otherwise false is returned.</returns>
        public bool GetBooleanValueFromConfigFile(string psKey)
        {
            return GetBooleanValueFromConfigFile(psKey, "false");
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <param name="psDefaultValue">Value to return if key not found.</param>
        /// <returns>Boolean value defined by the key if key is found; otherwise psDefaultValue is returned.</returns>
        public bool GetBooleanValueFromConfigFile(string psKey, string psDefaultValue)
        {
            string sKey = psKey;
            if (sKey == null)
                sKey = string.Empty;
            bool bConfigValue = true;
            string sConfigValue = GetConfigValue(psKey);
            if (sConfigValue.Length == 0)
                sConfigValue = "False";
            bConfigValue = AppTextGlobals.ConvertStringToBoolean(sConfigValue, psDefaultValue);
            return bConfigValue;
        }


        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <returns>Int value defined by the key if key is found; otherwise 0 is returned.</returns>
        public int GetIntValueFromConfigFile(string psKey)
        {
            return GetIntValueFromConfigFile(psKey, 0);
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <param name="pnDefaultValue">Value to return if key not found.</param>
        /// <returns>Int value defined by the key if key is found; otherwise psDefaultValue is returned.</returns>
        public int GetIntValueFromConfigFile(string psKey, int pnDefaultValue)
        {
            string sKey = psKey;
            if (sKey == null)
                sKey = string.Empty;
            int nConfigValue = 0;
            string sConfigValue = GetConfigValue(psKey);
            nConfigValue = AppTextGlobals.ConvertStringToInt(sConfigValue, pnDefaultValue);
            return nConfigValue;
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <returns>Long value defined by the key if key is found; otherwise 0 is returned.</returns>
        public long GetLongValueFromConfigFile(string psKey)
        {
            return GetLongValueFromConfigFile(psKey, 0);
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <param name="pnDefaultValue">Value to return if key not found.</param>
        /// <returns>Long value defined by the key if key is found; otherwise psDefaultValue is returned.</returns>
        public long GetLongValueFromConfigFile(string psKey, long pnDefaultValue)
        {
            string sKey = psKey;
            if (sKey == null)
                sKey = string.Empty;
            long nConfigValue = 0;
            string sConfigValue = GetConfigValue(psKey);
            nConfigValue = AppTextGlobals.ConvertStringToLong(sConfigValue, pnDefaultValue);
            return nConfigValue;
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <returns>Float value defined by the key if key is found; otherwise 0.0 is returned.</returns>
        public float GetFloatValueFromConfigFile(string psKey)
        {
            return GetFloatValueFromConfigFile(psKey, (float)0.0);
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <param name="pnDefaultValue">Value to return if key not found.</param>
        /// <returns>Float value defined by the key if key is found; otherwise psDefaultValue is returned.</returns>
        public float GetFloatValueFromConfigFile(string psKey, float pnDefaultValue)
        {
            string sKey = psKey;
            if (sKey == null)
                sKey = string.Empty;
            float nConfigValue = (float)0.0;
            string sConfigValue = GetConfigValue(psKey);
            nConfigValue = AppTextGlobals.ConvertStringToFloat(sConfigValue, pnDefaultValue);
            return nConfigValue;
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <returns>Double value defined by the key if key is found; otherwise 0.0 is returned.</returns>
        public double GetDoubleValueFromConfigFile(string psKey)
        {
            return GetDoubleValueFromConfigFile(psKey, (double)0.0);
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <param name="pnDefaultValue">Value to return if key not found.</param>
        /// <returns>Double value defined by the key if key is found; otherwise psDefaultValue is returned.</returns>
        public double GetDoubleValueFromConfigFile(string psKey, double pnDefaultValue)
        {
            string sKey = psKey;
            if (sKey == null)
                sKey = string.Empty;
            double nConfigValue = (double)0.0;
            string sConfigValue = GetConfigValue(psKey);
            nConfigValue = AppTextGlobals.ConvertStringToDouble(sConfigValue, pnDefaultValue);
            return nConfigValue;
        }

        /// <summary>
        /// Retrieves all the app settings in the application configuration file.
        /// </summary>
        /// <returns>Collection containing all the application settings defined in the application configuration file.</returns>
        public KeyValueConfigurationCollection GetAllAppSettings()
        {
            //Configuration config
            //    = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            AppSettingsSection oAppSettingsSection = _config.AppSettings;
            KeyValueConfigurationCollection oAppSettings = oAppSettingsSection.Settings;

            return oAppSettings;
        }

        /// <summary>
        /// Retrieves the app settings for the executable specified in the psExecutableFilePath parameter.
        /// </summary>
        /// <param name="psExecutableFilePath">Path to executable file.</param>
        /// <returns>Collection containing all the application settings defined in the application configuration file for the specified executable file.</returns>
        public KeyValueConfigurationCollection GetAllAppSettings(string psExecutableFilePath)
        {
            if (File.Exists(psExecutableFilePath) == false)
                return null;

            Configuration config
                = ConfigurationManager.OpenExeConfiguration(psExecutableFilePath);

            AppSettingsSection oAppSettingsSection = config.AppSettings;
            KeyValueConfigurationCollection oAppSettings = oAppSettingsSection.Settings;

            return oAppSettings;
        }

        /// <summary>
        /// Modifies the configuration key specified in the parameter.
        /// </summary>
        /// <param name="configElement">Key/Value pair.</param>
        public void SetConfigValue(KeyValuePair<string, string> configElement)
        {
            //Configuration config
            //    = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            if (_config.AppSettings.Settings[configElement.Key] != null)
                _config.AppSettings.Settings[configElement.Key].Value = configElement.Value;
            else
                _config.AppSettings.Settings.Add(configElement.Key, configElement.Value);

            _config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// Modifies the configuration key specified in the parameter.
        /// </summary>
        /// <param name="configElements">Array of Key/Value pairs.</param>
        public void SetConfigValue(KeyValuePair<string, string>[] configElements)
        {
            //Configuration config
            //    = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            //config.AppSettings.Settings.Add(configKeyVal);
            if (configElements.Length > 0)
            {
                for (int i = 0; i < configElements.Length; i++)
                {
                    if (_config.AppSettings.Settings[configElements[i].Key] != null)
                        _config.AppSettings.Settings[configElements[i].Key].Value = configElements[i].Value;
                    else
                        _config.AppSettings.Settings.Add(configElements[i].Key, configElements[i].Value);
                }
            }

            _config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

        }

        /// <summary>
        /// Modifies the configuration key specified in the parameter.
        /// </summary>
        /// <param name="configElements">Collection of Key/Value pairs.</param>
        public void SetConfigValue(List<KeyValuePair<string, string>> configElements)
        {
            //Configuration config
            //    = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            //config.AppSettings.Settings.Add(configKeyVal);
            if (configElements.Count > 0)
            {
                for (int i = 0; i < configElements.Count; i++)
                {
                    if (_config.AppSettings.Settings[configElements[i].Key] != null)
                        _config.AppSettings.Settings[configElements[i].Key].Value = configElements[i].Value;
                    else
                        _config.AppSettings.Settings.Add(configElements[i].Key, configElements[i].Value);
                }
            }

            _config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

        }


    }//end class

}//end namespace
