using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands.ThreeDSPaymentInitialize;
public class ThreeDSInitializeResponse
{
    public string Status { get; set; }
    public string Locale { get; set; }
    public string ConversationId { get; set; }
    public string ThreeDSHtmlContent { get; set; }
    public string PaymentId { get; set; }
    public string Checksum { get; set; }
    public long SystemTime { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }

    public ThreeDSInitializeResponse()
    {
        Status = string.Empty;
        Locale = string.Empty;
        ConversationId = string.Empty;
        ThreeDSHtmlContent = string.Empty;
        PaymentId = string.Empty;
        Checksum = string.Empty;
        ErrorCode = string.Empty;
        ErrorMessage = string.Empty;
    }

    public ThreeDSInitializeResponse(string errorCode, string errorMessage, string status, string locale, string conversationId, string threeDSHtmlContent, string paymentId, string checksum, long systemTime)
    {
        Status = status;
        Locale = locale;
        ConversationId = conversationId;
        ThreeDSHtmlContent = threeDSHtmlContent;
        PaymentId = paymentId;
        Checksum = checksum;
        SystemTime = systemTime;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}
