using MarketplaceApp.Data.Enums;

namespace MarketplaceApp.Data.Entities.Models;

public class Transakcija
{
    private static int _nextId = 1;

    public Transakcija(int proizvodId, int kupacId, int prodavacId, DateTime datumTransakcije, TransactionEnum transactionType, decimal prodavacShare, decimal proizvodCijena, decimal marketPlaceShare)
    {
        ProizvodId = proizvodId;
        KupacId = kupacId;
        ProdavacId = prodavacId;
        DatumTransakcije = datumTransakcije;
        TransactionType = transactionType;
        ProdavacShare = prodavacShare;
        ProizvodCijena = proizvodCijena;
        MarketPlaceShare = marketPlaceShare;
        Id = _nextId++;

    }

    public int Id { get; set; }
    public int ProizvodId { get; set; }
    public int KupacId { get; set; }
    public int ProdavacId { get; set; }
    public DateTime DatumTransakcije { get; set; }
    public TransactionEnum TransactionType { get; set; }
    public decimal MarketPlaceShare { get; set; }
    public decimal ProizvodCijena { get; set; }
    public decimal ProdavacShare { get; set; }
    
    public virtual Proizvod Proizvod { get; set; }

    public virtual Prodavac Prodavac { get; set; }
    public virtual Kupac Kupac { get; set; }

}