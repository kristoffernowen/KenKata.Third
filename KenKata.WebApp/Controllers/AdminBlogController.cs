using KenKata.Shared.Models;
using KenKata.Shared.Models.Blog;
using KenKata.Shared.Models.Entities;
using KenKata.WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenKata.WebApp.Controllers
{
    public class AdminBlogController : Controller
    {
        private readonly SqlContext _sqlContext;

        public AdminBlogController(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        [Route("admin/Blog")]
        public async Task<IActionResult> Index()
        {
            var post = await _sqlContext.Posts.Include(x=>x.BlogCategory).ToListAsync();
            if (post != null)
            {
                return View(post);
            }else
            return View();
        }
        
        public IActionResult Create()
        {
            var model = new CreateBlogPostModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogPostModel model)
        {
            if (ModelState.IsValid)
            {
                //kontrollera om rubrik finns redan
                var post = await _sqlContext.Posts.FirstOrDefaultAsync(x => x.Rubrik == model.Rubrik);

                if(post==null)
                {
                    var postEntity = new PostEntity();
                    postEntity.Rubrik = model.Rubrik;
                    postEntity.Author = model.Author;
                    postEntity.Text = model.Text;
                    postEntity.Created = DateTime.Now;
                    postEntity.Updated = DateTime.Now;
                    postEntity.ImgUrl=model.ImgUrl;

                    //kontrollera om categori finns
                    var category = await _sqlContext.BlogCategories.FirstOrDefaultAsync(x => x.Name == model.category);
                    if (category != null)
                    {
                        postEntity.BlogCategoryId = category.Id;
                    }
                    else
                    {
                        //skapa categori
                        var categoryEntity = new BlogCategoryEntity { Name = model.category };
                        _sqlContext.BlogCategories.Add(categoryEntity);
                        await _sqlContext.SaveChangesAsync();
                        postEntity.BlogCategoryId = categoryEntity.Id;
                    }

                    try
                    {
                        _sqlContext.Posts.Add(postEntity);
                        await _sqlContext.SaveChangesAsync();
                    }
                    catch
                    {
                        var posty = postEntity;
                    }
                    



                    //kontrollera taggs, annars lägg in nya
                    var tag1 = await _sqlContext.Tags.FirstOrDefaultAsync(x => x.Name == model.Tag1);
                    var tag2 = await _sqlContext.Tags.FirstOrDefaultAsync(x => x.Name == model.Tag2);

                    var postTag1 = new PostTagsEntity();
                    var postTag2 = new PostTagsEntity();

                    if (tag1 == null)
                    {
                        var tagsentity = new TagEntity { Name = model.Tag1 };
                        _sqlContext.Tags.Add(tagsentity);
                        await _sqlContext.SaveChangesAsync();
                        postTag1.TagId=tagsentity.Id;
                        postTag1.PostId = postEntity.Id;

                    }
                    else
                    {
                        postTag1.TagId = tag1.Id;
                        postTag1.PostId = postEntity.Id;
                    }

                    if (tag2 == null)
                    {
                        var tagsentity = new TagEntity { Name = model.Tag2 };
                        _sqlContext.Tags.Add(tagsentity);
                        await _sqlContext.SaveChangesAsync();
                        postTag2.TagId=tagsentity.Id;
                        postTag2.PostId = postEntity.Id;

                    }
                    else
                    {
                        postTag2.TagId = tag2.Id;
                        postTag2.PostId = postEntity.Id;
                    }
                    try
                    {
                        _sqlContext.PostTags.Add(postTag1);
                        _sqlContext.PostTags.Add(postTag2);
                        await _sqlContext.SaveChangesAsync();
                    }
                    catch
                    {
                        var posttags = postTag1;
                    }
                    
                    return RedirectToAction("Index");
                }

                    return View();
            }
            else
            return View();

         }
        [HttpGet]
        [Route("admin/Blog/Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var postEntity = await _sqlContext.Posts.Include(x=>x.PostTags).Include(x=>x.BlogCategory).FirstOrDefaultAsync(x => x.Id == id);

            if(postEntity != null)
            {
                var List = new List<String>();
                foreach (var postTag in postEntity.PostTags)
                {
                    var tag = await _sqlContext.Tags.FirstOrDefaultAsync(x => x.Id == postTag.TagId);
                    if (tag != null)
                    {
                        List.Add(tag.Name);
                    }
                    
                }
                var model = new UpdateBlogPostModel()
                {
                    Rubrik = postEntity.Rubrik,
                    Author= postEntity.Author,
                    ImgUrl=postEntity.ImgUrl,
                    Text = postEntity.Text,
                    Tag1= List.First(),
                    Tag2 = List.Last(),
                    Category=postEntity.BlogCategory.Name


                };
                return View(model);
            }

            return BadRequest("post Not found");
        }
        [Route("admin/Blog/Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateBlogPostModel model)
        {
            if (ModelState.IsValid)
            {
                var categoryExist = await _sqlContext.BlogCategories.FirstOrDefaultAsync(x => x.Name == model.Category);
                var postEntity = await _sqlContext.Posts.Include(x=>x.PostTags).FirstOrDefaultAsync(x => x.Id == model.Id);

                
                if (postEntity != null)
                {
                    if (categoryExist == null)
                    {
                        var categoryEntity = new BlogCategoryEntity { Name = model.Category };
                        _sqlContext.BlogCategories.Add(categoryEntity);
                        await _sqlContext.SaveChangesAsync();
                        postEntity.BlogCategoryId = categoryEntity.Id;
                    }else
                        postEntity.BlogCategoryId = categoryExist.Id;


                    var tag1 = await _sqlContext.Tags.FirstOrDefaultAsync(x => x.Name == model.Tag1);
                    var tag2 = await _sqlContext.Tags.FirstOrDefaultAsync(x => x.Name == model.Tag2);

                    var postTag1 = postEntity.PostTags.First();
                    var postTag2 = postEntity.PostTags.Last();


                    if (tag1 == null)
                    {
                        var tagsentity = new TagEntity { Name = model.Tag1 };
                        _sqlContext.Tags.Add(tagsentity);
                        await _sqlContext.SaveChangesAsync();
                        postTag1.TagId = tagsentity.Id;
                        postTag1.PostId = postEntity.Id;

                    }else
                    {
                        postTag1.TagId = tag1.Id;
                        postTag1.PostId = postEntity.Id;
                    }

                    if (tag2 == null)
                    {
                        var tagsentity = new TagEntity { Name = model.Tag2 };
                        _sqlContext.Tags.Add(tagsentity);
                        await _sqlContext.SaveChangesAsync();
                        postTag2.TagId = tagsentity.Id;
                        postTag2.PostId = postEntity.Id;
                    }else
                    {
                        postTag2.TagId = tag2.Id;
                        postTag2.PostId = postEntity.Id;
                    }

                    postEntity.Author = model.Author;
                    postEntity.Rubrik = model.Rubrik;
                    postEntity.Text = model.Text;
                    postEntity.Updated= DateTime.Now;
                    postEntity.ImgUrl = model.ImgUrl;

                    _sqlContext.Posts.Update(postEntity);
                    await _sqlContext.SaveChangesAsync();
                    return RedirectToAction("Index");

                    
                }
                
            }

            return View(model);
        }


        public async  Task<IActionResult> Delete(int id)
        {
            var deleteBlogPost = await _sqlContext.Posts.Include(x=>x.PostTags).FirstOrDefaultAsync(x => x.Id == id);

            if (deleteBlogPost != null)
            {
                _sqlContext.Remove(deleteBlogPost);
                await _sqlContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
