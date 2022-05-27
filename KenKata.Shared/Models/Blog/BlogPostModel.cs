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
