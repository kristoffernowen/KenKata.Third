using KenKata.Shared.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models
{
    public class ProductModelForm
    {
        public ProductModelForm()
        {
        }

        public ProductModelForm(int id, string name, string description, decimal price, string color, string imgUrl, int categorySelected, IEnumerable<CategoryEntity> categoryList, string errorM)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Color = color;
            ImgUrl = imgUrl;
            CategorySelected = categorySelected;
            this.categoryList = categoryList;
            ErrorM = errorM;
        }

        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Price")]
        [Required(ErrorMessage = "This field is required")]
        public decimal Price { get; set; }

        [Display(Name = "color")]
        [Required(ErrorMessage = "This field is required")]
        public string Color { get; set; } = "";

        [Display(Name = "Image Url")]
        [Required]
        public string ImgUrl { get; set; } = "";
        public int CategorySelected { get; set; }

        [Display(Name = "Category")]
        //[Required]
        public IEnumerable<CategoryEntity> categoryList { get; set; } =new List<CategoryEntity>();
        public string ErrorM { get; set; } = "";
    }
}
