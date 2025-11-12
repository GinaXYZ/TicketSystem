using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Data.Repositories;
using TicketSystem.Services;
using TicketSystem.Services.Interfaces;
using TicketSystem.ViewModels;


namespace TicketSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            builder.Services.AddScoped<TicketService>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseStaticFiles();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapGet("/api/tickets", async ([Microsoft.AspNetCore.Mvc.FromServices] TicketService service) =>
            {
                return Results.Ok(await service.GetAllTicketsAsync());
            })
            .WithName("GetTickets")
            .WithOpenApi();

            app.MapPost("/api/tickets", async ([Microsoft.AspNetCore.Mvc.FromBody] CreateTicketRequest request, [Microsoft.AspNetCore.Mvc.FromServices] TicketService service) =>
            {
                var ticket = await service.CreateTicketAsync(request);
                return Results.Created($"/api/tickets/{ticket.Id}", ticket);
            })
            .WithName("CreateTicket")
            .WithOpenApi();

            app.Run();
        }
    }
}
