using FluentValidation;

namespace Work.Dtos.EmployeeDtos
{
    public class UpdateEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int DepartmentId { get; set; }
       
    }
    public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto> 
    {
        public UpdateEmployeeDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(15);
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(3).MaximumLength(15);
            RuleFor(x=>x.BirthDate).NotEmpty();
            RuleFor(x=>x.DepartmentId).NotEmpty();
           
        }
    }
}
