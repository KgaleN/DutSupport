using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Plugins;
using Support.Data;
using Support.Models;
using Support.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Support.EmailDependencies;
using Microsoft.Graph;

namespace Support.Controllers
{
    public class ReplyController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static List<Ticket> ticketSelected;
        public static List<Models.Chat> ticketChat;
        private readonly GraphServiceClient _graphServiceClient;




        public ReplyController(ApplicationDbContext context, GraphServiceClient graphServiceClient)
        {
            _context = context;
            _graphServiceClient = graphServiceClient;
        }

        public async Task<IActionResult> DisplaySupportEmpTickets()
        {
            var users = await _graphServiceClient.Me.Request().GetAsync();

            if (users.JobTitle== "Support Lead") 
            {
               
                return RedirectToAction( "DisplayTickets", "Ticket");
            }
            if (users.JobTitle == "Lecturer") 
            {
                var empId = users.Id;
                var specificEmp = _context.SupportEmployees.Include(tickets => tickets.AssignedTickets).Where(supportEmp => supportEmp.Id == empId).FirstOrDefault();
                return View(specificEmp); 
            }


            return RedirectToAction("AccessDenied");
        }
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> CloseTicket(int ticketId)
        {
            var foundTicket = _context.Tickets.FirstOrDefault(ticket => ticket.Id == ticketId);
            foundTicket.Status = TicketStatus.Closed;
            _context.SaveChanges();
          
            return RedirectToAction("DisplayTicketChat");

        }
            [HttpGet]
        public async Task<IActionResult> FindTicketChat(int Id)
        {
            ticketSelected = await _context.Tickets.Include(ticket => ticket.Attachments).Where(x => x.Id == Id).ToListAsync();
            ticketChat = await _context.Chats.Include(x => x.Responses).Where(ticket => ticket.TicketId == Id).ToListAsync();
            
            return RedirectToAction("DisplayTicketChat");
        }

       
            public async Task<IActionResult> DisplayTicketChat()
        {
            /*//get all tickets that are unassigned
            var ticketSelected = await _context.Tickets.Include(ticket => ticket.Attachments).Where(x => x.Id == Id).ToListAsync();
            //get all support members
            var ticketChat = await _context.Chats.Include(x => x.Responses).Where(ticket => ticket.TicketId == Id).ToListAsync();

*/
            
            var viewModel = new ChatViewModel
            {
                ticket = ticketSelected,
                chat = ticketChat
            };

           

                return View(viewModel);
    
        }

        public async Task<IActionResult> SendReply(int id, string recipient, string body, string subject)
        {
            var emp = await _graphServiceClient.Me.Request().GetAsync();


            var specificEmp = await _context.SupportEmployees.Include(tickets => tickets.AssignedTickets).Include(chats=>chats.Chats).Where(supportEmp => supportEmp.Id == emp.Id).FirstOrDefaultAsync();

            Emailer email = new Emailer();
            email.sendEmail(emp.Mail, recipient, subject, body, specificEmp.Password);

            var response = new TicketResponse
            {

                Subject = subject,
                Body = body,
                EmailSender = specificEmp.Email,
                EmailRecipient = recipient,
                TicketId = id
            };

            if (IfChatIsNull(id))
            {
                var chat = new Models.Chat { UserEmail = recipient, TicketId = id, Responses = new List<TicketResponse>() };
                _context.Chats.Add(chat);
                _context.SaveChanges();
            }
            else {
                var item = _context.Chats.Include(response => response.Responses).Where(chat => chat.TicketId == id).FirstOrDefault();
                item.Responses.Add(response);
                _context.SaveChanges();

            }
            
                



            ticketSelected = await _context.Tickets.Include(ticket => ticket.Attachments).Where(x => x.Id == id).ToListAsync();
            ticketChat = await _context.Chats.Include(x => x.Responses).Where(ticket => ticket.TicketId == id).ToListAsync();
            
            return RedirectToAction("DisplayTicketChat");
        }


        public bool IfChatIsNull(int id) 
        {
           
            var tcktChat = _context.Chats.Where(ticketChat => ticketChat.TicketId == id).ToList();

            if (tcktChat.Count==0)
            {return true;}

            else 
            { return false; }

            }

    }
}
