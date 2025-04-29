namespace IMS_InventoryManagmentSystem_.Models
{
    public class WareHouse
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Location { get; set; } 

        ICollection<ProductWareHouse> ProductWareHouses { get; set; }

    }
}
