namespace Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingrediente>> GetAllIngredientsAsync();
        Task<Ingrediente> GetIngredientByIdAsync(int id);
        Task CreateIngredientAsync(Ingrediente ingrediente);
        Task UpdateIngredientAsync(Ingrediente ingrediente);
        Task DeleteIngredientAsync(int id);
    }
}
