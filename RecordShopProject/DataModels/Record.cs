using System.ComponentModel.DataAnnotations;

namespace RecordShopProject.DataModels
{
    public class Record
    {
       
        public int RecordId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}
