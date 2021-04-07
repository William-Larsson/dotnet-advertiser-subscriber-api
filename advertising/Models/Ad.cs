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
        public long Id { get; set; }

        public int Price { get; set; } // Varans pris

        public string Content { get; set; } // Innehåll

        public string Headline { get; set; } // Rubrik

        public int AdCost { get; set; } // Annonspris
    }
}