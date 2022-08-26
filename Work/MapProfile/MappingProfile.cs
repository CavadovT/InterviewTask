using AutoMapper;
using Work.Data.Entites;
using Work.Dtos.EmployeeDtos;

namespace Work.MapProfile
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee,DetailEmployeeDto>();
        }
    }
}
