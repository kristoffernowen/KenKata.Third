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

        public GetAllViewModel(IEnumerable<ProductModel> product)
        {
            this.product = product;
        }

        public GetAllViewModel(IEnumerable<CategoryEntity> category)
        {
            this.category = category;
        }

        public GetAllViewModel(IEnumerable<ProductModel> product, IEnumerable<CategoryEntity> category)
        {
            this.product = product;
            this.category = category;
        }

        public GetAllViewModel(IEnumerable<ProductModel> product, IEnumerable<CategoryEntity> category, int selectedCategoryId)
        {
            this.product = product;
            this.category = category;
            SelectedCategoryId = selectedCategoryId;
        }

        public IEnumerable<ProductModel> product { get; set; }
        public IEnumerable<CategoryEntity> category { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}
