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
                name: "locations",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DDD = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    UserRole = table.Column<byte>(type: "tinyint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocationId = table.Column<byte>(type: "tinyint", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "beecontracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdUser = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdUser = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdBeeContract = table.Column<int>(type: "int", nullable: false),
                    IdHive = table.Column<int>(type: "int", nullable: false),
                    BeeContractId = table.Column<int>(type: "int", nullable: true),
                    HiveId = table.Column<int>(type: "int", nullable: true)
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
                table: "locations",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "LastUpdatedAt", "Name", "DDD" },
                values: new object[,]
                {
                    { (byte)1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "São Paulo - Capital", 11 },
                    { (byte)2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "São José dos Campos e Região", 12 },
                    { (byte)3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Baixada Santista e Região", 13 },
                    { (byte)4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Bauru e Região", 14 },
                    { (byte)5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Sorocaba e Região", 15 },
                    { (byte)6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ribeirão Preto e Região", 16 },
                    { (byte)7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "São José do Rio Preto e Região", 17 },
                    { (byte)8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Presidente Prudente e Região", 18 },
                    { (byte)9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Campinas e Região", 19 },
                    { (byte)10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Rio de Janeiro - Capital", 21 },
                    { (byte)11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Campos dos Goytacazes e Região", 22 },
                    { (byte)12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Volta Redonda e Região", 24 },
                    { (byte)13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Vitória e Região", 27 },
                    { (byte)14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Sul do Espírito Santo", 28 },
                    { (byte)15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Belo Horizonte e Região", 31 },
                    { (byte)16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Juiz de Fora e Região", 32 },
                    { (byte)17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Governador Valadares e Região", 33 },
                    { (byte)18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Uberlândia e Região", 34 },
                    { (byte)19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Poços de Caldas e Região", 35 },
                    { (byte)20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Divinópolis e Região", 37 },
                    { (byte)21, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Montes Claros e Região", 38 },
                    { (byte)22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Curitiba e Região", 41 },
                    { (byte)23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ponta Grossa e Região", 42 },
                    { (byte)24, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Londrina e Região", 43 },
                    { (byte)25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Maringá e Região", 44 },
                    { (byte)26, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Foz do Iguaçu e Região", 45 },
                    { (byte)27, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Pato Branco e Região", 46 },
                    { (byte)28, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Joinville e Região", 47 },
                    { (byte)29, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Florianópolis e Região", 48 },
                    { (byte)30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Chapecó e Região", 49 },
                    { (byte)31, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Porto Alegre e Região", 51 },
                    { (byte)32, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Pelotas e Região", 53 },
                    { (byte)33, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Caxias do Sul e Região", 54 },
                    { (byte)34, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Santa Maria e Região", 55 },
                    { (byte)35, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Distrito Federal", 61 },
                    { (byte)36, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Goiânia e Região", 62 },
                    { (byte)37, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Tocantins", 63 },
                    { (byte)38, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Rio Verde e Região", 64 },
                    { (byte)39, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Cuiabá e Região", 65 },
                    { (byte)40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Rondonópolis e Região", 66 },
                    { (byte)41, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Campo Grande e Região", 67 },
                    { (byte)42, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Acre", 68 },
                    { (byte)43, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Rondônia", 69 },
                    { (byte)44, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Salvador e Região", 71 },
                    { (byte)45, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ilhéus e Região", 73 },
                    { (byte)46, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Juazeiro e Região", 74 },
                    { (byte)47, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Feira de Santana e Região", 75 },
                    { (byte)48, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Barreiras e Região", 77 },
                    { (byte)49, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Sergipe", 79 },
                    { (byte)50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Recife e Região", 81 },
                    { (byte)51, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Alagoas", 82 },
                    { (byte)52, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Paraíba", 83 },
                    { (byte)53, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Rio Grande do Norte", 84 },
                    { (byte)54, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Fortaleza e Região", 85 },
                    { (byte)55, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Teresina e Região", 86 },
                    { (byte)56, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Petrolina e Região", 87 },
                    { (byte)57, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Região do Cariri", 88 },
                    { (byte)58, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Piauí exceto Teresina", 89 },
                    { (byte)59, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Belém e Região", 91 },
                    { (byte)60, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Manaus e Região", 92 },
                    { (byte)61, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Santarém e Região", 93 },
                    { (byte)62, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Marabá e Região", 94 },
                    { (byte)63, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Roraima", 95 },
                    { (byte)64, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Amapá", 96 },
                    { (byte)65, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Amazonas", 97 },
                    { (byte)66, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "São Luís e Região", 98 },
                    { (byte)67, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Imperatriz e Região", 99 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "LastUpdatedAt", "LocationId", "Name", "Surname", "UserRole", "PasswordHash", "CPF", "Email", "Phone" },
                values: new object[,]
                {
                    { 10000, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, (byte)1, "Administrador", "(built-in)", (byte)1, "BGcEw9QQNyBOf+rLF/xrMboZKa035bzLBqgGpTBJTrE8Fk2TwAMbe8N49SbaM2Ro", "80455390037", "admin@ubee.com", "11-983594962" },
                    { 10001, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, (byte)1, "Cleber", "(built-in)", (byte)2, "AxX8E7IFxv4rSTXU40IRjY6oPLVOq1y1tp0O5/vabDT/SPZlOWdktbiKCz2YLdzJ", "87622041068", "cleber@ubee.com", "11-992504176" },
                    { 10002, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, (byte)29, "Diego", "(built-in)", (byte)3, "SlZEzmsPcuYfe8GRqN9lMLqv5KJpVmGpChaRoS5YVYQo/sSdeY6G5xj+nLF7zxJR", "40070242003", "diego@ubee.com", "48-91662888" },
                    { 10003, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, (byte)1, "Lucas", "(built-in)", (byte)3, "Wa+ZKmUcoWjcVPjQwVzY3tok2Thcejh2fGlA2lwZXv2oZ0NxL6Kb71NPYB8LP2De", "99872134057", "lucas@ubee.com", "11-994635700" },
                    { 10004, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, (byte)5, "Rafael", "(built-in)", (byte)2, "tiNsfaj8kjCoJJcJeNyQqn03Ym4vuQldu3T+QL0AtJ9OzfkZcwo8UCd5+UcTDzEa", "46074925070", "rafael@ubee.com", "15-998106370" },
                    { 10005, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, (byte)4, "Wesley", "(built-in)", (byte)3, "V8xyPoBEnUEUKLq5dxW5hqk8yiD42kfs1BMd8fKRkgrL9Ad1cA95US4avnA4TPYz", "10096759070", "wesley@ubee.com", "14-981343266" }
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
                name: "IX_users_LocationId",
                table: "users",
                column: "LocationId");
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

            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
