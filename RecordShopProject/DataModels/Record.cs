using System.ComponentModel.DataAnnotations;

namespace RecordShopProject.DataModels
{
    public class Record
    {
       
        public int RecordId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
