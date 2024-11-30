using MarketplaceApp.Data;
using MarketplaceApp.Data.Entities.Models;
using MarketplaceApp.Data.Enums;
using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Contracts.Proizvod;
using MarketplaceApp.Domain.Enum;

namespace MarketplaceApp.Domain.Repositories;

public class ProizvodRepository
{
    public ResultResponse<ProizvodDto> GetAllProizvodi(ProizvodSearchRequest request)
    {
        var proizvodi = ApplicationDbContext.Proizvodi
            .Join(ApplicationDbContext.Prodavaci,
                proizvod => proizvod.ProdavacId,
                prodavac => prodavac.Id,
                (proizvod, prodavac) => new { Proizvod = proizvod, Prodavac = prodavac })
            .Join(ApplicationDbContext.KategorijeProizvoda,
                combined => combined.Proizvod.KategorijaProizvodaId,
                kategorija => kategorija.Id,
                (combined, kategorija) => new ProizvodDto
                {
                    Id = combined.Proizvod.Id,
                    Naziv = combined.Proizvod.Naziv,
                    Opis = combined.Proizvod.Opis,
                    Cijena = combined.Proizvod.Cijena,
                    Status = combined.Proizvod.Status.ToString(),
                    Prodavac = combined.Prodavac.Ime,
                    ProdavacId = combined.Prodavac.Id,
                    Kategorija = kategorija.Naziv,
                    KategorijaId = kategorija.Id
                })
            .ToList();
        if (CurrentUser.KorisnikType == KorisnikEnum.Prodavac)
        {
            proizvodi = proizvodi.Where(x => x.ProdavacId == CurrentUser.CurrentUserId).ToList();
        }
        else
        {
            proizvodi = proizvodi.Where(x => x.Status == StatusEnum.Dostupno.ToString()).ToList();
        }

        if (request.KategorijaProizvodaId != 0)
        {
            proizvodi = proizvodi.Where(x => x.KategorijaId == request.KategorijaProizvodaId).ToList();
        }
        
        if (proizvodi.Count == 0)
        {
            return new ResultResponse<ProizvodDto>(ResponseResultType.NoData, "Nema proizvoda!");
        }
        return new ResultResponse<ProizvodDto>(ResponseResultType.Success, null, proizvodi);
    }

    public ResultResponse<int> CreateProizvod(CreateProizvodRequest request)
    {
        #region Validacije

        if (CurrentUser.KorisnikType != KorisnikEnum.Prodavac)
        {
            return new ResultResponse<int>(ResponseResultType.InternalError, "Nemate prava za kreiranje proizvoda!");

        }

        var kategorija =
            ApplicationDbContext.KategorijeProizvoda.FirstOrDefault(x => x.Id == request.KategorijaProizvodaId);
        if (kategorija == null)
        {
            return new ResultResponse<int>(ResponseResultType.NotFound, "Nepostojeća kategorija");

        }

        if (string.IsNullOrEmpty(request.Naziv))
        {
            return new ResultResponse<int>(ResponseResultType.ValidationError, "Naziv ne smije biti prazan!");

        }

        if (request.Cijena <= 0)
        {
            return new ResultResponse<int>(ResponseResultType.ValidationError, "Cijena mora biti pozitivan broj");
        }

        #endregion

        var noviProizvod = new Proizvod(
            request.Naziv,
            request.Opis,
            request.Cijena,
            StatusEnum.Dostupno,
            CurrentUser.CurrentUserId,
            request.KategorijaProizvodaId);
        ApplicationDbContext.Proizvodi.Add(noviProizvod);
        var response = new ResultResponse<int>(ResponseResultType.Success, null,
            new List<int>(){noviProizvod.Id});
        return response;

    }

    public ResultResponse<int> AzurirajCijenuProizvoda(UpdateProizvodRequest request)
    {
        #region Validacija

        var proizvod = ApplicationDbContext.Proizvodi.SingleOrDefault(x => x.Id == request.Id);
        if (proizvod == null)
        {
            return new ResultResponse<int>(ResponseResultType.NotFound, "Nepostojeći proizvod");
        }

        if (proizvod.ProdavacId != CurrentUser.CurrentUserId)
        {
            return new ResultResponse<int>(ResponseResultType.InternalError, "Nemate prava mjenjati ovaj proizvod!");
        }
        if (request.Cijena <= 0)
        {
            return new ResultResponse<int>(ResponseResultType.ValidationError, "Cijena mora biti pozitivan broj");
        }
        #endregion
        
        proizvod.Cijena = request.Cijena;
        var response = new ResultResponse<int>(ResponseResultType.Success, null,
            new List<int>(){proizvod.Id});
        return response;
    }
}