using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discounting.Infrastructure.Migrations.IntegrationEventLog
{
    /// <inheritdoc />
    public partial class AddIntegrationEventLogEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "integration_event_log",
                columns: table => new
                {
                    eventid = table.Column<Guid>(name: "event_id", type: "uuid", nullable: false),
                    eventtypename = table.Column<string>(name: "event_type_name", type: "text", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    timessent = table.Column<int>(name: "times_sent", type: "integer", nullable: false),
                    creationtime = table.Column<DateTime>(name: "creation_time", type: "timestamp with time zone", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integration_event_log", x => x.eventid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "integration_event_log");
        }
    }
}
