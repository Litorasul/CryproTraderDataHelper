using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTraderDataHelperAPI.Models
{
    public class Trade
    {
        [Key]
        public int Id { get; set; } // Should be long 
        [Required(ErrorMessage = "Time is required.")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public double Price { get; set; } // Should be decimal
        [ForeignKey("Symbol")]
        public int SymbolId { get; set; }
        public Symbol Symbol { get; set; }

        [ForeignKey("MinutelyAverage")]
        public int? MinutelyAverageId { get; set; }
        public MinutelyAverage? MinutelyAverage { get; set; }
        [ForeignKey("DailyAverage")]
        public int? DailyAverageId { get; set; }
        public DailyAverage? DailyAverage { get; set; }
        [ForeignKey("WeeklyAverage")]
        public int? WeeklyAverageId { get; set; }
        public WeeklyAverage? WeeklyAverage { get; set; }


    }
}
