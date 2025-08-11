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
                    string isExist = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures");
                    if(!Directory.Exists(isExist))
                    {
                        Directory.CreateDirectory(isExist);
                    }
                    string filePath = Path.Combine(isExist, eMployee.Id+extension);
                    if(extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        ModelState.AddModelError("Picture", "Only .jpg, .png, and .jpeg files are allowed.");
                        return View(eMployee);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        eMployee.Picture.CopyTo(stream);
                    }
                    eMployee.PicturePath = "/Pictures/" + eMployee.Id + extension;
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
        public IActionResult Edit(int? id)
        {
            if(id==null)
            { return BadRequest(); }
            var obj = _context.Employees.Find(id);

            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(EMployee eMployee)
        {
            if (ModelState.IsValid)
            {
                if (eMployee.Picture != null)
                {
                    string fileName = Path.GetFileName(eMployee.Picture.FileName);
                    string extension = Path.GetExtension(fileName).ToLower();
                    string oldpath = eMployee.PicturePath;

                    string oldpathcombine =  Directory.GetCurrentDirectory()+ "/wwwroot"+ oldpath;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", eMployee.Id + extension);
                    if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        ModelState.AddModelError("Picture", "Only .jpg, .png, and .jpeg files are allowed.");
                        return View(eMployee);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        eMployee.Picture.CopyTo(stream);
                    }
                    if (System.IO.File.Exists(oldpathcombine))
                    {
                        System.IO.File.Delete(oldpathcombine);
                    }
                    eMployee.PicturePath = "/Pictures/" + eMployee.Id + extension;
                }
                else
                {
                    eMployee.PicturePath = eMployee.PicturePath;
                }
                _context.Employees.Update(eMployee);
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
                ModelState.AddModelError(" ", message);
            }

            return View();
        }
    }
}
