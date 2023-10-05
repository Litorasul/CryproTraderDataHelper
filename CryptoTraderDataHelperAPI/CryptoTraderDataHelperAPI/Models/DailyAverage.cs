using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTraderDataHelperAPI.Models;


public class DailyAverage
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Price is required.")]
    public double Price { get; set; }
    public DateOnly Time { get; set; }
    [ForeignKey("Symbol")]
    public int SymbolId { get; set; }
    public Symbol Symbol { get; set; }
}
