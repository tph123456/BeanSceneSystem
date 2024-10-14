using BeanSceneSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BeanSceneSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffPermission> StaffPermissions { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Raw SQL commands to drop and re-create the check constraint
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.StaffID); // Explicitly set the primary key

                entity.ToTable(t =>
                    t.HasCheckConstraint("CHK_StaffType", "StaffType IN ('Staff', 'Manager')")
                );

                entity.HasData(
                    new Staff { StaffID = 1, StaffType = "Staff", FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com", Phone = "555-123-4567", Password = "hashed_password_4" },
                    new Staff { StaffID = 2, StaffType = "Manager", FirstName = "David", LastName = "Wilson", Email = "david.wilson@example.com", Phone = "888-987-6543", Password = "hashed_password_5" },
                    new Staff { StaffID = 3, StaffType = "Staff", FirstName = "Eva", LastName = "Johnson", Email = "eva.johnson@example.com", Phone = "777-555-8888", Password = "hashed_password_6" }
                );
            });

            modelBuilder.Entity<StaffPermission>(entity =>
            {
                entity.HasKey(e => e.PermissionID); // Explicitly set the primary key
                entity.ToTable(t =>
                    t.HasCheckConstraint("CHK_PermissionType", "PermissionType IN ('Admin', 'User')")
                );
                entity.HasOne(e => e.Staff)
                      .WithMany()
                      .HasForeignKey(e => e.StaffID);
                entity.HasData(
                    new StaffPermission { PermissionID = 1, StaffID = 2, TableName = "All", PermissionType = "Admin" }
                );
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.SittingID); // Explicitly set the primary key
                entity.ToTable(t =>
                    t.HasCheckConstraint("CHK_SittingType", "SType IN ('Breakfast', 'Lunch', 'Dinner', 'Special Event')")
                );
                entity.HasData(
                    new Schedule { SittingID = 1, SType = "Breakfast", StartDateTime = new DateTime(2023, 10, 16, 8, 0, 0), EndDateTime = new DateTime(2023, 10, 16, 10, 0, 0), SCapacity = 30, Status = "Open" },
                    new Schedule { SittingID = 2, SType = "Lunch", StartDateTime = new DateTime(2023, 10, 16, 12, 0, 0), EndDateTime = new DateTime(2023, 10, 16, 14, 0, 0), SCapacity = 40, Status = "Open" },
                    new Schedule { SittingID = 3, SType = "Dinner", StartDateTime = new DateTime(2023, 10, 16, 18, 0, 0), EndDateTime = new DateTime(2023, 10, 16, 20, 0, 0), SCapacity = 50, Status = "Closed" },
                    new Schedule { SittingID = 4, SType = "Special Event", StartDateTime = new DateTime(2023, 11, 20, 19, 0, 0), EndDateTime = new DateTime(2023, 11, 20, 22, 0, 0), SCapacity = 60, Status = "Open" }
                );
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.AreaID); // Explicitly set the primary key
                entity.ToTable(t =>
                    t.HasCheckConstraint("CHK_AreaName", "AreaName IN ('Main', 'Outside', 'Balcony')")
                );
                entity.HasData(
                    new Area { AreaID = 1, AreaName = "Main" },
                    new Area { AreaID = 2, AreaName = "Outside" },
                    new Area { AreaID = 3, AreaName = "Balcony" }
                );
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasKey(e => e.TableID); // Explicitly set the primary key
                entity.ToTable(t =>
                    t.HasCheckConstraint("CHK_TableStatus", "TableStatus IN ('Booked', 'Free')")
                );
                entity.HasData(
                    new Table { TableID = 1, AreaID = 1, TableName = "M1", TableStatus = "Free" },
                    new Table { TableID = 2, AreaID = 1, TableName = "M2", TableStatus = "Free" },
                    new Table { TableID = 3, AreaID = 1, TableName = "M3", TableStatus = "Free" },
                    new Table { TableID = 4, AreaID = 1, TableName = "M4", TableStatus = "Free" },
                    new Table { TableID = 5, AreaID = 1, TableName = "M5", TableStatus = "Free" },
                    new Table { TableID = 6, AreaID = 1, TableName = "M6", TableStatus = "Free" },
                    new Table { TableID = 7, AreaID = 1, TableName = "M7", TableStatus = "Free" },
                    new Table { TableID = 8, AreaID = 1, TableName = "M8", TableStatus = "Free" },
                    new Table { TableID = 9, AreaID = 1, TableName = "M9", TableStatus = "Free" },
                    new Table { TableID = 10, AreaID = 1, TableName = "M10", TableStatus = "Free" },
                    new Table { TableID = 11, AreaID = 2, TableName = "O1", TableStatus = "Free" },
                    new Table { TableID = 12, AreaID = 2, TableName = "O2", TableStatus = "Free" },
                    new Table { TableID = 13, AreaID = 2, TableName = "O3", TableStatus = "Free" },
                    new Table { TableID = 14, AreaID = 2, TableName = "O4", TableStatus = "Free" },
                    new Table { TableID = 15, AreaID = 2, TableName = "O5", TableStatus = "Free" },
                    new Table { TableID = 16, AreaID = 2, TableName = "O6", TableStatus = "Free" },
                    new Table { TableID = 17, AreaID = 2, TableName = "O7", TableStatus = "Free" },
                    new Table { TableID = 18, AreaID = 2, TableName = "O8", TableStatus = "Free" },
                    new Table { TableID = 19, AreaID = 2, TableName = "O9", TableStatus = "Free" },
                    new Table { TableID = 20, AreaID = 2, TableName = "O10", TableStatus = "Free" },
                    new Table { TableID = 21, AreaID = 3, TableName = "B1", TableStatus = "Free" },
                    new Table { TableID = 22, AreaID = 3, TableName = "B2", TableStatus = "Free" },
                    new Table { TableID = 23, AreaID = 3, TableName = "B3", TableStatus = "Free" },
                    new Table { TableID = 24, AreaID = 3, TableName = "B4", TableStatus = "Free" },
                    new Table { TableID = 25, AreaID = 3, TableName = "B5", TableStatus = "Free" },
                    new Table { TableID = 26, AreaID = 3, TableName = "B6", TableStatus = "Free" },
                    new Table { TableID = 27, AreaID = 3, TableName = "B7", TableStatus = "Free" },
                    new Table { TableID = 28, AreaID = 3, TableName = "B8", TableStatus = "Free" },
                    new Table { TableID = 29, AreaID = 3, TableName = "B9", TableStatus = "Free" },
                    new Table { TableID = 30, AreaID = 3, TableName = "B10", TableStatus = "Free" }
                );
            });

            //modelBuilder.Entity<Reservation>().HasData(
            //    new Reservation { ReservationID = 1, SittingID = 2, GuestName = "Sarah Johnson", Email = "sarah@example.com", Phone = "444-555-6666", StartTime = new DateTime(2023, 10, 17, 19, 30, 0), Duration = 90, NumOfGuests = 3, ReservationSource = "Mobile", Notes = "Near the bar", Status = "Pending", TableID = 5 },
            //    new Reservation { ReservationID = 2, SittingID = 2, GuestName = "Michael Wilson", Email = "michael@example.com", Phone = "777-888-9999", StartTime = new DateTime(2023, 10, 17, 19, 45, 0), Duration = 120, NumOfGuests = 4, ReservationSource = "Email", Notes = "Special dietary needs", Status = "Confirmed", TableID = 9 },
            //    new Reservation { ReservationID = 3, SittingID = 4, GuestName = "Grace Brown", Email = "grace@example.com", Phone = null, StartTime = new DateTime(2023, 10, 19, 13, 0, 0), Duration = 90, NumOfGuests = 2, ReservationSource = "In-person", Notes = null, Status = "Seated", TableID = 8 },
            //    new Reservation { ReservationID = 4, SittingID = 4, GuestName = "Oliver Taylor", Email = "oliver@example.com", Phone = "222-333-4444", StartTime = new DateTime(2023, 10, 19, 13, 15, 0), Duration = 60, NumOfGuests = 2, ReservationSource = "Mobile", Notes = "Preferred by the window", Status = "Confirmed", TableID = 6 },
            //    new Reservation { ReservationID = 5, SittingID = 1, GuestName = "Emma Clark", Email = "emma@example.com", Phone = null, StartTime = new DateTime(2023, 10, 16, 8, 30, 0), Duration = 120, NumOfGuests = 4, ReservationSource = "Online", Notes = "Celebrating a birthday", Status = "Completed", TableID = 3 },
            //    new Reservation { ReservationID = 6, SittingID = 3, GuestName = "William Smith", Email = "william@example.com", Phone = "123-987-6543", StartTime = new DateTime(2023, 10, 18, 20, 0, 0), Duration = 90, NumOfGuests = 2, ReservationSource = "Phone", Notes = "Quiet area", Status = "Confirmed", TableID = 10 },
            //    new Reservation { ReservationID = 7, SittingID = 2, GuestName = "Sophia Wilson", Email = "sophia@example.com", Phone = null, StartTime = new DateTime(2023, 10, 17, 20, 30, 0), Duration = 90, NumOfGuests = 3, ReservationSource = "Mobile", Notes = null, Status = "Cancelled", TableID = 11 },
            //    new Reservation { ReservationID = 8, SittingID = 1, GuestName = "James Adams", Email = "james@example.com", Phone = "777-555-8888", StartTime = new DateTime(2023, 10, 16, 10, 0, 0), Duration = 60, NumOfGuests = 2, ReservationSource = "In-person", Notes = null, Status = "Seated", TableID = 4 },
            //    new Reservation { ReservationID = 9, SittingID = 3, GuestName = "Ava Harris", Email = "ava@example.com", Phone = "111-999-3333", StartTime = new DateTime(2023, 10, 18, 21, 0, 0), Duration = 90, NumOfGuests = 2, ReservationSource = "Online", Notes = "Vegetarian menu", Status = "Confirmed", TableID = 7 },
            //    new Reservation { ReservationID = 10, SittingID = 4, GuestName = "Liam Lee", Email = "liam@example.com", Phone = null, StartTime = new DateTime(2023, 10, 19, 14, 0, 0), Duration = 120, NumOfGuests = 6, ReservationSource = "Mobile", Notes = "Large group", Status = "Confirmed", TableID = 1 }
            //);
        }
    }
}
