using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionRuler
{
    public class ExceptionTxtLogger:IExceptionHandler
    {
        private string _logFile;

        public void Initialize(object parameter)
        {
            _logFile = parameter as string;
        }

        public void HandleException(string source, string method, Exception ex)
        {
            if (string.IsNullOrEmpty(_logFile))
            {
                _logFile = GetType().Assembly.Location + ".txt";
            }

            string path = Path.GetDirectoryName(_logFile);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FileMode mode;
            if (File.Exists(_logFile))
            {
                mode = FileMode.Append;
            }
            else
            {
                mode = FileMode.Create;
            }

            using (FileStream fs = new FileStream(_logFile, mode, FileSystemRights.Write, FileShare.None,8, FileOptions.None))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "=={0}==={1}======", "Exception", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff")));
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "The method [{0}] encounterred an exception in source [{1}]", method, source));
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "Message:{0}", ex.Message));
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "StackTrace:{0}", ex.StackTrace));
                    sb.AppendLine("Inner exceptions:");

                    Exception inner = ex.InnerException;
                    while (inner != null)
                    {
                        sb.AppendLine(inner.Message);
                        inner = inner.InnerException;
                    }

                    writer.Write(sb.ToString());
                }
            }
        }
    }
}
