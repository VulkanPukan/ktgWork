using System.Linq;
using StrengthTracker2.Common;
using BLENT = StrengthTracker2.Core.Repository.Entities.Actors;
using IBL = StrengthTracker2.Core.Repository.Contracts.Account;
using IDAL = StrengthTracker2.Core.Functional.Contracts.Account;
using DAL = StrengthTracker2.Persistence.Functional.Account;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.Actors;
using StrengthTracker2.Persistence.Mapping;
using System.Collections.Generic;
using System;

namespace StrengthTracker2.Persistence.Repository.Account
{
    public class State : IBL.IState
    {
          private readonly IDAL.IState _IState;

          public State()
          {
              _IState = new DAL.State();
          }

        /// <summary>
        /// List all sports in the system. Returns all or only active based on isActive flag
        /// </summary>
        /// <param name="isActive">Only active sports needed</param>
        /// <returns>List of Sports else null</returns>
        public List<BLENT.State> ListStates()
        {
            List<BLENT.State> lstStates = new List<BLENT.State>();

            List<DALENT.State> lstDALStates = _IState.ListStates();

            if (lstDALStates != null && lstDALStates.Count > 0)
            {
                foreach (DALENT.State dalState in lstDALStates)
                {
                    BLENT.State state = new BLENT.State();

                    PropertyCopy.Copy(dalState, state);

                    lstStates.Add(state);
                }
            }

            return lstStates;
        }
    }

}
