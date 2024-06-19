namespace WebAPI.Controllers.Dtos;

public class CallBackResponse
{
    public string Status { get; set; }
    public string PaymentId { get; set; }
    public string ConversationData { get; set; }
    public string MdStatus { get; set; }
    public string ConversationId { get; set; }
    public string Signature { get; set; }

    public CallBackResponse()
    {
        Status = string.Empty;
        PaymentId = string.Empty;
        ConversationData = string.Empty;
        ConversationId = string.Empty;
        Signature = string.Empty;
    }

    public CallBackResponse(string status, string paymentId, string conversationData, string mdStatus, string conversationId, string signature)
    {
        Status = status;
        PaymentId = paymentId;
        ConversationData = conversationData;
        MdStatus = mdStatus;
        ConversationId = conversationId;
        Signature = signature;
    }
}
