using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SelfHostApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostApi.Repository
{
    public class AuthRepository : IDisposable
    {
        private DataContext db;

        private UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            db = new DataContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = db.Clients.Find(clientId);
            return client;
        }

        //RefreshToken
        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = db.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();
            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }
            db.RefreshTokens.Add(token);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await db.RefreshTokens.FindAsync(refreshTokenId);
            if (refreshToken != null)
            {
                db.RefreshTokens.Remove(refreshToken);
                return await db.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            db.RefreshTokens.Remove(refreshToken);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await db.RefreshTokens.FindAsync(refreshTokenId);
            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return db.RefreshTokens.ToList();
        }

        public void Dispose()
        {
            db.Dispose();
            _userManager.Dispose();
        }
    }
}
