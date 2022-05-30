using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models.Blog
{
    public class BlogsModel
    {
        //Till blog första sidan
        public int Id { get; set; }
        public string Rubrik { get; set; }
        public string ShortText { get; set; }
        public string Arthur { get; set; }
        public DateTime DateCreated { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
        public string ImgUrl { get; set; }
        
    }
}
