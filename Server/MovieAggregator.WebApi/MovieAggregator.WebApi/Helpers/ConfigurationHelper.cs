using System.ComponentModel;
using System.Configuration;

namespace MovieAggregator.WebApi.Helpers
{
    public static class ConfigurationHelper
    {
        public static T GetValueFromConfiguration<T>(string key)
        {
            string configValue = ConfigurationManager.AppSettings[key];
            try
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(configValue);
            }
            catch
            {
                return default(T);
            }
        }
    }
}