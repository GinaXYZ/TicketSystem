using Microsoft.AspNetCore.Builder;
using TicketSystem.Services;
using TicketSystem.ViewModels;


namespace TicketSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.AddScoped<TicketService>();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapGet("/api/tickets", (TicketService service) =>
            {
                return Results.Ok(service.GetAllTickets());

            })
            .WithName("GetTickets")
            .WithOpenApi();

            app.MapPost("/api/tickets", (CreateTicketRequest request, TicketService service) =>
            {
                var ticket = service.CreateTicket(request);
                return Results.Created($"/api/tickets/{ticket.Id}", ticket);
            })
                .WithName("CreateTicket")
                .WithOpenApi();



            app.Run();
        }
    }
}
