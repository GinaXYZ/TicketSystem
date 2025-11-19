using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Data.Repositories;
using TicketSystem.Services;
using TicketSystem.Services.Interfaces;
using TicketSystem.ViewModels;
using TicketSystem.Models;

namespace TicketSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<TicketService>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";
                    options.AccessDeniedPath = "/Home/AccessDenied";
                });

            builder.Services.AddControllers();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var canConnect = await context.Database.CanConnectAsync();

                if (canConnect && !await context.TICKET.AnyAsync())
                {
                    context.TICKET.AddRange(
                        new Ticket
                        {
                            Titel = "Sample Ticket 1",
                            Beschreibung = "This is a sample ticket.",
                            Erstellungsdatum = DateTime.UtcNow,
                            Kategorie_Id = 1,
                            Status_Id = 1,
                            Priorität_Id = 1,
                            Ersteller_Id = 1
                        },
                        new Ticket
                        {
                            Titel = "Sample Ticket 2",
                            Beschreibung = "This is another sample ticket.",
                            Erstellungsdatum = DateTime.UtcNow,
                            Kategorie_Id = 1,
                            Status_Id = 1,
                            Priorität_Id = 2,
                            Ersteller_Id = 1
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
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
