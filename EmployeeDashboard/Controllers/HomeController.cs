using EmployeeDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
namespace EmployeeDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeDbContext _context;

        public HomeController(EmployeeDbContext context)
        {
            _context = context;
        }

        // Index action with pagination
        public IActionResult Index(string search, string filterDept, string sortColumn, string sortOrder, int page = 1)
        {
            // Change page size from 1 to 4 employees per page
            int pageSize = 4;
            var employees = _context.Employees.AsQueryable();

            // Search Filter
            if (!string.IsNullOrEmpty(search))
            {
                employees = employees.Where(e => e.Name!.Contains(search));
                ViewBag.Search = search;
            }

            // Department Filter
            if (!string.IsNullOrEmpty(filterDept))
            {
                employees = employees.Where(e => e.Department == filterDept);
                ViewBag.FilterDept = filterDept;
            }

            // Sorting - Keep default sort but no forced filters
            if (string.IsNullOrEmpty(sortColumn))
            {
                sortColumn = "Name"; // default sort by Name
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc"; // default to ascending order
            }

            // Sort logic remains the same
            if (sortOrder == "asc")
            {
                employees = sortColumn switch
                {
                    "Salary" => employees.OrderBy(e => e.Salary),
                    "JoiningDate" => employees.OrderBy(e => e.JoiningDate),
                    _ => employees.OrderBy(e => e.Name),
                };
            }
            else
            {
                employees = sortColumn switch
                {
                    "Salary" => employees.OrderByDescending(e => e.Salary),
                    "JoiningDate" => employees.OrderByDescending(e => e.JoiningDate),
                    _ => employees.OrderByDescending(e => e.Name),
                };
            }

            // Store sort information for view
            ViewBag.SortColumn = sortColumn;
            ViewBag.SortOrder = sortOrder;

            // Get the total count BEFORE pagination
            int totalEmployees = employees.Count();

            // Calculate total pages
            int totalPages = (int)Math.Ceiling(totalEmployees / (double)pageSize);

            // Ensure page is within valid range
            if (page < 1) page = 1;
            if (page > totalPages && totalPages > 0) page = totalPages;

            // Get the data for the current page
            var paginatedEmployees = employees.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Pass pagination info to the view
            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            ViewBag.TotalEmployees = totalEmployees;

            // Get departments for filtering
            ViewBag.Departments = _context.Employees.Select(e => e.Department).Distinct().ToList();

            return View(paginatedEmployees);
        }
    }
}