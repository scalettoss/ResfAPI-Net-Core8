namespace TrackingOrderSystem.Responses
{
    public class ResponseBase
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public enum Status { 
            Success, 
            Failure 
        }
        public Status ResponseStatus { get; set; }
        public ResponseBase(Status responseStatus, string message, object data = null)
        {
            ResponseStatus = responseStatus;
            Message = message;
            Data = data;
        }
    }
}
