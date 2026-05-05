using Microsoft.EntityFrameworkCore;
using Moq;
using RecordShopProject.DataModels;
using RecordShopProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopProject.Tests
{
    public class RecordRepositoryTests
    {

        [Test]
        public void GetAllRecords_ShouldReturnListOfRecords()
        {
            //Arrange
            var TestDb = new DbContextOptionsBuilder<RecordShopDBContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using var listOfRecords = new RecordShopDBContext(TestDb);

            listOfRecords.Records.AddRange(
                new Record { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 },
                new Record { RecordId = 2, Title = "Test Album 2", Artist = "Test Artist 2", Genre = "Pop", Year = 2005, Price = 15, Stock = 3 }
            );
            listOfRecords.SaveChanges();

            var repository = new RecordsRepository(listOfRecords);

            // Act
            var records = repository.GetAllRecords();

            // Assert
            Assert.AreEqual(2, records.Count);
        }
    }
}
