using System.Collections.Generic;
using System.Linq;
using TimeWarpGames.Entities;
using System.Web;


namespace TimeWarpGames.Bll
{
    public class ShoppingCartBll
    {
        private const string SessionKey = "ShoppingCart";

        private HttpSessionStateBase _session;

        public ShoppingCartBll(HttpSessionStateBase session)
        {
            _session = session;
        }

        public List<ShoppingCartItem> GetCart()
        {
            if (_session[SessionKey] == null)
                _session[SessionKey] = new List<ShoppingCartItem>();

            return (List<ShoppingCartItem>)_session[SessionKey];
        }

        public void SaveCart(List<ShoppingCartItem> cart)
        {
            _session[SessionKey] = cart;
        }

        public void AddProduct(ShoppingCartItem newItem)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(i => i.ProductId == newItem.ProductId);

            if (existingItem != null)
                existingItem.Quantity += newItem.Quantity;
            else
                cart.Add(newItem);

            SaveCart(cart);
        }

        public void RemoveProduct(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                if (quantity <= 0)
                    cart.Remove(item);
                else
                    item.Quantity = quantity;

                SaveCart(cart);
            }
        }

        public void ClearCart()
        {
            _session[SessionKey] = null;
        }

        public decimal GetTotal()
        {
            var cart = GetCart();
            return cart.Sum(item => item.TotalPrice);
        }

        public int GetItemCount()
        {
            var cart = GetCart();
            return cart.Sum(item => item.Quantity);
        }

        public int GetQuantityInCart(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            return item?.Quantity ?? 0;
        }
    }
}