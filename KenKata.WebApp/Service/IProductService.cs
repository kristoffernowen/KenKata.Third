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
        Task<ProductModel> Get(int productId);
        Task<Result> Create(ProductModelForm form);
        Task<Result> Update(int productId, ProductModelForm model);
        Task<Result> Delete(int productId);
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
                product.ImgUrl = productEntity.ImgUrl;
                
            }

            return product;
        }

        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            //REMOVE START. (Only temporarly for making content in database under the development phase.)
            var p1 = new ProductEntity() { Name = "A KARMAMIA MILA DRESS (SHORT)", Description = "Den supersnygga klänning i leo från Karmamia Cph är ett måste i garderoben. Den lilla coola klänningen är en given räddare i nöden. Passar till alla tillfällen och håller i flera år framöver. Du kan både kan bära till fest och jobb. Material: 100% Satin polyester", Color = "Black", Price = 2190, ImgUrl= "https://dst15js82dk7j.cloudfront.net/251590/97805085-2Zgv6.jpg" };
            var p2 = new ProductEntity() { Name = "A NÜ DANMARK JANE TOP CREME", Description = "Säsongens snyggaste topp med så snygga detaljer. Så följsam i tygget och så fin alla outfits. Normal i storleken", Color = "White", Price = 699, ImgUrl= "https://dst15js82dk7j.cloudfront.net/251590/97496254-HMyZE.jpg" };
            var p3 = new ProductEntity() { Name = "IOAKU BERRY SPARKLE BRACELET", Description = "NYHET!!! Elegant och väldgt unik armband från IOAKU Design: Fanny Ek. Armbandet är tillverkade i alloy med en plätering av 18k guld för optimal hållbarhet. Stenarna på smycken är gjorda av kristaller. Alla smycken är testade för nickel, bly och kadmium.BERRY SPARKLE BRACELET - är elegant,kvinnlig samt väldigt cool armand med exklusiv desig.Levereras i en exklusiv smyckesbox från IOAKU", Color = "Gold", Price = 999 ,ImgUrl= "https://dst15js82dk7j.cloudfront.net/251590/97018889-NckZX.jpg" };
            var p4 = new ProductEntity() { Name = "A IOAKU QUEEN BI BLACK CRYSTAL", Description = "LIMITED EDITION! QUEEN BI Exclusive designed big brooch by Jannike ”Bisse” Nordström with the bee body full of shimmering black crystals and engraved logo.The jewellery is made with brass and multiple layers of real metal. 18K gold or silver plating for optimum durability. Nickel, cadmium and lead free.Eco friendly production.Sent in an special edition shimmering pearl box printed with the specially designed shiny gold logo of QUEEN BI by IOAKU", Color = "Gold", Price = 599, ImgUrl = "https://dst15js82dk7j.cloudfront.net/251590/95989119-2Yvor.jpg" };
            var exist = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Name == p1.Name || x.Name == p2.Name || x.Name == p3.Name);
            if (exist == null)
            {
                _sqlContext.Products.Add(p1);
                _sqlContext.Products.Add(p2);
                _sqlContext.Products.Add(p3);
                _sqlContext.Products.Add(p4);
                await _sqlContext.SaveChangesAsync();
            }
            //REMOVE END
            return await _sqlContext.Products.ToListAsync();
        }

        public async Task<Result> Create(ProductModelForm form)
        {
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
                    ImgUrl = form.ImgUrl
                    
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
    }

    public class Result
    {
        public bool Success { get; set; } = false;
    }

}
