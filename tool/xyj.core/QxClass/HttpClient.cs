using System;
using RestSharp;

namespace xyj.core.QxClass
{
    public class HttpClient : RestClient
    {
        public HttpClient(Uri baseUrl) : base(baseUrl)
        {
        }
        public HttpClient(string baseUrl) : base(baseUrl)
        {
        }
    }
}
