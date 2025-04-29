namespace EmployeeDashboard.Models;

public class Employee
{

    public int EmployeeID { get; set; }
    public string? Name { get; set; }
    public string? Department {  get; set; }
    public decimal Salary {  get; set; }
    public DateTime JoiningDate { get; set; }
    public Employee() { }

    public Employee(int employeeID, string name, string department, decimal salary, DateTime joiningDate)
    {
        EmployeeID = employeeID;
        Name = name;
        Department = department;
        Salary = salary;
        JoiningDate = joiningDate;

    }

}
