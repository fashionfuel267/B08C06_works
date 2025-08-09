using B08C06_w01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace B08C06_w01.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeCOntext _context;
        public EmployeeController(EmployeeCOntext context)
        {
            this._context = context;

        }
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }
        public IActionResult Create()
        {


            return View();
        }
        [HttpPost]
        public IActionResult Create(EMployee eMployee)
        {
            if (ModelState.IsValid)
            {
                if (eMployee.Picture != null)
                {
                    string fileName = Path.GetFileName(eMployee.Picture.FileName);
                    string extension = Path.GetExtension(fileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", eMployee.Name+extension);
                    if(extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        ModelState.AddModelError("Picture", "Only .jpg, .png, and .jpeg files are allowed.");
                        return View(eMployee);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        eMployee.Picture.CopyTo(stream);
                    }
                    eMployee.PicturePath = "/Pictures/" + eMployee.Name + extension;
                }
                _context.Employees.Add(eMployee);
                if (_context.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
            }
         else
            {
                var message = string.Join(" | ", ModelState.Values
        .SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage));
                ModelState.AddModelError(" ",  message);
            }

            return View();
        }
    }
}
