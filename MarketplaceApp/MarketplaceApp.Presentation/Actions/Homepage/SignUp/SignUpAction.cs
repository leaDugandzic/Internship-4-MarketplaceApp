using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Contracts.Kupac;
using MarketplaceApp.Domain.Contracts.Prodavac;
using MarketplaceApp.Domain.Enum;
using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Extensions;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Actions.Homepage.SignUp;

public class SignUpAction : IAction
{
    private readonly KupacRepository _kupacRepository;
    private readonly ProdavacRepository _prodavacRepository;
    public string Name { get; set; } = "Sign Up";
    public int MenuIndex { get; set; }
    
    public SignUpAction(KupacRepository kupacRepository, ProdavacRepository prodavacRepository)
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
        var email = ActionExtensions.CorrectEmailChoice();
        var ime = ActionExtensions.CorrectImeChoice();
        
        if (korisnikType == KorisnikEnum.Kupac)
        {
            var balance = ActionExtensions.CorrectBalanceChoice();
            var request = new RegisterKupacRequest()
            {
                Ime = ime,
                Balance = balance,
                Email = email,
            };
            var success = SignUpKupac(request);
            return new Tuple<bool,KorisnikEnum>(success, KorisnikEnum.Kupac);

        }
        else
        {
            var request = new RegisterProdavacRequest()
            {
                Ime = ime,
                Email = email,
            };
            var success = SignUpProdavac(request);
            return new Tuple<bool,KorisnikEnum>(success, KorisnikEnum.Prodavac);
        }
    }

    private bool SignUpKupac(RegisterKupacRequest request)
    {
        var kupac = _kupacRepository.GetByEmail(request.Email);
        if (kupac.Success)
        {
            Console.WriteLine($"{ResponseResultType.ValidationError} - {"Kupac sa navedenim email-om vec postoji!"}");
            return false;
            
        }
        
        var result = _kupacRepository.RegistracijaKupca(request);
        if (result.Success)
        {
            CurrentUser.CurrentUserId=result.Data.First();
            CurrentUser.CurrentUserName=request.Ime;
            CurrentUser.KorisnikType = KorisnikEnum.Kupac;
            return true;
        }
        else
        {
            Console.WriteLine($"{result.ResultType} - {result.ErrorMessage}");
            return false;
        }
        
        
        
    }
    
    private bool SignUpProdavac(RegisterProdavacRequest request)
    {
        var prodavac = _prodavacRepository.GetByEmail(request.Email);
        if (prodavac.Success)
        {
            Console.WriteLine($"{ResponseResultType.ValidationError} - {"Kupac sa navedenim email-om vec postoji!"}");
            return false;
            
        }
        
        var result = _prodavacRepository.RegistracijaProdavac(request);
        if (result.Success)
        {
            CurrentUser.CurrentUserId=result.Data.First();
            CurrentUser.CurrentUserName=request.Ime;
            CurrentUser.KorisnikType = KorisnikEnum.Prodavac;
            return true;
        }
        else
        {
            Console.WriteLine($"{result.ResultType} - {result.ErrorMessage}");
            return false;
        }
        
        
    }
}