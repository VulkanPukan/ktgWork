using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using StrengthTracker2.DependencyResolution.DependencyResolution;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Contracts.Account;

namespace StrengthTracker2.Test
{
    [TestClass]
    public class Authentication
    {
        [TestMethod]
        public void Login()
        {
            IoC.Initialize();

            ILogin _iLogin = ObjectFactory.GetInstance<ILogin>();

            User loginUser = new User();
            loginUser.UserName = "admin";
            loginUser.Password = "Password123";

            User authenticatedUser = _iLogin.Authenticate(loginUser);

            Assert.AreNotEqual(null, authenticatedUser);
        }

        [TestMethod]
        public void IncorrectLogin()
        {
            IoC.Initialize();

            ILogin _iLogin = ObjectFactory.GetInstance<ILogin>();

            User loginUser = new User();
            loginUser.UserName = "admin";
            loginUser.Password = "Password@123";

            User authenticatedUser = _iLogin.Authenticate(loginUser);

            Assert.AreEqual(null, authenticatedUser);
        }
    }
}
