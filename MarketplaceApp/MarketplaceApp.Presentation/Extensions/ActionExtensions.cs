using MarketplaceApp.Data.Enums;
using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Enum;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Actions;
using MarketplaceApp.Presentation.Factories;
using MarketplaceApp.Presentation.Helpers;

namespace MarketplaceApp.Presentation.Extensions;

public static class ActionExtensions
{
    public static void PrintActions()
    {
        var homepageActions = HomepageFactory.CreateActions();
        homepageActions.PrintActionsAndOpen();
    }

    public static void PrintActions(KorisnikEnum type)
    {
        if (type == KorisnikEnum.Prodavac)
        {
            var mainMenuActions = MainMenuProdavacFactory.CreateActions();
            mainMenuActions.PrintActionsAndOpen();
        }
        else
        {
            var mainMenuActions = MainMenuKupacFactory.CreateActions();
            mainMenuActions.PrintActionsAndOpen();
        }
    }
    public static void ClearCurrentUser()
    {
        CurrentUser.CurrentUserId = 0;
    }
    
    public static KorisnikEnum ReadKorisnikTip()
    {
        while (true)
        {
            Reader.TryReadNumber($"Odaberite ulogu :\n" +
                                 $"{(int)KorisnikEnum.Kupac} - {KorisnikEnum.Kupac.ToString()}\n" +
                                 $"{(int)KorisnikEnum.Prodavac} - {KorisnikEnum.Prodavac.ToString()}", out int korisnikTip);

            if (korisnikTip == (int)KorisnikEnum.Kupac)
            {
                return KorisnikEnum.Kupac;
            }
            else if (korisnikTip == (int)KorisnikEnum.Prodavac)
            {
                return KorisnikEnum.Prodavac;
            }
            else
            {
                Console.WriteLine("Nepostojeća opcija! Pokušajte ponovno");
            }
        }
    }

    public static string CorrectImeChoice()
    {
        string? ime = Reader.ReadImeInput();
        while (string.IsNullOrEmpty(ime))
        {
            bool cont = Reader.DoYouWantToContinue();
            if (cont)
                ime = Reader.ReadImeInput();
            else
                PrintActions();
        }

        return ime;
    }

    public static decimal CorrectBalanceChoice()
    {
        while (true)
        {
            Console.Write("Unesite iznos balansa: ");
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal balance) && balance >= 0)
            {
                return balance;
            }

            Console.WriteLine("Neispravan unos! Molimo unesite valjani broj (pozitivni decimalni broj).");
            bool cont = Reader.DoYouWantToContinue();
            if (!cont)
                PrintActions();
        }
        
    }
    public static string CorrectEmailChoice()
    {
        string? email = Reader.ReadEmailInput();
        while (email == null)
        {
            bool cont = Reader.DoYouWantToContinue();
            if (cont)
                email = EmailChoice();
            else
                PrintActions();
        }
        return email;
    }
    
    public static string? EmailChoice()
    {
        Console.Clear();
        string? email = Reader.ReadEmailInput();
        return email;
    }
    
    public static void SetActionIndexes(this IList<IAction> actions)
    {
        var index = 0;
        foreach (var action in actions)
        {
            action.MenuIndex = ++index;
        }
    }
    
    private static void PrintActions(IList<IAction> actions)
    {
        Console.Clear();
        if (CurrentUser.CurrentUserId != 0)
        {
            Console.WriteLine($"Racun: {CurrentUser.CurrentUserName}({CurrentUser.KorisnikType})\n");
        }
        foreach (var action in actions)
        {
            Console.WriteLine($"{action.MenuIndex}. {action.Name}");
        }
    }
    
    private static void PrintErrorMessage(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
    public static void PrintActionsAndOpen(this IList<IAction> actions)
    {
        const string INVALID_INPUT_MSG = "Please type in number!";
        const string INVALID_ACTION_MSG = "Please select valid action!";

        var isExitSelected = false;
        do
        {
            PrintActions(actions);

            var isValidInput = int.TryParse(Console.ReadLine(), out var actionIndex);
            if (!isValidInput)
            {
                PrintErrorMessage(INVALID_INPUT_MSG);
                continue;
            }

            var action = actions.FirstOrDefault(a => a.MenuIndex == actionIndex);
            if (action is null)
            {
                PrintErrorMessage(INVALID_ACTION_MSG);
                continue;
            }

            action.Open();

            isExitSelected = action is ExitMenuAction;
        } while (!isExitSelected);
    }
}