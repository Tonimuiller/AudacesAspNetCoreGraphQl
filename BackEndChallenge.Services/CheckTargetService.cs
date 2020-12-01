using BackendChallenge.Core.Model;
using BackendChallenge.Core.Repository;
using BackendChallenge.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendChallenge.Services
{
    public class CheckTargetService : ICheckTargetService
    {
        private readonly ICheckTargetRepository checkTargetRepository;

        public CheckTargetService(ICheckTargetRepository checkTargetRepository)
        {
            this.checkTargetRepository = checkTargetRepository;
        }

        public async Task<IEnumerable<int>> CheckAsync(CheckTarget checkTarget, bool skipLog = false)
        {
            if (checkTarget == null)
            {
                throw new ArgumentNullException(nameof(checkTarget));
            }

            if (!skipLog)
            {
                await this.checkTargetRepository.SaveCheckTargetAsync(checkTarget);
            }            

            if (checkTarget.Sequence == null
                || !checkTarget.Sequence.Any()
                || checkTarget.Target == 0)
            {
                return new List<int>();
            }

            var quotients = new List<int>();
            var sequence = checkTarget.Sequence.OrderByDescending(it => it);
            foreach (var item in sequence)
            {
                quotients.Add(checkTarget.Target / item);
            }

            for (var current = 0; current < sequence.Count(); current++)
            {
                if (quotients.ElementAt(current) <= 0)
                {
                    continue;
                }

                var target = checkTarget.Target;
                while (quotients.ElementAt(current) > 0)
                {
                    var currentValue = sequence.ElementAt(current) * quotients.ElementAt(current);
                    target -= currentValue;
                    if (target == 0)
                    {
                        return Enumerable
                            .Range(0, quotients.ElementAt(current))
                            .Select(el => sequence.ElementAt(current));
                    }

                    var newSquence = new List<int>();
                    for (var i = current + 1; i < sequence.Count(); i++)
                    {
                        newSquence.Add(sequence.ElementAt(i));
                    }

                    var sequenceToCheck = (await CheckAsync(new CheckTarget { Target = target, Sequence = newSquence }, true)).ToList();
                    sequenceToCheck.AddRange(Enumerable
                            .Range(0, quotients.ElementAt(current))
                            .Select(el => sequence.ElementAt(current)));

                    if (sequenceToCheck.Sum() == checkTarget.Target)
                    {
                        return sequenceToCheck.OrderBy(it => it);
                    }

                    quotients[current]--;
                }
            }

            return new List<int>();
        }
    }
}
