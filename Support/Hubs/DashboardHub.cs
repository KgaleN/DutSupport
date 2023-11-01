namespace Support.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Support.Data;
    using Support.Models;
    using System;

    public class DashboardHub : Hub
    {
        private readonly IServiceProvider _serviceProvider;
        /*protected IHubContext<DashboardHub> _hub;*/
      

        public DashboardHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
           
        }
        /*private readonly ApplicationDbContext _context;

        public DashboardHub(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public async Task SendProducts()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var tickets = dbContext.Tickets.Include(ticket => ticket.Attachments).Where(x => x.Status == TicketStatus.Unassigned).ToList();

                //get all support members
                var support = await dbContext.SupportEmployees.Include(ticket => ticket.AssignedTickets).ToListAsync();



                //creates the view model displaying ticket, support and attachment info
                var viewModel = new LeadViewModel
                {
                    tickets = tickets,
                    supporters = support

                };
                // await Clients.All.SendAsync("ReceivedProducts", products);
                // Use the dbContext for a specific operation.

                await Clients.All.SendAsync("ReceivedProducts", viewModel);
            }
           
          


        }
    }

}
