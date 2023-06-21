using System.Security.Authentication;
using DataAccess.Models;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        private bool disposed = false;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> CheckIfUserExist(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Name == username))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CheckUserPassword(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Name == user.Name);
            if (dbUser.Password == user.Password)
            {
                return true;
            }

            return false;
        }


        public async Task<int> AddUser(User user)
        {
           var entity = await _context.Users.AddAsync(user);
           return entity.Entity.Id;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
