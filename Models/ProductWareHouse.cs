namespace IMS_InventoryManagmentSystem_.Models
{
    public class ProductWareHouse
    {
        public int Id { get; set; }

        public int productId { get; set; }
        public Product Product { get; set; }

        public int WareHouseId { get; set; }
        public WareHouse WareHouse { get; set; }

        public int Quantity { get; set; }
    }
}
