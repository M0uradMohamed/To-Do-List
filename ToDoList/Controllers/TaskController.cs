using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        public ApplicationDbContext Context = new ApplicationDbContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int MemberId)
        {
            return View(MemberId);
        }
            [HttpPost]
        public IActionResult Create(TaskToDo task, IFormFile file )
        {
            task.CreatedDate = DateTime.Now;
            if (file != null)
            {
                string random = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file.FileName);


                string fileName = random + extension;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
               task.fileLocation  = filePath;

            }

            Context.Tasks.Add(task);
            Context.SaveChanges();
            return RedirectToAction("index", "member", new { id = task.MemberId });
        }

    }
}
