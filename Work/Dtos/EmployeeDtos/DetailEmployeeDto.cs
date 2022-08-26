using FluentValidation;

namespace Work.Dtos.EmployeeDtos
{
    public class DetailEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreateDate { get; set; }
    }
   
}
