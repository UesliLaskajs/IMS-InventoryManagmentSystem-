using IMS_InventoryManagmentSystem_.Models;

namespace IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo
{
    public interface IUserRepository
    {

        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task RemoveUserAsync(int userId);

    }
}
