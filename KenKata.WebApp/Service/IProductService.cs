using KenKata.Shared.Models;
using KenKata.Shared.Models.Entities;
using KenKata.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace KenKata.WebApp.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAll();       
        Task<ProductModel> Get(int productId);
        Task<Result> Create(ProductModelForm form,int category);
        Task<Result> Update(int productId, ProductModelForm model);
        Task<Result> Delete(int productId);
        Task<IEnumerable<ProductModel>> GetProductByCategory(int CategoryId);
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
            var productEntity = await _sqlContext.Products.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id == productId);
            if(productEntity != null)
            {
                product.Id = productEntity.Id;
                product.Name = productEntity.Name;
                product.Description = productEntity.Description;
                product.Color= productEntity.Color;
                product.Price = productEntity.Price;
                product.ImgUrl = productEntity.ImgUrl;
                product.Category = productEntity.Category.Name;
                
            }

            return product;
        }

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
           
            //REMOVE START. (Only temporarly for making content in database under the development phase.)
            var p1 = new ProductEntity() { Name = "Gold Rings", Description = "Jag är beskrivning för guld ringar", Color = "Guld", Price = 129, ImgUrl= "https://cdn.pixabay.com/photo/2018/08/16/19/56/wedding-rings-3611277_960_720.jpg", CategoryId=1};
            var p2 = new ProductEntity() { Name = "Armband Silver", Description = "Jag är beskrivning för armband silver", Color = "Silver", Price = 69, ImgUrl= "https://cdn.pixabay.com/photo/2017/07/24/18/47/diamonds-2535677_960_720.jpg", CategoryId = 2 };
            var p3 = new ProductEntity() { Name = "Pärlan väska", Description = "	beskrivning pärlan väskan", Color = "Black", Price = 99 ,ImgUrl= "https://cdn.pixabay.com/photo/2015/11/20/03/53/package-1052370_960_720.jpg", CategoryId = 2 };
            var p4 = new ProductEntity() { Name = "Skor svarta", Description = "svarta skor description", Color = "Black", Price = 59, ImgUrl = "https://cdn.pixabay.com/photo/2017/09/15/13/28/black-shoes-2752226_960_720.jpg",CategoryId=1 };
            var exist = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Name == p1.Name || x.Name == p2.Name || x.Name == p3.Name);
            if (exist == null)
            {
                _sqlContext.Products.Add(p1);
                _sqlContext.Products.Add(p2);
                _sqlContext.Products.Add(p3);
                _sqlContext.Products.Add(p4);
                await _sqlContext.SaveChangesAsync();
            }
            
            var list = new List<ProductModel>();
                //inkludera category
                foreach(var product in await _sqlContext.Products.Include(x => x.Category).ToListAsync())
            {
                list.Add(new ProductModel(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Color,
                    product.Price,
                    product.ImgUrl,
                    product.Category.Name
                    ));
            }

            return list;
        }
        
        public async Task<Result> Create(ProductModelForm form, int category)
        {
            var categorySelect = category;
            //var category = form.categoryList;
            //var category = await _sqlContext.Categories.FirstOrDefaultAsync(x=>x.Id=form.categoryList);
            var ProductNameExist = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Name == form.Name);
            if (ProductNameExist == null)
            {
                
                var productEntity = new ProductEntity
                {
                    Name = form.Name,
                    Description = form.
                    Description,
                    Color = form.Color,
                    Price = form.Price,
                    ImgUrl = form.ImgUrl,
                    CategoryId=category
                    
                };
                _sqlContext.Products.Add(productEntity);
                await _sqlContext.SaveChangesAsync();
                return new Result { Success = true };
            }
            return new Result { Success = false };

        }



        public async Task<Result> Update(int productId, ProductModelForm model)
        {
            var product = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product != null)
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.Color = model.Color;
                product.Price = model.Price;
                product.ImgUrl = model.ImgUrl;
                product.CategoryId = model.CategorySelected;
                _sqlContext.Update(product);
                await _sqlContext.SaveChangesAsync();
                return new Result { Success = true };
            }

            return new Result { Success = false };
        }

        public async Task<Result> Delete(int productId)
        {
            var p = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (p != null)
            {
                _sqlContext.Remove(p);
                await _sqlContext.SaveChangesAsync();
                return new Result { Success=true};
            }
            return new Result { Success=false };
        }

        public async Task<IEnumerable<ProductModel>> GetProductByCategory(int CategoryId)
        {
            var list = new List<ProductModel>();

            foreach(var product in await _sqlContext.Products.Where(x => x.CategoryId == CategoryId).Include(x=>x.Category).ToListAsync())
            {
                list.Add(new ProductModel(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Color,
                    product.Price,
                    product.ImgUrl,
                    product.Category.Name
                    ));
            } 

            return list;
        }
    }

    public class Result
    {
        public bool Success { get; set; } = false;
    }

}
