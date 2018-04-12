using RestSharp;

namespace xyj.core.QxClass
{
    public class HttpRequest : RestRequest
    {
        public HttpRequest(string resource, Method method) : base(resource, method)
        {
        }
    }
}
