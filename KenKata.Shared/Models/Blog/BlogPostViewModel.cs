using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models.Blog
{
    public class BlogPostViewModel
    {
        //till Blog/post/id
        public BlogPostModel BlogPost { get; set; }// Till vänster sida som visar hela blogg inlägget
        public IEnumerable<TagModel> Tags { get; set; } //till höger sida- taggs
        public IEnumerable<CategoryModel> Categories { get; set; } //till höger sida categorys
        public IEnumerable<BlogsModel> Blogs { get; set; } //Till höger sida -lista av recent post
    }
}
