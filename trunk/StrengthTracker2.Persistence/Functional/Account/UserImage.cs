using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DAL = StrengthTracker2.Core.Functional.DBEntities.Actors;

namespace StrengthTracker2.Persistence.Functional.Account
{
    public class UserImage
    {
        /// <summary>
        /// Gets List of UserImage from Userids
        /// </summary>
        /// <param name="sqlConnection">Open sql connection</param>
        /// <param name="userIds">comma seperated user ids</param>
        /// <returns></returns>
        public List<DAL.UserImage> ListUserImagesById(SqlConnection sqlConnection, string userIds)
        {
            var lstUserImages =
                sqlConnection.Query<DAL.UserImage>("Select * from UserImage where UserID in (" + userIds + ")").ToList();
            return lstUserImages;
        }
    }
}