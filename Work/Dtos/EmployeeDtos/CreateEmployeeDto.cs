using FluentValidation;

namespace Work.Dtos.EmployeeDtos
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public int DepartmentId { get; set; }

    }
    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto> 
    {
        public CreateEmployeeDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(15).MinimumLength(3);
            RuleFor(e => e.Surname).NotEmpty().MinimumLength(3).MaximumLength(15);
            RuleFor(e => e.DepartmentId).NotEmpty();
            RuleFor(e=>e.BirthDate).NotEmpty();
        }
    }
}
