using System.Net;

namespace MagicVilMagicVilla_web.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsUsccess { get; set; } = true;
        public List<string> ErrorMessages  { get; set; }
        public object Result { get; set; }
    }
}
