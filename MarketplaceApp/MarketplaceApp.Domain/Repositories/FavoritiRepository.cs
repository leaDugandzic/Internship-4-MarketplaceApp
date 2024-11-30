using MarketplaceApp.Data;
using MarketplaceApp.Data.Entities.Models;
using MarketplaceApp.Data.Enums;
using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Contracts.Proizvod;

namespace MarketplaceApp.Domain.Repositories;

public class FavoritiRepository
{
    public ResultResponse<int> DodajFavorit(int proizvodId)
    {
        var proizvod = ApplicationDbContext.Proizvodi.SingleOrDefault(x => x.Id == proizvodId && x.Status == StatusEnum.Dostupno);
        if (proizvod == null)
        {
            return new ResultResponse<int>(ResponseResultType.NoData, "Proizvod ili ne postoji ili više nije dostupan!");
        }

        var noviFavorit = new Favorit(proizvodId, CurrentUser.CurrentUserId);
        ApplicationDbContext.Favoriti.Add(noviFavorit);
        return new ResultResponse<int>(ResponseResultType.Success, null, new List<int>(){noviFavorit.Id});

    }

    public ResultResponse<ProizvodDto> DohvatiFavorite()
    {
        var favoritiProizvodi = ApplicationDbContext.Favoriti
            .Where(f => f.KupacId == CurrentUser.CurrentUserId)
            .Join(ApplicationDbContext.Proizvodi,
                favorit => favorit.ProizvodId,
                proizvod => proizvod.Id,
                (favorit, proizvod) => new { favorit, proizvod })
            .Join(ApplicationDbContext.Prodavaci,
                fp => fp.proizvod.ProdavacId,
                prodavac => prodavac.Id,
                (fp, prodavac) => new { fp.favorit, fp.proizvod, prodavac })
            .Join(ApplicationDbContext.KategorijeProizvoda,
                fp => fp.proizvod.KategorijaProizvodaId,
                kategorija => kategorija.Id,
                (fp, kategorija) => new ProizvodDto
                {
                    Id = fp.proizvod.Id,
                    Naziv = fp.proizvod.Naziv,
                    Opis = fp.proizvod.Opis,
                    Cijena = fp.proizvod.Cijena,
                    Status = fp.proizvod.Status.ToString(),
                    Prodavac = fp.prodavac.Ime,
                    ProdavacId = fp.prodavac.Id,
                    Kategorija = kategorija.Naziv
                })
            .ToList();

        if (favoritiProizvodi.Count == 0)
        {
            return new ResultResponse<ProizvodDto>(ResponseResultType.NoData, "Nema favorita!");

        }
        return new ResultResponse<ProizvodDto>(ResponseResultType.Success, null, favoritiProizvodi);

    }
}