using Moq;
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
        public async Task GetAllRecords_ReturnsAllRecords()
        {
            // Arrange
            var testRecords = new List<Record>
            {
                new Record { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 },
                new Record { RecordId = 2, Title = "Test Album 2", Artist = "Test Artist 2", Genre = "Pop", Year = 2005, Price = 15, Stock = 3 }
            };
            _recordRepositoryMock.Setup(repo => repo.GetAllRecords()).ReturnsAsync(testRecords);
            // Act
            var result = await _recordService.GetAllRecords();
            // Assert
            Assert.That(result, Is.EqualTo(testRecords));
        }

        [Test]
        public async Task GetRecordById_ReturnsCorrectRecord()
        {
            // Arrange
            var testRecord = new Record { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 };
            _recordRepositoryMock.Setup(repo => repo.GetRecordById(1)).ReturnsAsync(testRecord);
            // Act
            var result = await _recordService.GetRecordById(1);
            // Assert
            Assert.That(result, Is.EqualTo(testRecord));
        }

        [Test]
        public async Task AddRecord_ReturnsAddedRecord()
        {
            // Arrange
            var newRecord = new Record { RecordId = 3, Title = "Test Album 3", Artist = "Test Artist 3", Genre = "Jazz", Year = 2010, Price = 20, Stock = 2 };
           
            _recordRepositoryMock.Setup(repo => repo.AddRecord(newRecord)).ReturnsAsync(newRecord);
            // Act
            var result = await _recordService.AddRecord(newRecord);
            // Assert
            Assert.That(result, Is.EqualTo(newRecord));
        }

        [Test]
        public async Task EditRecord_ReturnsEditedRecord()
        {
            // Arrange
            var id = 5;

            var updatedRecord = new Record
            { Title = "Updated Album", Artist = "Updated Artist", Genre = "Rock", Year = 2024, Price = 15, Stock = 12 };

            var returnedRecord = new Record
            { RecordId = 5, Title = "Updated Album", Artist = "Updated Artist", Genre = "Rock", Year = 2024, Price = 15, Stock = 12 };

            _recordRepositoryMock.Setup(repo => repo.EditRecord(id, updatedRecord)).ReturnsAsync(returnedRecord);

            // Act
            var result = await _recordService.EditRecord(id, updatedRecord);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.RecordId, Is.EqualTo(returnedRecord.RecordId));
            Assert.That(result.Title, Is.EqualTo(returnedRecord.Title));

            _recordRepositoryMock.Verify( repo => repo.EditRecord(id, updatedRecord), Times.Once );
        }

        [Test]
        public async Task DeleteRecord_ReturnsTrueWhenDeleted()
        {
            // Arrange
            var id = 1;
            _recordRepositoryMock.Setup(repo => repo.DeleteRecord(id)).ReturnsAsync(true);
            // Act
            var result = await _recordService.DeleteRecord(id);
            // Assert
            Assert.IsTrue(result);
        }
    }
}
