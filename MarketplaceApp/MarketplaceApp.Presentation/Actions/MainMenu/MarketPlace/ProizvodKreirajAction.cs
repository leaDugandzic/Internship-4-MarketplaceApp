using MarketplaceApp.Domain.Contracts.Proizvod;
using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class ProizvodKreirajAction : IAction
{
    private readonly ProizvodRepository _proizvodRepository;

    public ProizvodKreirajAction(ProizvodRepository proizvodRepository)
    {
        _proizvodRepository = proizvodRepository;
    }

    public string Name { get; set; } = "Kreiraj novi proizvod";
    public int MenuIndex { get; set; }
    public void Open()
    {
        Console.Clear();
        Console.WriteLine("Unesi naziv proizvoda:");
        var naziv = Console.ReadLine();
        Console.WriteLine("Unesi opis proizvoda:");
        var opis = Console.ReadLine();
        Console.WriteLine("Unesi cijenu proizvoda:");
        var cijena = decimal.Parse(Console.ReadLine());
        Console.WriteLine("Unesi id kategorije:");
        var kategorijaId = int.Parse(Console.ReadLine());
        var request = new CreateProizvodRequest()
        {
            Naziv = naziv,
            Cijena = cijena,
            Opis = opis,
            KategorijaProizvodaId = kategorijaId
        };
        var result = _proizvodRepository.CreateProizvod(request);
        if (result.Success)
        {
            Console.WriteLine("Proizvod kreiran!");
        }
        else
        {
            Writer.Write($"{result.ResultType} : {result.ErrorMessage}");
        }
        Thread.Sleep(1500);

    }
}