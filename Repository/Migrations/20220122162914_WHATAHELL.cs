using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class WHATAHELL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalMovedWeightKg",
                table: "TrainingSessionExercises",
                newName: "TotalMovedWeight");

            migrationBuilder.RenameColumn(
                name: "TotalDistanceMeters",
                table: "TrainingSessionExercises",
                newName: "TotalDistance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalMovedWeight",
                table: "TrainingSessionExercises",
                newName: "TotalMovedWeightKg");

            migrationBuilder.RenameColumn(
                name: "TotalDistance",
                table: "TrainingSessionExercises",
                newName: "TotalDistanceMeters");
        }
    }
}
