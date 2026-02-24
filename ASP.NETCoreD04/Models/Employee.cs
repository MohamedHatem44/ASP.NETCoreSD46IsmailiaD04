using System.ComponentModel;

namespace ASP.NETCoreD04.Models
{
    public class Employee
    {
        /*------------------------------------------------------------------*/
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public DateOnly? DOB { get; set; }
        /*------------------------------------------------------------------*/
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;
        /*------------------------------------------------------------------*/
    }
}
