namespace MarketplaceApp.Data.Entities.Models;

public class Kupac
{
     private static int _nextId = 1;
    public Kupac(string ime, string email, decimal balance)
    {
        Ime = ime;
        Email = email;
        Balance = balance;
        Id = _nextId++;
    }

    public int Id { get; set; }
    public string Ime { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    
    public ICollection<Favorit> Favoriti { get; set; } = new List<Favorit>();
    public ICollection<Transakcija> Transakcije { get; set; } = new List<Transakcija>();
    

}