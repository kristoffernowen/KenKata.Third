using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenKata.Shared.Models.Entities
{
    public class PostEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Rubrik { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Text { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string ImgUrl { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Author { get; set; }
        [Required]
        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
        [Required]

        public int BlogCategoryId { get; set; }
        public BlogCategoryEntity BlogCategory { get; set; }
        public virtual ICollection<PostTagsEntity> PostTags { get; set; }

    }
}
