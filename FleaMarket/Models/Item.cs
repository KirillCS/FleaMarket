using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleaMarket.Models
{
    public enum PriceType
    {
        Free,
        Contract,
        Definite
    }

    public class Item
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(4096)]
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool TradeEnabled { get; set; }

        [NotMapped]
        public PriceType PriceType =>
            Price switch
            {
                0 => PriceType.Free,
                null => PriceType.Contract,
                _ => PriceType.Definite
            };
    }
}
