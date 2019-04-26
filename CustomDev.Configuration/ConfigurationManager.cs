using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace CustomDev.Configuration
{
    public static class ConfigurationManager
    {
        private static IConfiguration _configuration;

        static ConfigurationManager()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.Add(new ConfigurationLegacyProvider());
            builder.AddJsonFile("appsettings.json", true);
            _configuration = builder.Build();
        }

        public static void Setup(ConfigurationBuilder builder)
        {
            _configuration = builder.Build();
        }

        #region Static method
        public static string GetString(string key, string defaultValue = null)
        {
            string value = _configuration[key];
            
            return String.IsNullOrEmpty(value) ? defaultValue : value;
        }

        public static bool? GetBool(string key)
        {
            string value = _configuration[key];
            bool result;

            if (bool.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static bool GetBool(string key, bool defaultValue)
        {
            string value = _configuration[key];
            bool result;

            if (bool.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public static T? GetEnum<T>(string key) where T:struct, IComparable, IFormattable, IConvertible
        {
            T result;
            string value = _configuration[key];

            if (Enum.TryParse<T>(value, out result))
            {
                return result;
            }
            else
            {
                return null;
            }            
        }

        public static T GetEnum<T>(string key, T defaultValue) where T : struct, IComparable, IFormattable, IConvertible
        {
            T result;
            string value = _configuration[key];

            if (Enum.TryParse<T>(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public static int? GetInt32(string key)
        {
            string value = _configuration[key];
            int result;

            if (int.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static int GetInt32(string key, int defaultValue)
        {
            string value = _configuration[key];
            int result;

            if (int.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
        #endregion       
    }
}
