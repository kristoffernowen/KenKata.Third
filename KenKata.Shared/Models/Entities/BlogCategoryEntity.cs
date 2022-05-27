namespace KenKata.Shared.Models.Entities
{
    public class BlogCategoryEntity
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public ICollection<PostEntity> Post { get; set; }
    }
}
