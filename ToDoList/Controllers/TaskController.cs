using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Create(TaskToDo task, IFormFile file)
        {
            task.CreatedDate = DateTime.Now;
            if (file != null)
            {
                string random = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file.FileName);


                string fileName = random + extension;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", fileName);

                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files");


                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                task.FileName = fileName;

            }


            Context.Tasks.Add(task);
            Context.SaveChanges();

            TempData["success"] = "new task has been added";
            return RedirectToAction("index", "member", new { id = task.MemberId });
        }
        public IActionResult Edit(int id)
        {
            var task = Context.Tasks.Find(id);

            return View(task);
        }
        [HttpPost]
        public IActionResult Edit(TaskToDo task)
        {
            Context.Tasks.Update(task);
            Context.SaveChanges();

            return RedirectToAction("index", "member", new { id = task.MemberId });
        }
        public IActionResult Delete(int taskId)
        {

            var task = Context.Tasks.Find(taskId);
            int loc = task.MemberId;
            Context.Tasks.Remove(task);
            Context.SaveChanges();
            return RedirectToAction("index", "member", new { id = loc });
        }
        public IActionResult DownloadFile(string fileName, int loc)
        {
            if (fileName != null)
            {

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "application/pdf", fileName);
                }
            }
            TempData["noFile"] = "there is no file to download";
            return RedirectToAction("index", "member", new { id = loc });

        }

    }
}
