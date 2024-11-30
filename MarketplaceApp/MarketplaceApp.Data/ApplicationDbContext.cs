using MarketplaceApp.Data.Entities.Models;

namespace MarketplaceApp.Data;

public static class ApplicationDbContext
{
    public static List<Kupac> Kupci { get; set; } = Seed.DefaultKupci;
    public static List<Prodavac> Prodavaci { get; set; } = Seed.DefaultProdavaci;
    public static List<Proizvod> Proizvodi { get; set; } = Seed.DefaultProizvodi;
    public static List<Transakcija> Transakcije { get; set; } = new List<Transakcija>();
    public static List<Favorit> Favoriti { get; set; } = new List<Favorit>();
    public static List<KategorijeProizvoda> KategorijeProizvoda { get; set; } = Seed.DefaultKategorijeProizvoda;
      
}