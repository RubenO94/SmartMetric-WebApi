using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Exceptions
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }
}
