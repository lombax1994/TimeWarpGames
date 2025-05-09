namespace TimeWarpGames.Entities
{

    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;

        // Voeg de Stock van het product toe
        public int Stock { get; set; }

        // Constructor
        public ShoppingCartItem(int productId, string name, int quantity, decimal price, int stock)
        {
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            Price = price;
            Stock = stock; // Voeg voorraad toe aan de constructor
        }
    }
}