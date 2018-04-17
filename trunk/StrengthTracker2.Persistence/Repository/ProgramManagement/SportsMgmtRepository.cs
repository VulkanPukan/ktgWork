using System.Collections.Generic;
using System.Linq;
using BLENT = StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using BL = StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using iDAL = StrengthTracker2.Core.Functional.Contracts.ProgramManagement;
using DAL = StrengthTracker2.Persistence.Functional.ProgramManagement;


namespace StrengthTracker2.Persistence.Repository.ProgramManagement
{
    public class SportsMgmtRepository : BL.ISportsManagementRepository
    {
        private readonly iDAL.ISportsManagement sportMenagement;

        public SportsMgmtRepository()
        {
            sportMenagement = new DAL.Sport();
        }

        /// <summary>
        /// List all sports in the system. Returns all or only active based on isActive flag
        /// </summary>
        /// <param name="isActive">Only active sports needed</param>
        /// <returns>List of Sports else null</returns>
        public List<BLENT.Sport> ListSports(bool isActive)
        {
            return sportMenagement.ListSports(true).Select(s=>Convert(s)).ToList();
        }

	    public int SaveOrGetSportId(BLENT.Sport sport)
	    {
		    var savedSport = sportMenagement.GetByName(sport.Name);
		    if (savedSport == null)
		    {
			    var castedSport = new DALENT.Sport()
			    {
				    Name = sport.Name,
				    isActive = sport.isActive
			    };
			    return sportMenagement.Add(castedSport);
		    }
		    return savedSport.Id;
	    }

        public BLENT.Sport GetSportByName(string sportName)
        {
            return Convert(sportMenagement.GetByName(sportName));
        }

        public BLENT.Sport GetSportById(int sportId)
        {
            return Convert(sportMenagement.GetById(sportId));
        }

        public BLENT.Sport Convert(DALENT.Sport sport)
        {
            return new BLENT.Sport
            {
                Id = sport.Id,
                isActive = sport.isActive,
                Name = sport.Name,
                Order = sport.Order
            };
        }

        public DALENT.Sport Convert(BLENT.Sport sport)
        {
            return new DALENT.Sport
            {
                Id = sport.Id,
                isActive = sport.isActive,
                Name = sport.Name,
                Order = sport.Order
            };
        }
    }
}
