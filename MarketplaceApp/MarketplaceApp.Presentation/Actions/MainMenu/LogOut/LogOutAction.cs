using MarketplaceApp.Domain.Common;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Extensions;

namespace MarketplaceApp.Presentation.Actions.MainMenu.LogOut;

public class LogOutAction : IAction
{
    public string Name { get; set; } = "Log out";
    public int MenuIndex { get; set; }

    public void Open()
    {
        Console.Clear();
        ActionExtensions.ClearCurrentUser();
        ActionExtensions.PrintActions();
    }
}