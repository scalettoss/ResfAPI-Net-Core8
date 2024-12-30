namespace TrackingOrderSystem.Responses
{
    public class ErrorResponse : ResponseBase
    {
        public int ErrorCode { get; set; }
        public ErrorResponse(string message, int errorCode, object data = null)
            : base(Status.Failure, message, data)
        {
            ErrorCode = errorCode;
        }
    }
}
