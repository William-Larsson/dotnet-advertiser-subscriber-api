using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace advertising.Models
{
    // Class that represents an advertizer in the DB.
    // Used with EF Core to generate tables and operations. 
    [Table("tbl_advertisers")]
    public class Advertiser
    {
        // General information
        [Key] // Primary key
        [Column("adv_advertiser_id")]
        public long Id { get; set; }

        [Required]
        [Column("adv_phone_number")]
        public string PhoneNumber { get; set; }


        // Address data for distribution
        [Required]
        [Column("adv_distribution_address")]
        public string DistributionAddress { get; set; }

        [Required]
        [Column("adv_distribution_zip_code")]
        public string ZipCode { get; set; }

        [Required]
        [Column("adv_distribution_city")]
        public string City { get; set; }


        // Address data for invoice 
        [Required]
        [Column("adv_invoice_address")]
        public string InvoiceAddress { get; set; }

        [Required]
        [Column("adv_invoice_zip_code")]
        public string InvoiceZipCode { get; set; }

        [Required]
        [Column("adv_invoice_city")]
        public string InvoiceCity { get; set; }


        // Data specific to the advertizer. 
        // Are they a subscriber or an organization etc..
        [Required]
        [Column("adv_is_organization")]
        public bool isOrganization { get; set; } // true if organization

        // If not an organization
        [Column("adv_first_name")]
        public string Firstname { get; set; } 

        [Column("adv_last_name")] 
        public string Lastname { get; set; } 

        [Column("adv_personal_id")] 
        public long? PersonalId { get; set; } 

        [Column("adv_subscriber_id")]
        public long? SubscriberId { get; set; } 

        // If is an organization
        [Column("adv_organization_name")]
        public string OrganizationName { get; set; }

        [Column("adv_organization_number")]
        public long? OrganizationNumber { get; set; } 


        // Database on-to-many relation with Ad. 
        public List<Ad> Ads { get; set; }
    }
}