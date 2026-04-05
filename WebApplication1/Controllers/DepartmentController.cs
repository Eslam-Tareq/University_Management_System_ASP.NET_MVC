using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Utils;
using WebApplication1.ViewModels.Department;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepo _dept;
       public DepartmentController(IDepartmentRepo dept)
        {
            _dept = dept;
        }
        public IActionResult GetAll()
        {
            var depts = _dept.GetAll();
            return View(depts);
        }
        public IActionResult Details(int id)
        {

            var dept = _dept.GetByIdWithDetails(id);
            
            if (dept is not null) {

                var deptData = new DepartmentDetailsViewModel()
                {
                    Name = dept.Name,
                    Location = dept.Location,
                    Dept_Id = dept.Dept_Id,
                    PhoneNumber = dept.PhoneNumber,
                    Students_Names = new List<string>()
                };
                foreach (var s in dept.Students)
                {
                    deptData.Students_Names.Add(s.Name);
                }
                return View(deptData);
            }
            else throw new CustomException("Department not found", StatusCodes.Status404NotFound);

        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult New ()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [CheckLocation]
        public IActionResult New (Department department)
        {
            if(ModelState.IsValid)
            {
                _dept.Add(department);
                TempData["SuccessMsg"] = "Department Created Successfully";

                return RedirectToAction("GetAll");
            }
            else
            {
                return View(nameof(New), department);
            }
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var depart = _dept.GetById(id);
            if (depart is not null)
            {
                
                return View(depart);
            }
             throw new CustomException("Department not found", StatusCodes.Status404NotFound);

        }
        [Authorize(Roles = "Admin")]

        [HttpPost]

        public IActionResult Edit(Department dept)
        {
            if (ModelState.IsValid)
            {

                _dept.Update(dept);
                TempData["SuccessMsg"] = "Department Updated Successfully";

                return RedirectToAction("GetAll");


            }
            else
            {
               
                return View("Edit", dept);
            }
        }
    }
}
