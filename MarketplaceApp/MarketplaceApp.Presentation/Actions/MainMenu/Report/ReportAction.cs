using MarketplaceApp.Presentation.Abstractions;

namespace MarketplaceApp.Presentation.Actions.MainMenu.Report;

public class ReportAction : BaseMenuAction
{
    public ReportAction(IList<IAction> actions) : base(actions)
    {
        Name = "Report";
    }
    
    public override void Open()
    {
        Console.Clear();
        Console.WriteLine("Report");
        base.Open();
    }
    
}