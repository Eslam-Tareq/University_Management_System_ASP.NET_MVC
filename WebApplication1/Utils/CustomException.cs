namespace WebApplication1.Utils
{
    public class CustomException:Exception 
    {
        public int StatusCode { get; set; }
        public string Status { set; get; }
        public CustomException() : base()
        {
            StatusCode = StatusCodes.Status500InternalServerError;
            this.Status = $"{StatusCode}".StartsWith("4") ? "fail" : "error";

        }
        public CustomException(string _message,int _StatusCode= StatusCodes.Status500InternalServerError):base(_message)
        {
            StatusCode = _StatusCode;
            this.Status = $"{ StatusCode}".StartsWith("4") ? "fail" : "error";

        }
    }
}
