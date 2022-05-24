

namespace KenKata.Shared.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<CartItem>();
        }
        public List<CartItem>? Items { get; set; }

        public int TotalQuantity
        {
            get
            {
                int _value = 0;

                if (Items.Count > 0)
                {
                    foreach (var item in Items)
                    {
                        _value += item.Quantity;
                    }
                }

                return _value;
            }
        }

        public decimal TotalPrice
        {
            get
            {
                decimal _value = 0;

                if (Items.Count > 0)
                {
                    foreach (var item in Items)
                    {
                        _value += item.Product.Price * item.Quantity;
                    }
                }

                return _value;
            }
        }
    }
}
