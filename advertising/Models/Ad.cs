using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace advertising.Models
{

    // Class that represents an advertisement entity in the DB. 
    // Used with EF Core to generate tables and operations.   
    [Table("tbl_ads")]
    public class Ad
    {   
        [Key]
        [Column("ad_ad_id")]
        public long Id { get; set; }

        [Required]
        [Column("ad_price")]
        public int Price { get; set; } // Varans pris

        [Required]
        [Column("ad_content")]
        public string Content { get; set; } // Innehåll

        [Required]
        [Column("ad_headline")]
        public string Headline { get; set; } // Rubrik

        [Required]
        [Column("ad_ad_cost")]
        public int AdCost { get; set; } // Annonspris


        // Database many-to-one relation with Advertiser.
        [Required]
        [ForeignKey("ad_fk_advertiser_id")]
        public long AdvertiserId { get; set; } // Foreign Key

        public Advertiser Advertiser { get; set; }
    }
}