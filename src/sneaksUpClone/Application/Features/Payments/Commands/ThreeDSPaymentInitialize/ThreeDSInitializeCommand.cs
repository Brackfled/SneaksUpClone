using Application.Features.Baskets.Rules;
using Application.Features.Payments.Rules;
using Application.Features.Users.Rules;
using Application.Services.Addresses;
using Application.Services.Baskets;
using Application.Services.PaymentService;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Domain.Entities;
using Iyzipay.Model;
using Iyzipay.Request;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands.ThreeDSPaymentInitialize;
public class ThreeDSInitializeCommand: IRequest<ThreeDSInitializeResponse>
{
    public Guid UserId { get; set; }
    public string UserIp { get; set; }
    public ThreeDSPaymentDto ThreeDSPaymentDto { get; set; }

    public class ThreeDSInitializeCommandHandler : IRequestHandler<ThreeDSInitializeCommand, ThreeDSInitializeResponse>
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        private readonly IBasketService _basketService;
        private readonly IAddressService _addressService;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly BasketBusinessRules _basketBusinessRules;
        private readonly PaymentBusinessRules _paymentBusinessRules;

        public ThreeDSInitializeCommandHandler(IPaymentService paymentService, IUserService userService,
            IBasketService basketService, IAddressService addressService, IBasketItemRepository basketItemRepository,
            UserBusinessRules userBusinessRules, BasketBusinessRules basketBusinessRules,
            PaymentBusinessRules paymentBusinessRules)
        {
            _paymentService = paymentService;
            _userService = userService;
            _basketService = basketService;
            _addressService = addressService;
            _basketItemRepository = basketItemRepository;
            _userBusinessRules = userBusinessRules;
            _basketBusinessRules = basketBusinessRules;
            _paymentBusinessRules = paymentBusinessRules;
        }

        public async Task<ThreeDSInitializeResponse> Handle(ThreeDSInitializeCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(u => u.Id == request.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            Domain.Entities.Address? address = await _addressService.GetAsync(a => a.UserId == user.Id);
            Basket? basket = await _basketService.GetAsync(b => b.UserId == user.Id);
            await _basketBusinessRules.TotalPriceMustBeGreaterThenZero(basket);

            IPaginate<Domain.Entities.BasketItem>? basketItems = await _basketItemRepository.GetListAsync(predicate: bi => bi.BasketId == basket.Id, size: 1000, index: 0, include: bi => bi.Include(bi => bi.Product).Include(bi => bi.Basket));
            List<Iyzipay.Model.BasketItem> basketItemList = new List<Iyzipay.Model.BasketItem>();

            foreach (var item in basketItems.Items)
            {
                Iyzipay.Model.BasketItem basketItem = new()
                {
                    Id = basket.Id.ToString(),
                    Price = item.Product.Price.ToString("F2", CultureInfo.InvariantCulture),
                    Name = item.Product.Name,
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Category1 = "baba"
                };

                basketItemList.Add(basketItem);
            }

            await _paymentBusinessRules.BasketPriceMustBeEqual(basket, basketItems.Items);

            CreatePaymentRequest paymentRequest = new CreatePaymentRequest();
            paymentRequest.Locale = Locale.TR.ToString();
            paymentRequest.ConversationId = Guid.NewGuid().ToString();
            paymentRequest.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            paymentRequest.PaymentChannel = PaymentChannel.WEB.ToString();
            paymentRequest.Installment = request.ThreeDSPaymentDto.Installment;
            paymentRequest.PaidPrice = request.ThreeDSPaymentDto.PaidPrice;
            paymentRequest.Price = basket.TotalPrice.ToString("F2", CultureInfo.InvariantCulture);
            paymentRequest.Currency = request.ThreeDSPaymentDto.Currency;
            paymentRequest.CallbackUrl = "http://localhost:60805/api/Payment/PayCallBack";

            Buyer buyer = new Buyer();
            buyer.Id = user.Id.ToString();
            buyer.Name = user.FirstName;
            buyer.Surname = user.LastName;
            buyer.IdentityNumber = request.ThreeDSPaymentDto.IdentityNumber;
            buyer.Email = user.Email;
            buyer.GsmNumber = request.ThreeDSPaymentDto.GsmNumber;
            buyer.RegistrationAddress = address.AddressName;
            buyer.City = address.City;
            buyer.Country = address.Country;
            buyer.ZipCode = address.ZipCode;
            buyer.Ip = request.UserIp;
            paymentRequest.Buyer = buyer;

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = request.ThreeDSPaymentDto.CardHolderName;
            paymentCard.CardNumber = request.ThreeDSPaymentDto.CardNumber;
            paymentCard.ExpireYear = request.ThreeDSPaymentDto.ExpireYear;
            paymentCard.ExpireMonth = request.ThreeDSPaymentDto.ExpireMonth;
            paymentCard.Cvc = request.ThreeDSPaymentDto.Cvc;
            paymentCard.RegisterCard = 0;
            paymentRequest.PaymentCard = paymentCard;

            Iyzipay.Model.Address shippingAddress = new Iyzipay.Model.Address();
            shippingAddress.Description = address.AddressName;
            shippingAddress.City = address.City;
            shippingAddress.Country = address.Country;
            shippingAddress.ContactName = address.ContactName;
            shippingAddress.ZipCode = address.ZipCode;
            paymentRequest.ShippingAddress = shippingAddress;

            Iyzipay.Model.Address billingAddress = new Iyzipay.Model.Address();
            billingAddress.Description = address.AddressName;
            billingAddress.ZipCode = address.ZipCode;
            billingAddress.ContactName = address.ContactName;
            billingAddress.City = address.City;
            billingAddress.Country = address.Country;
            paymentRequest.BillingAddress = billingAddress;

            paymentRequest.BasketId = basket.Id.ToString();
            paymentRequest.BasketItems = basketItemList;

            ThreeDSInitializeResponse response = await _paymentService.ThreeDSPaymentInitialize(paymentRequest);

            if(response.Status == "success")
            {
                basket.TotalPrice = 0.00;
                await _basketService.UpdateAsync(basket);
                await _basketItemRepository.DeleteRangeAsync(basketItems.Items,permanent:true);
            }
            return response;
        }
    }
}
