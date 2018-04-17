using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.DBEntities.Security
{
    public class RolePermission
    {
        /// <summary>
        /// Roles Permission ID Primary Key
        /// </summary>
        public int RolePermissionID { get; set; }
        /// <summary>
        /// Role ID - foreign key Role.RoleID
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// Permission ID - foreign key Permission.PermissionID
        /// </summary>
        public int PermissionID { get; set; }
        /// <summary>
        /// Can View Rights
        /// </summary>
        public Nullable<bool> CanView { get; set; }
        /// <summary>
        /// Can Add Rights
        /// </summary>
        public Nullable<bool> CanAdd { get; set; }
        /// <summary>
        /// Can Update Rights
        /// </summary>
        public Nullable<bool> CanEdit { get; set; }
        /// <summary>
        /// Can Delete Rights
        /// </summary>
        public Nullable<bool> CanDelete { get; set; }
        /// <summary>
        /// Created By - userid who created the permission
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Created Date - Record Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Updated By - user id of the user updating the record
        /// </summary>
        public int UpdatedBy { get; set; }
        /// <summary>
        /// Update Date - Record Updated Date
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
