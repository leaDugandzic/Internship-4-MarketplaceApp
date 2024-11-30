using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.MainMenu.Report;

public class ZaradaByDateAction : IAction
{
    private readonly ProdavacRepository _prodavacRepository;

    public ZaradaByDateAction(ProdavacRepository prodavacRepository)
    {
        _prodavacRepository = prodavacRepository;
    }

    public string Name { get; set; } = "Zarada iz raspona datuma";
    public int MenuIndex { get; set; }
    public void Open()
    {
        Console.WriteLine("Unesite raspon datuma:");

        DateTime datumOd = Reader.ReadDate("Unesite početni datum (u formatu dd.MM.yyyy): ");

        DateTime datumDo = Reader.ReadDate("Unesite završni datum (u formatu dd.MM.yyyy): ");

        if (datumOd > datumDo)
        {
            Console.WriteLine("Greška: Početni datum ne može biti nakon završnog datuma.");
        }
        var result = _prodavacRepository.PeriodZarada(datumOd, datumDo);
        if (result.Success)
        {
            Writer.Write($"Zarada u odabranom periodu: {result.Data.First()}");
        }
        else
        {
            Writer.Write($"{result.ResultType} : {result.ErrorMessage}");

        }
        Console.WriteLine("Pritisni bilo koju tipku za povratak na prethodni menu");
        Console.ReadLine();
    }
}