using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class ProizvodVratiAction : IAction
{
    private readonly MarketPlaceRepository _marketPlaceRepository;

    public ProizvodVratiAction(MarketPlaceRepository marketPlaceRepository)
    {
        _marketPlaceRepository = marketPlaceRepository;
    }

    public string Name { get; set; } = "Vrati proizvod";
    public int MenuIndex { get; set; }
    public void Open()
    {
        Console.Clear();
        Console.WriteLine("Unesite id proizvoda iz vašeg vlasništva: ");
        var id = int.Parse(Console.ReadLine());
        var result = _marketPlaceRepository.VratiProizvod(id);
        if (result.Success)
        {
            Writer.Write("Proizvod uspješno vraćen!");
        }
        else
        {
            Writer.Write($"{result.ResultType} : {result.ErrorMessage}");
        }
        Thread.Sleep(2000);

    }
}