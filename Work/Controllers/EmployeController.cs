using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Work.Data;
using Work.Data.Entites;
using Work.Dtos.EmployeeDtos;
using Work.Extetntions;
using Work.Helpers;

namespace Work.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Add Employe
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto employee)
        {
            var newemploye = new Employee()
            {
                Name = employee.Name,
                Surname = employee.Surname,
                BirthDate = DateTime.Parse(employee.BirthDate),
                CreateDate = DateTime.Now,
                DepartmentId = employee.DepartmentId,
            };
            await _context.Employees.AddAsync(newemploye);
            await _context.SaveChangesAsync();

            return Ok("Created");
        }
        /// <summary>
        /// Employees get all actions
        /// </summary>
        /// <returns></returns>
        [HttpGet("A")]
        public async Task<IActionResult> GetAll()
        {
            List<DetailEmployeeDto> employees = await _context.Employees
               .ProjectTo<DetailEmployeeDto>(_mapper.ConfigurationProvider)
               .AsQueryable().AsNoTracking()
               .ToListAsync();

            ListDto<DetailEmployeeDto> listemployees = new ListDto<DetailEmployeeDto>()
            {
                Items = employees,
                TotalCount = employees.Count(),
            };
            return Ok(listemployees);
        }
        /// <summary>
        /// Get one Employe by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int? id)
        {
            if (id == null) RedirectToAction("GetAll");

            var dbE = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(c => c.Id == id);
            if (dbE == null) return NotFound();


            var result = _mapper.Map<DetailEmployeeDto>(dbE);

            return Ok(result);
        }
        /// <summary>
        /// Delete Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest("Enter id please");

            var dbE = await _context.Employees.FindAsync(id);
            if (dbE == null) return NotFound();

            _context.Remove(dbE);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }
        /// <summary>
        /// Update Employee 
        /// </summary>
        /// <param name="updateEmployee"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmploye(int? id, UpdateEmployeeDto updateEmployee)
        {
            if (id == null) return BadRequest();
            var dbE = await _context.Employees.FindAsync(id);
            if (dbE == null) return NotFound();

            bool ExistName = await _context.Employees
                .AnyAsync(e => e.Name.ToLower().Trim() == updateEmployee.Name.ToLower().Trim() && e.Id != dbE.Id);
            if (ExistName) return StatusCode(409, "Employee already exist");

            bool ExistSurname = await _context.Employees
                .AnyAsync(e => e.Surname.ToLower().Trim() == updateEmployee.Name.ToLower().Trim() && e.Id != dbE.Id);
            if (ExistSurname) return StatusCode(409, "Employee already exist");

            dbE.Name = updateEmployee.Name;
            dbE.Surname = updateEmployee.Surname;
            dbE.BirthDate = updateEmployee.BirthDate;
            dbE.DepartmentId = updateEmployee.DepartmentId;

            await _context.SaveChangesAsync();

            return Ok("Updated");
        }
        /// <summary>
        /// Filter Get method
        /// </summary>
        /// <param name="pageFilter"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="depid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<Employee>>> GetFilteredEmployees([FromQuery] FilterEmployeeDto f)
        {

            var employees = _context.Employees.AsQueryable();
            if (f.Id!=0)
            {
                employees = employees.Where(e => e.Id == f.Id);
            }
            if (f.Name != null)
            {
                employees = employees.Where(e => e.Name.ToLower().Contains(f.Name.ToLower()));
            }
            if (f.Surname != null)
            {
                employees = employees.Where(e => e.Surname.ToLower().Contains(f.Surname.ToLower()));
            }
            if (f.DepartmentId != 0)
            {
                employees = employees.Where(e => e.DepartmentId == f.DepartmentId);
            }

            var pagedList = await PageList<Employee>.CreateAsync(employees, f.pageFilter.PageNumber, f.pageFilter.PageSize);
            Response.AddPaginationHeader(pagedList.CurrentPage, pagedList.PageSize, pagedList.TotalCount, pagedList.TotalPages);

            return pagedList.ToList();
        }

    }
}
