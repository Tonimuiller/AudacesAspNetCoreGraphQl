using BackendChallenge.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendChallenge.Core.Services
{
    public interface ICheckTargetService
    {
        Task<IEnumerable<int>> CheckAsync(CheckTarget checkTarget, bool skipLog = false);
    }
}