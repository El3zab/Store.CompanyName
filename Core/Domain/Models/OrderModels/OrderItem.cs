namespace Domain.Models.OrderModels
{
    public class OrderItem : BaseEntity<Guid>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItem myProperty, int quntity, decimal price)
        {
            MyProperty = myProperty;
            Quntity = quntity;
            Price = price;
        }

        public ProductInOrderItem MyProperty { get; set; }
        public int Quntity { get; set; }
        public decimal Price { get; set; }
    }
}