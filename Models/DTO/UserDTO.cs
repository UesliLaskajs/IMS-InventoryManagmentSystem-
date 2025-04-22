namespace IMS_InventoryManagmentSystem_.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public  string UserName { get; set; } = string.Empty;
        public  string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;


    }
}
