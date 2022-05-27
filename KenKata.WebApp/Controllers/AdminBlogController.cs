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
            var post = await _sqlContext.Posts.ToListAsync();
            if (post != null)
            {
                return View(post);
            }else
            return View();
        }
        
        public IActionResult Create()
        {
            var model = new BlogPostModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BlogPostModel model)
        {
            if (ModelState.IsValid)
            {
                //kontrollera om rubrik finns redan
                var post = await _sqlContext.Posts.FirstOrDefaultAsync(x => x.Rubrik == model.Rubrik);

                if (post != null)
                {
                    return View();
                }
                else
                {
                    var postEntity = new PostEntity();
                    postEntity.Rubrik = model.Rubrik;
                    postEntity.Author = model.Author;

                    //kontrollera om categori finns
                    //var category = await _sqlContext.BlogCategories.FirstOrDefaultAsync(x => x.Name == model.category);
                    //if (category != null)
                    //{
                    //    postEntity.BlogCategoryId = category.Id;
                    //}
                    //else
                    //{
                    //    var categoryEntity = new CategoryEntity { Name = model.category };
                    //    _sqlContext.Categories.Add(categoryEntity);
                    //    await _sqlContext.SaveChangesAsync();
                    //    postEntity.BlogCategoryId = categoryEntity.Id;
                    //}

                    //kontrollera taggs
                    var tag1 = await _sqlContext.Tags.FirstOrDefaultAsync(x => x.Name == model.Tag1);
                    var tag2 = await _sqlContext.Tags.FirstOrDefaultAsync(x => x.Name == model.Tag2);
                    if (tag1 == null) 
                    {
                        var tagsentity = new TagEntity { Name = model.Tag1};
                        _sqlContext.Tags.Add(tagsentity);
                        await _sqlContext.SaveChangesAsync();
                    }
                    if (tag2 == null)
                    {
                        var tagsentity = new TagEntity { Name = model.Tag2 };
                        _sqlContext.Tags.Add(tagsentity);
                        
                    }
                    //kontrollera om postTags har en rad med tagId & postId
                    var postTags = await _sqlContext.PostTags.FirstOrDefaultAsync(x => x.TagId == tag1.Id && x.PostId == post.Id);
                    
                    if(postTags == null)
                    {
                        _sqlContext.PostTags.Add(new PostTagsEntity { PostId = post.Id, TagId = tag1.Id });
                        _sqlContext.PostTags.Add(new PostTagsEntity { PostId = post.Id, TagId = tag2.Id });
                    }
                    
                     await _sqlContext.SaveChangesAsync();

                    //var tagEntity = await _sqlContext.Tags.FirstOrDefaultAsync(x=>x.Name=);

                    //foreach (var tag in model.tags)
                    //{
                    //    var tags = await _sqlContext.Tags.FirstOrDefaultAsync(x => x.Name == tag.TagName);
                    //    if (tags ==null)
                    //    {
                    //        var tagsentity = new TagEntity { Name = tag.TagName };
                    //        _sqlContext.Tags.Add(tagsentity);
                    //        await _sqlContext.SaveChangesAsync();
                    //    }
                    //    //kollar om det finns posttags med samma tag id och postid
                    //    if(! await _sqlContext.PostTags.AnyAsync(x=>x.TagId.ToString() == tag.Id && x.PostId==post.Id))
                    //    {
                    //        //om det inte existerar skapa ny...Blir fel här!!!
                    //        _sqlContext.PostTags.Add(new PostTagsEntity { PostId = post.Id, TagId = tags.Id });
                    //    }
                    //}

                    //var postTags = await _sqlContext.PostTags.FirstOrDefaultAsync(x => x.PostId == post.Id && x.TagId ==);
                    return Redirect("Index");
                }
                             
                    
            }else
            return View();

         }


    }
}
