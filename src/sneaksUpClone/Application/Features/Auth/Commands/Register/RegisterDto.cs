using NArchitecture.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register;
public class RegisterDto: UserForRegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public RegisterDto()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public RegisterDto(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
