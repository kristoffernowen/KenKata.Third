using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenKata.Shared.Models.Entities
{
    public class BlogCategoryEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public int Name { get; set; }

        public ICollection<PostEntity> Post { get; set; }
    }
}
