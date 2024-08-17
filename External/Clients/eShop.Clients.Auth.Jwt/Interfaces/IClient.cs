using System.Security.Claims;

namespace eShop.Clients.Auth.Jwt.Interfaces;

public interface IClient
{
    public abstract ClaimsPrincipal User { get; }

    public virtual string UserName => FindFirst("UserName")?.Value
        ?? throw new UnauthorizedAccessException("User has no claim 'UserName'");


    public virtual long ClientId
    {
        get
        {
            string value = FindFirst("ClientId")?.Value
                ?? throw new UnauthorizedAccessException("User has no claim 'ClientId'");

            return long.Parse(value);
        }
    }

    public virtual long Phone
    {
        get
        {
            string value = FindFirst("Phone")?.Value
                ?? throw new UnauthorizedAccessException("User has no claim 'Phone'");

            return long.Parse(value);
        }
    }

    public virtual string Email => FindFirst("Email")?.Value
        ?? throw new UnauthorizedAccessException("User has no claim 'Email'");


    public virtual IEnumerable<Claim> Claims => User.Claims;

    public virtual bool IsInRole(string role) => User.IsInRole(role);

    public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match) => User.FindAll(match);
    public virtual IEnumerable<Claim> FindAll(string type) => User.FindAll(type);

    public virtual Claim FindFirst(Predicate<Claim> match) => User.FindFirst(match);
    public virtual Claim FindFirst(string type) => User.FindFirst(type);

    public virtual bool HasClaim(Predicate<Claim> match) => User.HasClaim(match);
    public virtual bool HasClaim(string type, string value) => User.HasClaim(type, value);
}
