using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using QuanLySanBong.Classes;
using QuanLySanBong.Data;
using QuanLySanBong.Models;

namespace QuanLySanBong.Controllers
{
    public class AccessController : Controller
    {
        private FootballContext db;
        public AccessController(FootballContext context)
        {
            db = context;
        }

        [HttpGet]      
        public ActionResult Login()
        {
                return View();           
        }

        [HttpPost]
        public ActionResult Login(String username, String password)
        {
            User user = db.User.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                ConstVar.User = user;
                return RedirectToAction("ListSanBong", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register(User? user)
        {
            return View(user);
        }


        [HttpPost]
        public IActionResult CreateUser([FromForm] User user)
        {
            //if (db.User.Where(p => p.Username == user.Username).Count() > 0)
            //    ModelState.AddModelError("Username", "Đã tồn tại tên đăng nhập");
            //if (db.User.Where(p => p.PhoneNumber == user.PhoneNumber).Count() > 0)
            //    ModelState.AddModelError("PhoneNumber", "Số điện thoại đã được đăng ký");
            //if (!ModelState.IsValid)
            //{
            //    return View("Register", user);
            //}
            user.LoaiUser = false;
            db.User.Add(user);
            Cart cart = new Cart();
            cart.User = user;
            cart.Total = 0;
            db.Cart.Add(cart);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

    }
}
