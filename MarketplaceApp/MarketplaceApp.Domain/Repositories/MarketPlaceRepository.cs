using MarketplaceApp.Data;
using MarketplaceApp.Data.Entities.Models;
using MarketplaceApp.Data.Enums;
using MarketplaceApp.Domain.Common;

namespace MarketplaceApp.Domain.Repositories;

public class MarketPlaceRepository
{
    public ResultResponse<int> KupiProizvod(int proizvodId)
    {
        var proizvod = ApplicationDbContext.Proizvodi.Where(x => x.Id == proizvodId).FirstOrDefault();
        if (proizvod == null)
        {
            return new ResultResponse<int>(ResponseResultType.NotFound, "Nepostojeći proizvod");
        }

        if (proizvod.Status == StatusEnum.Prodano)
        {
            return new ResultResponse<int>(ResponseResultType.ValidationError, "Proizvod je prodan!");
        }
        var kupac = ApplicationDbContext.Kupci.Where(x => x.Id == CurrentUser.CurrentUserId).First();
        if (kupac.Balance < proizvod.Cijena)
        {
            return new ResultResponse<int>(ResponseResultType.ValidationError, "Nemate dovoljno sredstava na racunu!"); //brvn wnvxs xex punmjc
        }
        var prodavac = ApplicationDbContext.Prodavaci.Where(x=> x.Id == proizvod.ProdavacId).FirstOrDefault();
        
        proizvod.Status = StatusEnum.Prodano;
        kupac.Balance -= proizvod.Cijena;
        var transakcija = new Transakcija(
            proizvod.Id, 
            kupac.Id, 
            prodavac.Id, 
            DateTime.Now, 
            TransactionEnum.Prodaja, 
            proizvod.Cijena*0.95m,
            proizvod.Cijena, 
            proizvod.Cijena*0.05m);
        ApplicationDbContext.Transakcije.Add(transakcija);
        var response = new ResultResponse<int>(ResponseResultType.Success, null,
            new List<int>(){transakcija.Id});
        return response;
    }

    public ResultResponse<int> VratiProizvod(int proizvodId)
    {
        var transakcija = ApplicationDbContext.Transakcije.Where(x => x.ProizvodId == proizvodId && x.KupacId == CurrentUser.CurrentUserId).FirstOrDefault();
        if (transakcija == null)
        {
            var errorResponse = new ResultResponse<int>(ResponseResultType.NotFound, "Proizvod nije u vašem vlasništvu");
            return errorResponse;
        }
        var proizvod = ApplicationDbContext.Proizvodi.Where(x => x.Id == proizvodId).First();
        proizvod.Status = StatusEnum.Dostupno;
        decimal povratIznos = transakcija.ProizvodCijena * 0.80m;
        var kupac = ApplicationDbContext.Kupci.Where(x => x.Id == CurrentUser.CurrentUserId).First();
        kupac.Balance += povratIznos;
        var prodavac = ApplicationDbContext.Prodavaci.Where(x => x.Id == proizvod.ProdavacId).First();
        var novaTransakcija = new Transakcija(proizvodId, kupac.Id, prodavac.Id, DateTime.Now, TransactionEnum.Povrat,
            -povratIznos, transakcija.ProizvodCijena, 0);
        ApplicationDbContext.Transakcije.Add(novaTransakcija);
        var response = new ResultResponse<int>(ResponseResultType.Success, null, new List<int>(){novaTransakcija.Id});
        return response;

    }
}