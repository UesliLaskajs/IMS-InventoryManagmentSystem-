namespace IMS_InventoryManagmentSystem_.Models.DTO
{
    public class AuthDto
    {

            public string? Token { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
            public DateTime Expiration { get; set; }
        
    }
}
