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
    public class MovementTypeRepository : BL.IMovementTypeRepository
    {
        iDAL.IMovementTypeMgmt _imovementTypeMgmt;
        /// <summary>
        /// Constructor
        /// </summary>
        public MovementTypeRepository()
        {
            _imovementTypeMgmt = new DAL.MovemenTypeMgmt();
        }

        /// <summary>
        /// Lists all movement type
        /// </summary>
        /// <returns>List of movement types else null</returns>
        public List<BLENT.MovementType> ListAllMovementTypes()
        {
            List<BLENT.MovementType> lstMovementType = new List<BLENT.MovementType>();

            List<DALENT.MovementType> lstDALMovmentType = _imovementTypeMgmt.ListAllMovementTypes();

            if (lstDALMovmentType != null && lstDALMovmentType.Count > 0)
            {
                foreach (DALENT.MovementType dalMovementType in lstDALMovmentType)
                {
                    BLENT.MovementType movementType = new BLENT.MovementType();

                    PropertyCopy.Copy(dalMovementType, movementType);

                    lstMovementType.Add(movementType);
                }
            }

            return lstMovementType;
        }
    }
}
