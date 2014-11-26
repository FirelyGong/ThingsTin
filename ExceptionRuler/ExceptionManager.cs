using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionRuler
{
    public class ExceptionManager
    {
        public void ProcessException(string source, string method, Exception exception, IExceptionHandler handler, bool rethrow)
        {
            if (handler != null)
            {
                handler.HandleException(source, method, exception);
            }

            if (rethrow)
            {
                throw new KnownException(source, method,
                    string.Format(CultureInfo.InvariantCulture,
                    "The method [{0}] encounterred an exception in source [{1}], Look into inner exception for detail.",
                    method, source), exception);
            }
        }
    }
}
