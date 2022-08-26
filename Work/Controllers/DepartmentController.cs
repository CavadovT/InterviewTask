using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Work.Data;
using Work.Data.Entites;
using Work.Dtos.DepartmentDtos;

namespace Work.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Create Department Post Action
        /// </summary>
        /// <param name="createDepartmentDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto createDepartmentDto) 
        {
            bool IsExistDepartment =await  _context.Departments.AnyAsync(c => c.Name.Trim().ToLower() == createDepartmentDto.Name.Trim().ToLower());
            if (IsExistDepartment) return StatusCode(409, "Already exist department");

            Department newDep = new Department()
            {
                Name = createDepartmentDto.Name,
                CreateDate = DateTime.Now,
            };
            await _context.Departments.AddAsync(newDep);
            await _context.SaveChangesAsync();

            return StatusCode(201,"Created");
        }
    }
}
