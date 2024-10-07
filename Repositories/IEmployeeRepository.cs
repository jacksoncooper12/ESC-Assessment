using ESC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESC.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        Task<IEnumerable<Employee>> SearchEmployeesAsync(
            int? employee_id,
            string first_name,
            string last_name,
            string email,
            string department_name,
            string country_name,
            string region_name);
    }
}