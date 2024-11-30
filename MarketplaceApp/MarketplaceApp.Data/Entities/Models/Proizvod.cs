using MarketplaceApp.Data.Enums;

namespace MarketplaceApp.Data.Entities.Models;

public class Proizvod
{
    private static int _nextId = 1;

    public Proizvod(string naziv, string opis, decimal cijena, StatusEnum status, int prodavacId, int kategorijaProizvodaId)
    {
        Naziv = naziv;
        Opis = opis;
        Cijena = cijena;
        Status = status;
        ProdavacId = prodavacId;
        KategorijaProizvodaId = kategorijaProizvodaId;
        Id = _nextId++;

    }

    public int Id { get; set; }
    public string Naziv { get; set; }
    public string Opis { get; set; }
    public decimal Cijena { get; set; }
    public StatusEnum Status { get; set; }
    public int ProdavacId { get; set; }
    public int KategorijaProizvodaId { get; set; }
    
    public Prodavac Prodavac { get; set; }
    public KategorijeProizvoda KategorijeProizvoda { get; set; }
    public ICollection<Transakcija> Transakcije { get; set; } =  new List<Transakcija>();

}