using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models.Blog
{
    public class CreateBlogPostModel
    {
        public string Rubrik { get; set; }
        public string ImgUrl { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Author { get; set; }
        public string category { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
    }
}
