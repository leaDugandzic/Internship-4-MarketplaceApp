using MarketplaceApp.Domain.Contracts.Proizvod;
using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class ProizvodListAction : IAction
{
    private readonly ProizvodRepository _proizvodRepository;
    public string Name { get; set; } = "Prikaži proizvode";
    public int MenuIndex { get; set; }
    
    public ProizvodListAction(ProizvodRepository proizvodRepository)
    {
        _proizvodRepository = proizvodRepository;
    }
    public void Open()
    {
        Console.Clear();
        Console.WriteLine("Unesite id kategorije za filtriranje (0 ako ne želite filter): ");
        var id = int.Parse(Console.ReadLine());
        var request = new ProizvodSearchRequest()
        {
            KategorijaProizvodaId = id
        };
        var result = _proizvodRepository.GetAllProizvodi(request);
        if (result.Data != null && result.Data.Count() > 0)
        {
            Writer.Write(result.Data);
        }
        Console.WriteLine("Pritisni bilo koju tipku za povratak na prethodni menu");
        Console.ReadLine();
    }
}