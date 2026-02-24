using ASP.NETCoreD04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASP.NETCoreD04.Data.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        /*------------------------------------------------------------------*/
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // One To Mane 
            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .IsRequired();
        }
        /*------------------------------------------------------------------*/
    }
}
