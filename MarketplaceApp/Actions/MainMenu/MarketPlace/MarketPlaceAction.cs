using MarketplaceApp.Presentation.Abstractions;

namespace MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;

public class MarketPlaceAction : BaseMenuAction
{
    public MarketPlaceAction(IList<IAction> actions) : base(actions)
    {
        Name = "MarketPlace";
    }
    
    public override void Open()
    {
        Console.Clear();
        Console.WriteLine("Marketplace");
        base.Open();
    }
}