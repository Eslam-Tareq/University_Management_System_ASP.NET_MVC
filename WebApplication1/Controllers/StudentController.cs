using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Utils;
using WebApplication1.ViewModels.Student;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        IStudentRepo _stu;
        public StudentController(IStudentRepo stu)
        {
            _stu = stu;
        }
        public IActionResult CheckEmailUnique(string Email , int SSN)
        {
            var student = _stu.GetByEmail(Email);
            if (student is not null)
            {
                if (student.SSN == SSN) return Json(true);
                else return Json(false);
                
            }
             return Json(true);
        }
        public IActionResult GetAll()
        {
            var students = _stu.GetAll();
            return View(students);
        }
        [AuthorizeFilter]

        public IActionResult Details(int id)
        {
            var stu = _stu.GetByIdWithDetails(id);
            if (stu == null)
            {
                throw new CustomException("user not found", StatusCodes.Status404NotFound);
            }
            
            return View(stu);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult New(StudentCreateViewModel vm, [FromServices] IDepartmentRepo _dept)
        {
            if (ModelState.IsValid)
            {
                string fileName = null ;

                if (vm.ImageFile != null && vm.ImageFile.ContentType.StartsWith("image"))
                {
                    UploadFIles.UploadImage (ref fileName, vm.ImageFile);
                }

             
                var student = vm.Adapt<Student>();
                student.Image=fileName;
                _stu.Add(student);
                TempData["SuccessMsg"] ="Student Created Successfully";
                return RedirectToAction(nameof(GetAll));
            }else
            {
                var depts = _dept.GetAll();
                ViewBag.Departs = depts;

                return View("New",vm);
            }
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult New([FromServices] IDepartmentRepo _dept)
        {
             var depts = _dept.GetAll();
                ViewBag.Departs = depts;


            return View();
        }
        [Authorize(Roles = "Admin")]


        [HttpGet]
        public IActionResult Edit(int id, [FromServices] IDepartmentRepo _dept)
        {
            var std = _stu.GetById(id);
            if(std is not null)
            {
               
                var vm = std.Adapt<StudentUpdateViewModel>();
                 var depts = _dept.GetAll();
                ViewBag.Departs = depts;

                return View(vm);
            }
            throw new CustomException("user not found", StatusCodes.Status404NotFound);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit (StudentUpdateViewModel vm, [FromServices] IDepartmentRepo _dept)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (vm.ImageFile != null && vm.ImageFile.ContentType.StartsWith("image"))
                {
                    string uploadsFolder = UploadFIles.UploadImage(ref fileName, vm.ImageFile);

                    if (!string.IsNullOrEmpty(vm.Image))
                    {
                        string oldImagePath = Path.Combine(uploadsFolder, vm.Image);

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }
                var updatedStudent = vm.Adapt<Student>();
                updatedStudent.Image = fileName ?? updatedStudent.Image;
                _stu.Update(updatedStudent);
                TempData["SuccessMsg"] = "Student Updated Successfully";

                return RedirectToAction("GetAll");
                

            }
            else
            {
                var depts = _dept.GetAll();
                ViewBag.Departs = depts;

                return View("Edit", vm);
            }
        }
        //public IActionResult Test()
        //{
        //    string output = "";
        //    foreach (var item in db.Courses.FirstOrDefault().Topics)
        //    {
        //        output += item;
        //    }
        //    return Content(output);
        //}

    }
}
