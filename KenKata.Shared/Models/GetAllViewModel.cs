using KenKata.Shared.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models
{
    public class GetAllViewModel
    {
        public GetAllViewModel()
        {
        }

        

        public IEnumerable<ProductModel> product { get; set; }
        public IEnumerable<CategoryEntity> category { get; set; }
        public IEnumerable<ColorEntity> colors { get; set; }
        public int SelectedCategoryId { get; set; }
        public string SelectedColor { get; set; }

    }
}
