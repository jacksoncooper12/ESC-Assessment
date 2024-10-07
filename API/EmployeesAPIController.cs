using ESC.Models;
using ESC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ESC.API
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesAPIController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesAPIController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _repository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees(
            [FromQuery] int? employee_id,
            [FromQuery] string first_name = null,
            [FromQuery] string last_name = null,
            [FromQuery] string email = null,
            [FromQuery] string department_name = null,
            [FromQuery] string country_name = null,
            [FromQuery] string region_name = null)
        {
            if (!employee_id.HasValue && string.IsNullOrEmpty(first_name) && string.IsNullOrEmpty(last_name)
                && string.IsNullOrEmpty(email) && string.IsNullOrEmpty(department_name)
                && string.IsNullOrEmpty(country_name) && string.IsNullOrEmpty(region_name))
            {
                return BadRequest("At least one parameter must be provided.");
            }

            var employees = await _repository.SearchEmployeesAsync(
                employee_id, first_name, last_name, email, department_name, country_name, region_name);

            return Ok(employees);
        }
    }
}
