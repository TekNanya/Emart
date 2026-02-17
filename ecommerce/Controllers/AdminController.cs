using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var allOrders = await _context.Orders.ToListAsync();

        var model = new AdminDashboardViewModel
        {
            ProductCount = await _context.Products.CountAsync(),
            ReviewCount = await _context.Reviews.CountAsync(),
            OrderCount = allOrders.Count,
            TotalRevenue = allOrders.Sum(o => o.TotalAmount),
            LatestOrders = allOrders.OrderByDescending(o => o.OrderDate).Take(5).ToList(),

            // Counts for the Donut Chart
            NewCount = allOrders.Count(o => o.Status == "New"),
            ProcessingCount = allOrders.Count(o => o.Status == "Processing" || o.Status == "Shipped"),
            DeliveredCount = allOrders.Count(o => o.Status == "Completed")
        };

        // Generate Revenue Chart Data for the last 7 days
        var last7Days = Enumerable.Range(0, 7)
            .Select(i => DateTime.Now.Date.AddDays(-i))
            .OrderBy(d => d).ToList();

        foreach (var day in last7Days)
        {
            model.ChartLabels.Add(day.ToString("dd.MM"));
            var dailyRevenue = allOrders
                .Where(o => o.OrderDate.Date == day)
                .Sum(o => o.TotalAmount);
            model.ChartData.Add(dailyRevenue);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int orderId, string newStatus)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            order.Status = newStatus;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}