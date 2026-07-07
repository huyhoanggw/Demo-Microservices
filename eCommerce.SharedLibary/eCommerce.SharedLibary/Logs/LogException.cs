using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLibary.Logs
{
    public class LogException
    {
        public static void LogExceptions(Exception ex) 
        {
            LogToFile(ex.Message);
            LogToConsole(ex. Message);
            LogToDebugger(ex.Message);
        }

        public static void LogToDebugger(string message)
           => Logger.None.Debug(message);


        public static void LogToConsole(string message)
         => Logger.None.Warning(message);

        public static void LogToFile(string message)
          => Logger.None.Information(message);
    }
}
