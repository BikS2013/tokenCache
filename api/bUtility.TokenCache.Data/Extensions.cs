using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Data
{
    public static class Extensions
    {
        public static string ToJSON(this Exception ex)
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.Formatting = Newtonsoft.Json.Formatting.Indented;
            var text = Newtonsoft.Json.JsonConvert.SerializeObject(ex, settings);
            return text;
        }

        static string eventSource = "cache log";

        public static void ToEventLog(this string message, EventLogEntryType logType = EventLogEntryType.Information)
        {
            try
            {
                System.Diagnostics.EventLog.WriteEntry(
                    eventSource,
                    message,
                    logType);
            }
            catch
            {
                throw new Exception("Failed to Log Message: " + message);
            }
        }
        public static void ToEventLog(this Exception ex, EventLogEntryType logType = EventLogEntryType.Error)
        {
            try
            {
                System.Diagnostics.EventLog.WriteEntry(
                    eventSource,
                    ToJSON(ex),
                    logType);
            }
            catch
            {
                ex.Message.ToEventLog(EventLogEntryType.Error);
                throw new Exception("Error Logging Exception.", ex);
            }
        }
    }
}
