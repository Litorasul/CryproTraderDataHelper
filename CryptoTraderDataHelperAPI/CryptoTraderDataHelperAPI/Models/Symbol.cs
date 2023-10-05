using System.ComponentModel.DataAnnotations;

namespace CryptoTraderDataHelperAPI.Models;

public class Symbol
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Symbol Name is required.")]
    public string Name { get; set; }
}
