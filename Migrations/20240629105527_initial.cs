using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeanSceneSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    AreaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreaID);
                    table.CheckConstraint("CHK_AreaName", "AreaName IN ('Main', 'Outside', 'Balcony')");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    SittingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SCapacity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.SittingID);
                    table.CheckConstraint("CHK_SittingType", "SType IN ('Breakfast', 'Lunch', 'Dinner', 'Special Event')");
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffID);
                    table.CheckConstraint("CHK_StaffType", "StaffType IN ('Staff', 'Manager')");
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaID = table.Column<int>(type: "int", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TableStatus = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableID);
                    table.CheckConstraint("CHK_TableStatus", "TableStatus IN ('Seated', 'Free')");
                    table.ForeignKey(
                        name: "FK_Tables_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "AreaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffPermissions",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffID = table.Column<int>(type: "int", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PermissionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPermissions", x => x.PermissionID);
                    table.CheckConstraint("CHK_PermissionType", "PermissionType IN ('Admin', 'User')");
                    table.ForeignKey(
                        name: "FK_StaffPermissions_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SittingID = table.Column<int>(type: "int", nullable: false),
                    GuestName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    NumOfGuests = table.Column<int>(type: "int", nullable: false),
                    ReservationSource = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TableID = table.Column<int>(type: "int", nullable: true),
                    MemberID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Schedules_SittingID",
                        column: x => x.SittingID,
                        principalTable: "Schedules",
                        principalColumn: "SittingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Tables_TableID",
                        column: x => x.TableID,
                        principalTable: "Tables",
                        principalColumn: "TableID");
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "AreaID", "AreaName" },
                values: new object[,]
                {
                    { 1, "Main" },
                    { 2, "Outside" },
                    { 3, "Balcony" }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "SittingID", "EndDateTime", "SCapacity", "SType", "StartDateTime", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), 30, "Breakfast", new DateTime(2023, 10, 16, 8, 0, 0, 0, DateTimeKind.Unspecified), "Open" },
                    { 2, new DateTime(2023, 10, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), 40, "Lunch", new DateTime(2023, 10, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), "Open" },
                    { 3, new DateTime(2023, 10, 16, 20, 0, 0, 0, DateTimeKind.Unspecified), 50, "Dinner", new DateTime(2023, 10, 16, 18, 0, 0, 0, DateTimeKind.Unspecified), "Closed" },
                    { 4, new DateTime(2023, 11, 20, 22, 0, 0, 0, DateTimeKind.Unspecified), 60, "Special Event", new DateTime(2023, 11, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), "Open" }
                });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "StaffID", "Email", "FirstName", "LastName", "Password", "Phone", "StaffType" },
                values: new object[,]
                {
                    { 1, "alice.smith@example.com", "Alice", "Smith", "hashed_password_4", "555-123-4567", "Staff" },
                    { 2, "david.wilson@example.com", "David", "Wilson", "hashed_password_5", "888-987-6543", "Manager" },
                    { 3, "eva.johnson@example.com", "Eva", "Johnson", "hashed_password_6", "777-555-8888", "Staff" }
                });

            migrationBuilder.InsertData(
                table: "StaffPermissions",
                columns: new[] { "PermissionID", "PermissionType", "StaffID", "TableName" },
                values: new object[] { 1, "Admin", 2, "All" });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableID", "AreaID", "TableName", "TableStatus" },
                values: new object[,]
                {
                    { 1, 1, "M1", "Free" },
                    { 2, 1, "M2", "Free" },
                    { 3, 1, "M3", "Free" },
                    { 4, 1, "M4", "Free" },
                    { 5, 1, "M5", "Free" },
                    { 6, 1, "M6", "Free" },
                    { 7, 1, "M7", "Free" },
                    { 8, 1, "M8", "Free" },
                    { 9, 1, "M9", "Free" },
                    { 10, 1, "M10", "Free" },
                    { 11, 2, "O1", "Free" },
                    { 12, 2, "O2", "Free" },
                    { 13, 2, "O3", "Free" },
                    { 14, 2, "O4", "Free" },
                    { 15, 2, "O5", "Free" },
                    { 16, 2, "O6", "Free" },
                    { 17, 2, "O7", "Free" },
                    { 18, 2, "O8", "Free" },
                    { 19, 2, "O9", "Free" },
                    { 20, 2, "O10", "Free" },
                    { 21, 3, "B1", "Free" },
                    { 22, 3, "B2", "Free" },
                    { 23, 3, "B3", "Free" },
                    { 24, 3, "B4", "Free" },
                    { 25, 3, "B5", "Free" },
                    { 26, 3, "B6", "Free" },
                    { 27, 3, "B7", "Free" },
                    { 28, 3, "B8", "Free" },
                    { 29, 3, "B9", "Free" },
                    { 30, 3, "B10", "Free" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SittingID",
                table: "Reservations",
                column: "SittingID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableID",
                table: "Reservations",
                column: "TableID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffPermissions_StaffID",
                table: "StaffPermissions",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_AreaID",
                table: "Tables",
                column: "AreaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "StaffPermissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
