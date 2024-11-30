namespace MarketplaceApp.Presentation.Abstractions;

public interface IAction
{
    string Name { get; set; }

    int MenuIndex { get; set; }

    void Open();
}