using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace LocalNotes.Site.Server
{
    public class TextLogging
    {
        public void LogMessage(string logMessage)
        {
            DateTime now = DateTime.Now;
            string strLogFileExt = "log";
            string strLogPath = HttpContext.Current.Server.MapPath("~/Log");

            // Create Directory if it does not exist ...
            DirectoryInfo di = new DirectoryInfo(strLogPath);
            if (!di.Exists)
            {
                di.Create();
            }

            // Build the logging file name ...
            string strLogFile = BuildLogFileName(now, strLogPath, "MessageLog", strLogFileExt);

            // Add the entry to the logging file ...
            using (var writer = File.AppendText(strLogFile))
            {
                string strTime = now.ToString("MM/dd/yyyy hh:mm:ss tt");
                writer.WriteLine("{0}\t{1}", strTime, logMessage);
                writer.Close();
            }

            // Clean up any old log files

            // Get the DaysToRetainLog key value from the appSettings in the web.config file ...
            string strDaysToRetainLog = ConfigurationManager.AppSettings["DaysToRetainLog"];

            // Confirm the value is numeric ...
            if (int.TryParse(strDaysToRetainLog, out int intDaysToRetain))
            {
                // Send the details to the method to determine if the log files need to be removed ...
                RemoveOldLogFiles(strLogPath, intDaysToRetain, strLogFileExt);
            }
        }

        public void LogMessage(Exception ex)
        {
            DateTime now = DateTime.Now;
            string strLogFileExt = "log";
            string strLogPath = HttpContext.Current.Server.MapPath("~/Log");

            // Create Directory if it does not exist ...
            DirectoryInfo di = new DirectoryInfo(strLogPath);
            if (!di.Exists)
            {
                di.Create();
            }

            // Build the logging file name ...
            string strLogFile = BuildLogFileName(now, strLogPath, "ExceptionLog", strLogFileExt);

            string message = string.Format("Time: {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += new String('-', 60);
            message += Environment.NewLine;
            message += string.Format("Error HResult: {0}", ex.HResult);
            message += Environment.NewLine;
            message += string.Format("Error Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("Error Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("Stack Trace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Target Site: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += new String('-', 60);
            message += Environment.NewLine;

            using (var writer = File.AppendText(strLogFile))
            {
                writer.WriteLine(message);
                writer.Close();
            }

            // Clean up any old log files

            // Get the DaysToRetainLog key value from the appSettings in the web.config file ...
            string strDaysToRetainLog = ConfigurationManager.AppSettings["DaysToRetainLog"];

            // Confirm the value is numeric ...
            if (int.TryParse(strDaysToRetainLog, out int intDaysToRetain))
            {
                // Send the details to the method to determine if the log files need to be removed ...
                RemoveOldLogFiles(strLogPath, intDaysToRetain, strLogFileExt);
            }
        }

        #region HELPER SECTION

        /// <summary>
        /// Prepare the name of the file used to record the logging details.
        /// </summary>
        /// <param name="logDateTime">The date-time of the log entry.</param>
        /// <param name="logDirectory">">The folder path where log files are stored.</param>
        /// <param name="logNamePrefix">The text value to prepend the log file name with. Example: "ExceptionLog" or "MessageLog"</param>
        /// <param name="logFileExt">The file type extention for the log text file. Example: "log" or "txt"</param>
        /// <returns></returns>
        private string BuildLogFileName(DateTime logDateTime, string logDirectory, string logNamePrefix, string logFileExt)
        {
            //string strCurrentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string strReturn = String.Empty;
            //Reference clsKitRef = new Reference();
            //string strKitRef = String.Empty;

            // Present DateTime Format ...
            string strFormatDate = "yyyyMMdd";
            string strDate = logDateTime.ToString(strFormatDate);

            // ProductName_YYYYMMDD.ext
            string strFileName = String.Format("{0}_{1}.{2}", logNamePrefix, strDate, logFileExt);

            // Prepare the return by combining the folder path and the file name ...
            strReturn = System.IO.Path.Combine(logDirectory, strFileName);

            return strReturn;
        }

        /// <summary>
        /// Clear out any logging files which are past their retention period defined in days.
        /// </summary>
        /// <param name="logDirectory">The folder path where log files are stored.</param>
        /// <param name="logDaysToRetain">The number of days log files are retained.</param>
        /// <param name="logFileExt">The file type extention for the log text file. Example: "log" or "txt"</param>
        private void RemoveOldLogFiles(string logDirectory, int logDaysToRetain, string logFileExt)
        {
            //string strCurrentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string strReturn = String.Empty;
            //Reference clsKitRef = new Reference();
            //string strKitRef = String.Empty;

            string strSearchPattern = String.Format("*.{0}", logFileExt);
            var files = new DirectoryInfo(logDirectory).GetFiles(strSearchPattern);
            foreach (var file in files)
            {
                if (DateTime.UtcNow - file.CreationTimeUtc > TimeSpan.FromDays(logDaysToRetain))
                {
                    File.Delete(file.FullName);
                }
            }
        }

        #endregion
    }
}