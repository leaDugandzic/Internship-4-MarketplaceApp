using MarketplaceApp.Data.Entities.Models;
using MarketplaceApp.Data.Enums;

namespace MarketplaceApp.Data;

public static class Seed
{
    public static readonly List<Kupac> DefaultKupci = new List<Kupac>()
    {
        new Kupac("Mia Perić", "mia.peric@gmail.com", 7500),
        new Kupac("Luka Vuković", "luka.vukovic@gmail.com", 2000),
        new Kupac("Ema Novak", "ema.novak@gmail.com", 4500),
        new Kupac("Petar Grgić", "petar.grgic@gmail.com", 1200),
    };

    public static readonly List<Prodavac> DefaultProdavaci = new List<Prodavac>()
    {
        new Prodavac("TehnoShop", "info@tehnoshop.hr"),
        new Prodavac("Knjigomanija", "kontakt@knjigomanija.hr"),
        new Prodavac("Moda Trend", "info@modatrend.hr"),
        new Prodavac("Digitalni Svijet", "podrska@digitalsvijet.hr"),
    };

    public static readonly List<Proizvod> DefaultProizvodi = new List<Proizvod>()
    {
        new Proizvod("Samsung Galaxy S21", "Pametni telefon s naprednim značajkama", 7999, StatusEnum.Dostupno, 1, 1),
        new Proizvod("HP Pavilion Laptop", "Moderan i snažan laptop za svakodnevne potrebe", 5499, StatusEnum.Dostupno, 4, 4),
        new Proizvod("Knjiga: Umijeće ratovanja", "Klasična vojna filozofija Sun Tzua", 120, StatusEnum.Dostupno, 2, 2),
        new Proizvod("Ženska jakna", "Topla zimska jakna u crnoj boji", 899, StatusEnum.Dostupno, 3, 2),
        new Proizvod("iPhone 13", "Najnoviji model iPhone telefona", 9499, StatusEnum.Dostupno, 1, 1),
    };

    public static readonly List<KategorijeProizvoda> DefaultKategorijeProizvoda = new List<KategorijeProizvoda>()
    {
        new KategorijeProizvoda("Mobiteli"),
        new KategorijeProizvoda("Odjeća"),
        new KategorijeProizvoda("Knjige"),
        new KategorijeProizvoda("Laptopi"),
    };

    

}