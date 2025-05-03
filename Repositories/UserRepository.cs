using IMS_InventoryManagmentSystem_.Data;
using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;

namespace IMS_InventoryManagmentSystem_.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDbContext _dbContext;
        public readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationDbContext dbContext,ILogger<UserRepository> logger )
        {
            _dbContext = dbContext;
            _logger=logger;
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.User.AddAsync(user); 
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return  await _dbContext.User.ToListAsync();
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task RemoveUserAsync(int userId)
        {
            var userDeletion = await _dbContext.User.FindAsync(userId);
            if (userDeletion != null)
            {
                _dbContext.User.Remove(userDeletion);
                await  _dbContext.SaveChangesAsync();
            }
            else
            {
                Log.Error("User is Not Found");
            }

        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async  Task UpdateUserAsync(User user)
        {
                var userTobeUpdated = await _dbContext.User.FindAsync(user);
                if (userTobeUpdated != null) {
                    _dbContext.User.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    Log.Error("User to be Updated is Not Found");
                }

        }
    }
}
