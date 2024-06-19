using Application.Features.Baskets.Constants;
using Application.Features.Payments.Constants;
using Application.Services.PaymentService;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Rules;
public class PaymentBusinessRules : BaseBusinessRules
{
    private readonly IPaymentService _paymentService;
    private readonly ILocalizationService _localizationService;

    public PaymentBusinessRules(IPaymentService paymentService, ILocalizationService localizationService)
    {
        _paymentService = paymentService;
        _localizationService = localizationService;
    }

    public async Task BasketPriceMustBeEqual(Basket basket, IList<BasketItem> basketItems)
    {
        double basketItemTotalPrice = 0.00 ;

        foreach(BasketItem item in basketItems)
        {
            basketItemTotalPrice = basketItemTotalPrice + item.Product.Price;
        }

        if (basketItemTotalPrice != basket.TotalPrice)
            await throwBusinessException(PaymentBusinessMessages.BasketPricesShouldBeEqual);
    }

    private async Task throwBusinessException(string messageKey)
    {
        throw new BusinessException(messageKey);
    }
}
