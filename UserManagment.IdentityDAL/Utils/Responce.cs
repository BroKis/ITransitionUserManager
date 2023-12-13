
using UserManagement.IdentityDAL.Auxillary;

namespace UserManagement.IdentityDAL.Utils
{
    public class Response<T>
    {
        public T Data { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Description { get; set; } = string.Empty;

        public Response<T> GetResponse(T data, StatusCode code, string description)
        {
            return new Response<T> { Data = data, StatusCode = code, Description = description };
        }
    }
}
