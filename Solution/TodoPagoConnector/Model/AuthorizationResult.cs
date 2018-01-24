using System.Collections.Generic;

namespace TodoPagoConnector.Model
{
    public class AuthorizationResult
    {
        public const string PUBLIC_REQUEST_KEY = "PublicRequestKey";
        public const string REQUEST_KEY = "RequestKey";
        public const string STATUS_CODE = "StatusCode";
        public const string STATUS_MESSAGE = "StatusMessage";
        public const string URL_REQUEST = "URL_Request";

        public AuthorizationResult(Dictionary<string, object> res)
        {
            StatusCode = (int)res[STATUS_CODE];
            StatusMessage = (string)res[STATUS_MESSAGE];
            URL_Request = (string)res[URL_REQUEST];
            RequestKey = (string)res[REQUEST_KEY];
            PublicRequestKey = (string)res[PUBLIC_REQUEST_KEY];
        }

        public string PublicRequestKey { get; private set; }
        public string RequestKey { get; private set; }
        public int StatusCode { get; private set; }
        public string StatusMessage { get; private set; }
        public string URL_Request { get; private set; }
    }
}