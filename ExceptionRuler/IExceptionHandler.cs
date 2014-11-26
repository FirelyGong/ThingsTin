using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionRuler
{
    public interface IExceptionHandler
    {
        void Initialize(object parameter);

        void HandleException(string source, string method, Exception ex);
    }
}
