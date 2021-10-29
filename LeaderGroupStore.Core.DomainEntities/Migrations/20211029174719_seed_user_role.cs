using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaderGroupStore.Core.DomainEntities.Migrations
{
    public partial class seed_user_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "27fb1397-f0d9-4340-9af6-047cc3be06bc", "b6ff83a9-0e82-4fd3-b692-89209bc33dcb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7180c53f-ac7d-4539-ace9-4b5e32890dd2", "bbfa5d0e-0ef5-487f-a3a0-8ea5ee71b690" });
        }
    }
}
