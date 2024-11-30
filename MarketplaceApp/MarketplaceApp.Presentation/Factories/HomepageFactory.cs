using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Actions;
using MarketplaceApp.Presentation.Actions.Homepage.Login;
using MarketplaceApp.Presentation.Actions.Homepage.SignUp;
using MarketplaceApp.Presentation.Extensions;

namespace MarketplaceApp.Presentation.Factories;

public class HomepageFactory
{
    public static IList<IAction> CreateActions()
    {
        var kupacRepository = new KupacRepository();
        var prodavacRepository = new ProdavacRepository();
        var actions = new List<IAction>()
        {
            new LoginAction(kupacRepository, prodavacRepository),
            new SignUpAction(kupacRepository, prodavacRepository),
            new ExitMenuAction()
        };
        actions.SetActionIndexes();

        return actions;
    }
}