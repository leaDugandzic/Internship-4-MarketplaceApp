using MarketplaceApp.Data;
using MarketplaceApp.Data.Entities.Models;
using MarketplaceApp.Data.Enums;
using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Contracts.Kupac;
using MarketplaceApp.Domain.Contracts.Prodavac;
using MarketplaceApp.Domain.Contracts.Proizvod;
using MarketplaceApp.Domain.Enum;

namespace MarketplaceApp.Domain.Repositories;

public class ProdavacRepository
{
    
    public ResultResponse<int> RegistracijaProdavac(RegisterProdavacRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Ime))
        {
            return new ResultResponse<int>(ResponseResultType.ValidationError, "Polja nisu ispravno unesena!");
        }

        var noviProdavac = new Prodavac(request.Ime, request.Email);
        ApplicationDbContext.Prodavaci.Add(noviProdavac);
        return new ResultResponse<int>(ResponseResultType.Success, null,
            new List<int>(){noviProdavac.Id});
    }
    
    
    public ResultResponse<ProdavacDto> GetByEmail(string email)
    {
        var prodavac = ApplicationDbContext.Prodavaci.FirstOrDefault(x => x.Email == email);
        if (prodavac == null)
        {
            return new ResultResponse<ProdavacDto>(ResponseResultType.NotFound, "Nepostojeći email!");
        }

        var dto = new ProdavacDto()
        {
            Ime = prodavac.Ime,
            Email = prodavac.Email,
            Id = prodavac.Id,
        };
        return new ResultResponse<ProdavacDto>(ResponseResultType.Success, null, new List<ProdavacDto>(){dto});
    }
    
    public ResultResponse<ProdavacDto> GetInfo()
    {
        var kupac = ApplicationDbContext.Prodavaci.SingleOrDefault(x => x.Id == CurrentUser.CurrentUserId);
        if (kupac == null)
        {
            var errorResponse = new ResultResponse<ProdavacDto>(ResponseResultType.InternalError, "Nešto je pošlo po krivu!");
            return errorResponse;
        }
        var dto = new ProdavacDto()
        {
            Ime = kupac.Ime,
            Email = kupac.Email,
            Id = kupac.Id,
        };
        var response = new ResultResponse<ProdavacDto>(ResponseResultType.Success, null, new List<ProdavacDto>(){dto});
        return response;
    }
    
    
    public ResultResponse<ProizvodDto> GetAllOwned()
    {
        
        var proizvodi = ApplicationDbContext.Proizvodi
            .Join(ApplicationDbContext.KategorijeProizvoda,
                proizvod => proizvod.KategorijaProizvodaId,
                kategorija => kategorija.Id,
                (proizvod, kategorija) => new ProizvodDto
                {
                    Id = proizvod.Id,
                    Naziv = proizvod.Naziv,
                    Opis = proizvod.Opis,
                    Cijena = proizvod.Cijena,
                    Status = proizvod.Status.ToString(),
                    Kategorija = kategorija.Naziv,
                    ProdavacId = proizvod.ProdavacId,
                    Prodavac = CurrentUser.CurrentUserName
                })
            .ToList();
        proizvodi = proizvodi.Where(x => x.ProdavacId == CurrentUser.CurrentUserId && x.Status == StatusEnum.Dostupno.ToString()).ToList();
        if (proizvodi.Count == 0)
        {
            return new ResultResponse<ProizvodDto>(ResponseResultType.NoData, "Nema proizvoda!");

        }
        
        return new ResultResponse<ProizvodDto>(ResponseResultType.Success, null, proizvodi);

    }

    public ResultResponse<decimal> TotalZarada()
    {
        var transakcije = ApplicationDbContext.Transakcije.Where(x => x.ProdavacId == CurrentUser.CurrentUserId).ToList();
        if (transakcije.Count == 0)
        {
            var errorResponse = new ResultResponse<decimal>(ResponseResultType.NoData, "Nemate transakcije!");
            return errorResponse;
        }

        var prodavacShares = transakcije.Select(x => x.ProdavacShare).ToList();
        decimal total = prodavacShares.Sum();
        return new ResultResponse<decimal>(ResponseResultType.Success, null, new List<decimal>(){total});
    }

    public ResultResponse<decimal> PeriodZarada(DateTime datumOd, DateTime datumDo)
    {
        var transakcije = ApplicationDbContext.Transakcije
            .Where(x => x.ProdavacId == CurrentUser.CurrentUserId)
            .Where(x => x.DatumTransakcije >= datumOd && x.DatumTransakcije <= datumDo)
            .ToList();
        if (transakcije.Count == 0)
        {
            var errorResponse = new ResultResponse<decimal>(ResponseResultType.NoData, "Nemate transakcije u odabranom periodu!");
            return errorResponse;
        }
        var prodavacShares = transakcije.Select(x => x.ProdavacShare).ToList();
        decimal total = prodavacShares.Sum();
        return new ResultResponse<decimal>(ResponseResultType.Success, null, new List<decimal>(){total});
    }
}