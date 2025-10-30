using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace AccountManagement.Configuration.Permissions
{
    public class AccountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {

                     "Account",new List<PermissionDto>
                     {
                         new PermissionDto(AccountPermissions.ListAccounts,"ListAccounts"),
                         new PermissionDto(AccountPermissions.CreateAccount,"CreateAccount"),
                         new PermissionDto(AccountPermissions.EditAccount,"EditCustomerDiscount"),
                         new PermissionDto(AccountPermissions.ChangePasswordAccount,"ChangePasswordAccount")
                     }
                },
                {
                     "Role",new List<PermissionDto>
                     {
                         new PermissionDto(AccountPermissions.ListRoles,"ListRoles"),
                         new PermissionDto(AccountPermissions.CreateRole,"CreateRole"),
                         new PermissionDto(AccountPermissions.EditRole,"EditRole")
                     }
                }
            };
        }
    }
}