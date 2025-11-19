using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class SyncModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

        // Falls bereits eine Tabelle [TICKET] existiert (z.B. durch abgebrochene Migration), löschen
            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "TICKET");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TICKET",
                newName: "Titel");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TICKET",
                newName: "Beschreibung");

            migrationBuilder.AddColumn<int>(
                name: "Ersteller_Id",
                table: "TICKET",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Erstellungsdatum",
                table: "TICKET",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fälligkeitsdatum",
                table: "TICKET",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Kategorie_Id",
                table: "TICKET",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priorität_Id",
                table: "TICKET",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status_Id",
                table: "TICKET",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TICKET",
                table: "TICKET",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TICKET",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "Ersteller_Id",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "Erstellungsdatum",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "Fälligkeitsdatum",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "Kategorie_Id",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "Priorität_Id",
                table: "TICKET");

            migrationBuilder.DropColumn(
                name: "Status_Id",
                table: "TICKET");

            migrationBuilder.RenameTable(
                name: "TICKET",
                newName: "Tickets");

            migrationBuilder.RenameColumn(
                name: "Titel",
                table: "Tickets",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Beschreibung",
                table: "Tickets",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");
        }
    }
}
