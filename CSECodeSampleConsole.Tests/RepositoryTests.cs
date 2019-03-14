using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSECodeSampleConsole.Tests
{
    [TestClass]
    public class RepositoryTests
    {

        private IRepository<Person> _repo;

        /// <summary>
        /// These tests are written with respect to the default in-memory collection of Person objects initialized
        /// in the constructor of the InPersonRepository class. Changing initialization values may effect test results.
        /// </summary>
        public RepositoryTests()
        {
            _repo = new InMemoryPeopleRepository();
        }

        [TestMethod]
        public void TryFindReturnsFalse()
        {
            Assert.IsFalse(_repo.TryFind("Someone", out List<Person> matches));
        }

        [TestMethod]
        public void TryFindReturnsTrue()
        {
            var firstPerson = _repo.GetAll().First();
            Assert.IsTrue(_repo.TryFind(firstPerson.Name, out List<Person> matches));
        }

        [TestMethod]
        public void TryFindReturnsListWithSingleMatch()
        {
            var firstPerson = _repo.GetAll().First();
            
            _repo.TryFind(firstPerson.Name, out List<Person> matches);

            Assert.AreEqual(1, matches.Count);
        }

        [TestMethod]
        public void TryFindReturnsListWithTwoMatches()
        {
            // Default repository collection Contains "John Smith" and "Jane Doe"
            // Searching 'J' should yeild 2 results as both Names contain J

            _repo.TryFind("J", out List<Person> matches);
            Assert.AreEqual(2, matches.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TryFindTakesEmptyStringThrowsException()
        {
            _repo.TryFind(String.Empty, out List<Person> matches);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TryFindAgainstNullCollectionThrowsException()
        {
            InMemoryPeopleRepository nullRepo = null;
            nullRepo.TryFind("Some Person", out List<Person> matches);
        }

        [TestMethod]
        public void GetAllReturnsDefaultListOfFourPeople()
        {
            Assert.AreEqual(4, _repo.GetAll().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetAllThrowsExceptionForNullRepo()
        {
            InMemoryPeopleRepository nullRepo = null;
            var results = nullRepo.GetAll();
        }

        [TestMethod]
        public void CreateAddsNewPersonIncreasesCollectionCountByOne()
        {
            var initalCollectionCount = _repo.GetAll().Count;

            _repo.Create("Test Person");

            Assert.AreEqual(initalCollectionCount + 1, _repo.GetAll().Count);
        }
        [TestMethod]
        public void CreateAddsNewPersonAndIsFoundInCollection()
        {
            
            _repo.Create("Test Person");

            Assert.IsTrue(_repo.TryFind("Test Person", out List<Person> matches));
            Assert.AreEqual("Test Person", matches.First().Name);
        }
    }
}
