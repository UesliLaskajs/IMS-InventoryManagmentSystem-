namespace IMS_InventoryManagmentSystem_.Models
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int Quantity { get; set; }

        public string? Sku  {get;set;}

        public string? Barcode { get; set;}

    }
}
