namespace TrackingOrderSystem.Responses
{
    public class SuccessResponse : ResponseBase
    {
        public SuccessResponse(string message, object data = null)
        : base(Status.Success, message, data) { }
    }
}
