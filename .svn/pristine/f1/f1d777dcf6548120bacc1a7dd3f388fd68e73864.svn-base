using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using StrengthTracker2.DependencyResolution.DependencyResolution;
using StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;

namespace StrengthTracker2.Test
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ListPrograms()
        {
            IoC.Initialize();

            IProgramManagementRepository _iProgramMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
            var list = _iProgramMgmtRepository.ListPrograms(false);

            Assert.AreNotEqual(list, null);
        }

        [TestMethod]
        public void GetProgramByID()
        {
            IoC.Initialize();

            IProgramManagementRepository _iProgramMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
            Program prog = _iProgramMgmtRepository.GetProgrambyID(1);

            Assert.AreNotEqual(prog, null);
        }
    }
}
