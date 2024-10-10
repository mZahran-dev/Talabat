using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Talabat.APIS.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message) 
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "bad Request",
                401 => "UnAuthorized",
                404 => "Not Found",
                500 => "Server Error",
                _ => null,
            };
        }  
    }
}
