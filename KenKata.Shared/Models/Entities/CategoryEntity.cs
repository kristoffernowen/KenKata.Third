using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KenKata.Shared.Models.Entities
{
    public class CategoryEntity
    {
        public CategoryEntity()
        {
        }

        public CategoryEntity(string name)
        {
            Name = name;
        }

        public CategoryEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public CategoryEntity(int id, string name, ICollection<ProductEntity> products)
        {
            Id = id;
            Name = name;
            this.products = products;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        public ICollection<ProductEntity> products { get; set; }
    }
}
