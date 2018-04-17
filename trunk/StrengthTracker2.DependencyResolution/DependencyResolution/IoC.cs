// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using StructureMap;
using StructureMap.Graph;
using StrengthTracker2.Core.Repository.Contracts.Account;
using StrengthTracker2.Persistence.Repository.Security;
using StrengthTracker2.Core.Repository.Contracts.Security;
using MyProgrammer.Core.Services;
using MyProgrammer.Mail;
using StrengthTracker2.Core.Repository.Contracts.AthleteManagement;
using StrengthTracker2.Persistence.Repository.Customers;
using StrengthTracker2.Core.Repository.Contracts.Customers;
using StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using StrengthTracker2.Core.Repository.Contracts.Registration;
using StrengthTracker2.Persistence.Repository.AthleteManagement;
using StrengthTracker2.Persistence.Repository.ProgramManagement;
using Account = StrengthTracker2.Persistence.Repository.Account.Account;
using StrengthTracker2.Core.Repository.Contracts.TKGMaster;
using StrengthTracker2.Persistence.Repository.TKGMaster;
using StrengthTracker2.Core.Repository.Contracts.Workout;
using StrengthTracker2.Persistence.Repository.Registration;
using StrengthTracker2.Persistence.Repository.Workout;
using StrengthTracker2.Core.Repository.Contracts.Teams;
using StrengthTracker2.Persistence.Repository.Teams;
using StrengthTracker2.Repository.Repository.Account;
using StrengthTracker2.Persistence.Repository.Account;

namespace StrengthTracker2.DependencyResolution.DependencyResolution {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                            x.For<Core.Repository.Contracts.Account.ILogin>().Use<Persistence.Repository.Account.Login>();
                            x.For<IMailService>().Use<MailServiceImpl>();
                            x.For<IAccount>().Use<Account>();
                            x.For<IRoleManagement>().Use<RoleManagement>();
                            x.For<IApplicationServerManagementRepository>().Use<ApplicationServerManagementRepository>();
                            x.For<ICustomerContactManagementRepository>().Use<CustomerContactManagementRepository>();
                            x.For<ICustomerManagementRepository>().Use<CustomerManagementRepository>();
                            x.For<ICustomerPricingManagementRepository>().Use<CustomerPricingManagementRepository>();
                            x.For<IDatabaseServerManagementRepository>().Use<DatabaseServerManagementRepository>();
                            x.For<IAthleteManagementRepository>().Use<AthleteManagementRepository>();
                            x.For<IProgramManagementRepository>().Use<ProgramRepository>();
                            x.For<ISportsManagementRepository>().Use<SportsMgmtRepository>();
                            x.For<IPositionMgmtRepository>().Use<PositionMgmtRepository>();
                            x.For<IMovementTypeRepository>().Use<MovementTypeRepository>();
                            x.For<ICustomerContactManagementRepository>().Use<CustomerContactManagementRepository>();
                            x.For<ICustomerPricingManagementRepository>().Use<CustomerPricingManagementRepository>();
                            x.For<ICustomerLocationMgmtRepository>().Use<CustomerLocationMgmtRepository>();
                            x.For<ILocationContactManagementRepository>().Use<LocationContactManagementRepository>();
                            x.For<ILocationPricingManagementRepository>().Use<LocationPricingManagementRepository>();
                            x.For<IState>().Use<StrengthTracker2.Persistence.Repository.Account.State>();
                            // Added for user role mapping by srinivas
                            x.For<ICustomerLocationRoleMgmtRepository>().Use<StrengthTracker2.Persistence.Repository.Customers.CustomerLocationRoleManagement>();
                            x.For<StrengthTracker2.Core.Repository.Contracts.TKGMaster.ICustomerMasterMgmtRepository>().Use<StrengthTracker2.Persistence.Repository.TKGMaster.CustomerMasterMgmtRepository>();
                            x.For<IWorkoutManagement>().Use<WorkoutManagementRepository>();
	                        x.For<IRegistrationManager>().Use<RegistrationManager>();
	                        x.For<IAthleteCalculationService>().Use<AthleteCalculationService>();
                            x.For<IExerciseManagement>().Use<ExerciseRepository>();
                            x.For<ITeamManagmentRepository>().Use<TeamManagementRepository>();
                            x.For<IUOMManagementRepository>().Use<UOMManagementRepository>();
                            x.For<IUserDataVisibilityRepository>().Use<UserDataVisibilityRepository>();
                            x.For<IAthleteFilterRepository>().Use<AthleteFilterRepository>();
                            x.For<IUserHistoryRepository>().Use<UserHistoryRepository>();
                            x.For<ICustomerCategoryTypeFilter>().Use<CustomerCategoryTypeFilter>();
                        });
            return ObjectFactory.Container;
        }
    }
}