using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string> Failures { get; }

        public ValidationException(IDictionary<string, string> failures) : base("Validation failed.")
        {
            Failures = failures;
        }
    }
}
