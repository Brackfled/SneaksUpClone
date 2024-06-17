using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands.BinCheck;
public class BinCheckResponse
{
    public string Bin { get; set; }
    public string CardType { get; set; }
    public string CardAssociation { get; set; }
    public string CardFamily { get; set; }
    public string BankName { get; set; }
    public long BankCode { get; set; }
    public int Commercial { get; set; }
    public string Locale { get; set; }
    public string ConversationId { get; set; }

    public BinCheckResponse()
    {
        Bin = string.Empty;
        CardType = string.Empty;
        CardAssociation = string.Empty;
        CardFamily = string.Empty;
        BankName = string.Empty;
        Locale = string.Empty;
        ConversationId = string.Empty;
    }

    public BinCheckResponse(string bin, string cardType, string cardAssociation, string cardFamily, string bankName, long bankCode, int commercial, string locale, string conversationId)
    {
        Bin = bin;
        CardType = cardType;
        CardAssociation = cardAssociation;
        CardFamily = cardFamily;
        BankName = bankName;
        BankCode = bankCode;
        Commercial = commercial;
        Locale = locale;
        ConversationId = conversationId;
    }
}
