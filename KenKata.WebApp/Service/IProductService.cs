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
        Task<IEnumerable<ProductModel>> GetProdúctByColor(string Color);
        Task<IEnumerable<ColorEntity>> GetAllColors();
    }

    public class ProductService : IProductService
    {
        private readonly SqlContext _sqlContext;
        private readonly ICategoryService _categoryService; //ta bort sen!

        public ProductService(SqlContext sqlContext, ICategoryService categoryService)
        {
            _sqlContext = sqlContext;
            _categoryService = categoryService;
        }

        public async Task<ProductModel> Get(int productId)
        {

            var product = new ProductModel();
            var productEntity = await _sqlContext.Products.Include(x=>x.Category).Include(x=>x.Color).Include(x=>x.ProductInventory).FirstOrDefaultAsync(x=>x.Id == productId);
            if(productEntity != null)
            {
                product.Id = productEntity.Id;
                product.Name = productEntity.Name;
                product.Description = productEntity.Description;
                product.Color= productEntity.Color.Name;
                product.Price = productEntity.Price;
                product.ImgUrl = productEntity.ImgUrl;
                product.Category = productEntity.Category.Name;
                product.Quantity = productEntity.ProductInventory.Quantity;
            }
            return product;
        }

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            //REMOVE START. (Only temporarly for making content in database under the development phase.)
            //var color1 = new ColorEntity() { Name = "Red" };
            //var color2 = new ColorEntity() { Name = "Black" };
            //var colorExist = await _sqlContext.Colors.FirstOrDefaultAsync(x => x.Name == color1.Name || x.Name == color2.Name);

            //if (colorExist == null)
            //{
            //    _sqlContext.Colors.Add(color1);
            //    _sqlContext.Colors.Add(color2);
            //    await _sqlContext.SaveChangesAsync();
            //}
            //var initializeCategorys = await _categoryService.GetAll();

            //var inventory = new ProductInventoryEntity() { Quantity=20};
            //var Quantityexist= await _sqlContext.ProductsInventory.FirstOrDefaultAsync(x => x.Quantity == 20);
            //if(Quantityexist ==null)
            //{
            //    _sqlContext.ProductsInventory.Add(inventory);
            //    await _sqlContext.SaveChangesAsync();
            //}

            //var p1 = new ProductEntity() { Name = "Gold Ring", Description = "Jag är beskrivning för guld ringar", ColorId = color1.Id, Price = 129, ImgUrl = "https://cdn.pixabay.com/photo/2018/08/16/19/56/wedding-rings-3611277_960_720.jpg", CategoryId = , ProductInventoryId=Quantityexist.Id };
            //var p2 = new ProductEntity() { Name = "Armband", Description = "Jag är beskrivning för armband silver", ColorId = color1.Id, Price = 69, ImgUrl = "https://cdn.pixabay.com/photo/2017/07/24/18/47/diamonds-2535677_960_720.jpg", CategoryId = 2, ProductInventoryId = Quantityexist.Id };
            //var p3 = new ProductEntity() { Name = "väska", Description = "	beskrivning pärlan väskan", ColorId = color2.Id, Price = 99, ImgUrl = "https://cdn.pixabay.com/photo/2015/11/20/03/53/package-1052370_960_720.jpg", CategoryId = 2, ProductInventoryId = Quantityexist.Id };
            //var p4 = new ProductEntity() { Name = "Skor", Description = "svarta skor description", ColorId = color2.Id, Price = 59, ImgUrl = "https://cdn.pixabay.com/photo/2017/09/15/13/28/black-shoes-2752226_960_720.jpg", CategoryId = 1, ProductInventoryId = Quantityexist.Id };
            //var exist = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Name == p1.Name || x.Name == p2.Name || x.Name == p3.Name);
            //if (exist == null)
            //{
            //    _sqlContext.Products.Add(p1);
            //    _sqlContext.Products.Add(p2);
            //    _sqlContext.Products.Add(p3);
            //    _sqlContext.Products.Add(p4);
            //    await _sqlContext.SaveChangesAsync();
            //}

            //REMOVE END


            var list = new List<ProductModel>();
                //inkludera category
                foreach(var product in await _sqlContext.Products.Include(x => x.Category).Include(x=>x.Color).Include(x=>x.ProductInventory).ToListAsync())
            {
                list.Add(new ProductModel(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Color.Name,
                    product.Price,
                    product.ImgUrl,
                    product.Category.Name,
                    product.ProductInventory.Quantity
                    ));
            }
            
            return list;
        }
        
        public async Task<Result> Create(ProductModelForm form, int category)
        {
            var categorySelect = category;
            var ProductNameExist = await _sqlContext.Products.AnyAsync(x => x.Name == form.Name);
            var colorExist = await _sqlContext.Colors.AnyAsync(x => x.Name == form.Color);
            var productInventoryExist = await _sqlContext.ProductsInventory.AnyAsync(x => x.Quantity == form.Quantity);

            if (!colorExist)
            {
                _sqlContext.Colors.Add(new ColorEntity { Name = form.Color });
                await _sqlContext.SaveChangesAsync();   
            }

            if (!productInventoryExist)
            {
                _sqlContext.ProductsInventory.Add(new ProductInventoryEntity { Quantity = form.Quantity });
                await _sqlContext.SaveChangesAsync();
            }

            if (!ProductNameExist)
            {
                var productInventory = await _sqlContext.ProductsInventory.FirstOrDefaultAsync(x => x.Quantity==form.Quantity);
                var color = await _sqlContext.Colors.FirstOrDefaultAsync(x => x.Name == form.Color);
                var productEntity = new ProductEntity
                {
                    Name = form.Name,
                    Description = form.Description,
                    ColorId = color.Id,
                    Price = form.Price,
                    ProductInventoryId=productInventory.Id,
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
            
            var colorExist = await _sqlContext.Colors.FirstOrDefaultAsync(x => x.Name == model.Color);
            if (colorExist == null)
            {
                _sqlContext.Colors.Add(new ColorEntity { Name = model.Color });
                await _sqlContext.SaveChangesAsync();
            }
            var productInventoryExist = await _sqlContext.ProductsInventory.FirstOrDefaultAsync(x => x.Quantity == model.Quantity);
            if (productInventoryExist == null)
            {
                _sqlContext.ProductsInventory.Add(new ProductInventoryEntity { Quantity = model.Quantity });
                await _sqlContext.SaveChangesAsync();
            }
            var inventory = await _sqlContext.ProductsInventory.Where(x => x.Quantity == model.Quantity).FirstOrDefaultAsync();
            var color = await _sqlContext.Colors.Where(x => x.Name == model.Color).FirstOrDefaultAsync();
            if (product != null)
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.ColorId = color.Id;
                product.Price = model.Price;
                product.ProductInventoryId = inventory.Id;
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
            foreach(var product in await _sqlContext.Products.Where(x => x.CategoryId == CategoryId).Include(x=>x.Category).Include(x=>x.Color).ToListAsync())
            {
                list.Add(new ProductModel(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Color.Name,
                    product.Price,
                    product.ImgUrl,
                    product.Category.Name
                    ));
            } 
            return list;
        }

        public async Task<IEnumerable<ProductModel>> GetProdúctByColor(string Color)
        {
            var productByColor = new List<ProductModel>();
            foreach (var product in await _sqlContext.Products.Where(x => x.Color.Name== Color).Include(x => x.Category).ToListAsync())
            {
                productByColor.Add(new ProductModel(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Color.Name,
                    product.Price,
                    product.ImgUrl,
                    product.Category.Name
                    ));
            }
            return productByColor;
        }

        public async Task<IEnumerable<ColorEntity>> GetAllColors()
        {
            var colorEntity = await _sqlContext.Colors.Include(x=>x.products).ToListAsync();
            return colorEntity;
        }
    }

    public class Result
    {
        public bool Success { get; set; } = false;
    }

}
