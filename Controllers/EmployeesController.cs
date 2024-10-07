using Microsoft.AspNetCore.Mvc;

namespace ESC.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult ViewEmployees()
        {
            return View();
        }
    }
}