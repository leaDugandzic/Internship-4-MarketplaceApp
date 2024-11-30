using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Enum;
using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class ProizvodPosjedAction : IAction
{
    private readonly KupacRepository _kupacRepository;
    private readonly ProdavacRepository _prodavacRepository;

    public ProizvodPosjedAction(KupacRepository kupacRepository, ProdavacRepository prodavacRepository)
    {
        _kupacRepository = kupacRepository;
        _prodavacRepository = prodavacRepository;
    }

    public string Name { get; set; } = "Vlasništvo";
    public int MenuIndex { get; set; }
    
    public void Open()
    {
        Console.Clear();
        if (CurrentUser.KorisnikType == KorisnikEnum.Kupac)
        {
            var result = _kupacRepository.OwnedProizvodi();
            if (result.Data != null && result.Data.Count() > 0)
            {
                Writer.Write(result.Data);
            }
            else
            {
                Writer.Write($"{result.ResultType}:{result.ErrorMessage}");
            }
        }
        else
        {
            var result = _prodavacRepository.GetAllOwned();
            if (result.Data != null && result.Data.Count() > 0)
            {
                Writer.Write(result.Data);
            }
            else
            {
                Writer.Write($"{result.ResultType}:{result.ErrorMessage}");
            }
        }
        
        Console.WriteLine("Pritisni bilo koju tipku za povratak na prethodni menu");
        Console.ReadLine();
    }
}