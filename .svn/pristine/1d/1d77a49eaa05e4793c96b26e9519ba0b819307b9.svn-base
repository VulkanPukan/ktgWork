using StrengthTracker2.Core.Repository.Contracts.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Contracts.Account;

namespace StrengthTracker2.Persistence.Repository.Registration
{
    public class RegistrationManager : IRegistrationManager
    {
        private readonly IAccount _accountService;

        public RegistrationManager()
        {
            //Use DI in future;
            _accountService = new Account.Account();
        }

        public void Register(UserDetails userDetails)
        {
            _accountService.AddUser(userDetails);
        }
    }
}
