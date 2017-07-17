using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Outsurance.Assessment.Tests
{
    [TestClass]
    public class UnitTestProgram
    {
        [TestMethod]
        public void LoadData_ShouldPopulateList()
        {
            //Arrange
            Program p = new Program();

            //Act
            p.LoadData();

            //Assert
            Assert.IsNotNull(p.People);
        }

        [TestMethod]
        public void GetFrequencyOfName_ShouldOrderByCountThenName()
        {
            //Arrange
            Program p = new Program();
            p.People = new List<Person>
            {
                new Person { FirtsName = "D" },
                new Person { Surname = "A" },
                new Person { FirtsName = "B" },
                new Person { Surname = "C" },                
                new Person { Surname = "E" },
                new Person { FirtsName = "A" }
            };

            //Act
            var result = p.GetFrequencyOfName();

            //Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("A, 2", result[0]);
            Assert.AreEqual("B, 1", result[1]);
            Assert.AreEqual("C, 1", result[2]);
            Assert.AreEqual("D, 1", result[3]);
            Assert.AreEqual("E, 1", result[4]);
        }

        [TestMethod]
        public void GetAddresses_ShouldSortByStreet()
        {
            //Arrange
            Program p = new Program();
            p.People = new List<Person>
            {
                new Person { Address = new Address("14 BBB") },
                new Person { Address = new Address("10 AAA") },
                new Person { Address = new Address("16 DDD") },
                new Person { Address = new Address("12 FFF") },
                new Person { Address = new Address("1 CCC") }
            };

            //Act
            var result = p.GetAddresses();

            //Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(0, result.IndexOf("10 AAA"));
            Assert.AreEqual(1, result.IndexOf("14 BBB"));
            Assert.AreEqual(2, result.IndexOf("1 CCC"));
            Assert.AreEqual(3, result.IndexOf("16 DDD"));
            Assert.AreEqual(4, result.IndexOf("12 FFF"));
        }

        [TestMethod]
        public void SaveToFiles_ShouldCreateFile()
        {
            //Arrange
            Program p = new Program();

            //Act
            p.SaveToFile(new List<string> { "1", "2" }, "testFile.txt");

            //Assert
            Assert.IsTrue(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "testFile.txt")));

            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "testFile.txt"));
        }
    }
}
