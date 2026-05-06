using Microsoft.EntityFrameworkCore;
using Moq;
using RecordShopProject.DataModels;
using RecordShopProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


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

            var listOfRecords = new RecordShopDBContext(TestDb);

            listOfRecords.Records.AddRange(
                new Record 
                { 
                    RecordId = 1, 
                    Title = "Test Album 1", 
                    Artist = "Test Artist 1", 
                    Genre = "Rock", 
                    Year = 2000, 
                    Price = 10, 
                    Stock = 5 
                },
                new Record 
                { 
                    RecordId = 2, 
                    Title = "Test Album 2", 
                    Artist = "Test Artist 2", 
                    Genre = "Pop", 
                    Year = 2005, 
                    Price = 15, 
                    Stock = 3 
                }
            );
            listOfRecords.SaveChanges();

            var repository = new RecordsRepository(listOfRecords);

            // Act
            var records = repository.GetAllRecords();

            // Assert
            Assert.AreEqual(2, records.Count);
        }

        [Test]
        public void GetRecordById_ShouldReturnCorrectRecord()
        {
            //Arrange
            var TestDb = new DbContextOptionsBuilder<RecordShopDBContext>()
                .UseInMemoryDatabase("Test2Db")
                .Options;

            var listOfRecords = new RecordShopDBContext(TestDb);

            listOfRecords.Records.AddRange(
                new Record 
                {   
                    RecordId = 1, 
                    Title = "Test Album 1", 
                    Artist = "Test Artist 1", 
                    Genre = "Rock", 
                    Year = 2000, 
                    Price = 10, 
                    Stock = 5 
                },
                new Record 
                {   
                    RecordId = 2, 
                    Title = "Test Album 2", 
                    Artist = "Test Artist 2", 
                    Genre = "Pop", Year = 2005, 
                    Price = 15, 
                    Stock = 3 
                }
            );
            listOfRecords.SaveChanges();

            var repository = new RecordsRepository(listOfRecords);

            // Act
            var record = repository.GetRecordById(1);

            // Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(1, record.RecordId);

        }

        [Test]
        public void AddRecord_ShouldAddRecordToDatabase()
        {
            // Arrange
            var TestDb = new DbContextOptionsBuilder<RecordShopDBContext>()
                .UseInMemoryDatabase("Test3Db")
                .Options;

            var context = new RecordShopDBContext(TestDb);
            var repository = new RecordsRepository(context);

            var newRecord = new Record
            {
                Title = "Test Album 1",
                Artist = "Test Artist 1",
                Genre = "Jazz",
                Year = 2010,
                Price = 20,
                Stock = 2
            };

            // Act
            var createdRecord = repository.AddRecord(newRecord);

            // Assert
            Assert.IsNotNull(createdRecord);
            Assert.That(createdRecord.RecordId, Is.EqualTo(1));

            var recordFromDb = context.Records.FirstOrDefault(repo => repo.RecordId == createdRecord.RecordId);
            Assert.IsNotNull(recordFromDb);
            Assert.AreEqual("Test Album 1", recordFromDb.Title);
        }
    }
}
