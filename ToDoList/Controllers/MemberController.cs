using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class MemberController : Controller
    {
        public ApplicationDbContext Context = new ApplicationDbContext();
        public IActionResult Index(int id)
        {
            var member = Context.Members.Include(m => m.Tasks).Where(m => m.Id == id).FirstOrDefault();
            if (member != null)
                return View(member);

            return RedirectToAction("Index", "Home");

        }
        public IActionResult Create()
        {
            if (Request.Cookies["memberName"] == null)
            {
                return View();
            }
            int memberId = int.Parse(Request.Cookies["memberId"]);
            return RedirectToAction("index", new { id = memberId });
        }
        [HttpPost]
        public IActionResult Create(Member member)
        {
            Context.Members.Add(member);
            Context.SaveChanges();

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Secure = true;
            cookieOptions.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("memberName", member.Name, cookieOptions);
            Response.Cookies.Append("memberId", member.Id.ToString(), cookieOptions);

            return RedirectToAction("index", new { id = member.Id });
        }
        public IActionResult Edit(int i)
        {
            return View();
        }
    }
}
