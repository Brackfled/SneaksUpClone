using Application.Features.Payments.Commands.BinCheck;
using AutoMapper;
using Iyzipay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Profiles;
public class MappingProfiles:  Profile
{
    public MappingProfiles()
    {
        CreateMap<BinNumber, BinCheckResponse>().ReverseMap();
    }
}
