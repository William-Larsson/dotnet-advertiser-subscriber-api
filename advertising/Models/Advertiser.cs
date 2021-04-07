using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace advertising.Models
{
    [Table("tbl_advertisers")]
    public class Advertiser
    {

        // TODO: continue to fill this in!!

        // General information

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }


        // Address data for delivery

        public string DistributionAddress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }


        // Address data for invoice 

        public string InvoiceDistributionAddress { get; set; }
        public string InvoiceZipCode { get; set; }
        public string InvoiceCity { get; set; }

        // Data specific to the advertizer. 
        // Are they a subscriber or an organization etc..

        public string Lastname { get; set; }

        public bool isOrganization { get; set; }

        public long? OrganizationNumber { get; set; }

    }
}