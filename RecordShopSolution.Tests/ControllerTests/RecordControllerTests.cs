using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordShopProject.Controller;
using RecordShopProject.DataModels;
using RecordShopProject.Service;

namespace RecordShopProject.Tests.ControllerTests
{
    public class RecordControllerTests
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
        public async Task GetAllRecords_ShouldReturnAllRecords()
        {
            // Arrange
            var testRecords = new List<Record>
            {
                new Record { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 },
                new Record { RecordId = 2, Title = "Test Album 2", Artist = "Test Artist 2", Genre = "Pop", Year = 2005, Price = 15, Stock = 3 }
            };

            _recordServiceMock.Setup(repo => repo.GetAllRecords()).ReturnsAsync(testRecords);


            // Act
            var result = await _recordController.GetAllRecords() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);

            var returnedRecords = result.Value as List<Record>;

            Assert.That(returnedRecords, Is.Not.Null);
            Assert.That(returnedRecords.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetRecordById_ShouldReturnCorrectRecord()
        {
            // Arrange
            var testRecord = new Record { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 };

            _recordServiceMock.Setup(repo => repo.GetRecordById(1)).ReturnsAsync(testRecord);

            // Act
            var result = await _recordController.GetRecordById(1) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            var returnedRecord = result.Value as Record;

            Assert.That(returnedRecord, Is.Not.Null);
            Assert.That(returnedRecord.RecordId, Is.EqualTo(testRecord.RecordId));
        }

        [Test]
        public async Task GetRecordById_ShouldReturnNotFoundForInvalidId()
        {
            // Arrange
            _recordServiceMock.Setup(repo => repo.GetRecordById(999)).ReturnsAsync((Record)null);
            // Act
            var result = await _recordController.GetRecordById(999);
            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task GetRecordById_ShouldReturnBadRequest_ForNonPositiveId()
        {
            // Act
            var result = await _recordController.GetRecordById(0);

            // Assert
            Assert.That((result as BadRequestObjectResult).Value, Is.EqualTo("Invalid record ID"));
        }


        [Test]
        public async Task AddRecord_ShouldReturnCreatedRecord()
        {
            // Arrange
            var newRecord = new Record { RecordId = 3, Title = "Test Album 3", Artist = "Test Artist 3", Genre = "Jazz", Year = 2010, Price = 20, Stock = 2 };
            _recordServiceMock.Setup(repo => repo.AddRecord(newRecord)).ReturnsAsync(newRecord);
            // Act
            var result = await _recordController.AddRecord(newRecord) as CreatedAtActionResult;
            // Assert
            Assert.That(result, Is.Not.Null);

            var createdRecord = result.Value as Record;

            Assert.That(createdRecord, Is.Not.Null);
            Assert.That(createdRecord.RecordId, Is.EqualTo(3));
        }

        [Test]
        public async Task AddRecord_ShouldReturnBadRequestForNullRecord()
        {
            // Act
            var result = await _recordController.AddRecord(null);
            // Assert
            Assert.That((result as BadRequestObjectResult).Value, Is.EqualTo("Record cannot be null"));
        }

        [Test]
        public async Task EditRecord_ShouldReturnEditedRecord()
        {
            // Arrange
            var id = 1;

            var updatedRecord = new Record
            { Title = "Updated Album", Artist = "Updated Artist", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 };

            var returnedRecord = new Record
            { RecordId = id, Title = "Updated Album", Artist = "Updated Artist", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 };

            _recordServiceMock.Setup(service => service.EditRecord(id, updatedRecord)).ReturnsAsync(returnedRecord);

            // Act
            var result = await _recordController.EditRecord(id, updatedRecord) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);

            var editedRecord = result.Value as Record;
            Assert.That(editedRecord, Is.Not.Null);

            Assert.That(editedRecord.Title, Is.EqualTo("Updated Album"));
            Assert.That(editedRecord.RecordId, Is.EqualTo(id));
        }

        [Test]
        public async Task EditRecord_ShouldReturnNotFoundForInvalidId()
        {
            // Arrange
            var id = 999;
            var updatedRecord = new Record
            { Title = "Updated Album", Artist = "Updated Artist", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 };
            _recordServiceMock.Setup(service => service.EditRecord(id, updatedRecord)).ReturnsAsync((Record)null);
            
            // Act
            var result = await _recordController.EditRecord(id, updatedRecord);
            
            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteRecord_ShouldReturnOkForSuccessfulDeletion()
        {
            // Arrange
            var id = 1;
            _recordServiceMock.Setup(service => service.DeleteRecord(id)).ReturnsAsync(true);
            // Act
            var result = await _recordController.DeleteRecord(id) as NoContentResult;
            // Assert
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public async Task DeleteRecord_ShouldReturnNotFound_WhenRecordDoesNotExist()
        {
            // Arrange
            var id = 1;
            _recordServiceMock.Setup(service => service.DeleteRecord(id)).ReturnsAsync(false);

            // Act
            var result = await _recordController.DeleteRecord(id) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }
    }

}
