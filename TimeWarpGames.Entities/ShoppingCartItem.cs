namespace TimeWarpGames.Entities
{
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;

        public ShoppingCartItem(int productId, string name, int quantity, decimal price)
        {
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}