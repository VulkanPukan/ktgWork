using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.Security
{
    public class Permission
    {
        /// <summary>
        /// Permission ID Primary Key
        /// </summary>
        public int PermissionID { get; set; }
        /// <summary>
        /// PermissionName
        /// </summary>
        public string PermissionName { get; set; }
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

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
