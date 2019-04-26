using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CustomDev.Configuration
{
    public class ConfigurationLegacyProvider : ConfigurationProvider, IConfigurationSource
    {
        private string _assemblyPath;

        public ConfigurationLegacyProvider()
        {

        }

        public ConfigurationLegacyProvider(string assemblyPath)
        {
            _assemblyPath = assemblyPath;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return this;
        }

        public override void Load()
        {
            // We use reflection to get access to System.Configuration.Configuration. In that way,
            // if the class is not available (under DotNet Core environment), there is no compilation
            // error.
            Type configurationManagerType = Type.GetType("System.Configuration.ConfigurationManager, System.Configuration, Version = 4.0.0.0, Culture = neutral, PublicKeyToken = b03f5f7f11d50a3a");            

            if (configurationManagerType != null)
            {
                PropertyInfo indexerProperty;
                IEnumerable allKeysInstance;
                object appSettingsInstance = null;

                if (String.IsNullOrEmpty(_assemblyPath))
                {
                    PropertyInfo appSettingsProperty = configurationManagerType.GetProperty("AppSettings");
                    appSettingsInstance = appSettingsProperty.GetValue(null, null);

                    Type appSettingsType = appSettingsInstance.GetType();

                    PropertyInfo allKeysProperty = appSettingsType.GetProperty("AllKeys");
                    allKeysInstance = allKeysProperty.GetValue(appSettingsInstance) as IEnumerable;

                    PropertyInfo[] indexerProperties = appSettingsType.GetProperties().Where(x => x.Name == "Item" && x.GetMethod.GetParameters().FirstOrDefault(y => y.ParameterType == typeof(string)) != null).ToArray();
                    indexerProperty = indexerProperties.FirstOrDefault();

                    foreach (string key in allKeysInstance)
                    {
                        object value = indexerProperty.GetValue(appSettingsInstance, new object[] { key });
                        this.Data.Add(key, value.ToString());
                    }
                }
                else
                {
                    MethodInfo[] openExeConfigurationMethods = configurationManagerType.GetMethods().Where(x => x.Name == "OpenExeConfiguration").ToArray();
                    MethodInfo openExeConfigurationMethod = openExeConfigurationMethods.First(x => x.GetParameters().Length == 1 && x.GetParameters()[0].ParameterType == typeof(string));

                    object o = openExeConfigurationMethod.Invoke(null, new object[] { _assemblyPath });
                    if (o != null)
                    {
                        Type type = o.GetType();
                        PropertyInfo appSettingsProperty = type.GetProperty("AppSettings");
                        appSettingsInstance = appSettingsProperty.GetValue(o, null);
                        Type appSettingsType = appSettingsInstance.GetType();
                        PropertyInfo settingsProperty = appSettingsType.GetProperty("Settings");
                        appSettingsInstance = settingsProperty.GetValue(appSettingsInstance);

                        appSettingsType = appSettingsInstance.GetType();

                        PropertyInfo allKeysProperty = appSettingsType.GetProperty("AllKeys");
                        allKeysInstance = allKeysProperty.GetValue(appSettingsInstance) as IEnumerable;

                        PropertyInfo[] indexerProperties = appSettingsType.GetProperties().Where(x => x.Name == "Item" && x.GetMethod.GetParameters().FirstOrDefault(y => y.ParameterType == typeof(string)) != null).ToArray();
                        indexerProperty = indexerProperties.FirstOrDefault();

                        foreach (string key in allKeysInstance)
                        {                            
                            object value = indexerProperty.GetValue(appSettingsInstance, new object[] { key });
                            Type valueType = value.GetType();
                            PropertyInfo valueProperty = valueType.GetProperty("Value");
                            value = valueProperty.GetValue(value);
                            this.Data.Add(key, value.ToString());
                        }
                    }
                }                
            }
        }
    }
}
