using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Persistence.Mapping;
using ENT = StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using iBL = StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using iDAL = StrengthTracker2.Core.Functional.Contracts.ProgramManagement;
using DAL = StrengthTracker2.Persistence.Functional.ProgramManagement;

namespace StrengthTracker2.Persistence.Repository.ProgramManagement
{
    public class UOMManagementRepository : iBL.IUOMManagementRepository
    {
        private readonly iDAL.IUOMManagement _uomMgmt = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public UOMManagementRepository()
        {
            _uomMgmt = new DAL.UOMManagement();
        }

        /// <summary>
        /// Gets all the UOM in the system
        /// </summary>
        /// <returns>returns list of UOM else null</returns>
        public List<ENT.UOM> GetAllUOM()
        {
            List<ENT.UOM> lstResult = null;
            ENT.UOM uom = null;

            List<DALENT.UOM> lstUOM = _uomMgmt.GetAllUOM();

            if (lstUOM != null && lstUOM.Count > 0)
            {
                lstResult = new List<ENT.UOM>();

                foreach (DALENT.UOM u in lstUOM)
                {
                    uom = new ENT.UOM();

                    PropertyCopy.Copy(u, uom);

                    lstResult.Add(uom);
                }
            }

            return lstResult;
        }

    }
}
