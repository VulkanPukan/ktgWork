using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLENT = StrengthTracker2.Core.Repository.Entities.Actors;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.Actors;
using IBL = StrengthTracker2.Core.Repository.Contracts.Account;
using IDAL = StrengthTracker2.Core.Functional.Contracts.Account;
using DAL = StrengthTracker2.Persistence.Functional.Account;
using StrengthTracker2.Persistence.Mapping;
using StrengthTracker2.Common;
    
namespace StrengthTracker2.Persistence.Repository.Account
{
    public class Login : IBL.ILogin
    {
        IDAL.ILogin _iLogin;
        /// <summary>
        /// Constrcutor
        /// </summary>
        public Login()
        {
            _iLogin = new DAL.Login();
        }

        public BLENT.User Authenticate(BLENT.User user)
        {
            BLENT.User authenticatedUser = new BLENT.User();
            DALENT.User dalUser = new DALENT.User();

            user.Password = SecurityUtility.EncryptText(user.Password);

            //PropertyCopy.Copy(user, dalUser);
            dalUser.UserName = user.UserName;
            dalUser.Password = user.Password;

            dalUser = _iLogin.Authenticate(dalUser);

            if (dalUser != null)
            {
                PropertyCopy.Copy(dalUser, authenticatedUser);
            }
            else
                authenticatedUser = null;

            return authenticatedUser;
        }
    }
}
