using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace IMS_InventoryManagmentSystem_.Service.IService
{
    public interface IUserService
    {
        Task<AuthDto> RegisterAsync(RegisterDTO registerDTO);
        Task<ActionResult> LoginAsync(LoginDto loginDTO);

    }
}
