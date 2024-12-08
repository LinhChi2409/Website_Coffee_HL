using HighLandCoffeeWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HighLandCoffeeWebsite.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        CoffeeDataContext db = new CoffeeDataContext();
        public ActionResult Index()
        {
            // Get the currently logged-in user
            User currentUser = Session["user"] as User;
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not logged in
            }

            // Fetch only the most recent order for the logged-in user
            var latestOrder = db.Orders
                                .Where(o => o.UserId == currentUser.UserId)  // Filter by current user
                                .OrderByDescending(o => o.OrderDate)  // Order by most recent first
                                .FirstOrDefault();  // Get only the most recent order

            if (latestOrder == null)
            {
                return View(); // Return an empty view if no orders found
            }

            // Fetch the order items for the latest order
            var orderItems = db.OrderItems.Where(oi => oi.OrderId == latestOrder.OrderId).ToList();

            // Create the InvoiceViewModel
            var viewModel = new InvoiceViewModel
            {
                Order = latestOrder,
                OrderItems = orderItems
            };

            return View(viewModel); // Pass the InvoiceViewModel to the view
        }
        [HttpPost]
        public ActionResult CreateOrder(int productId, string size, double price, string name, int quantity)
        {
            var user = db.Users.SingleOrDefault(u => u.UserId == 3);
            if (user == null)
            {
                return RedirectToAction("UserNotFound");
            }

            // Tạo đơn hàng mới và lưu vào cơ sở dữ liệu
            var order = new Order
            {
                UserId = user.UserId,
                OrderDate = DateTime.Now,
                TotalAmount = 0,
                Address = user.Address,
                Phone = user.PhoneNumber,
                FullName = user.FullName
            };

            db.Orders.InsertOnSubmit(order);
            db.SubmitChanges(); // Lưu đơn hàng

            var orderId = order.OrderId; // Lấy OrderId của đơn hàng vừa tạo

            // Tạo OrderItem từ dữ liệu sản phẩm mua
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                Size = size,
                Price = quantity * price
            };

            // Thêm OrderItem vào cơ sở dữ liệu
            db.OrderItems.InsertOnSubmit(orderItem);
            db.SubmitChanges(); // Lưu OrderItem

            // Cập nhật tổng số tiền của đơn hàng
            order.TotalAmount = orderItem.Price;
            db.SubmitChanges(); // Lưu lại tổng số tiền đơn hàng

            // Lấy tất cả thông tin đơn hàng và các OrderItems từ cơ sở dữ liệu
            var orderDetails = (from o in db.Orders
                                where o.OrderId == orderId
                                select new
                                {
                                    o.OrderId,
                                    o.UserId,
                                    o.OrderDate,
                                    o.TotalAmount,
                                    o.Address,
                                    o.Phone,
                                    o.User.FullName,
                                    OrderItems = (from oi in db.OrderItems
                                                  where oi.OrderId == o.OrderId
                                                  select new
                                                  {
                                                      oi.ProductId,
                                                      oi.Quantity,
                                                      oi.Size,
                                                      oi.Price
                                                  }).ToList()
                                }).ToList();


            if (orderDetails.Count == 0)
            {
                return RedirectToAction("OrderNotFound");
            }
            ViewBag.ProductName = name;


            return View(orderDetails);
        }
    }
}