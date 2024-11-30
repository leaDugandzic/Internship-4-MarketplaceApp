using MarketplaceApp.Domain.Contracts.KategorijaProizvoda;
using MarketplaceApp.Domain.Contracts.Kupac;
using MarketplaceApp.Domain.Contracts.Prodavac;
using MarketplaceApp.Domain.Contracts.Proizvod;
using MarketplaceApp.Domain.Repositories;

namespace MarketplaceApp.Presentation.Helpers;

public class Writer
{
    public static void Write(List<ProizvodDto> proizvodi)
    {
        foreach (var proizvod in proizvodi)
        {
            Console.WriteLine($"{nameof(proizvod.Id)}: {proizvod.Id}\n"+
                              $"{nameof(proizvod.Naziv)}: {proizvod.Naziv}\n" +
                              $"{nameof(proizvod.Opis)}: {proizvod.Opis}\n" +
                              $"{nameof(proizvod.Cijena)}: {proizvod.Cijena}\n" +
                              $"{nameof(proizvod.Status)}: {proizvod.Status}\n" +
                              $"{nameof(proizvod.Prodavac)}: {proizvod.Prodavac}\n" +
                              $"{nameof(proizvod.Kategorija)}: {proizvod.Kategorija}\n"+
                              ("----------------------------------------"));
        }

    }
    
    public static void Write(List<KategorijaProizvodaDto> kategorije)
    {
        foreach (var kategorija in kategorije)
        {
            Console.WriteLine($"{nameof(kategorija.Id)}: {kategorija.Id}\n"+
                              $"{nameof(kategorija.Naziv)}: {kategorija.Naziv}\n"+
                              ("----------------------------------------"));

        }

    }
    
    public static void Write(KupacDto kupac)
    {
        Console.WriteLine($"{nameof(kupac.Id)}: {kupac.Id}\n" +
                          $"{nameof(kupac.Ime)}: {kupac.Ime}\n" +
                          $"{nameof(kupac.Email)}: {kupac.Email}\n" +
                          $"{nameof(kupac.Balance)}: {kupac.Balance}\n"+
                          ("----------------------------------------"));


    }
    
    public static void Write(ProdavacDto prodavac)
    {
        Console.WriteLine($"{nameof(prodavac.Id)}: {prodavac.Id}\n" +
                          $"{nameof(prodavac.Ime)}: {prodavac.Ime}\n" +
                          $"{nameof(prodavac.Email)}: {prodavac.Email}\n"+
                          ("----------------------------------------"));
    }
    
    public static void Write(List<ProizvodiOwnedDto> proizvodi)
    {
        foreach (var proizvod in proizvodi)
        {
            Console.WriteLine($"{nameof(proizvod.Id)}: {proizvod.Id}\n"+
                              $"{nameof(proizvod.Naziv)}: {proizvod.Naziv}\n" +
                              $"{nameof(proizvod.Opis)}: {proizvod.Opis}\n" +
                              $"{nameof(proizvod.Cijena)}: {proizvod.Cijena}\n" +
                              $"{nameof(proizvod.Prodavac)}: {proizvod.Prodavac}\n" +
                              $"{nameof(proizvod.Kategorija)}: {proizvod.Kategorija}\n" +
                              $"{nameof(proizvod.DatumTransakcije)}: {proizvod.DatumTransakcije}\n"+
                              ("----------------------------------------"));
        }

    }
    
    
    public static void Write(string output)
    {
        Console.WriteLine(output);
    }

    public static void Error(string message)
    {
        Console.WriteLine(message);
        Thread.Sleep(1000);
        Console.Clear();
    }

    public static bool GenerateRandomString()
    {
        Guid myGuid = Guid.NewGuid();
        string fourRandomLatters = myGuid.ToString().Substring(0, 5);
        if (!ContainsLetter(fourRandomLatters) || !ContainsNumber(fourRandomLatters))
            return GenerateRandomString();
        Console.WriteLine("To check you are not a bot repeat this text: " + fourRandomLatters);
        Reader.ReadInput(out string input);
        if (input == fourRandomLatters)
            return true;
        return false;
    }

    public static bool ContainsLetter(string line)
    {
        foreach (char c in line)
            if (char.IsLetter(c))
                return true;
        return false;
    }

    public static bool ContainsNumber(string line)
    {
        foreach (char c in line)
            if (char.IsDigit(c))
                return true;
        return false;
    }

    
}