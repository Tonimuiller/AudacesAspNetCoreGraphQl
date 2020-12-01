using BackendChallenge.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendChallenge.Core.Repository
{
    public interface ICheckTargetRepository
    {
        Task<IEnumerable<CheckTarget>> GetCheckTargetByPeriodAsync(DateTime start, DateTime end);
        Task SaveCheckTargetAsync(CheckTarget checkTarget);
    }
}
