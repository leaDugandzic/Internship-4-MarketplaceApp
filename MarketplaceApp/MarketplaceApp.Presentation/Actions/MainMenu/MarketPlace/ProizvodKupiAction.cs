using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class ProizvodKupiAction : IAction
{
    private readonly MarketPlaceRepository _marketPlaceRepository;
    public string Name { get; set; } = "Kupi proizvod";
    public int MenuIndex { get; set; }
    
    public ProizvodKupiAction(MarketPlaceRepository marketPlaceRepository)
    {
        _marketPlaceRepository = marketPlaceRepository;
    }
    public void Open()
    {
        Console.Clear();
        Console.WriteLine("Id proizvoda za kupnju: ");
        var id = int.Parse(Console.ReadLine());
        var result = _marketPlaceRepository.KupiProizvod(id);
        if (result.Success)
        {
            Console.WriteLine("Hvala na kupnji!");
            Thread.Sleep(1500);
        }
        else
        {
            Console.WriteLine($"{result.ResultType}: {result.ErrorMessage}");
            Thread.Sleep(1500);
        }

    }
}