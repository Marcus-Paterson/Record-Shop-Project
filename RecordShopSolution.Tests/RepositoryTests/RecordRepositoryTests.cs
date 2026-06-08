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
            Assert.That(records.Count, Is.EqualTo(2));
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
                    Genre = "Pop",
                    Year = 2005,
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
            Assert.That(record.RecordId, Is.EqualTo(1));

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
            Assert.That(recordFromDb.Title, Is.EqualTo("Test Album 1"));
        }

        [Test]
        public void EditRecord_ShouldUpdateExistingRecord()
        {
            // Arrange
            var TestDb = new DbContextOptionsBuilder<RecordShopDBContext>()
                .UseInMemoryDatabase("Test4Db")
                .Options;
            var context = new RecordShopDBContext(TestDb);
            var repository = new RecordsRepository(context);
            var existingRecord = new Record
            {
                Title = "Test Album 1",
                Artist = "Test Artist 1",
                Genre = "Rock",
                Year = 2000,
                Price = 10,
                Stock = 5
            };
            context.Records.Add(existingRecord);
            context.SaveChanges();
            var updatedRecord = new Record
            {
                Title = "Updated Album",
                Artist = "Updated Artist",
                Genre = "Pop",
                Year = 2020,
                Price = 25,
                Stock = 10
            };
            // Act
            var result = repository.EditRecord(existingRecord.RecordId, updatedRecord);
            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.RecordId, Is.EqualTo(existingRecord.RecordId));
            Assert.That(result.Title, Is.EqualTo("Updated Album"));

            var recordFromDb = context.Records.Find(existingRecord.RecordId);
            Assert.That(recordFromDb.Title, Is.EqualTo("Updated Album"));
        }

        [Test]
        public void DeleteRecord_ShouldRemoveRecordFromDatabase()
        {
            // Arrange
            var TestDb = new DbContextOptionsBuilder<RecordShopDBContext>()
                .UseInMemoryDatabase("Test5Db")
                .Options;
            var context = new RecordShopDBContext(TestDb);
            var repository = new RecordsRepository(context);
            var recordToDelete = new Record
            {
                Title = "Test Album 1",
                Artist = "Test Artist 1",
                Genre = "Rock",
                Year = 2000,
                Price = 10,
                Stock = 5
            };
            context.Records.Add(recordToDelete);
            context.SaveChanges();
            // Act
            var result = repository.DeleteRecord(recordToDelete.RecordId);
            // Assert
            Assert.IsTrue(result);
            var recordFromDb = context.Records.Find(recordToDelete.RecordId);
            Assert.IsNull(recordFromDb);
        }
    }
}
