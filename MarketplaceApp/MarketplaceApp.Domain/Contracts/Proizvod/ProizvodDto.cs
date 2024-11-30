using MarketplaceApp.Data.Enums;

namespace MarketplaceApp.Domain.Contracts.Proizvod;

public class ProizvodDto
{
    public int Id { get; set; }
    public string Naziv { get; set; }
    public string Opis { get; set; }
    public decimal Cijena { get; set; }
    public string Status { get; set; }
    public string Prodavac { get; set; }
    public int ProdavacId { get; set; }
    public string Kategorija { get; set; }
    public int KategorijaId { get; set; }
}