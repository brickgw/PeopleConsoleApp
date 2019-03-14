using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSECodeSampleConsole.Tests
{
    [TestClass]
    public class UniqueIdTests
    {
        private readonly List<int> _sequentialIds;
        private readonly List<int> _nonSequentialIds;
        private readonly List<int> _offsetIds;

        public UniqueIdTests()
        {
            _sequentialIds = new List<int>()
            {
                1,2,3,4,5
            };

            _nonSequentialIds = new List<int>()
            {
                1,4,5,6,19,3,25
            };

            _offsetIds = new List<int>()
            {
                2,3,4,5
            };
        }

        [TestMethod]
        public void GenerateIdCreatesUniqueIdAgainstSeqentialList()
        {
            var id = UniqueId.GenerateId(_sequentialIds);

            Assert.IsFalse(_sequentialIds.Contains(id));
        }

        [TestMethod]
        public void GenerateIdCreatesUniqueIdAgainstNonSeqentialList()
        {
            var id = UniqueId.GenerateId(_nonSequentialIds);

            Assert.IsFalse(_nonSequentialIds.Contains(id));
        }

        [TestMethod]
        public void GenerateIdCreatesUniqueIdOf2ForANonSequentialList()
        {
            // The non sequential list of integers has a minimum missing value of 2
            // Validates that GenerateId will create that missing value as an id
            var id = UniqueId.GenerateId(_nonSequentialIds);
            Assert.AreEqual(2, id);
        }

        [TestMethod]
        public void GenerateIdCreatesIdsForEachMissingIdInNonSequentialList()
        {
            var idRange = _nonSequentialIds.Max();

            while (_nonSequentialIds.Count != idRange)
            {
                var generatedId = UniqueId.GenerateId(_nonSequentialIds);
                _nonSequentialIds.Add(generatedId);
            }

            for (int i = 0; i < _nonSequentialIds.Count; i++)
            {
                Assert.IsTrue(_nonSequentialIds.Contains(i + 1));
            }
        }

        [TestMethod]
        public void GenerateIdCreatesNextSequentialId()
        {
            var id = UniqueId.GenerateId(_sequentialIds);
            Assert.AreEqual(6, id);
        }

        [TestMethod]
        public void GenerateIdCreatesMinimumMissingIdForOffsetList()
        {
            var id = UniqueId.GenerateId(_offsetIds);
            Assert.AreEqual(1, id);
        }
    }
}
