using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using ViewModels;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        {
            return await _context.Ordini
                .Include(o => o.ProdottiOrdinati)
                .ThenInclude(po => po.Prodotto)
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    Indirizzo = o.Indirizzo,
                    NoteAggiuntive = o.NoteAggiuntive,
                    Evaso = o.Evaso,
                    UserId = o.UtenteId.ToString(), // Convertire int a string
                    Status = o.Evaso ? "Evaded" : "Pending",
                    ProdottiOrdinati = o.ProdottiOrdinati.Select(po => new ProductViewModel
                    {
                        Id = po.ProdottoId,
                        Nome = po.Prodotto.Nome,
                        Foto = po.Prodotto.Foto,
                        Prezzo = po.Prodotto.Prezzo,
                        Tempodiconsegna = po.Prodotto.Tempodiconsegna,
                        Ingredienti = string.Join(", ", po.Prodotto.Ingredienti.Select(i => i.Nome))
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int id)
        {
            var ordine = await _context.Ordini
                .Include(o => o.ProdottiOrdinati)
                .ThenInclude(po => po.Prodotto)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (ordine == null)
            {
                return null;
            }

            return new OrderViewModel
            {
                Id = ordine.Id,
                Indirizzo = ordine.Indirizzo,
                NoteAggiuntive = ordine.NoteAggiuntive,
                Evaso = ordine.Evaso,
                UserId = ordine.UtenteId.ToString(), // Convertire int a string
                Status = ordine.Evaso ? "Evaded" : "Pending",
                ProdottiOrdinati = ordine.ProdottiOrdinati.Select(po => new ProductViewModel
                {
                    Id = po.ProdottoId,
                    Nome = po.Prodotto.Nome,
                    Foto = po.Prodotto.Foto,
                    Prezzo = po.Prodotto.Prezzo,
                    Tempodiconsegna = po.Prodotto.Tempodiconsegna,
                    Ingredienti = string.Join(", ", po.Prodotto.Ingredienti.Select(i => i.Nome))
                }).ToList()
            };
        }

        public async Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string userId)
        {
            var userIdInt = int.Parse(userId); // Conversione da string a int

            return await _context.Ordini
                .Include(o => o.ProdottiOrdinati)
                .ThenInclude(po => po.Prodotto)
                .Where(o => o.UtenteId == userIdInt) // Cambiato da o.Utente.Id.ToString() == userId
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    Indirizzo = o.Indirizzo,
                    NoteAggiuntive = o.NoteAggiuntive,
                    Evaso = o.Evaso,
                    UserId = o.UtenteId.ToString(), // Convertire int a string
                    Status = o.Evaso ? "Evaded" : "Pending",
                    ProdottiOrdinati = o.ProdottiOrdinati.Select(po => new ProductViewModel
                    {
                        Id = po.ProdottoId,
                        Nome = po.Prodotto.Nome,
                        Foto = po.Prodotto.Foto,
                        Prezzo = po.Prodotto.Prezzo,
                        Tempodiconsegna = po.Prodotto.Tempodiconsegna,
                        Ingredienti = string.Join(", ", po.Prodotto.Ingredienti.Select(i => i.Nome))
                    }).ToList()
                }).ToListAsync();
        }

        public async Task CreateOrderAsync(OrderViewModel model)
        {
            var ordine = new Ordine
            {
                Indirizzo = model.Indirizzo,
                NoteAggiuntive = model.NoteAggiuntive,
                Evaso = model.Evaso,
                UtenteId = int.Parse(model.UserId), // Conversione da string a int
                ProdottiOrdinati = model.ProdottiOrdinati.Select(po => new ProdottoOrdinato
                {
                    ProdottoId = po.Id,
                    Quantita = 1
                }).ToList()
            };

            _context.Ordini.Add(ordine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(OrderViewModel model)
        {
            var ordine = await _context.Ordini
                .Include(o => o.ProdottiOrdinati)
                .FirstOrDefaultAsync(o => o.Id == model.Id);

            if (ordine != null)
            {
                ordine.Indirizzo = model.Indirizzo;
                ordine.NoteAggiuntive = model.NoteAggiuntive;
                ordine.Evaso = model.Evaso;
                ordine.UtenteId = int.Parse(model.UserId); // Conversione da string a int
                ordine.ProdottiOrdinati = model.ProdottiOrdinati.Select(po => new ProdottoOrdinato
                {
                    ProdottoId = po.Id,
                    Quantita = 1 // Assumption: Quantity is 1
                }).ToList();

                _context.Ordini.Update(ordine);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var ordine = await _context.Ordini.FindAsync(id);
            if (ordine != null)
            {
                _context.Ordini.Remove(ordine);
                await _context.SaveChangesAsync();
            }
        }
    }
}
