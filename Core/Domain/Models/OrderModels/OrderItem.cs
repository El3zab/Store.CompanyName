namespace Domain.Models.OrderModels
{
    public class OrderItem : BaseEntity<Guid>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItem product, int quntity, decimal price)
        {
            Product = product;
            Quntity = quntity;
            Price = price;
        }

        public ProductInOrderItem Product { get; set; }
        public int Quntity { get; set; }
        public decimal Price { get; set; }
    }
}