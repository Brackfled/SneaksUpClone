using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands.ThreeDSPaymentInitialize;
public class ThreeDSPaymentDto
{
    public string PaidPrice {  get; set; }
    public int Installment {  get; set; }
    public string CardHolderName { get; set; }
    public string CardNumber { get; set; }
    public string ExpireYear { get; set; }
    public string ExpireMonth { get; set; }
    public string Cvc {  get; set; }
    public string IdentityNumber { get; set; }
    public string GsmNumber { get; set; }
    public string Currency { get; set; }

    public ThreeDSPaymentDto()
    {
        PaidPrice = string.Empty;
        CardHolderName = string.Empty;
        CardNumber = string.Empty;
        ExpireYear = string.Empty;
        ExpireMonth = string.Empty;
        Cvc = string.Empty;
        IdentityNumber = string.Empty;
        GsmNumber = string.Empty;
        Currency = string.Empty;
    }

    public ThreeDSPaymentDto(string paidPrice, int ınstallment, string cardHolderName, string cardNumber, string expireYear, string expireMonth, string cvc, string ıdentityNumber, string gsmNumber, string currency)
    {
        PaidPrice = paidPrice;
        Installment = ınstallment;
        CardHolderName = cardHolderName;
        CardNumber = cardNumber;
        ExpireYear = expireYear;
        ExpireMonth = expireMonth;
        Cvc = cvc;
        IdentityNumber = ıdentityNumber;
        GsmNumber = gsmNumber;
        Currency = currency;
    }
}
