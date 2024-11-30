using MarketplaceApp.Data.Entities.Models;
using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Enum;
using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Extensions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.Homepage.Login;

public class LoginAction : IAction
{
    private readonly KupacRepository _kupacRepository;
    private readonly ProdavacRepository _prodavacRepository;
    public string Name { get; set; } = "Login";
    public int MenuIndex { get; set; }

    public LoginAction(KupacRepository kupacRepository, ProdavacRepository prodavacRepository)
    {
        _kupacRepository = kupacRepository;
        _prodavacRepository = prodavacRepository;
    }

    public void Open()
    {
        var result = Checker();
        while (!result.Item1)
        {
            bool cont = Reader.DoYouWantToContinue();
            if (cont)
                result = Checker();
            else
                ActionExtensions.PrintActions();
        }
        ActionExtensions.PrintActions(result.Item2);
    }
    private Tuple<bool,KorisnikEnum> Checker()
    {
        Console.Clear();
        var korisnikType = ActionExtensions.ReadKorisnikTip();
        var email = EmailInputChecker();
        if (korisnikType == KorisnikEnum.Kupac)
        {
            var success = FindKupac(email);
            return new Tuple<bool,KorisnikEnum>(success, KorisnikEnum.Kupac);

        }
        else
        {
            var success = FindProdavac(email);
            return new Tuple<bool,KorisnikEnum>(success, KorisnikEnum.Prodavac);
        }
    }
    
    
    
    private string EmailInputChecker()
    {
        while (true)
        {
            Reader.TryReadLine("Unesite email", out string email);
            if (!string.IsNullOrEmpty(email))
            {
                return email;
            }
            else
            {
                Console.WriteLine("Email ne može biti prazan! Pokušajte ponovno");
            }
        }
    }
    private bool FindKupac(string email)
    {
        var kupac = _kupacRepository.GetByEmail(email);
        if (kupac.Success)
        {
            CurrentUser.CurrentUserId=kupac.Data.First().Id;
            CurrentUser.CurrentUserName = kupac.Data.First().Ime;
            CurrentUser.KorisnikType = KorisnikEnum.Kupac;
            return true;
        }
        Console.WriteLine($"{kupac.ResultType.ToString()} - {kupac.ErrorMessage}");
        return false;

    }
    private bool FindProdavac(string email)
    {
        var prodavac = _prodavacRepository.GetByEmail(email);
        if (prodavac.Success)
        {
            CurrentUser.CurrentUserId=prodavac.Data.First().Id;
            CurrentUser.CurrentUserName = prodavac.Data.First().Ime;
            CurrentUser.KorisnikType = KorisnikEnum.Prodavac;
            return true;
        }
        Console.WriteLine($"{prodavac.ResultType.ToString()} - {prodavac.ErrorMessage}");
        return false;
    }

}