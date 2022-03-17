using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ladeskab_Classes
{
    public class LogFile
    {
        public long IdCounter { set; get; }

        public LogFile()
        {
            IdCounter = 0;
        }

        public void AddLogEntry(string message)
        {
            StringBuilder logString = new StringBuilder();

            logString.Append("Id: " + IdCounter + " - ");
            logString.Append("Log time: " + DateTime.Now + " - ");
            logString.Append("Message: " + message + "\n");

            File.WriteAllText("LogOutput.txt", logString.ToString());
            
            IdCounter++;
        }

        public void FakeAddLogEntry(string message, List<string> logList)
        {
            logList = new List<string>();
            StringBuilder logString = new StringBuilder();

            logString.Append("Id: " + IdCounter + " - ");
            logString.Append("Log time: " + DateTime.Now + " - ");
            logString.Append("Message: " + message + "\n");

            logList.Add(message);

            IdCounter++;
        }
    }
}
