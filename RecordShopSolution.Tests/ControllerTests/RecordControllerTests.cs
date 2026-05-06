using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordShopProject.Controller;
using RecordShopProject.DataModels;
using RecordShopProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordShopProject.Tests.ControllerTests
{
    internal class RecordControllerTests
    {
        private Mock<IRecordsService> _recordServiceMock;
        private RecordsController _recordController;

        [SetUp]
        public void Setup()
        {
            _recordServiceMock = new Mock<IRecordsService>();
            _recordController = new RecordsController(_recordServiceMock.Object);
        }


        [Test]
        public void GetAllRecords_ShouldReturnAllRecords()
        {
            // Arrange
            var testRecords = new List<Record>
            {
                new Record { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 },
                new Record { RecordId = 2, Title = "Test Album 2", Artist = "Test Artist 2", Genre = "Pop", Year = 2005, Price = 15, Stock = 3 }
            };

            _recordServiceMock.Setup(repo => repo.GetAllRecords()).Returns(testRecords);


            // Act
            var result = _recordController.GetAllRecords() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);

            var returnedRecords = result.Value as List<Record>;

            Assert.IsNotNull(returnedRecords);
            Assert.AreEqual(2, returnedRecords.Count);
        }

        [Test]
        public void GetRecordById_ShouldReturnCorrectRecord()
        {
            // Arrange
            var testRecord = new Record 
            { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 };
           
            _recordServiceMock.Setup(repo => repo.GetRecordById(1)).Returns(testRecord);
           
            // Act
            var result = _recordController.GetRecordById(1) as OkObjectResult;
           
            // Assert
            Assert.IsNotNull(result);
            var returnedRecord = result.Value as Record;
            Assert.IsNotNull(returnedRecord);
            Assert.AreEqual(testRecord.RecordId, returnedRecord.RecordId);
        }
    }
}
