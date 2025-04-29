using EmployeeDashboard.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseInMemoryDatabase("EmployeeDb"));

var app = builder.Build();

// Seed database with sample data
SeedDatabase(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//  data insertion
void SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

    // Clear existing data to ensure we have exactly the records we want
    if (context.Employees.Any())
    {
        context.Employees.RemoveRange(context.Employees);
        context.SaveChanges();
    }

    // Add more employee records to ensure we have enough for pagination
    context.Employees.AddRange(
        new Employee
        {
            Name = "John Smith",
            Department = "Engineering",
            Salary = 75000,
            JoiningDate = new DateTime(2022, 3, 15)
        },
        new Employee
        {
            Name = "Sarah Johnson",
            Department = "Marketing",
            Salary = 68000,
            JoiningDate = new DateTime(2021, 8, 10)
        },
        new Employee
        {
            Name = "Michael Brown",
            Department = "Finance",
            Salary = 82000,
            JoiningDate = new DateTime(2020, 5, 22)
        },
        new Employee
        {
            Name = "Emily Davis",
            Department = "Human Resources",
            Salary = 65000,
            JoiningDate = new DateTime(2023, 1, 8)
        },
        new Employee
        {
            Name = "Robert Wilson",
            Department = "Engineering",
            Salary = 78000,
            JoiningDate = new DateTime(2021, 11, 30)
        },
        new Employee
        {
            Name = "Jessica White",
            Department = "Marketing",
            Salary = 71000,
            JoiningDate = new DateTime(2022, 9, 14)
        },
        new Employee
        {
            Name = "David Miller",
            Department = "Finance",
            Salary = 85000,
            JoiningDate = new DateTime(2019, 7, 23)
        },
        new Employee
        {
            Name = "Jennifer Taylor",
            Department = "Human Resources",
            Salary = 69000,
            JoiningDate = new DateTime(2022, 6, 17)
        },
        new Employee
        {
            Name = "James Anderson",
            Department = "Engineering",
            Salary = 80000,
            JoiningDate = new DateTime(2020, 10, 5)
        },
        new Employee
        {
            Name = "Ashley Thomas",
            Department = "Marketing",
            Salary = 67000,
            JoiningDate = new DateTime(2021, 4, 12)
        },
        new Employee
        {
            Name = "Daniel Garcia",
            Department = "Finance",
            Salary = 83000,
            JoiningDate = new DateTime(2018, 12, 3)
        },
        new Employee
        {
            Name = "Amanda Martinez",
            Department = "Human Resources",
            Salary = 66000,
            JoiningDate = new DateTime(2022, 8, 29)
        }
    );

    // Save the data to the database
    context.SaveChanges();
}