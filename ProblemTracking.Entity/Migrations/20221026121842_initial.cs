using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProblemTracking.Entity.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Machine",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestigateStep",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StepDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestigateStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestigateStep_Machine_MachineId",
                        column: x => x.MachineId,
                        principalSchema: "dbo",
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Problem",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Problem_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProblemInvestigate",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: true),
                    StepToSolvedId = table.Column<int>(type: "int", nullable: true),
                    SolveStatus = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemInvestigate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProblemInvestigate_InvestigateStep_StepToSolvedId",
                        column: x => x.StepToSolvedId,
                        principalSchema: "dbo",
                        principalTable: "InvestigateStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProblemInvestigate_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalSchema: "dbo",
                        principalTable: "Problem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Machine",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "MachineDescription", "MachineName", "ModifiedDate", "ModifiedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 26, 19, 18, 42, 304, DateTimeKind.Local).AddTicks(2407), null, "This machine use outside factory plaese investigate dust issue.", "Machine1", null, null },
                    { 2, new DateTime(2022, 10, 26, 19, 18, 42, 304, DateTimeKind.Local).AddTicks(8966), null, "This machine use outside factory plaese investigate dust issue.", "Machine1", null, null },
                    { 3, new DateTime(2022, 10, 26, 19, 18, 42, 304, DateTimeKind.Local).AddTicks(8980), null, "This machine use outside factory plaese investigate dust issue.", "Machine1", null, null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "FirstName", "LastName", "ModifiedDate", "ModifiedUserId", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("80d36942-b9be-4c34-8fc9-9761f33eb210"), new DateTime(2022, 10, 26, 19, 18, 42, 305, DateTimeKind.Local).AddTicks(9526), null, "Hamlet", "last 1", null, null, "12345", "User1" },
                    { new Guid("d737bd1a-28e6-47b7-ab6e-1c755f63178f"), new DateTime(2022, 10, 26, 19, 18, 42, 306, DateTimeKind.Local).AddTicks(98), null, "King Lear", "last 2", null, null, "12345", "User2" },
                    { new Guid("79a48d2d-1ce4-49c0-a75e-b7ebd7be8576"), new DateTime(2022, 10, 26, 19, 18, 42, 306, DateTimeKind.Local).AddTicks(103), null, "Othello", "last 3", null, null, "12345", "User3" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "InvestigateStep",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "MachineId", "ModifiedDate", "ModifiedUserId", "Order", "StepDetail", "StepName" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 26, 19, 18, 42, 305, DateTimeKind.Local).AddTicks(6383), null, 1, null, null, 1, "Normally, this machince stuck because of some object break it.", "Step1 Check surface" },
                    { 2, new DateTime(2022, 10, 26, 19, 18, 42, 305, DateTimeKind.Local).AddTicks(7038), null, 1, null, null, 2, "Make sure you not unplug it.", "Step2 Check electricity" },
                    { 3, new DateTime(2022, 10, 26, 19, 18, 42, 305, DateTimeKind.Local).AddTicks(7042), null, 1, null, null, 3, "Sometime we use it all day.", "Step3 Check all liquid" },
                    { 4, new DateTime(2022, 10, 26, 19, 18, 42, 305, DateTimeKind.Local).AddTicks(7044), null, 2, null, null, 1, "Normally, this machince stuck because of some object break it.2", "Step1 Check surface 2 " },
                    { 5, new DateTime(2022, 10, 26, 19, 18, 42, 305, DateTimeKind.Local).AddTicks(7045), null, 2, null, null, 2, "Make sure you not unplug it.2", "Step2 Check electricity 2" },
                    { 6, new DateTime(2022, 10, 26, 19, 18, 42, 305, DateTimeKind.Local).AddTicks(7046), null, 2, null, null, 3, "Sometime we use it all day.2", "Step3 Check all liquid 2" },
                    { 7, new DateTime(2022, 10, 26, 19, 18, 42, 305, DateTimeKind.Local).AddTicks(7047), null, 3, null, null, 1, "Normally, this machince stuck because of some object break it.3", "Step1 Check surface 3 " },
                    { 8, new DateTime(2022, 10, 26, 19, 18, 42, 305, DateTimeKind.Local).AddTicks(7551), null, 3, null, null, 2, "Make sure you not unplug it.3", "Step2 Check electricity 3" },
                    { 9, new DateTime(2022, 10, 26, 19, 18, 42, 305, DateTimeKind.Local).AddTicks(7555), null, 3, null, null, 3, "Sometime we use it all day.3", "Step3 Check all liquid 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestigateStep_MachineId",
                schema: "dbo",
                table: "InvestigateStep",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Problem_UserId",
                schema: "dbo",
                table: "Problem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemInvestigate_ProblemId",
                schema: "dbo",
                table: "ProblemInvestigate",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemInvestigate_StepToSolvedId",
                schema: "dbo",
                table: "ProblemInvestigate",
                column: "StepToSolvedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProblemInvestigate",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InvestigateStep",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Problem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Machine",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
