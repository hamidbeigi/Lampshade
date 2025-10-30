using _01_LampshadeQuery.Contracts.AdminDashboard;
using _01_LampshadeQuery.Query;
using Microsoft.Extensions.DependencyInjection;

namespace _01_LampshadeQuery.QueryConfiguration
{
    public class QueryBootstrapper
    {

        public static void Configure(IServiceCollection services)
        {
            //services.AddTransient<IProductQuery, ProductQuery>();
            //services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            //services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IAdminDashboardQuery, AdminDashboardQuery>();
        }

    }
}
