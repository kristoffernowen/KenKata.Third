using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models
{
    public class ShoppingCartModel
    {
        public ShoppingCartModel()
        {
            Items = new List<CartItemModel>();
        }
        public List<CartItemModel>? Items { get; set; }

        public int SubTotalPrice { get; set; }

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
