using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordShopProject.Repository;
using RecordShopProject.Service;
using RecordShopProject.DataModels;

namespace RecordShopProject.Tests.ServiceTests
{
    public class RecordServiceTests
    {
        private Mock<IRecordsRepository> _recordRepositoryMock;
        private RecordsService _recordService;

        [SetUp]
        public void Setup()
        {
            _recordRepositoryMock = new Mock<IRecordsRepository>();
            _recordService = new RecordsService(_recordRepositoryMock.Object);
        }
        [Test]
        public void GetAllRecords_ReturnsAllRecords()
        {
            // Arrange
            var testRecords = new List<Record>
            {
                new Record { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 },
                new Record { RecordId = 2, Title = "Test Album 2", Artist = "Test Artist 2", Genre = "Pop", Year = 2005, Price = 15, Stock = 3 }
            };
            _recordRepositoryMock.Setup(repo => repo.GetAllRecords()).Returns(testRecords);
            // Act
            var result = _recordService.GetAllRecords();
            // Assert
            Assert.That(result, Is.EqualTo(testRecords));
        }

        [Test]
        public void GetRecordById_ReturnsCorrectRecord()
        {
            // Arrange
            var testRecord = new Record { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 };
            _recordRepositoryMock.Setup(repo => repo.GetRecordById(1)).Returns(testRecord);
            // Act
            var result = _recordService.GetRecordById(1);
            // Assert
            Assert.That(result, Is.EqualTo(testRecord));
        }

        [Test]
        public void AddRecord_ReturnsAddedRecord()
        {
            // Arrange
            var newRecord = new Record { RecordId = 3, Title = "Test Album 3", Artist = "Test Artist 3", Genre = "Jazz", Year = 2010, Price = 20, Stock = 2 };
            _recordRepositoryMock.Setup(repo => repo.AddRecord(newRecord)).Returns(newRecord);
            // Act
            var result = _recordService.AddRecord(newRecord);
            // Assert
            Assert.That(result, Is.EqualTo(newRecord));
        }
    }
}
