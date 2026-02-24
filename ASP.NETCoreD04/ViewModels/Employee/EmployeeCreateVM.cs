using ASP.NETCoreD04.CustomViladators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreD04.ViewModels.Employee
{
    public class EmployeeCreateVM
    {
        /*------------------------------------------------------------------*/
        #region Get From Form
        [MinLength(3)]
        [MaxLength(20)]
        [Required(ErrorMessage ="Name is mandatory")]
        public string Name { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 5)]
        public string? Address { get; set; }

        [Required]
        [Range(20, 50)]
        public int Age { get; set; }

        [Required]
        [Range(1000, 5000)]
        public decimal Salary { get; set; }

        //[RegularExpression]
        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        [EmailAddress]
        [Remote(action: "IsEmailAvailable", controller: "Employee", ErrorMessage = "Email already exists")] // on change => call IsEmailAvailable Action => Pass Email Value
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required]
        [MinAge(20)]
        [DataType(DataType.Date)]
        public DateOnly? DOB { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        #endregion
        /*------------------------------------------------------------------*/
        #region Send To Form
        public List<SelectListItem>? Departments { get; set; }
        #endregion
        /*------------------------------------------------------------------*/
    }
}
