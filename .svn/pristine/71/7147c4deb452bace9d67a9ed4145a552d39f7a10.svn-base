using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Persistence.Mapping;
using BLENT = StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using BL = StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using iDAL = StrengthTracker2.Core.Functional.Contracts.ProgramManagement;
using DAL = StrengthTracker2.Persistence.Functional.ProgramManagement;

namespace StrengthTracker2.Persistence.Repository.ProgramManagement
{
    public class PositionMgmtRepository : BL.IPositionMgmtRepository
    {
        private readonly iDAL.IPositionMgmt _iPositionMgmt;

        public PositionMgmtRepository()
        {
            _iPositionMgmt = new DAL.PositionMgmt();
        }

        /// <summary>
        /// List all sports in the system. Returns all or only active based on isActive flag
        /// </summary>
        /// <param name="sportID"></param>
        /// <returns>List of Sports else null</returns>
        public List<BLENT.Position> ListPositionBySportID(int sportID)
        {
            List<BLENT.Position> lstPosition = new List<BLENT.Position>();

            List<DALENT.Position> lstDALPosition = _iPositionMgmt.ListPositionBySportID(sportID);

            if (lstDALPosition.Any())
            {
                foreach (DALENT.Position dalPosition in lstDALPosition)
                {
                    BLENT.Position position = new BLENT.Position();

                    PropertyCopy.Copy(dalPosition, position);

                    lstPosition.Add(position);
                }
            }

            return lstPosition;
        }

        public List<BLENT.Position> ListPositions()
        {
            List<BLENT.Position> lstPosition = new List<BLENT.Position>();

            List<DALENT.Position> lstDALPosition = _iPositionMgmt.ListPositions();

            if (lstDALPosition.Any())
            {
                foreach (DALENT.Position dalPosition in lstDALPosition)
                {
                    BLENT.Position position = new BLENT.Position();

                    PropertyCopy.Copy(dalPosition, position);

                    lstPosition.Add(position);
                }
            }

            return lstPosition;
        }
    }
}
