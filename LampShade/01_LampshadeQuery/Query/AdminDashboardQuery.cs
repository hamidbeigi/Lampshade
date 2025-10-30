using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.AdminDashboard;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class AdminDashboardQuery : IAdminDashboardQuery
    {
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;

        public AdminDashboardQuery(ShopContext shopContext, AccountContext accountContext)
        {
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public DashboardStatsDto GetDashboardStats()
        {
            return new DashboardStatsDto
            {
                TotalUsers = _accountContext.Accounts.Count(),
                CollaboratorUsers = _accountContext.Accounts.Count(x => x.RoleId == 8),
                NewOrders = _shopContext.Orders.Count(x => !x.IsPaid && !x.IsCanceled),
                TotalSales = _shopContext.Orders.Count(x => x.IsPaid),
                MonthlySalesChart = GetMonthlySalesChart()
            };
        }

        private List<ChartDto> GetMonthlySalesChart()
        {
            try
            {
                var pc = new PersianCalendar();
                int year = pc.GetYear(DateTime.Now);

                var monthlySales = Enumerable.Range(1, 12)
                    .Select(month =>
                    {
                        var (start, end) = Tools.GetPersianMonthRange(year, month);
                        return _shopContext.Orders
                            .AsNoTracking() // تغییر ایمن
                            .Where(o => o.IsPaid && o.CreationDate >= start && o.CreationDate <= end) // توجه به <=
                            .Sum(o => (int?)o.TotalAmount) ?? 0;
                    })
                    .ToList();

                return new List<ChartDto>
        {
            new ChartDto
            {
                //Label = "فروش کل",
                Data = monthlySales,
                //BackgroundColor = Enumerable.Repeat("#0077b6", 12).ToArray(),
                //BorderColor = "#023e8a"
            }
        };
            }
            catch
            {
                // نسخه قبلی به عنوان Fallback
                var monthlySales = new List<int>();
                var pc = new PersianCalendar();
                /* ... کد قبلی دقیقاً اینجا ... */
                return new List<ChartDto> { /* ... */ };
            }
        }
    }
}