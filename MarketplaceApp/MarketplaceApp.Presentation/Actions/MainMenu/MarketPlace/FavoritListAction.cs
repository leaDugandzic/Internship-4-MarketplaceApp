using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class FavoritListAction : IAction
{
    private readonly FavoritiRepository _favoritiRepository;

    public FavoritListAction(FavoritiRepository favoritiRepository)
    {
        _favoritiRepository = favoritiRepository;
    }

    public string Name { get; set; } = "Pregledaj favorite";
    public int MenuIndex { get; set; }
    public void Open()
    {
        Console.Clear();

        var result = _favoritiRepository.DohvatiFavorite();
        if (result.Success)
        {
            Writer.Write(result.Data);
        }
        else
        {
            Writer.Write($"{result.ResultType} : {result.ErrorMessage}");
        }
        Console.WriteLine("Pritisni bilo koju tipku za povratak na prethodni menu");
        Console.ReadLine();
    }
}