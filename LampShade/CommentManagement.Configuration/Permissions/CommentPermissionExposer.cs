using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace CommentManagement.Configuration.Permissions
{
    public class CommentPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {

                     "Comment",new List<PermissionDto>
                     {
                         new PermissionDto(CommentPermissions.ListComments,"ListComments"),
                         new PermissionDto(CommentPermissions.ConfirmComment,"ConfirmComment"),
                         new PermissionDto(CommentPermissions.CancelComment,"CancelComment")
                     }
                }
            };
        }
    }
}