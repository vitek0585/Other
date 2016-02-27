using System;
using System.Net;

namespace WorkDataMVC.Util.Exceptions
{
    public class HttpBinderException:Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpBinderException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}