using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class FavoritDodajAction : IAction
{
    private readonly FavoritiRepository _favoritiRepository;

    public FavoritDodajAction(FavoritiRepository favoritiRepository)
    {
        _favoritiRepository = favoritiRepository;
    }

    public string Name { get; set; } = "Dodaj proizvod u favorite";
    public int MenuIndex { get; set; }
    public void Open()
    {
        Console.Clear();
        Console.WriteLine("Unesi id proizvoda koji zelite dodat u favorite: ");
        var favoriteId = int.Parse(Console.ReadLine());
        var result = _favoritiRepository.DodajFavorit(favoriteId);
        if (result.Success)
        {
            Writer.Write("Uspješno dodan proizvod u favorite");
        }
        else
        {
            Writer.Write($"{result.ResultType} : {result.ErrorMessage}");
        }
        Thread.Sleep(2000);
    }
}