using System.Collections.Generic;

namespace _01_LampshadeQuery.Contracts.AdminDashboard
{
    public class DashboardStatsDto
    {
        public int TotalUsers { get; set; }
        public int CollaboratorUsers { get; set; }
        public int NewOrders { get; set; }
        public int TotalSales { get; set; }
        public List<ChartDto> MonthlySalesChart { get; set; }
    }
    public class ChartDto
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
        public string[] BackgroundColor { get; set; }
        public string BorderColor { get; set; }
    }
}