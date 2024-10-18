using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace uBee.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "beecontracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beecontracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_beecontracts_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "hives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hives_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "contractedhives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBeeContract = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHive = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BeeContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractedhives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contractedhives_beecontracts_BeeContractId",
                        column: x => x.BeeContractId,
                        principalTable: "beecontracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_contractedhives_beecontracts_IdBeeContract",
                        column: x => x.IdBeeContract,
                        principalTable: "beecontracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_contractedhives_hives_HiveId",
                        column: x => x.HiveId,
                        principalTable: "hives",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_contractedhives_hives_IdHive",
                        column: x => x.IdHive,
                        principalTable: "hives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "LastUpdatedAt", "Location", "Name", "Phone", "Surname", "UserRole", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("18e74db3-4205-423b-9ffd-a9bf38aff665"), "lucas@ubee.com", null, 11, "Lucas", "999999993", "(built-in)", 2, "Wa+ZKmUcoWjcVPjQwVzY3tok2Thcejh2fGlA2lwZXv2oZ0NxL6Kb71NPYB8LP2De" },
                    { new Guid("5b108440-fa72-4373-9f32-c962addb105e"), "wesley@ubee.com", null, 21, "Wesley", "999999995", "(built-in)", 2, "V8xyPoBEnUEUKLq5dxW5hqk8yiD42kfs1BMd8fKRkgrL9Ad1cA95US4avnA4TPYz" },
                    { new Guid("6e409a41-6cbb-4f8d-92f3-c05b6bb6e181"), "rafael@ubee.com", null, 11, "Rafael", "999999994", "(built-in)", 3, "tiNsfaj8kjCoJJcJeNyQqn03Ym4vuQldu3T+QL0AtJ9OzfkZcwo8UCd5+UcTDzEa" },
                    { new Guid("b31ea7f4-b852-45d1-ae38-684310c6fb17"), "diego@ubee.com", null, 15, "Diego", "999999992", "(built-in)", 2, "SlZEzmsPcuYfe8GRqN9lMLqv5KJpVmGpChaRoS5YVYQo/sSdeY6G5xj+nLF7zxJR" },
                    { new Guid("c5901c93-817a-43d4-a5ab-be2d3aee1ee3"), "cleber@ubee.com", null, 15, "Cleber", "999999991", "(built-in)", 3, "AxX8E7IFxv4rSTXU40IRjY6oPLVOq1y1tp0O5/vabDT/SPZlOWdktbiKCz2YLdzJ" },
                    { new Guid("d9612923-19ce-4af6-a166-94d919e42fa3"), "admin@ubee.com", null, 11, "Administrador", "999999999", "(built-in)", 1, "BGcEw9QQNyBOf+rLF/xrMboZKa035bzLBqgGpTBJTrE8Fk2TwAMbe8N49SbaM2Ro" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_beecontracts_IdUser",
                table: "beecontracts",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_contractedhives_BeeContractId",
                table: "contractedhives",
                column: "BeeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_contractedhives_HiveId",
                table: "contractedhives",
                column: "HiveId");

            migrationBuilder.CreateIndex(
                name: "IX_contractedhives_IdBeeContract",
                table: "contractedhives",
                column: "IdBeeContract");

            migrationBuilder.CreateIndex(
                name: "IX_contractedhives_IdHive",
                table: "contractedhives",
                column: "IdHive");

            migrationBuilder.CreateIndex(
                name: "IX_hives_IdUser",
                table: "hives",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Phone",
                table: "users",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contractedhives");

            migrationBuilder.DropTable(
                name: "beecontracts");

            migrationBuilder.DropTable(
                name: "hives");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
