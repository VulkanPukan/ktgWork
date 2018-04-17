using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DAL = StrengthTracker2.Core.Functional.DBEntities.Actors;

namespace StrengthTracker2.Persistence.Functional.Account
{
    public class Contact
    {
        /// <summary>
        /// Get List of Contacts by its IDs
        /// </summary>
        /// <param name="sqlConnection">Open SQL connection</param>
        /// <param name="userIDs">Comma seperated userIds</param>
        /// <returns>List of Contacts</returns>
        public List<DAL.Contact> ListContactsById(SqlConnection sqlConnection, string userIDs)
        {
            var lstContacts = sqlConnection.Query<DAL.Contact>("Select * from Contact where UserID in (" + userIDs + ")").ToList();
            return lstContacts;
        }
    }
}