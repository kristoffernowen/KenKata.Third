using KenKata.Shared.Models;
using KenKata.Shared.Models.Blog;
using KenKata.WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenKata.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly SqlContext _sqlContext;

        public BlogController(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<IActionResult> Blog()
        {
            var list = new List<BlogPostModel>();

            var posts = await _sqlContext.Posts.Include(x => x.BlogCategory).ToListAsync();
            var postTags = await _sqlContext.PostTags.ToListAsync();
            var tagList = await _sqlContext.Tags.ToListAsync();

            foreach (var post in posts)
            {
                var taggs = new List<TagModel>();
                foreach (var posttag in postTags.Where(x => x.PostId == post.Id))
                {
                    foreach (var tag in tagList.Where(x => x.Id == posttag.TagId))
                    {
                        taggs.Add(new TagModel { TagName = tag.Name });
                    }
                }
                list.Add(new BlogPostModel
                {
                    Id = post.Id,
                    Author = post.Author,
                    Rubrik = post.Rubrik,
                    DateCreated=post.Created,
                    Text = post.Text.Substring(0,10),
                    ImgUrl = post.ImgUrl,
                    tags = taggs,
                });
            }
            return View(list);
        }

        public IActionResult Post(int id)
        {
            return View();
        }
    }
}
