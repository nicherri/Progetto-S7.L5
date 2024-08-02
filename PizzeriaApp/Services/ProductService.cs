using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            return await _context.Prodotti
                .Include(p => p.Ingredienti)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Foto = p.Foto,
                    Prezzo = p.Prezzo,
                    Tempodiconsegna = p.Tempodiconsegna,
                    Ingredienti = string.Join(", ", p.Ingredienti.Select(i => i.Nome))
                })
                .ToListAsync();
        }

        public async Task CreateProductAsync(ProductViewModel model)
        {
            var product = new Prodotto
            {
                Nome = model.Nome,
                Foto = model.Foto,
                Prezzo = model.Prezzo,
                Tempodiconsegna = model.Tempodiconsegna,
                Ingredienti = model.Ingredienti.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                               .Select(i => new Ingrediente { Nome = i.Trim() }).ToList()
            };

            _context.Prodotti.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int id)
        {
            var product = await _context.Prodotti
                .Include(p => p.Ingredienti)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            return new ProductViewModel
            {
                Id = product.Id,
                Nome = product.Nome,
                Foto = product.Foto,
                Prezzo = product.Prezzo,
                Tempodiconsegna = product.Tempodiconsegna,
                Ingredienti = string.Join(", ", product.Ingredienti.Select(i => i.Nome))
            };
        }

        public async Task UpdateProductAsync(ProductViewModel model)
        {
            var product = await _context.Prodotti
                .Include(p => p.Ingredienti)
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product != null)
            {
                product.Nome = model.Nome;
                product.Foto = model.Foto;
                product.Prezzo = model.Prezzo;
                product.Tempodiconsegna = model.Tempodiconsegna;
                product.Ingredienti = model.Ingredienti.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                       .Select(i => new Ingrediente { Nome = i.Trim() }).ToList();

                _context.Prodotti.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Prodotti.FindAsync(id);
            if (product != null)
            {
                _context.Prodotti.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
