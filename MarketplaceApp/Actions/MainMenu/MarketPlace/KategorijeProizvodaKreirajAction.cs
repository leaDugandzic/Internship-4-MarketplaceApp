using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class KategorijeProizvodaKreirajAction : IAction
{
    private readonly KategorijaProizvodaRepository _kategorijaProizvodaRepository;

    public KategorijeProizvodaKreirajAction(KategorijaProizvodaRepository kategorijaProizvodaRepository)
    {
        _kategorijaProizvodaRepository = kategorijaProizvodaRepository;
    }

    public string Name { get; set; } = "Kreiraj novu kategoriju proizvoda";
    public int MenuIndex { get; set; }
    public void Open()
    {
        Console.Clear();
        Console.WriteLine("Unesi naziv nove kategorije proizvoda: ");
        var naziv = Console.ReadLine();
        var result = _kategorijaProizvodaRepository.CreateKategorijaProizvoda(naziv);
        if (result.Success)
        {
            Writer.Write("Kategorija proizvoda uspješno kreirana!");
        }
        else
        {
            Writer.Write($"{result.ResultType} : {result.ErrorMessage}");
        }
        Thread.Sleep(2000);
    }
}