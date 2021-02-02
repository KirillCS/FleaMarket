using FleaMarket.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleaMarket.Models
{
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

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime PublishingDate { get; set; }

        [Required]
        public string UserId { get; set; }

        public int? CoverId { get; set; }


        public User User { get; set; }

        public Image Cover { get; set; }

        public List<Image> Images { get; set; }

        public List<Category> Categories { get; set; }


        [NotMapped]
        public PriceType PriceType =>
            Price switch
            {
                0 => PriceType.Free,
                null => PriceType.Contract,
                _ => PriceType.Definite
            };

        [NotMapped]
        public bool HasCover => CoverId != null;
    }
}
