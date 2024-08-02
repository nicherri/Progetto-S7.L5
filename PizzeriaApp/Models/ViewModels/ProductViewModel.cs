using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Url]
        public string Foto { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        public decimal Prezzo { get; set; }

        [Required]
        [Range(1, 120)]
        public int Tempodiconsegna { get; set; }

        [Required]
        public string Ingredienti { get; set; }
        public bool Disponibile { get; set; }  // Nuovo campo
    }
}
