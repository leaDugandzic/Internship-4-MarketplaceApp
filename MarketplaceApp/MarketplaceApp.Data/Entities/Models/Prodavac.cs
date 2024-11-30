namespace MarketplaceApp.Data.Entities.Models;

public class Prodavac
{
    private static int _nextId = 1;

    public Prodavac(string ime, string email)
    {
        Ime = ime;
        Email = email;
        Id = _nextId++;

    }

    public int Id { get; set; }
    public string Ime { get; set; }
    public string Email { get; set; }
    
    public ICollection<Transakcija> Transakcije { get; set; } = new List<Transakcija>();
    
    public ICollection<Proizvod> Proizvodi { get; set; } = new List<Proizvod>();
}