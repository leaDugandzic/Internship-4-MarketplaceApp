namespace MarketplaceApp.Domain.Contracts.Kupac;

public class ProizvodiOwnedDto
{
    public int Id { get; set; }
    public string Naziv { get; set; }
    public string Opis { get; set; }
    public decimal Cijena { get; set; }
    public string Prodavac { get; set; }
    public int ProdavacId { get; set; }
    public string Kategorija { get; set; }
    public DateTime DatumTransakcije { get; set; }
}