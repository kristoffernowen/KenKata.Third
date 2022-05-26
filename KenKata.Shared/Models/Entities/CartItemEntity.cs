using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models.Entities
{
    public class CartItemEntity
    {
        [Key] public int Id { get; set; }
        public ProductEntity Product { get; set; } = null!;
        public int Quantity { get; set; } = 1;
    }
}
