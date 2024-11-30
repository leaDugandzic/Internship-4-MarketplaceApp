using MarketplaceApp.Data.Enums;

namespace MarketplaceApp.Domain.Contracts.Kupac;

public class RegisterKupacRequest
{
    public string Ime { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
}