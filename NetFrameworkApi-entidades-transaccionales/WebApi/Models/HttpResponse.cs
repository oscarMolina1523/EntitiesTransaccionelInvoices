using System.Net;

namespace WebApi.Models
{
    public class HttpResponse
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
    }
}