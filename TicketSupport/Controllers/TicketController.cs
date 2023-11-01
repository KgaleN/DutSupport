using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Graph;
using TicketSupport.Data;
using TicketSupport.Models;

namespace TicketSupport.Controllers
{
    public class TicketController : Controller
    {
        Ticket ticket = new Ticket();
            
        private readonly ApplicationDbContext _context;
       


        private readonly GraphServiceClient _graphServiceClient;



        public TicketController(ApplicationDbContext context, GraphServiceClient graphServiceClient)
        {
            _context = context;
            _graphServiceClient = graphServiceClient;
        }

        //input ticket information view
        public Task<IActionResult> Query()
        {
            return Task.FromResult<IActionResult>(View()) ;
        }

        //save the ticket input information to the database
        public async Task<IActionResult> Submit(string subject, TicketType type, string body, List<IFormFile> formFiles)
        {
            //if (string.IsNullOrEmpty(subject))
            //assign values to the ticket object
            ticket.Subject = subject;
            ticket.Type = type;
            ticket.Body = body;
            ticket.TimeStamp = DateTime.Now;

            //edit later  
            var user = await _graphServiceClient.Me.Request().GetAsync();

            //edit later  
            ticket.UserEmail = user.Mail;

            int count=0;

            ticket.Attachments = new List<TicketAttachment>();

            foreach (var formFile in formFiles) 
            { 
                var memoryStream = new MemoryStream();
                TicketAttachment file = new TicketAttachment();

                formFile.CopyTo(memoryStream);

                //assign values to the ticket object
                file.FileData = memoryStream.ToArray();
                file.FileName = formFile.FileName;
                file.ContentType = formFile.ContentType;

                //assign attachments to ticket
                ticket.Attachments.Add(file);
                count++;

                if(count==formFiles.Count) 
                {
                    _context.Tickets.Add(ticket);
                    _context.SaveChanges();
                }
            }
            //save ticket object without attachments
            if (count==0)
            {
                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }
            return  RedirectToAction("Query");
        }
    }
}
