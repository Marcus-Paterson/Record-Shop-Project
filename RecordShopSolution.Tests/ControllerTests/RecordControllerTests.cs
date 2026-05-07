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

        [Test]
        public void GetRecordById_ShouldReturnNotFoundForInvalidId()
        {
            // Arrange
            _recordServiceMock.Setup(repo => repo.GetRecordById(999)).Returns((Record)null);
            // Act
            var result = _recordController.GetRecordById(999);
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void AddRecord_ShouldReturnCreatedRecord()
        {
            // Arrange
            var newRecord = new Record { RecordId = 3, Title = "Test Album 3", Artist = "Test Artist 3", Genre = "Jazz", Year = 2010, Price = 20, Stock = 2 };
            _recordServiceMock.Setup(repo => repo.AddRecord(newRecord)).Returns(newRecord);
            // Act
            var result = _recordController.AddRecord(newRecord) as CreatedAtActionResult;
            // Assert
            Assert.IsNotNull(result);
            var createdRecord = result.Value as Record;
            Assert.IsNotNull(createdRecord);
            Assert.AreEqual(3, createdRecord.RecordId);
        }

        [Test]
        public void AddRecord_ShouldReturnBadRequestForNullRecord()
        {
            // Act
            var result = _recordController.AddRecord(null);
            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void EditRecord_ShouldReturnEditedRecord()
        {
            // Arrange
            var id = 1;

            var updatedRecord = new Record
            {   Title = "Updated Album",
                Artist = "Updated Artist",
                Genre = "Rock",
                Year = 2000,
                Price = 10,
                Stock = 5
            };

            var returnedRecord = new Record
            {   RecordId = id,
                Title = "Updated Album",
                Artist = "Updated Artist",
                Genre = "Rock",
                Year = 2000,
                Price = 10,
                Stock = 5
            };

            _recordServiceMock.Setup(service => service.EditRecord(id, updatedRecord)).Returns(returnedRecord);

            // Act
            var result = _recordController.EditRecord(id, updatedRecord) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);

            var editedRecord = result.Value as Record;
            Assert.IsNotNull(editedRecord);

            Assert.AreEqual("Updated Album", editedRecord.Title);
            Assert.AreEqual(id, editedRecord.RecordId);
        }

        [Test]
        public void EditRecord_ShouldReturnNotFoundForInvalidId()
        {
            // Arrange
            var id = 999;
            var updatedRecord = new Record
            {
                Title = "Updated Album",
                Artist = "Updated Artist",
                Genre = "Rock",
                Year = 2000,
                Price = 10,
                Stock = 5
            };
            _recordServiceMock.Setup(service => service.EditRecord(id, updatedRecord)).Returns((Record)null);
            // Act
            var result = _recordController.EditRecord(id, updatedRecord);
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
         [Test]
         public void DeleteRecord_ShouldReturnOkForSuccessfulDeletion()
         {
             // Arrange
             var id = 1;
             _recordServiceMock.Setup(service => service.DeleteRecord(id)).Returns(true);
             // Act
             var result = _recordController.DeleteRecord(id) as NoContentResult;
             // Assert
             Assert.IsNotNull(result);
        }
        [Test]
        public void DeleteRecord_ShouldReturnNotFound_WhenRecordDoesNotExist()
        {
            // Arrange
            var id = 1;
            _recordServiceMock.Setup(service => service.DeleteRecord(id)).Returns(false);

            // Act
            var result = _recordController.DeleteRecord(id) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
    
}
