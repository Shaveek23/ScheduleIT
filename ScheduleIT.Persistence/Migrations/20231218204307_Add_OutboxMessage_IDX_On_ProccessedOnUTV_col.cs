using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleIT.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_OutboxMessage_IDX_On_ProccessedOnUTV_col : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_ProcessedOnUtc",
                table: "OutboxMessage",
                column: "ProcessedOnUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessage_ProcessedOnUtc",
                table: "OutboxMessage");
        }
    }
}
