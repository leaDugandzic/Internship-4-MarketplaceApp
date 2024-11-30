using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Extensions;

namespace MarketplaceApp.Presentation.Factories;

public class MainMenuProdavacFactory
{
    public static IList<IAction> CreateActions()
    {
        var actions = new List<IAction>()
        {
            LogOutFactory.Create(),
            SettingsFactory.Create(),
            MarketPlaceFactory.Create(),
            ReportFactory.Create(),
        };
        actions.SetActionIndexes();

        return actions;
    }
}