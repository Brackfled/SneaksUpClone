
using Application.Features.Payments.Commands.ThreeDSPaymentInitialize;
using Iyzipay.Model;
using Iyzipay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PaymentService;
public  interface IPaymentService
{
    Task<BinNumber> GetBinCheck(RetrieveBinNumberRequest retrieveBinNumberRequest);
    Task<InstallmentInfo> GetInstallmentInfo(RetrieveInstallmentInfoRequest retrieveInstallmentInfoRequest);
    Task<ThreeDSInitializeResponse> ThreeDSPaymentInitialize(CreateThreedsPaymentRequest createThreedsPaymentRequest);
}
