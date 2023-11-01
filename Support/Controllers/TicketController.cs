using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Support.Data;
using Support.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
/*using TicketSupport.Models;*/

namespace Support.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        //display lead support view
        public async Task<IActionResult> DisplayTickets(){

           //get all tickets that are unassigned
           var ticket = await _context.Tickets.Include(ticket => ticket.Attachments).Where(x=>x.Status==TicketStatus.Unassigned).ToListAsync();
           
           //get all support members
           var support= await _context.SupportEmployees.Include(ticket => ticket.AssignedTickets).ToListAsync();
            
          
           
            //creates the view model displaying ticket, support and attachment info
            var viewModel = new LeadViewModel
            {
                tickets = ticket,
                supporters = support
                                   
            };

            return View(viewModel);
        }

        //view assign priority, and supportmember to ticket
        public async Task<IActionResult> AssignTicket(int ticketId, string selectedRadio, string supportId)
        {
            //get selected ticket
            var foundTicket = _context.Tickets.FirstOrDefault(ticket => ticket.Id == ticketId);
            Priority priority;
          if(Enum.TryParse(selectedRadio, out priority)) 
            {
                //edit selected ticket data
                foundTicket.TicketPriority = priority;
                foundTicket.Status = TicketStatus.Open;

                //save edits.
                _context.SaveChanges();
            }
           
           
            
            //get selected support
            var foundEmployee = _context.SupportEmployees.Include(ticket => ticket.AssignedTickets).FirstOrDefault(emp => emp.Id == supportId);

            //assign ticket to support members
            foundEmployee.AssignedTickets.Add( foundTicket );
            
            
            _context.SaveChanges();

            return RedirectToAction("DisplayTickets");
        }

        public async Task<IActionResult> DisplayAttachment(int AttachmentId)
        {
            var file = _context.TicketAttachments.FindAsync(AttachmentId);

            return File(file.Result.FileData, file.Result.ContentType);
        }

        public async Task<IActionResult> AssignAll()
        {
            var AllUnassignedLists = await _context.Tickets.Where(x => x.Status == TicketStatus.Unassigned).ToListAsync();
            foreach (var ticket in AllUnassignedLists) 
            {
               
                ticket.TicketPriority= 0;
                ticket.Status= TicketStatus.Open;
                //get selected support
                var randomSupportEmployee = _context.SupportEmployees.OrderBy(x => Guid.NewGuid()).Include(ticket => ticket.AssignedTickets).FirstOrDefault();


                //assign ticket to support members
                randomSupportEmployee.AssignedTickets.Add(ticket);

                _context.SaveChanges();
            }
            return RedirectToAction("DisplayTickets");        
        }

    }
}
