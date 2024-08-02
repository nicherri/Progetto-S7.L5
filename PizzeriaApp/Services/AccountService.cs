using Data;
using Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Security.Claims;
using ViewModels;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AccountService> _logger;

        public AccountService(AppDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<AccountService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            if (model == null)
                return false;

            var user = new Utente { Nome = model.Nome, Email = model.Email, Password = model.Password };
            _context.Utenti.Add(user);
            await _context.SaveChangesAsync();

            var role = await _context.Ruoli.FirstOrDefaultAsync(r => r.Nome == "User");
            if (role == null)
            {
                role = new Ruolo { Nome = "User" };
                _context.Ruoli.Add(role);
                await _context.SaveChangesAsync();
            }

            _context.UtentiRuoli.Add(new UtenteRuolo { UtenteId = user.Id, RuoloId = role.Id });
            await _context.SaveChangesAsync();

            _httpContextAccessor.HttpContext.Session.SetString("UserId", user.Id.ToString());
            _httpContextAccessor.HttpContext.Session.SetString("UserName", user.Nome);

            return true;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            if (model == null)
                return false;

            var user = await _context.Utenti
                .Include(u => u.UtentiRuoli)
                .ThenInclude(ur => ur.Ruolo)
                .Where(u => u.Email == model.Email && u.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UserId", user.Id.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("UserName", user.Nome);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                foreach (var role in user.UtentiRuoli.Select(ur => ur.Ruolo.Nome))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = model.RememberMe };

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                if (user.UtentiRuoli.Any(ur => ur.Ruolo.Nome == "Admin"))
                {
                    _logger.LogInformation("User is an admin.");
                }
                else
                {
                    _logger.LogInformation("User is a regular user.");
                }

                return true;
            }

            return false;
        }

        public async Task<Utente> GetUserAsync(string email)
        {
            return await _context.Utenti
                .Include(u => u.UtentiRuoli)
                .ThenInclude(ur => ur.Ruolo)
                .FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _httpContextAccessor.HttpContext.Session.Remove("UserId");
            _httpContextAccessor.HttpContext.Session.Remove("UserName");
        }

        public Task<bool> RegisterAdminAsync(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Utente> AuthenticateAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}