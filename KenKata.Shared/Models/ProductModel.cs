using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
        }

        public ProductModel(int id, string name, string description, string color, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Price = price;
        }

        public ProductModel(int id, string name, string description, string color, decimal price, string imgUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Price = price;
            ImgUrl = imgUrl;
        }

        public ProductModel(int id, string name, string description, string color, decimal price, string imgUrl, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Price = price;
            ImgUrl = imgUrl;
            Category = category;
        }

        public int Id { get; set; }

        public string Name { get; set; } 

        public string Description { get; set; } 
        public string Color { get; set; }

        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public string Category { get; set; }

    }
}
