using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESC.Data;
using ESC.Models;
using Microsoft.EntityFrameworkCore;

namespace ESC.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRDbContext _context;

        public EmployeeRepository(HRDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Include(e => e.Dependents)
                .Include(e => e.Job)
                .Include(e => e.Department)
                    .ThenInclude(d => d.Location)
                        .ThenInclude(l => l.Country)
                            .ThenInclude(c => c.Region)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> SearchEmployeesAsync(
            int? employee_id,
            string first_name,
            string last_name,
            string email,
            string department_name,
            string country_name,
            string region_name)
        {
            var query = _context.Employees
                .Include(e => e.Dependents)
                .Include(e => e.Job)
                .Include(e => e.Department)
                    .ThenInclude(d => d.Location)
                        .ThenInclude(l => l.Country)
                            .ThenInclude(c => c.Region)
                .AsQueryable();

            if (employee_id.HasValue)
            {
                query = query.Where(e => e.EmployeeId == employee_id.Value);
            }

            if (!string.IsNullOrEmpty(first_name))
            {
                query = query.Where(e => e.FirstName == first_name);
            }

            if (!string.IsNullOrEmpty(last_name))
            {
                query = query.Where(e => e.LastName == last_name);
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(e => e.Email == email);
            }

            if (!string.IsNullOrEmpty(department_name))
            {
                query = query.Where(e => e.Department.DepartmentName == department_name);
            }

            if (!string.IsNullOrEmpty(country_name))
            {
                query = query.Where(e => e.Department.Location.Country.CountryName == country_name);
            }

            if (!string.IsNullOrEmpty(region_name))
            {
                query = query.Where(e => e.Department.Location.Country.Region.RegionName == region_name);
            }

            return await query.ToListAsync();
        }
    }
}