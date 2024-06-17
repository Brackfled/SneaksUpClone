﻿using Microsoft.AspNetCore.Http;
using Iyzipay;
using Microsoft.AspNetCore.Mvc;
using Iyzipay.Request;
using Iyzipay.Model;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using RestSharp;
using Microsoft.Extensions.Options;
using Domain.Models;
using Application.Services.PaymentService;
using Application.Features.Payments.Commands.BinCheck;
using Application.Features.Payments.Commands;
using Iyzipay.Model.V2.Transaction;
using Application.Features.Payments.Commands.ThreeDSPaymentInitialize;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : BaseController
{
    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly IPaymentService _paymentService;

    public PaymentController(System.Net.Http.HttpClient httpClient, IPaymentService paymentService)
    {
        _httpClient = httpClient;
        _paymentService = paymentService;
    }

    [HttpGet("ThreeDSInitialize")]
    public async Task<IActionResult> PayWithThreeDSInitialize()
    {
        ThreeDSInitializeCommand command = new ThreeDSInitializeCommand();
        ThreeDSInitializeResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("BinCheck")]
    public async Task<IActionResult> BinCheck([FromBody] string binNo)
    {
        BinCheckCommand command = new BinCheckCommand { BinNo = binNo };
        BinCheckResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("InstallmentInfo")]
    public async Task<IActionResult> GetInstallmentInfo([FromBody] InstallmentInfoCommand command)
    {
       InstallmentInfo response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("PayCallBack")]
    public async Task<IActionResult> PayCallBack([FromForm] IFormCollection callBackData)
    {

        return Ok(callBackData);
    }
}



public sealed record CallBackData (
        string Status,
        string PaymentId,
        string ConversationData,
        long ConversationId,
        string MdStatus
    );

