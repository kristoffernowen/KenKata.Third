using System.ComponentModel.DataAnnotations;

namespace KenKata.Shared.Models.Entities
{
    public class PostTagsEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PostId { get; set; }
        public PostEntity Post { get; set; }
        [Required]
        public int TagId { get; set; }
        public TagEntity Tag { get; set; }
    }
}
