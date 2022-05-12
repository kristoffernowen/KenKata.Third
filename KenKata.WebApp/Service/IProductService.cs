using KenKata.Shared.Models;
using KenKata.Shared.Models.Entities;
using KenKata.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace KenKata.WebApp.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductEntity>> GetAll();
        //Task<Bool> CreateAsync(ProductModelForm form);
        Task<ProductModel> Get(int productId);
        //Task<ProductModel> UpdateAsync();
        //Task DeleteAsync(int productId);
    }

    public class ProductService : IProductService
    {
        private readonly SqlContext _sqlContext;

        public ProductService(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<ProductModel> Get(int productId)
        {
            var product = new ProductModel();
            var productEntity = await _sqlContext.Products.FirstOrDefaultAsync(x=>x.Id == productId);
            if(productEntity != null)
            {
                product.Id = productEntity.Id;
                product.Name = productEntity.Name;
                product.Description = productEntity.Description;
                product.Color= productEntity.Color;
                product.Price = productEntity.Price;
            }

            return product;
        }

        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            //REMOVE START. (Only temporarly for making content in database under the development phase.)
            var p1 = new ProductEntity() { Name = "Väska", Description = "Jag är en beskrivning för väska", Color = "Red", Price = 290 };
            var p2 = new ProductEntity() { Name = "Armband ", Description = "Jag är beskrivning för armband", Color = "Red", Price = 466, };
            var p3 = new ProductEntity() { Name = "Armband Silver", Description = "Jag är beskrivning för armband silver", Color = "Red", Price = 400 };
            var exist = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Name == p1.Name || x.Name == p2.Name || x.Name == p3.Name);
            if (exist == null)
            {
                _sqlContext.Products.Add(p1);
                _sqlContext.Products.Add(p2);
                _sqlContext.Products.Add(p3);
                await _sqlContext.SaveChangesAsync();
            }
            //REMOVE END
            return await _sqlContext.Products.ToListAsync();
        }
    }

}
