using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ladeskab
{
    public class LogFile
    {
        long _idCounter = 0;

        public void AddLogEntry(string message)
        {
            StringBuilder logString = new StringBuilder();

            logString.Append("Id: " + _idCounter + " - ");
            logString.Append("Log time: " + DateTime.Now + " - ");
            logString.Append("Message: " + message);

            File.WriteAllText("LogOutput.txt", logString.ToString());
            
            _idCounter++;
        }
    }
}
