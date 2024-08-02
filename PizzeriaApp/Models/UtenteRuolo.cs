namespace Models
{
    public class UtenteRuolo
    {
        public int UtenteId { get; set; }
        public Utente Utente { get; set; }
        public int RuoloId { get; set; }
        public Ruolo Ruolo { get; set; }
    }
}
