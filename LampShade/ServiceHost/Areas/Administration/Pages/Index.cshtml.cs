using _01_LampshadeQuery.Contracts.AdminDashboard;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        public int TotalUsers { get; set; }
        public int CollaboratorUsers { get; set; }
        public int NewOrders { get; set; }
        public int TotalSales { get; set; }
        public List<ChartDto> BarLineDataSet { get; set; }

        private readonly IAdminDashboardQuery _adminDashboardQuery;

        public IndexModel(IAdminDashboardQuery adminDashboardQuery)
        {
            _adminDashboardQuery = adminDashboardQuery;
        }

        public void OnGet()
        {
            var result = _adminDashboardQuery.GetDashboardStats();
            TotalUsers = result.TotalUsers;
            CollaboratorUsers = result.CollaboratorUsers;
            NewOrders = result.NewOrders;
            TotalSales = result.TotalSales;
            BarLineDataSet = result.MonthlySalesChart;
        }
    }
}
