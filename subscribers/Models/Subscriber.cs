using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscribers.Models
{
    // Class that represents a subscriber entity in the DB. 
    // Used with EF Core to generate tables and operations.   
    [Table("tbl_subscribers")]   
    public class Subscriber
    {
        [Key] // Primary key
        [Column("sub_subscriber_id")]
        public long SubscriberId { get; set; } // Prenumerationsnummer

        [Required]
        [Column("sub_personal_id")]
        public long PersonalId { get; set; } // personnummer

        [Required]
        [Column("sub_first_name")]
        public string Firstname { get; set; } // förnamn

        [Required]
        [Column("sub_last_name")]
        public string Lastname { get; set; } // efternamn

        [Required]
        [Column("sub_distribution_address")]
        public string DistributionAddress { get; set; } // utdelningsadress

        [Required]
        [Column("sub_zip_code")]
        public string ZipCode { get; set; } // postnummer

        [Required]
        [Column("sub_city")]
        public string City { get; set; } // ort

        [Required]
        [Column("sub_phone_number")]
        public string PhoneNumber { get; set; } // telefonnummer
    }
}