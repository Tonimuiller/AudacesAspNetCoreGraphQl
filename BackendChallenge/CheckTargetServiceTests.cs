using BackendChallenge.Core.Model;
using BackendChallenge.Core.Repository;
using BackendChallenge.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace BackendChallenge.Tests
{
    public class CheckTargetServiceTests
    {
        private CheckTargetService checkTargetService;

        [SetUp]
        public void Setup()
        {
            var moqRepository = new Moq.Mock<ICheckTargetRepository>();
            this.checkTargetService = new CheckTargetService(moqRepository.Object);
        }

        [Test]
        public void Check_ValidSequenceAndTarget_GetNonEmptySequence()
        {
            var checkTarget = new CheckTarget
            {
                Target = 47,
                Sequence = new List<int> { 5, 20, 2, 1 }
            };

            var sequence = checkTargetService.CheckAsync(checkTarget).Result;
            Assert.IsNotEmpty(sequence);
        }

        [Test]
        public void Check_InvalidSequenceAndTarget_GetEmptySequence()
        {
            var checkTarget = new CheckTarget
            {
                Target = 101,
                Sequence = new List<int> { 3, 15, 10, 20 }
            };

            var sequence = checkTargetService.CheckAsync(checkTarget).Result;
            Assert.IsEmpty(sequence);
        }

        [Test]        
        public void Check_ZeroTargetNonEmptySequence_GetEmptySequence()
        {
            var checkTarget = new CheckTarget
            {
                Target = 0,
                Sequence = new List<int> { 1, 10, 3, 7 }
            };

            var sequence = checkTargetService.CheckAsync(checkTarget).Result;
            Assert.IsEmpty(sequence);
        }

        [Test]
        public void Check_EmptySequence_GetEmptySequence()
        {
            var checkTarget = new CheckTarget
            {
                Target = 63,
                Sequence = new List<int>()
            };

            var sequence = checkTargetService.CheckAsync(checkTarget).Result;
            Assert.IsEmpty(sequence);
        }
    }
}