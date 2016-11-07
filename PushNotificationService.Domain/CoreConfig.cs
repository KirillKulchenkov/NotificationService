using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain
{
    public class CoreConfig
    {
        public int TriesCount => GetIntValue(ConfigurationManager.AppSettings["TriesCount"], 3);
        public string HostAddress => GetStrValue(ConfigurationManager.AppSettings["HostAddress"], "http://localhost:81/");
        protected int GetIntValue(string strValue, int defaultValue)
        {
            int intValue;
            return int.TryParse(strValue, out intValue) ? intValue : defaultValue;
        }

        protected string GetStrValue(string strValue, string defaultValue)
        {
            return string.IsNullOrEmpty(strValue) ? defaultValue : strValue;
        }

        protected bool GetBoolValue(string strValue, bool defaultValue)
        {
            bool boolValue;
            return bool.TryParse(strValue, out boolValue) ? boolValue : defaultValue;
        }
    }
}
