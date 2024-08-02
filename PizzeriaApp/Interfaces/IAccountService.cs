using Models;
using ViewModels;

namespace Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<Utente> GetUserAsync(string email);
        Task<bool> RegisterAdminAsync(RegisterViewModel model);
        Task<Utente> AuthenticateAsync(string email, string password); // Aggiungi questa riga
    }
}
