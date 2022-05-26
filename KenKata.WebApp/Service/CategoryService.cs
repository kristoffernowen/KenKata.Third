using KenKata.Shared.Models;
using KenKata.Shared.Models.Entities;
using KenKata.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace KenKata.WebApp.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryEntity>> GetAll();
        Task<CategoryModel> Get(int categoryId);
        Task<Result> Create(CategoryModelForm form);
        Task<Result> Update(int Id, CategoryModelForm model);
        Task<Result> Delete(int categoryId);
    }
    public class CategoryService : ICategoryService
    {
        private readonly SqlContext _sqlContext;

        public CategoryService(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }
        public async Task<Result> Create(CategoryModelForm form)
        {
            var categoryNameExist = await _sqlContext.Categories.FirstOrDefaultAsync(x => x.Name == form.Name);
            if (categoryNameExist == null)
            {
                var categoryEntity = new CategoryEntity
                {
                    Name = form.Name,
                };
                _sqlContext.Categories.Add(categoryEntity);
                await _sqlContext.SaveChangesAsync();
                return new Result { Success = true };
            }
            return new Result { Success = false };
        }

        public async Task<Result> Delete(int categoryId)
        {
            var c = await _sqlContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            if (c != null)
            {
                _sqlContext.Remove(c);
                await _sqlContext.SaveChangesAsync();
                return new Result { Success = true };
            }
            return new Result { Success = false };
        }

        public async Task<CategoryModel> Get(int categoryId)
        {
            var category = new CategoryModel();
            var categoryEntity = await _sqlContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            if (categoryEntity != null)
            {
                category.Id = categoryEntity.Id;
                category.Name = categoryEntity.Name;
            }
            return category;
        }

        public async Task<IEnumerable<CategoryEntity>> GetAll()
        {
            
            //REMOVE START. (Only temporarly for making content in database under the development phase.)
            var p1 = new CategoryModelForm() { Name = "Men's" };
            var p2 = new CategoryModelForm() { Name = "Women's"};
            var p3 = new CategoryModelForm() { Name = "Kids" };
            var p4 = new CategoryModelForm() { Name = "Hats"};
          
            await Create(p1);
            await Create(p2);
            await Create(p3);
            await Create(p4);
          
            //REMOVE END
            return await _sqlContext.Categories.Include(x=>x.products).ThenInclude(x=>x.Color).ToListAsync();
        }

        public async Task<Result> Update(int Id, CategoryModelForm model)
        {
            var category = await _sqlContext.Categories.FirstOrDefaultAsync(x => x.Id == Id);

            if (category != null)
            {
                category.Name = model.Name;

                _sqlContext.Update(category);
                await _sqlContext.SaveChangesAsync();
                return new Result { Success = true };
            }

            return new Result { Success = false };
        }


    }
    
}
