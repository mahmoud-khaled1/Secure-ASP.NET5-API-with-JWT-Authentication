using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiWithJWT.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // User
            migrationBuilder.InsertData(table: "AspNetRoles",
                columns: new[] { "Id","Name", "NormalizedName", "ConcurrencyStamp" },
                values: new[] {Guid.NewGuid().ToString(),"User","User".ToUpper(),Guid.NewGuid().ToString()} );

            //Admin 
            migrationBuilder.InsertData(table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [AspNetRoles]");
        }
    }
}
