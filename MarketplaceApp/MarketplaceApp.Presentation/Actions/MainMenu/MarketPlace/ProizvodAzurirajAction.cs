using MarketplaceApp.Domain.Contracts.Proizvod;
using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class ProizvodAzurirajAction : IAction
{
    private readonly ProizvodRepository _proizvodRepository;

    public ProizvodAzurirajAction(ProizvodRepository proizvodRepository)
    {
        _proizvodRepository = proizvodRepository;
    }

    public string Name { get; set; } = "Ažuriraj Proizvod";
    public int MenuIndex { get; set; }
    public void Open()
    {
        Console.Clear();

        Console.WriteLine("Id proizvoda za ažuriranje: ");
        var id = int.Parse(Console.ReadLine());
        Console.WriteLine("Nova cijena: ");
        var cijena = decimal.Parse(Console.ReadLine());
        var request = new UpdateProizvodRequest()
        {
            Id = id,
            Cijena = cijena,
        };
        var result = _proizvodRepository.AzurirajCijenuProizvoda(request);
        if (result.Success)
        {
            Console.WriteLine("Proizvod ažuriran!");
        }
        else
        {
            Writer.Write($"{result.ResultType} : {result.ErrorMessage}");
        }
        Thread.Sleep(1500);

    }
}