using FinanceFlow.Data;
using FinanceFlow.Repositories;
using FinanceFlow.Repositories;
using FinanceFlow.Services;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<FinanceFlowDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FinFlowConnection")));

            // Add services to the container.

           // builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
           // builder.Services.AddOpenApi();

            // Repos

            builder.Services.AddScoped<IExpenseClaimRepository, ExpenseClaimRepository>();
            builder.Services.AddScoped<IDepartmentBudgetRepository, DepartmentBudgetRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            



            //Services

            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<IBudgetService, BudgetService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ILoginService, LoginService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.MapOpenApi();
            //}

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAngular");

            app.MapControllers();

            app.Run();

        }
    }
}