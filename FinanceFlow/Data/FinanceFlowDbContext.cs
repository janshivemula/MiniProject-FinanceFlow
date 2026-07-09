using FinanceFlow.Models;
using FinanceFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Data
{
    public class FinanceFlowDbContext : DbContext
    {
        public FinanceFlowDbContext(DbContextOptions<FinanceFlowDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Department> Departments { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<ExpenseClaim> ExpenseClaims { get; set; }
        public DbSet<DepartmentBudget> DepartmentBudgets { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            // Department Relationships
            
            modelBuilder.Entity<Department>()
                .HasMany(d => d.ExpenseClaims)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Budgets)
                .WithOne(b => b.Department)
                .HasForeignKey(b => b.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);


            // Category Relationship

            modelBuilder.Entity<ExpenseCategory>()
                .HasMany(c => c.ExpenseClaims)
                .WithOne(e => e.ExpenseCategory)
                .HasForeignKey(e => e.ExpenseCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            
            // Unique Budget Rule
            // (Dept + Month + Year)
            
            modelBuilder.Entity<DepartmentBudget>()
                .HasIndex(b => new { b.DepartmentId, b.Month, b.Year })
                .IsUnique();

            
            // Expense Claim Defalut
            
            modelBuilder.Entity<ExpenseClaim>()
                .Property(e => e.Status)
                .HasDefaultValue(ExpenseStatus.Pending);

            // Employee
            modelBuilder.Entity<Employee>()
               .HasOne(e => e.Department)
               .WithMany(d => d.Employees)
               .HasForeignKey(e => e.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExpenseClaim>()
                .HasOne(e => e.ApprovedByEmployee)
                .WithMany()
                .HasForeignKey(e => e.ApprovedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}