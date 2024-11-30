using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Enum;
using MarketplaceApp.Domain.Repositories;
using MarketplaceApp.Presentation.Abstractions;
using MarketplaceApp.Presentation.Actions;
using MarketplaceApp.Presentation.Actions.MainMenu.MarketPlace;
using MarketplaceApp.Presentation.Actions.MainMenu.Settings;

namespace MarketplaceApp.Presentation.Factories;

public class MarketPlaceFactory
{
    public static MarketPlaceAction Create()
    {
        var proizvodRepository = new ProizvodRepository();
        var marketplaceRepository = new MarketPlaceRepository();
        var kupacRepositor = new KupacRepository();
        var prodavacRepository = new ProdavacRepository();
        var favoritRepository = new FavoritiRepository();
        var kategorijaRepository = new KategorijaProizvodaRepository();

        var actions = new List<IAction>()
        {
            new ProizvodPosjedAction(kupacRepositor, prodavacRepository),
            new KategorijeProizvodaListAction(kategorijaRepository),
            new ExitMenuAction()
        };
        if (CurrentUser.KorisnikType == KorisnikEnum.Kupac)
        {
            actions.Add(new ProizvodListAction(proizvodRepository));
            actions.Add(new ProizvodKupiAction(marketplaceRepository));
            actions.Add(new ProizvodVratiAction(marketplaceRepository));
            actions.Add(new FavoritListAction(favoritRepository));
            actions.Add(new FavoritDodajAction(favoritRepository));
            
        }
        else
        {
            actions.Add(new ProizvodAzurirajAction(proizvodRepository));
            actions.Add(new KategorijeProizvodaKreirajAction(kategorijaRepository));
            actions.Add(new ProizvodKreirajAction(proizvodRepository));
        }
        var menuAction = new MarketPlaceAction(actions);
        return menuAction;
    }
}