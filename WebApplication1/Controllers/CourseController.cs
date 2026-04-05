using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Utils;
using WebApplication1.ViewModels.Course;

namespace WebApplication1.Controllers
{
    public class CourseController : Controller
    {

        ICourseRepo _crs;
        public CourseController(ICourseRepo crs)
        {
            _crs = crs;
        }
        public IActionResult GetAll()
        {
            var courses = _crs.GetAll();
            return View(courses);
        }
        public IActionResult Details(int id)
        {

            var crs = _crs.GetById(id);

            if (crs is not null)
            {

                
                return View(crs);
            }
            else throw new CustomException("Course not found", StatusCodes.Status404NotFound);

        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult New(CourseFormViewModel vm)
        {
            
            if (ModelState.IsValid)
            {

                var course = vm.Adapt<Course>();
                _crs.Add(course);
                TempData["SuccessMsg"] = "Course Created Successfully";

                return RedirectToAction("GetAll");
            }
            else
            {
                return View(nameof(New), vm);
            }
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _crs.GetById(id);
            
            if (course is not null)
            {
                var mappedCourse = course.Adapt<CourseUpdateViewModel>();
               mappedCourse.Topics= String.Join(",", course.Topics);
                return View(mappedCourse);
            }
            throw new CustomException("course not found", StatusCodes.Status404NotFound);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(CourseUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var course = vm.Adapt<Course>();

                _crs.Update(course);
                TempData["SuccessMsg"] = "Course Updated Successfully";

                return RedirectToAction("GetAll");


            }
            else
            {

                return View("Edit", vm);
            }
        }
    }
}
