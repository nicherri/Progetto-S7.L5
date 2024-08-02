using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class IngredientService : IIngredientService
    {
        private readonly AppDbContext _context;

        public IngredientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingrediente>> GetAllIngredientsAsync()
        {
            return await _context.Ingredienti.OrderBy(i => i.Nome).ToListAsync();
        }

        public async Task<Ingrediente> GetIngredientByIdAsync(int id)
        {
            return await _context.Ingredienti.FindAsync(id);
        }

        public async Task CreateIngredientAsync(Ingrediente ingrediente)
        {
            _context.Ingredienti.Add(ingrediente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIngredientAsync(Ingrediente ingrediente)
        {
            _context.Ingredienti.Update(ingrediente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingrediente = await _context.Ingredienti.FindAsync(id);
            if (ingrediente != null)
            {
                _context.Ingredienti.Remove(ingrediente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
