using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models
{
    public class CartItemModel
    {
        public ProductModel Product { get; set; } = null!;
        public int Quantity { get; set; } = 1;

        public decimal SubTotal
        {
            get
            {
                var _value = Product.Price * Quantity;

                return _value;
            }
        }
    }
}
