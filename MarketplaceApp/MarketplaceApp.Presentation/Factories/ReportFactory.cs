using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Enum;
using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Actions;
using MarketplaceApp.Presentation.Actions.MainMenu.Report;

namespace MarketplaceApp.Presentation.Factories;

public class ReportFactory
{
    public static ReportAction Create()
    {
        var prodavacRepository = new ProdavacRepository();

        var actions = new List<IAction>()
        {
            new TotalZaradaAction(prodavacRepository),
            new ZaradaByDateAction(prodavacRepository),
            new ExitMenuAction()
        };
        var menuAction = new ReportAction(actions);
        return menuAction;
    }
}