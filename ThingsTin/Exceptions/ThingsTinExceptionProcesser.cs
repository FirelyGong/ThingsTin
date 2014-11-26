using Common.AssemblyData;
using ExceptionRuler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsTin.Exceptions
{
    public class ThingsTinExceptionProcesser
    {
        private ExceptionManager _exceptionManager;

        public ThingsTinExceptionProcesser()
        {
            string path = new AssemblyDataHelper(new[] { "Ziwish" }).GetFilePath(@"exceptions.txt");
            _exceptionManager = new ExceptionManager();
            BLExceptionHandler = new ExceptionTxtLogger();
            BLExceptionHandler.Initialize(path);
        }

        public static readonly ThingsTinExceptionProcesser Instance = new ThingsTinExceptionProcesser();

        public IExceptionHandler BLExceptionHandler { get; set; }
        public IExceptionHandler UIExceptionHandler { get; set; }
        
        public void ProcessException(string source, string method, Exception ex)
        {
            _exceptionManager.ProcessException(source, method, ex, BLExceptionHandler, false);
            _exceptionManager.ProcessException(source, method, ex, UIExceptionHandler, false);
        }
    }
}
