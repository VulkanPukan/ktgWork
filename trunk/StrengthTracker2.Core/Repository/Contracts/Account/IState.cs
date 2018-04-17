using System;
using System.Collections.Generic;
using StrengthTracker2.Core.Repository.Entities.Actors;

namespace StrengthTracker2.Core.Repository.Contracts.Account
{
    public interface IState
    {
        List<State> ListStates();
    }
}
