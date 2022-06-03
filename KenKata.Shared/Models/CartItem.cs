

namespace KenKata.Shared.Models
{
    public class CartItem
    {
        public ProductModel Product { get; set; } = null!;
        public int Quantity { get; set; } = 1;
    }
}
