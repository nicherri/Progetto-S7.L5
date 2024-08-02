using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Utente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string Nome { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(20)]
        public string Password { get; set; }
        public ICollection<UtenteRuolo> UtentiRuoli { get; set; } = new List<UtenteRuolo>();
    }
}
