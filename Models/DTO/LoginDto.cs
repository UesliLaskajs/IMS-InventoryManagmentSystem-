namespace IMS_InventoryManagmentSystem_.Models.DTO
{
    public class LoginDto
    {
        public required string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
