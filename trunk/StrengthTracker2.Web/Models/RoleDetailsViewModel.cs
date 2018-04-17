using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class RoleDetailsViewModel
    {
        public RoleDetailsViewModel()
        {
            RolePermissions = new List<RolePermissionsViewModel>();
            Role= new RoleViewModel();
        }

        public RoleViewModel Role { get; set; }
        public List<RolePermissionsViewModel> RolePermissions { get; set; }
    }

    public class RoleViewModel
    {
            /// <summary>
            /// Role ID Primary Key
            /// </summary>
            public int RoleID { get; set; }
            /// <summary>
            /// Role Name - 'Administrator',SuperAdmin'
            /// </summary>
            public string RoleName { get; set; }

            /// <summary>
            /// RoleDescription - Description for role
            /// </summary>
            public string RoleDescription { get; set; }
            /// <summary>
            /// RoleType - 1 for System , 2 for Custom
            /// </summary>
            public int RoleType { get; set; }
            /// <summary>
            /// Is Role Active = 1 or Inactive - 0
            /// </summary>
            public bool IsActive { get; set; }
            /// <summary>
            /// Is Role Deleted - 1 else 0
            /// </summary>
            public bool IsDeleted { get; set; }
            /// <summary>
            /// Created by - logged in userid mainly Adminstrator
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

    public class RolePermissionsViewModel
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
        public string PermissionName{ get; set; }
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

        public string DisableAdd{ get; set; }
        public string DisableEdit { get; set; }
        public string DisableDelete { get; set; }
        public string DisableView { get; set; }

        public int RowID { get; set; }
    }
}