using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Ordine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UtenteId { get; set; }

        [ForeignKey("UtenteId")]
        public virtual Utente Utente { get; set; }

        public bool Evaso { get; set; }
        public string Indirizzo { get; set; }
        public string NoteAggiuntive { get; set; }
        public List<ProdottoOrdinato> ProdottiOrdinati { get; set; } = new();
    }
}
