namespace Talabat.APIS.Errors
{
    public class ApiValidationErroResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErroResponse() :base(400)
        {

        }
    }
}
