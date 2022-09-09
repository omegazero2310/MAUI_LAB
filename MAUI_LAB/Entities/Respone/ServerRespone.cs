using System.Net;

namespace MAUI_LAB.Entities.Respone
{
    public class ServerRespone
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
