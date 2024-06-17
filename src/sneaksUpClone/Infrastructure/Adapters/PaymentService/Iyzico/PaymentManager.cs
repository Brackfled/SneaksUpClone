using Application.Features.Payments.Commands.ThreeDSPaymentInitialize;
using Application.Services.PaymentService;
using Domain.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters.PaymentService.Iyzico;
public class PaymentManager : IPaymentService
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public PaymentManager(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private Iyzipay.Options options = new()
    {
        ApiKey = "sandbox-fKbJTebKx5dC4YnMPheD5L60HzlFPwLf",
        SecretKey = "sandbox-srhfAz9Bb5ZVNTunIRd8JPE5brcDjmj1",
        BaseUrl = "https://sandbox-api.iyzipay.com"
    };

    private string text = DateTime.Now.ToString("ddMMyyyyhhmmssffff");

    public async Task<InstallmentInfo> GetInstallmentInfo(RetrieveInstallmentInfoRequest retrieveInstallmentInfoRequest)
    {
        InstallmentInfo installmentInfo = InstallmentInfo.Retrieve(retrieveInstallmentInfoRequest,options);
        return installmentInfo;
    }

    public async Task<BinNumber> GetBinCheck(RetrieveBinNumberRequest retrieveBinNumberRequest)
    {
        BinNumber binNumber = BinNumber.Retrieve(retrieveBinNumberRequest,options);
        return binNumber;
    }

    public async Task<ThreeDSInitializeResponse> ThreeDSPaymentInitialize(CreateThreedsPaymentRequest createThreedsPaymentRequest)
    {


        string hashedString = HashGenerator.GenerateHash(options.ApiKey, options.SecretKey, text, createThreedsPaymentRequest);
        string authorizationString = $"IYZWS {options.ApiKey}:{hashedString}";

        Dictionary<string, string> headers = new Dictionary<string, string>
        {
            {"Accept","application/json"},
            {"x-iyzi-rnd", text},
            { "x-iyzi-client-version", IyzipayConstants.CLIENT_VERSION},
            { "Authorization", authorizationString}
        };

        HttpRequestMessage httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(options.BaseUrl + "/payment/3dsecure/initialize"),
            Content = JsonBuilder.ToJsonString(createThreedsPaymentRequest)
        };

        foreach (KeyValuePair<string, string> header in headers)
        {
            httpRequestMessage.Headers.Add(header.Key, header.Value);
        }

        HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);

        string result = response.Content.ReadAsStringAsync().Result;

        ThreeDSInitializeResponse serializedResponse = JsonConvert.DeserializeObject<ThreeDSInitializeResponse>(result);
        return serializedResponse;
    }
}
