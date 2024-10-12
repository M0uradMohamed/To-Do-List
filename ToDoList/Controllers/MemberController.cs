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
            var member = Context.Members.Include(m => m.Tasks).Where(m=>m.Id ==id).FirstOrDefault();
            if(member != null)
            return View(member);

           return RedirectToAction("Index", "Home");
            
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Member member)
        {
            Context.Members.Add(member);
            Context.SaveChanges();

            return RedirectToAction("index", new { id = member.Id });
        }
        public IActionResult Edit (int i)
        {
            return View(); 
        }
    }
}
