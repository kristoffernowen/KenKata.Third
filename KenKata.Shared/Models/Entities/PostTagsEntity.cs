namespace KenKata.Shared.Models.Entities
{
    public class PostTagsEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public PostEntity Post { get; set; }
        public int TagId { get; set; }
        public TagEntity Tag { get; set; }
    }
}
