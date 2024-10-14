using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeanSceneSystem.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationID",
                keyValue: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationID", "Duration", "Email", "GuestName", "MemberID", "Notes", "NumOfGuests", "Phone", "ReservationSource", "SittingID", "StartTime", "Status", "TableID" },
                values: new object[,]
                {
                    { 1, 90, "sarah@example.com", "Sarah Johnson", null, "Near the bar", 3, "444-555-6666", "Mobile", 2, new DateTime(2023, 10, 17, 19, 30, 0, 0, DateTimeKind.Unspecified), "Pending", 5 },
                    { 2, 120, "michael@example.com", "Michael Wilson", null, "Special dietary needs", 4, "777-888-9999", "Email", 2, new DateTime(2023, 10, 17, 19, 45, 0, 0, DateTimeKind.Unspecified), "Confirmed", 9 },
                    { 3, 90, "grace@example.com", "Grace Brown", null, null, 2, null, "In-person", 4, new DateTime(2023, 10, 19, 13, 0, 0, 0, DateTimeKind.Unspecified), "Seated", 8 },
                    { 4, 60, "oliver@example.com", "Oliver Taylor", null, "Preferred by the window", 2, "222-333-4444", "Mobile", 4, new DateTime(2023, 10, 19, 13, 15, 0, 0, DateTimeKind.Unspecified), "Confirmed", 6 },
                    { 5, 120, "emma@example.com", "Emma Clark", null, "Celebrating a birthday", 4, null, "Online", 1, new DateTime(2023, 10, 16, 8, 30, 0, 0, DateTimeKind.Unspecified), "Completed", 3 },
                    { 6, 90, "william@example.com", "William Smith", null, "Quiet area", 2, "123-987-6543", "Phone", 3, new DateTime(2023, 10, 18, 20, 0, 0, 0, DateTimeKind.Unspecified), "Confirmed", 10 },
                    { 7, 90, "sophia@example.com", "Sophia Wilson", null, null, 3, null, "Mobile", 2, new DateTime(2023, 10, 17, 20, 30, 0, 0, DateTimeKind.Unspecified), "Cancelled", 11 },
                    { 8, 60, "james@example.com", "James Adams", null, null, 2, "777-555-8888", "In-person", 1, new DateTime(2023, 10, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), "Seated", 4 },
                    { 9, 90, "ava@example.com", "Ava Harris", null, "Vegetarian menu", 2, "111-999-3333", "Online", 3, new DateTime(2023, 10, 18, 21, 0, 0, 0, DateTimeKind.Unspecified), "Confirmed", 7 },
                    { 10, 120, "liam@example.com", "Liam Lee", null, "Large group", 6, null, "Mobile", 4, new DateTime(2023, 10, 19, 14, 0, 0, 0, DateTimeKind.Unspecified), "Confirmed", 1 }
                });
        }
    }
}
