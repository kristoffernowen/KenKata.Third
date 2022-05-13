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
        //public int Id { get; set; }

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
        public string ErrorM { get; set; } = "";
    }
}
