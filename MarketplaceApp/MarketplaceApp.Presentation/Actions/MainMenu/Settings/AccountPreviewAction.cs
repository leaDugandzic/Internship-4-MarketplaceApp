using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Enum;
using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.Settings;

public class AccountPreviewAction : IAction
{
    private readonly KupacRepository _kupacRepository;
    private readonly ProdavacRepository _prodavacRepository;

    public AccountPreviewAction(KupacRepository kupacRepository, ProdavacRepository prodavacRepository)
    {
        _kupacRepository = kupacRepository;
        _prodavacRepository = prodavacRepository;
    }

    public string Name { get; set; } = "Racun";
    public int MenuIndex { get; set; }
    public void Open()
    {
        Console.Clear();
        if (CurrentUser.KorisnikType == KorisnikEnum.Kupac)
        {
            var result = _kupacRepository.GetInfo();
            if (result.Success)
            {
                Writer.Write(result.Data.First());
            }
            else
            {
                Writer.Write($"{result.ResultType} : {result.ErrorMessage}");
            }
        }
        else
        {
            var result = _prodavacRepository.GetInfo();
            if (result.Success)
            {
                Writer.Write(result.Data.First());
            }
            else
            {
                Writer.Write($"{result.ResultType} : {result.ErrorMessage}");
            }
        }
        Console.WriteLine("Pritisni bilo koju tipku za povratak na prethodni menu");
        Console.ReadLine();
    }
}