using FluentValidation;

namespace Work.Dtos.DepartmentDtos
{
    public class CreateDepartmentDto
    {
        public string Name { get; set; }
    }
    public class CreateDepartmentDtoValidator : AbstractValidator<CreateDepartmentDto> 
    {
        public CreateDepartmentDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5).MaximumLength(15);
        }
    }
}
