using KenKata.Shared.Models;
using KenKata.WebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace KenKata.WebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;

        public ShoppingCartController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var shoppingCart = new ShoppingCart();

            var sessionCart = HttpContext.Session.GetString("ShoppingCart");

            if (!string.IsNullOrEmpty(sessionCart))
            {
                shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);
            }

            

            return View(shoppingCart);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody]ShoppingCart cart)
        {
            var shoppingCart = new ShoppingCart();
            foreach (var product in cart.Items)
            {
                shoppingCart.Items.Add(product);
            }



            HttpContext.Session.SetString("ShoppingCart", JsonConvert.SerializeObject(shoppingCart));
            return RedirectToAction("Index");

        }

        

        public async Task<IActionResult> AddToCart(int id)
        {
            var shoppingCart = new ShoppingCart();
            var session = HttpContext.Session.GetString("ShoppingCart");

            if (id != 0)
            {
                if (!string.IsNullOrEmpty(session))
                {
                    shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(session);
                    int index = shoppingCart.Items.FindIndex(x => x.Product.Id == id);

                    if (index != -1)
                    {
                        shoppingCart.Items[index].Quantity += 1;
                    }
                    else
                    {
                        shoppingCart.Items.Add(new CartItem { Product = await _productService.Get(id) });
                    }
                }
                else
                {
                    shoppingCart.Items.Add(new CartItem { Product = await _productService.Get(id)});
                }
            }

            HttpContext.Session.SetString("ShoppingCart", JsonConvert.SerializeObject(shoppingCart));
            return new OkObjectResult(HttpContext.Session.GetString("ShoppingCart"));
        }
    }
}
