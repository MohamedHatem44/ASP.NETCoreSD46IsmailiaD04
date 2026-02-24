using ASP.NETCoreD04.Data.Context;
using ASP.NETCoreD04.Models;
using ASP.NETCoreD04.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreD04.Controllers
{
    public class EmployeeController : Controller
    {
        /*------------------------------------------------------------------*/
        // Context => DB => Data Access
        private readonly AppDbContext db = new AppDbContext();
        /*------------------------------------------------------------------*/
        // Index => List All => Main Action => Landing Page
        [HttpGet]
        public IActionResult Index()
        {
            // Get All Employees
            // Map From Domain Model To VM
            var employeesReadVM = db.Employees
                .Include(e => e.Department)
                .Select(e => new EmployeeReadVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary,
                    Department = e.Department!.Name
                }).ToList();

            return View(employeesReadVM);
        }
        /*------------------------------------------------------------------*/
        // View Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = db.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return RedirectToAction("Index");
            }

            // Map FromDomain Model To View Model
            var employeeReadVM = new EmployeeReadVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Department = employee.Department!.Name
            };

            return View(employeeReadVM);
        }
        /*------------------------------------------------------------------*/
        // V01
        // Create Employee
        // Get => Show Form
        [HttpGet]
        public IActionResult CreateV01()
        {
            ViewBag.Departments = new SelectList(db.Departments, "Id", "Name");
            return View();
        }
        /*------------------------------------------------------------------*/
        // V01
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateV01(Employee employee)
        {
            //// Custom Validation => Server Side Validation
            //if(employee.Age < 18)
            //{
            //    // 
            //}
            //if(employee.Salary < 0)
            //{
            //    // 
            //}
            //if(!string.IsNullOrEmpty(employee.Name) && employee.Name.Length > 100)
            //{
            //    // 
            //}

            ModelState.Remove("Department"); // Remove => Don't Validate This Property
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------*/
        // V02
        // Create Employee
        // Get => Show Form
        // Get Departments => For DropdownList
        [HttpGet]
        public IActionResult CreateV02()
        {
            var employeeCreateVM = new EmployeeCreateVM
            {
                Departments = GetDepartmentsForDropDown()
            };
            return View(employeeCreateVM);
        }
        /*------------------------------------------------------------------*/
        // V02
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateV02(EmployeeCreateVM employeeCreateVM)
        {
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError("", "Department is required");
                employeeCreateVM.Departments = GetDepartmentsForDropDown();
                return View(employeeCreateVM);
            }

            // Custom Validation => Server Side Validation
            //var isFound = db.Employees.Any(e => e.Email == employeeCreateVM.Email);
            //if (isFound)
            //{
            //    ModelState.AddModelError("Email", "Email already exists");
            //    employeeCreateVM.Departments = GetDepartmentsForDropDown();
            //    return View(employeeCreateVM);
            //}

            // employeeCreateVM => Don't Have Id
            // Map From VM To Domain Model
            var employee = new Employee
            {
                Name = employeeCreateVM.Name,
                Age = employeeCreateVM.Age,
                Salary = employeeCreateVM.Salary,
                Email = employeeCreateVM.Email,
                Password = employeeCreateVM.Password,
                DepartmentId = employeeCreateVM.DepartmentId
            };

            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = db.Employees.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }

            // Map Domain Model To VM
            var employeeEditVM = new EmployeeEditVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department!.Name,
                Departments = GetDepartmentsForDropDown()
            };
            return View(employeeEditVM);
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeEditVM employeeEditVM)
        {
            var employeeInDb = db.Employees.FirstOrDefault(e => e.Id == employeeEditVM.Id);
            if (employeeInDb == null)
            {
                return RedirectToAction("Index");
            }

            // Map From VM To Domain Model
            employeeInDb.Name = employeeEditVM.Name;
            employeeInDb.Age = employeeEditVM.Age;
            employeeInDb.Salary = employeeEditVM.Salary;
            employeeInDb.DepartmentId = employeeEditVM.DepartmentId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------*/
        public IActionResult Delete(int id)
        {
            var employee = db.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------*/
        [AcceptVerbs("GET", "POST")]
        public IActionResult IsEmailAvailable(string email)
        {
            var isFound = db.Employees.Any(e => e.Email == email);
            if (isFound)
            {
                return Json($"Email {email} already exists");
            }
            return Json(true);
        }
        /*------------------------------------------------------------------*/
        // Helper Method
        // DRY => Reusable Code => Don't Repeat Yourself
        private List<SelectListItem> GetDepartmentsForDropDown()
        {
            return db.Departments
             .Select(d => new SelectListItem
             {
                 Value = d.Id.ToString(),
                 Text = d.Name
             }).ToList();
        }
        /*------------------------------------------------------------------*/
    }
}
