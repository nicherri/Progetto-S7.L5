using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class CheckoutViewModel
    {
        public string UserId { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        [Required]
        public string NoteAggiuntive { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
