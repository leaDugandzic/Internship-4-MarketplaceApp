using MarketplaceApp.Data;
using MarketplaceApp.Data.Entities.Models;
using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Contracts.Kupac;
using MarketplaceApp.Domain.Contracts.Prodavac;

namespace MarketplaceApp.Domain.Repositories;

public class KupacRepository
{
    public ResultResponse<int> RegistracijaKupca(RegisterKupacRequest request)
    {
        if (request.Balance < 0 || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Ime))
        {
            return new ResultResponse<int>(ResponseResultType.ValidationError, "Polja nisu ispravno unesena!");
        }

        var noviKupac = new Kupac(request.Ime, request.Email, request.Balance);
        ApplicationDbContext.Kupci.Add(noviKupac);
        var response = new ResultResponse<int>(ResponseResultType.Success, null,
            new List<int>(){noviKupac.Id});
        return response;
    }

    public ResultResponse<KupacDto> GetInfo()
    {
        var kupac = ApplicationDbContext.Kupci.SingleOrDefault(x => x.Id == CurrentUser.CurrentUserId);
        if (kupac == null)
        {
            var errorResponse = new ResultResponse<KupacDto>(ResponseResultType.InternalError, "Nešto je pošlo po krivu!");
            return errorResponse;
        }
        var dto = new KupacDto()
        {
            Ime = kupac.Ime,
            Balance = kupac.Balance,
            Email = kupac.Email,
            Id = kupac.Id,
        };
        var response = new ResultResponse<KupacDto>(ResponseResultType.Success, null, new List<KupacDto>(){dto});
        return response;
    }
    public ResultResponse<KupacDto> GetByEmail(string email)
    {
        var kupac = ApplicationDbContext.Kupci.FirstOrDefault(x => x.Email == email);
        if (kupac == null)
        {
            var errorResponse = new ResultResponse<KupacDto>(ResponseResultType.NotFound, "Nepostojeći email!");
            return errorResponse;
        }

        var dto = new KupacDto()
        {
            Ime = kupac.Ime,
            Balance = kupac.Balance,
            Email = kupac.Email,
            Id = kupac.Id,
        };
        var response = new ResultResponse<KupacDto>(ResponseResultType.Success, null, new List<KupacDto>(){dto});
        return response;
    }

    public ResultResponse<ProizvodiOwnedDto> OwnedProizvodi()
    {
        var kupljeniProizvodi = ApplicationDbContext.Transakcije
            .Where(t => t.KupacId == CurrentUser.CurrentUserId)
            .Join(ApplicationDbContext.Proizvodi,
                transakcija => transakcija.ProizvodId,
                proizvod => proizvod.Id,
                (transakcija, proizvod) => new { transakcija, proizvod })
            .Join(ApplicationDbContext.Prodavaci,
                tp => tp.transakcija.ProdavacId,
                prodavac => prodavac.Id,
                (tp, prodavac) => new { tp.transakcija, tp.proizvod, prodavac })
            .Join(ApplicationDbContext.KategorijeProizvoda,
                tp => tp.proizvod.KategorijaProizvodaId,
                kategorija => kategorija.Id,
                (tp, kategorija) => new ProizvodiOwnedDto
                {
                    Id = tp.proizvod.Id,
                    Naziv = tp.proizvod.Naziv,
                    DatumTransakcije = tp.transakcija.DatumTransakcije,
                    Prodavac = tp.prodavac.Ime,
                    Cijena = tp.transakcija.ProizvodCijena,
                    Kategorija = kategorija.Naziv,
                    Opis = tp.proizvod.Opis,
                    ProdavacId = tp.transakcija.ProdavacId,
                })
            .ToList();

        if (kupljeniProizvodi.Count == 0)
        {
            return new ResultResponse<ProizvodiOwnedDto>(ResponseResultType.NoData, "Nema proizvoda!");
        }

        var response = new ResultResponse<ProizvodiOwnedDto>(ResponseResultType.Success, null,
            kupljeniProizvodi);
        return response;
    }
    
    
}