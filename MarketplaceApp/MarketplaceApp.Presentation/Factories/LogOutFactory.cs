using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Actions;
using MarketplaceApp.Presentation.Actions.Homepage.Login;
using MarketplaceApp.Presentation.Actions.MainMenu.LogOut;

namespace MarketplaceApp.Presentation.Factories;

public class LogOutFactory
{
    public static LogOutAction Create()
    {
        var menuAction = new LogOutAction();
        return menuAction;
    }
}