using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionRuler
{
    public class KnownException:Exception
    {
        private string _source;
        private string _method;
        
        public KnownException()
        {

        }

        public KnownException(string source, string method,string message)
            : base(message)
        {
            _source = source;
            _method = method;
        }

        public KnownException(string source, string method, string message, Exception innerException)
            : base(message, innerException)
        {
            _source = source;
            _method = method;
        }

        protected KnownException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public override string ToString()
        {
            StringBuilder sb=new StringBuilder(string.Format(CultureInfo.InvariantCulture,"{0}.{1}", _source,_method));
            Exception ex=InnerException;
            while(ex!=null)
            {
                if (ex is KnownException)
                {
                    sb.AppendFormat(CultureInfo.InvariantCulture, "->{0}", string.Format(CultureInfo.InvariantCulture,"{0}.{1}", ((KnownException)ex)._source,((KnownException)ex)._method));
                }
                else
                {
                    sb.AppendFormat(CultureInfo.InvariantCulture, "->{0}",ex.Message);
                }

                ex = ex.InnerException;
            }

            return sb.ToString();
        }
    }
}
