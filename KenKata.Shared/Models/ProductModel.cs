using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models
{
    public class ProductModel
    {
        public ProductModel(int id, string name, string description, string color, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Price = price;
        }

        public int Id { get; set; }

        //[Display(Name = "Name")]
        //[Required(ErrorMessage = "This field is required")]
        public string Name { get; set; } = string.Empty;

        //[Display(Name = "Description")]
        //[Required(ErrorMessage = "This field is required")]
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; }

        //[Display(Name = "Price")]
        //[Required(ErrorMessage = "This field is required")]
        public decimal Price { get; set; }
        //public string ErrorM { get; set; } = "";
    }
}
