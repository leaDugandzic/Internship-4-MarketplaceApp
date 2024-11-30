using MarketplaceApp.Data.Enums;

namespace MarketplaceApp.Domain.Contracts.Proizvod;

public class CreateProizvodRequest
{
    public string Naziv { get; set; }
    public string Opis { get; set; }
    public decimal Cijena { get; set; }
    public int KategorijaProizvodaId { get; set; } 
}