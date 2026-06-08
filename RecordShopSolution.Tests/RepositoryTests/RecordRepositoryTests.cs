using Microsoft.EntityFrameworkCore;
using RecordShopProject.DataModels;
using RecordShopProject.Repository;


namespace RecordShopProject.Tests
{
    public class RecordRepositoryTests
    {

        [Test]
        public async Task GetAllRecords_ShouldReturnListOfRecords()
        {
            //Arrange
            var TestDb = new DbContextOptionsBuilder<RecordShopDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new RecordShopDBContext(TestDb);

            context.Records.AddRange(
                new Record { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 },
                new Record { RecordId = 2, Title = "Test Album 2", Artist = "Test Artist 2", Genre = "Pop", Year = 2005, Price = 15, Stock = 3 }
            );
            await context.SaveChangesAsync();

            var repository = new RecordsRepository(context);

            // Act
            var records = await repository.GetAllRecords();

            // Assert
            Assert.That(records.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetRecordById_ShouldReturnCorrectRecord()
        {
            //Arrange
            var TestDb = new DbContextOptionsBuilder<RecordShopDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new RecordShopDBContext(TestDb);

            context.Records.AddRange(
                new Record { RecordId = 1, Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 },
                new Record { RecordId = 2, Title = "Test Album 2", Artist = "Test Artist 2", Genre = "Pop", Year = 2005, Price = 15, Stock = 3 }
            );
            await context.SaveChangesAsync();

            var repository = new RecordsRepository(context);

            // Act
            var record = await repository.GetRecordById(1);

            // Assert
            Assert.That(record, Is.Not.Null);
            Assert.That(record.RecordId, Is.EqualTo(1));

        }

        [Test]
        public async Task AddRecord_ShouldAddRecordToDatabase()
        {
            // Arrange
            var TestDb = new DbContextOptionsBuilder<RecordShopDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new RecordShopDBContext(TestDb); 
            var repository = new RecordsRepository(context);

            var newRecord = new Record { Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Jazz", Year = 2010, Price = 20, Stock = 2 };

            // Act
            var createdRecord = await repository.AddRecord(newRecord);

            // Assert
            Assert.That(createdRecord, Is.Not.Null);
            Assert.That(createdRecord.RecordId, Is.EqualTo(1));

            var recordFromDb = await context.Records.FirstOrDefaultAsync(repo => repo.RecordId == createdRecord.RecordId);
            Assert.That(recordFromDb, Is.Not.Null);
            Assert.That(recordFromDb.Title, Is.EqualTo("Test Album 1"));
        }

        [Test]
        public async Task EditRecord_ShouldUpdateExistingRecord()
        {
            // Arrange
            var TestDb = new DbContextOptionsBuilder<RecordShopDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new RecordShopDBContext(TestDb);
            var repository = new RecordsRepository(context);

            var existingRecord = new Record { Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 };
            
            context.Records.Add(existingRecord);
            await context.SaveChangesAsync();
            
            var updatedRecord = new Record
            { Title = "Updated Album", Artist = "Updated Artist", Genre = "Pop", Year = 2020, Price = 25, Stock = 10 };

            // Act
            var result = await repository.EditRecord(existingRecord.RecordId, updatedRecord);
            
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RecordId, Is.EqualTo(existingRecord.RecordId));
            Assert.That(result.Title, Is.EqualTo("Updated Album"));

            var recordFromDb = await context.Records.FindAsync(existingRecord.RecordId);
            Assert.That(recordFromDb.Title, Is.EqualTo("Updated Album"));
        }

        [Test]
        public async Task DeleteRecord_ShouldRemoveRecordFromDatabase()
        {
            // Arrange
            var TestDb = new DbContextOptionsBuilder<RecordShopDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new RecordShopDBContext(TestDb);
            var repository = new RecordsRepository(context);

            var recordToDelete = new Record { Title = "Test Album 1", Artist = "Test Artist 1", Genre = "Rock", Year = 2000, Price = 10, Stock = 5 };

            context.Records.Add(recordToDelete);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.DeleteRecord(recordToDelete.RecordId);
            
            // Assert
            Assert.That(result, Is.True);
            var recordFromDb = await context.Records.FindAsync(recordToDelete.RecordId);
            Assert.That(recordFromDb, Is.Null);
        }
    }
}
