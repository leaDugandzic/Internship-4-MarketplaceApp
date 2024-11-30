using MarketplaceApp.Presentation.Abstractions;

namespace MarketplaceApp.Presentation.Actions.MainMenu.Settings;

public class SettingsAction : BaseMenuAction
{
    public SettingsAction(IList<IAction> actions) : base(actions)
    {
        Name = "Postavke";
    }

    public override void Open()
    {
        Console.WriteLine("Postavke");
        base.Open();
    }
}