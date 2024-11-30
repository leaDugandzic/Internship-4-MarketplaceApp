using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class KategorijeProizvodaListAction : IAction
{
    private readonly KategorijaProizvodaRepository _kategorijaProizvodaRepository;

    public KategorijeProizvodaListAction(KategorijaProizvodaRepository kategorijaProizvodaRepository)
    {
        _kategorijaProizvodaRepository = kategorijaProizvodaRepository;
    }

    public string Name { get; set; } = "Kategorije proizvoda";
    public int MenuIndex { get; set; }
    public void Open()
    {
        Console.Clear();
        var result = _kategorijaProizvodaRepository.GetAllKategorijaProizvoda();
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