namespace IMS_InventoryManagmentSystem_.Models.DTO
{
    public class RegisterDTO
    {
        public int Id { get; set; }
        public required string UserName { get; set; } = string.Empty;
        public  string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
