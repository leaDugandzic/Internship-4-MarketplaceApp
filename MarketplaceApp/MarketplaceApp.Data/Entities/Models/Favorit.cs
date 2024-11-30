namespace MarketplaceApp.Data.Entities.Models;

public class Favorit
{
    private static int _nextId = 1;

    public Favorit(int proizvodId, int kupacId)
    {
        ProizvodId = proizvodId;
        KupacId = kupacId;
        Id = _nextId++;

    }

    public int Id { get; set; }
    public int ProizvodId { get; set; }
    public int KupacId { get; set; }
    
    public Proizvod Proizvod { get; set; }
    public Kupac Kupac { get; set; }

}