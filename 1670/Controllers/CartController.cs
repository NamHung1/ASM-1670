using _1670.Data;
using _1670.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace _1670.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<IActionResult> Index()
        {
            return _db.Product != null ?
            View(await _db.Product.ToListAsync()) :
            Problem("Entity set 'ApplicationDbContext.Book'  is null.");
            //var ShopContext = _db.Product.Include(o => o.Category);
            //return View(await ShopContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Product == null)
            {
                return NotFound();
            }
            var product = await _db.Product
              .Include(b => b.Category)
              .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public Product GetProductDetail(int id)
        {
            var product = _db.Product.Find(id);
            return product;
        }

        List<Cart> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("cart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(jsoncart);
            }
            return new List<Cart>();
        }

        public IActionResult ListCart()
        {
            return View(GetCartItems());
        }

        public IActionResult AddToCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart == null) //If cart is empty
            {
                var product = GetProductDetail(id);
                List<Cart> ListCart = new()
                {
                  new Cart
                  {
                    Product = product,
                    Quantity = 1
                  }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(ListCart));
            }
            else //if cart has at least 1 product
            {
                List<Cart> CartData = JsonConvert.DeserializeObject<List<Cart>>(cart);
                bool check = true;
                for (int i = 0; i < CartData.Count; i++)
                {
                    if (CartData[i].Product.Id == id)
                    {
                        CartData[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    CartData.Add(new Cart
                    {
                        Product = GetProductDetail(id),
                        Quantity = 1
                    });
                }
                //_signalRHub.Clients.All.SendAsync("itemAdded", new { message = "1 item added to cart!" });
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(CartData));
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult UpdateCart(int id, int quantity)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (quantity > 0)
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].Product.Id == id)
                        {
                            dataCart[i].Quantity = quantity;
                        }
                    }
                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                }
                return Ok(quantity);
            }
            return BadRequest();
        }

        public IActionResult DeleteCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));

                return RedirectToAction(nameof(ListCart));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult MakeOrder()
        {
            string UserId = GetUser();

            Order order = new Order();
            if (ModelState.IsValid)
            {
                order.UserId = UserId;
                order.OrderTime = DateTime.Now;
                _db.Add(order);
                _db.SaveChanges();
                TempData["OrderID"] = order.Id;
                HttpContext.Session.Remove("cart");
            }
            return RedirectToAction("Index", "Cart");
        }

        public string GetUser()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        //public IActionResult Index()
        //{
        //    var _product = GetAllProducts();
        //    ViewBag.product = _product;
        //    return View();
        //}
        //public List<Product> GetAllProducts()
        //{
        //    return _db.Product.ToList();
        //}
        //public Product GetDetailProduct(int id)
        //{
        //    var product = _db.Product.Find(id);
        //    return product;
        //}
        //public IActionResult AddCart(int id)
        //{
        //    var cart = HttpContext.Session.GetString("cart");
        //    if (cart == null)
        //    {
        //        var product = GetDetailProduct(id);
        //        List<Cart> listCart = new List<Cart>()
        //            {
        //                new Cart
        //                {
        //                    Product = product,
        //                    Quantity = 1
        //                }
        //            };
        //        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
        //    }
        //    else
        //    {
        //        List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
        //        bool check = true;
        //        for (int i = 0; i < dataCart.Count; i++)
        //        {
        //            if (dataCart[i].Product.Id == id)
        //            {
        //                dataCart[i].Quantity++;
        //                check = false;
        //            }
        //        }
        //        if (check)
        //        {
        //            dataCart.Add(new Cart
        //            {
        //                Product = GetDetailProduct(id),
        //                Quantity = 1
        //            });
        //        }
        //        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
        //    }
        //    return RedirectToAction("ListCart");

        //}
        //public IActionResult UpdateCart(int id, int quantity)
        //{
        //    var cart = HttpContext.Session.GetString("cart");
        //    if (cart != null)
        //    {
        //        List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
        //        if (quantity > 0)
        //        {
        //            for (int i = 0; i < dataCart.Count; i++)
        //            {
        //                if (dataCart[i].Product.Id == id)
        //                {
        //                    dataCart[i].Quantity = quantity;
        //                }
        //            }
        //            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
        //        }
        //        return Ok(quantity);
        //    }
        //    return BadRequest();

        //}
        //public IActionResult DeleteCart(int id)
        //{
        //    var cart = HttpContext.Session.GetString("cart");
        //    if (cart != null)
        //    {
        //        List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

        //        for (int i = 0; i < dataCart.Count; i++)
        //        {
        //            if (dataCart[i].Product.Id == id)
        //            {
        //                dataCart.RemoveAt(i);
        //            }
        //        }
        //        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));

        //        return RedirectToAction(nameof(ListCart));
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
        //public IActionResult ListCart()
        //{
        //    var cart = HttpContext.Session.GetString("cart");
        //    if (cart != null)
        //    {
        //        List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

        //        if (dataCart.Count > 0)
        //        {
        //            ViewBag.carts = dataCart;
        //            return View();
        //        }
        //        return RedirectToAction(nameof(ListCart));
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
