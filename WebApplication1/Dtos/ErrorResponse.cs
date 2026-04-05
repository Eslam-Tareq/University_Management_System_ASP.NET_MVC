namespace WebApplication1.Dtos
{
    public class ErrorResponse
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
