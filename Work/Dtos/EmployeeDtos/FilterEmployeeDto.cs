using System.ComponentModel.DataAnnotations;

namespace Work.Dtos.EmployeeDtos
{
    public class FilterEmployeeDto
    {
        public int Id { get; set; }=0;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }
        public int DepartmentId { get; set; }
    }
}
