namespace Talabat.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode , string? message = null) {

            StatusCode = statusCode;
            Message = message ?? GetDefultMessageForStatusCode(statusCode);
        }

        private string ? GetDefultMessageForStatusCode ( int statusCode)
        {
            return statusCode switch
            {
                400 => " Bad Request",
                401 => " Authorithed , you are not",
                404 => " not found",
                500 => "execption is found ",
                _ => null
            };
        }
    }
}
