using MarketplaceApp.Domain.Common;
using MarketplaceApp.Presentation.Abstractions;

namespace MarketplaceApp.Presentation.Actions;

public class ExitMenuAction : IAction
{
    public int MenuIndex { get; set; }
    public string Name { get; set; } = "Exit";

    public ExitMenuAction()
    {
    }

    public void Open()
    {
        
    }
}