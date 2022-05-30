﻿using KenKata.Shared.Models;
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

        public async Task<IActionResult> Post(int id)
        {
            var model = new BlogPostViewModel();

            var post = await _sqlContext.Posts.Where(x => x.Id == id).Include(x=>x.BlogCategory).FirstOrDefaultAsync(); //.Include(x=>x.PostTags.Where(x=>x.PostId==id))
            var postaggs= await _sqlContext.PostTags.Where(x => x.PostId == id).FirstOrDefaultAsync();
            var taggs = await _sqlContext.Tags.ToListAsync();

            //Create list to store the posts taggs
            var taggList = new List<TagModel>();
            foreach ( var tag in taggs.Where(x => x.Id == postaggs.Id)) 
            {
                taggList.Add(new TagModel {TagName=tag.Name});
            };

            // Skapa en Blogpost
            if (post != null) 
            {
                var blogpost = new BlogPostModel
                {
                    Id = post.Id,
                    Rubrik = post.Rubrik,
                    ImgUrl = post.ImgUrl,
                    Text = post.Text,
                    DateCreated = post.Created,
                    DateUpdated = post.Updated,
                    Author = post.Author,
                    category = post.BlogCategory.Name,
                    tags = taggList
                };
                model.BlogPost = blogpost;

                //List all Blogg posts/ Get alla posts  id and images to display in View.
                var allPosts = await _sqlContext.Posts.Include(x => x.BlogCategory).ToListAsync();
                var bloggList = new List<BlogsModel>();
                var categoryList = new List<CategoryModel>();
                foreach (var i in allPosts)
                {
                    bloggList.Add(new BlogsModel { Id = i.Id, ImgUrl = i.ImgUrl });
                    categoryList.Add(new CategoryModel { Id = i.BlogCategory.Id, Name = i.BlogCategory.Name });
                }

                model.Blogs = bloggList;
                model.Categories = categoryList;

                //List all taggs
                var allTaggList = new List<TagModel>();
                foreach (var tag in taggs)
                {
                    allTaggList.Add(new TagModel { Id=tag.Id, TagName = tag.Name });
                };
                model.Tags=allTaggList;

                return View(model);
            } 
            return View(model);

            
            
        }
    }
}
