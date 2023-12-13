using System.Net;

namespace ToDoApp.Models
{
    public class ResponseDto
    {
        public HttpStatusCode StatusCode { get; set; } =HttpStatusCode.OK;
        public string Message { get; set; }=string.Empty;
        public object Result { get; set; } = default!;
    }
}
