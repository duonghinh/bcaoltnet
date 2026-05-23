using Microsoft.EntityFrameworkCore;
using System;
using WarehouseManagementSystem.Core;
using WarehouseManagementSystem.Core.Security;
using WarehouseManagementSystem.Data.Models;


namespace WarehouseManagementSystem.Data.SeedingData;


public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var adminHash = PasswordHasher.Hash("admin123");
        var staffHash = PasswordHasher.Hash("staff123");
        var viewerHash = PasswordHasher.Hash("viewer123");

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = AppRoles.Admin, Description = "Toàn quyền hệ thống" },
            new Role { Id = 2, Name = AppRoles.Staff, Description = "Nhập xuất và quản lý hàng" },
            new Role { Id = 3, Name = AppRoles.Viewer, Description = "Xem dashboard và báo cáo" }
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", PasswordHash = adminHash, DisplayName = "Quản trị viên", RoleId = 1, IsActive = true, CreatedAt = new DateTime(2024, 1, 1) },
            new User { Id = 2, Username = "thukho", PasswordHash = staffHash, DisplayName = "Nhân viên kho", RoleId = 2, IsActive = true, CreatedAt = new DateTime(2024, 1, 1) },
            new User { Id = 3, Username = "xem", PasswordHash = viewerHash, DisplayName = "Người xem báo cáo", RoleId = 3, IsActive = true, CreatedAt = new DateTime(2024, 1, 1) }
        );

        modelBuilder.Entity<Warehouse>().HasData(
            new Warehouse { Id = 1, Name = "Main Warehouse", Address = "123 Street, City", Manager = "John Doe" }
        );

        modelBuilder.Entity<Item>().HasData(
            new Item { Id = 1, Code = "ITM001", Name = "Laptop", MeasurementUnit = "Piece" }
        );

        modelBuilder.Entity<Supplier>().HasData(
            new Supplier { Id = 1, Name = "TechSupplier Inc.", Phone = "123-456", Fax = "123-457", Mobile = "123456789", Email = "contact@techsupplier.com", Website = "www.techsupplier.com" }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, Name = "John Electronics", Phone = "987-654", Email = "john@electronics.com" }
        );

        modelBuilder.Entity<StockItem>().HasData(
            new StockItem { WarehouseId = 1, ItemId = 1, Quantity = 50 }
        );

        modelBuilder.Entity<SupplyOrder>().HasData(
            new SupplyOrder { Id = 1, WarehouseId = 1, SupplierId = 1, OrderNumber = "SO001", OrderDate = new DateTime(2024, 3, 6) }
        );

        modelBuilder.Entity<SupplyOrderDetail>().HasData(
            new SupplyOrderDetail { SupplyOrderId = 1, ItemId = 1, Quantity = 10, ProductionDate = new DateTime(2024, 2, 6), ExpirationDate = new DateTime(2026, 2, 6) }
        );

        modelBuilder.Entity<WithdrawalOrder>().HasData(
            new WithdrawalOrder { Id = 1, WarehouseId = 1, CustomerId = 1, OrderNumber = "WO001", OrderDate = new DateTime(2024, 3, 6) }
        );

        modelBuilder.Entity<WithdrawalOrderDetail>().HasData(
            new WithdrawalOrderDetail { WithdrawalOrderId = 1, ItemId = 1, Quantity = 5 }
        );
    }
}

