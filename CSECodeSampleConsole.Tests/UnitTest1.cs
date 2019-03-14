using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSECodeSampleConsole;
using System.Diagnostics;

namespace CSECodeSampleConsole.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var peopleRepo = new PeopleRepository();

            var id = peopleRepo.CreateUniqueId();

            Assert.AreNotEqual(0, id);
            Assert.AreNotEqual(1, id);
            Assert.AreNotEqual(2, id);
            Assert.AreNotEqual(3, id);
            Assert.AreNotEqual(4, id);

            Debug.WriteLine(id);
            foreach(var person in peopleRepo.GetAll())
            {
                Debug.WriteLine(person.Id + " " + person.Name);
                
            }
        }
    }
}
