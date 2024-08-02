using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Prodotto
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Nome { get; set; }

    [Range(0, 100)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Prezzo { get; set; }

    [Required, StringLength(128)]
    public required string Foto { get; set; }

    [Range(0, 60)]
    public int Tempodiconsegna { get; set; }

    public List<Ingrediente> Ingredienti { get; set; } = new();


    public ICollection<ProdottoOrdinato> ProdottiOrdinati { get; set; } = new List<ProdottoOrdinato>();
}
