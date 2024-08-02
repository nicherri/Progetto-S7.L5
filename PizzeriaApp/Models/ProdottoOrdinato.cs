using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProdottoOrdinato
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Prodotto")]
    public int ProdottoId { get; set; }
    public Prodotto Prodotto { get; set; }

    [ForeignKey("Ordine")]
    public int OrdineId { get; set; }
    public Ordine Ordine { get; set; }

    public int Quantita { get; set; }
}
