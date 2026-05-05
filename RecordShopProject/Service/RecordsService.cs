using RecordShopProject.DataModels;
using RecordShopProject.Repository;

namespace RecordShopProject.Service
{
    public interface IRecordsService
    {
        List<Record> GetAllRecords();
    }
    public class RecordsService : IRecordsService
    {
        private readonly IRecordsRepository _recordRepository;
        public RecordsService(IRecordsRepository repository)
        {
            _recordRepository = repository;
        }
        public List<Record> GetAllRecords()
        {
            return _recordRepository.GetAllRecords();
        }


    }
}
