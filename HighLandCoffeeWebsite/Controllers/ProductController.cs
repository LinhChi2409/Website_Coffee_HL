using HighLandCoffeeWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HighLandCoffeeWebsite.Controllers
{
    public class ProductController : Controller
    {
        
        CoffeeDataContext db = new CoffeeDataContext();
        // GET: Product
        public ActionResult ViewProduct()
        {
            var product = db.Products.ToList();

            return View(product);
        }
        public ActionResult ProductDetails(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var relatedProducts = db.Products
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != id)
                .Take(5)
                .ToList();

            ViewBag.RelatedProducts = relatedProducts;

            return View(product);
        }
        [HttpPost]
        public ActionResult AddToCart(int prodID, int quantity, string size, int price)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductId == prodID);

            if (product == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại" });
            }

            var user = Session["User"] as User; // Lấy thông tin người dùng từ Session
            if (user == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập trước khi thêm sản phẩm vào giỏ hàng" });
            }

            var userId = user.UserId;

            // Kiểm tra sản phẩm với cùng size đã tồn tại trong giỏ hàng chưa
            var existingItem = db.ShoppingCarts.FirstOrDefault(s => s.UserId == userId && s.ProductId == prodID && s.Size == size);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                db.SubmitChanges();
                return RedirectToAction("ViewCart", "Cart");
            }
            else
            {
                var cartItem = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = prodID,
                    Quantity = quantity,
                    Size = size,
                    Price = price
                };

                db.ShoppingCarts.InsertOnSubmit(cartItem);
                db.SubmitChanges();

                return RedirectToAction("ViewCart", "Cart");

            }
        }
        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("ViewProduct");
            }
            var results = db.Products
                            .Where(p => p.Name.Contains(query) || p.Category.Name.Contains(query))
                            .ToList();

            return View("ViewProduct", results);
        }

    }
}