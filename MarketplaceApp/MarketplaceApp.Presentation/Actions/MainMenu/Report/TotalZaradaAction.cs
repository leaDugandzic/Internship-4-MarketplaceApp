using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.Report;

public class TotalZaradaAction : IAction
{
    private readonly ProdavacRepository _prodavacRepository;

    public TotalZaradaAction(ProdavacRepository prodavacRepository)
    {
        _prodavacRepository = prodavacRepository;
    }

    public string Name { get; set; } = "TotalZarada";
    public int MenuIndex { get; set; }
    public void Open()
    {
        Console.Clear();
        var result = _prodavacRepository.TotalZarada();
        if (result.Success)
        {
            Writer.Write($"Ukupna zarada: {result.Data.First()}");
        }
        else
        {
            Writer.Write($"{result.ResultType}:{result.ErrorMessage}");

        }
        Console.WriteLine("Pritisni bilo koju tipku za povratak na prethodni menu");
        Console.ReadLine();
    }
}