using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProblemTracking.Entity.Migrations
{
    public partial class AddMachineRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                schema: "dbo",
                table: "Problem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Problem_MachineId",
                schema: "dbo",
                table: "Problem",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_Machine_MachineId",
                schema: "dbo",
                table: "Problem",
                column: "MachineId",
                principalSchema: "dbo",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.Sql(@"update Problem set MachineId ='1'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problem_Machine_MachineId",
                schema: "dbo",
                table: "Problem");

            migrationBuilder.DropIndex(
                name: "IX_Problem_MachineId",
                schema: "dbo",
                table: "Problem");

            migrationBuilder.DropColumn(
                name: "MachineId",
                schema: "dbo",
                table: "Problem");

           
        }
    }
}
