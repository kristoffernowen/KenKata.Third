using KenKata.Shared.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models
{
    public class BlogPostModel
    {
        public BlogPostModel()
        {
        }

        public BlogPostModel(int id, string rubrik, string imgUrl, string text, DateTime dateCreated, DateTime dateUpdated, string author, string category, IEnumerable<TagModel> tags)
        {
            Id = id;
            Rubrik = rubrik;
            ImgUrl = imgUrl;
            Text = text;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            Author = author;
            this.category = category;
            this.tags = tags;
        }

        public int Id { get; set; }
        public string Rubrik { get; set; }
        public string ImgUrl { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Author { get; set; }
        public string category { get; set; }
        public IEnumerable<TagModel> tags { get; set; }
    }
}
