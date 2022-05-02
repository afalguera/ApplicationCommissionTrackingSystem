using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primesoft.Common.Web.Exceptions
{
    public class EmailSendingException : Exception
    {
        public EmailSendingException(String message) : base(message) { }
    }
}
