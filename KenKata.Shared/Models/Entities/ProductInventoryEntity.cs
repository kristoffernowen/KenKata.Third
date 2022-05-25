using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models.Entities
{
    public class ProductInventoryEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public ICollection<ProductEntity> products { get; set; }

    }
}
