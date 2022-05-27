namespace KenKata.Shared.Models.Entities
{
    public class PostEntity
    {
        public int Id { get; set; }
        public string Rubrik { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategoryEntity BlogCategory { get; set; }
        public virtual ICollection<PostTagsEntity> PostTags { get; set; }

    }
}
