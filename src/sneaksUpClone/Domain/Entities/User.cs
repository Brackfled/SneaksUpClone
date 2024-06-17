namespace Domain.Entities;

public class User : NArchitecture.Core.Security.Entities.User<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<OtpAuthenticator> OtpAuthenticators { get; set; } = default!;
    public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; } = default!;
    public virtual ICollection<Basket> Baskets { get; set; } = default!;

    public User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public User(string firstName, string lastName, ICollection<UserOperationClaim> userOperationClaims, ICollection<RefreshToken> refreshTokens, ICollection<OtpAuthenticator> otpAuthenticators, ICollection<EmailAuthenticator> emailAuthenticators, ICollection<Basket> baskets)
    {
        FirstName = firstName;
        LastName = lastName;
        UserOperationClaims = userOperationClaims;
        RefreshTokens = refreshTokens;
        OtpAuthenticators = otpAuthenticators;
        EmailAuthenticators = emailAuthenticators;
        Baskets = baskets;
    }
}
