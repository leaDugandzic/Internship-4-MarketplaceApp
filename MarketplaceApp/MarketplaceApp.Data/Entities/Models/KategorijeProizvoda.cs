namespace MarketplaceApp.Data.Entities.Models;

public class KategorijeProizvoda
{
    private static int _nextId = 1;
    public KategorijeProizvoda(string naziv)
    {
        Naziv = naziv;
        Id = _nextId++;
    }

    public int Id { get; set; }
    public string Naziv { get; set; }
    public ICollection<Proizvod> Proizvodi { get; set; }
    
}