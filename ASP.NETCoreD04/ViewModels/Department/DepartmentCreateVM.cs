using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreD04.ViewModels.Department
{
    public class DepartmentCreateVM
    {
        /*------------------------------------------------------------------*/
        [Display(Name = "Department Name")] // Display => For UI
        //[DisplayName("H2")]       // DisplayName => For UI
        //[MinLength(3)]
        public required string Name { get; set; }
        /*------------------------------------------------------------------*/
    }
}
