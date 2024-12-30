namespace TrackingOrderSystem.Exceptios
{
    public class ExceptionHttp : Exception
    {
        public int StatusCode { get; }
        public ExceptionHttp(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }  
}
