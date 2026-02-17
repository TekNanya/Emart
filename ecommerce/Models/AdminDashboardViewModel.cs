using System.Collections.Generic;

namespace ecommerce.Models
{
    public class AdminDashboardViewModel
    {
        public decimal TotalRevenue { get; set; }
        public int OrderCount { get; set; }
        public int ProductCount { get; set; }
        public int ReviewCount { get; set; }

        // Data for the Table
        public List<Order> LatestOrders { get; set; } = new List<Order>();

        // Data for the Revenue Line Chart
        public List<string> ChartLabels { get; set; } = new List<string>();
        public List<decimal> ChartData { get; set; } = new List<decimal>();

        // Data for the Status Donut Chart
        public int NewCount { get; set; }
        public int ProcessingCount { get; set; }
        public int DeliveredCount { get; set; }
    }
}