namespace Common.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public int InventoryBalance { get; set; }
    }
}
