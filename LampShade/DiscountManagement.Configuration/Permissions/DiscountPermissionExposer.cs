using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace DiscountManagement.Configuration.Permissions
{
    public class DiscountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {

                     "CustomerDiscount",new List<PermissionDto>
                     {
                         new PermissionDto(DiscountPermissions.ListCustomerDiscounts,"ListCustomerDiscounts"),
                         new PermissionDto(DiscountPermissions.CreateCustomerDiscount,"CreateCustomerDiscount"),
                         new PermissionDto(DiscountPermissions.EditCustomerDiscount,"EditCustomerDiscount")
                     }
                },
                {
                     "ColleagueDiscount",new List<PermissionDto>
                     {
                         new PermissionDto(DiscountPermissions.ListColleagueDiscounts,"ListColleagueDiscounts"),
                         new PermissionDto(DiscountPermissions.CreateColleagueDiscount,"CreateColleagueDiscount"),
                         new PermissionDto(DiscountPermissions.EditColleagueDiscount,"EditColleagueDiscount"),
                         new PermissionDto(DiscountPermissions.RemoveColleagueDiscount,"RemoveColleagueDiscount"),
                         new PermissionDto(DiscountPermissions.RestoreColleagueDiscount,"RestoreColleagueDiscount")

                     }
                }
            };
        }
    }
}