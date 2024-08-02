using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Indirizzo { get; set; }
        [Required]
        public string NoteAggiuntive { get; set; }

        [Required]
        public bool Evaso { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Status { get; set; }

        public List<ProductViewModel> ProdottiOrdinati { get; set; } = new();
    }
}
