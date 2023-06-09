using Application.Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _dbContext;
        private readonly DbSet<User> _set;

        public UserRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<User>();

            if (_set == null)
                throw new DException($"Table {nameof(User)} doesn't exist");
        }

        public async Task<bool> UpdateUserEmailConfirmedFlag(string email)
        {
            var user = await GetUserByEmailAsync(email);

            if (user == null)
                return false;

            user.EmailConfirmed = true;
            _set.Update(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _set
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RemoveUserByEmail(string email)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null) return;

            _set.Remove(user);

            _dbContext.SaveChanges();
        }
    }
}
