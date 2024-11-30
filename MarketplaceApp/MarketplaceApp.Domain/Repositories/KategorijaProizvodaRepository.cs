using MarketplaceApp.Data;
using MarketplaceApp.Data.Entities.Models;
using MarketplaceApp.Domain.Common;
using MarketplaceApp.Domain.Contracts.KategorijaProizvoda;
using Exception = System.Exception;

namespace MarketplaceApp.Domain.Repositories;

public class KategorijaProizvodaRepository
{
       public ResultResponse<int> CreateKategorijaProizvoda(string naziv)
       {
          try
          {
             if (string.IsNullOrEmpty(naziv))
             {
                return new ResultResponse<int>(ResponseResultType.ValidationError, "Naziv ne smije biti prazan!");
             }
             var novaKategorijaProizvoda = new KategorijeProizvoda(naziv);
             ApplicationDbContext.KategorijeProizvoda.Add(novaKategorijaProizvoda);
             return new ResultResponse<int>(ResponseResultType.Success, null,
                new List<int>(novaKategorijaProizvoda.Id));
          }
          catch (Exception e)
          {
             return new ResultResponse<int>(ResponseResultType.InternalError, e.Message);
          }
          
       }

       public ResultResponse<KategorijaProizvodaDto> GetAllKategorijaProizvoda()
       {
          try
          {
               var kategorije = ApplicationDbContext.KategorijeProizvoda.ToList();
               if (kategorije.Count == 0)
               {
                  return new ResultResponse<KategorijaProizvodaDto>(ResponseResultType.NoData, "Nema podataka za kategorije proizvoda!");
               }
               var result = new List<KategorijaProizvodaDto>();
               foreach (var kategorija in kategorije)
               {
                  var oneKategorijaProizvoda = new KategorijaProizvodaDto
                  {
                     Id = kategorija.Id,
                     Naziv = kategorija.Naziv,
                  };
                  result.Add(oneKategorijaProizvoda);
               }
               return new ResultResponse<KategorijaProizvodaDto>(ResponseResultType.Success, null, result);
          }
          catch (Exception e)
          {
             Console.WriteLine(e); 
             return new ResultResponse<KategorijaProizvodaDto>(ResponseResultType.InternalError, e.Message);
          }
       }
}