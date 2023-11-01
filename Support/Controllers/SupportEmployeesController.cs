using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Graph;
using Support.Data;
using Support.Models;
namespace Support.Controllers
{
    public class SupportEmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GraphServiceClient _graphServiceClient;



        public SupportEmployeesController(ApplicationDbContext context, GraphServiceClient graphServiceClient)
        {
            _context = context;
            _graphServiceClient = graphServiceClient;
        }

        public async Task<IActionResult> DisplayAllSupportEmployees()
        {

            var allSupportEmp = await _context.SupportEmployees.ToListAsync();
            


            return View(allSupportEmp);
        }

        public async Task<IActionResult> SupportEmployeeStatistics(string supportId)
        {
            var supportEmpStats = await _context.SupportEmployees.Include(ticket=>ticket.AssignedTickets).Where(emp=>emp.Id==supportId).FirstOrDefaultAsync();
         

            return View(supportEmpStats);
        }
    }
}
