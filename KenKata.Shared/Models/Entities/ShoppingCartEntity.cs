using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models.Entities
{
    public class ShoppingCartEntity
    {
        [Key]
        public int Id { get; set; }
        public ShoppingCartEntity()
        {
            Items = new List<CartItemEntity>();
        }
        public List<CartItemEntity>? Items { get; set; }

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
