using System.Linq;
using System.Threading.Tasks;
using ToggleService.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ToggleService.WebApi
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
        {
            //create database schema if none exists
            _context.Database.EnsureCreated();


            await CreateUser("administrator@toggleservice.com", "sW5brAwaCu=a", "Administrator", "Full");
            await CreateUser("clienta@toggleservice.com", "c=ba?UdRet5C", "Feature", "ServiceA");
            await CreateUser("clientb@toggleservice.com", "c=ba?UdRet5C", "Feature", "ServiceB");
            await CreateUser("clientc@toggleservice.com", "c=ba?UdRet5C", "Feature", "ServiceC");
        }

        private async Task CreateUser(string user, string password, string role, string appkey = null)
        {
            if (!_context.Roles.Any(r => r.Name == role))
                await _roleManager.CreateAsync(new IdentityRole(role));
            else
                await _roleManager.UpdateAsync(new IdentityRole(role));

            var userFind = await _userManager.FindByNameAsync(user);

            switch (userFind)
            {
                case null:
                    var result =    await _userManager.CreateAsync(new ApplicationUser {UserName = user, Email = user, EmailConfirmed = false, AppKeyName = appkey},
                        password);
                    if(result.Succeeded)
                        await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), role);
                    break;
                default:
                    await _userManager.DeleteAsync(userFind);
                    break;
            }
        }
    }

    public interface IDbInitializer
    {
        void Initialize();
    }
}
