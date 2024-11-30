using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Enum;
using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Actions;
using MarketplaceApp.Presentation.Actions.MainMenu.Settings;
using MarketplaceApp.Presentation.Extensions;

namespace MarketplaceApp.Presentation.Factories;

public class SettingsFactory
{
    public static SettingsAction Create()
    {
        var kupacRepository = new KupacRepository();
        var prodavacRepository = new ProdavacRepository();
        var actions = new List<IAction>()
        {
            new AccountPreviewAction(kupacRepository, prodavacRepository),
            new ExitMenuAction()
        };
        var menuAction = new SettingsAction(actions);
        return menuAction;
    }
}