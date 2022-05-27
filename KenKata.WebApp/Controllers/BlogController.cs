using KenKata.Shared.Models;
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
            //var list = new List<BlogPostModel>();
            var posts = await _sqlContext.Posts.Include(x=>x.BlogCategory).ToListAsync();
            //foreach(var post in posts)
            //{
            //    list.Add(new BlogPostModel { 
            //        Id=post.Id,
            //        Author=post.Author,
            //        Rubrik=post.Rubrik,
                                    
            //    });
            //}
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            return View();
        }
    }
}
