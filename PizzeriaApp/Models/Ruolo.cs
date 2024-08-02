using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Ruolo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Nome { get; set; }
        public ICollection<UtenteRuolo> UtentiRuoli { get; set; } = new List<UtenteRuolo>();
    }
}
