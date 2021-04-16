using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace advertising.Models
{
    // A view model class that contains everything needed to 
    // create an Ad and an Advertiser. 
    // Contains the attributes need to create 
    public class CreateAdViewModel
    {
        // Attributes for an Ad
        [Required]
        [Display(Name = "Rubrik", Prompt = "Ange en rubrik för annonsen")]
        public string Headline { get; set; }

        [Required]
        [Display(Name = "Beskrivning", Prompt = "Beskriv annonsens innehåll")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Pris", Prompt = "Ange annonsens pris")]
        public int Price { get; set; }

        // TODO: Do I need AdvertiserId etc?


        // Attributes for an Advertiser
        [Required]
        [Display(Name = "Telefonnummer", Prompt = "Ange telefonnummer")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Utdelningsadress", Prompt = "Ange utdelningsadress")]
        public string DistributionAddress { get; set; }

        [Required]
        [Display(Name = "Postnummer", Prompt = "Ange postnummer")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Stad", Prompt = "Ange stad")]
        public string City { get; set; }

        [Display(Name = "Faktureringsadress", Prompt = "Ange adress för faktura")]
        public string InvoiceAddress { get; set; }

        [Display(Name = "Faktureringspostnummer", Prompt = "Ange postnummer för faktura")]
        public string InvoiceZipCode { get; set; }

        [Display(Name = "Faktureringsstad", Prompt = "Ange stad för faktura")]
        public string InvoiceCity { get; set; }



        // Data specific to the advertizer. 
        // Are they a subscriber or an organization etc..
        [Required]
        public bool isOrganization { get; set; }

        // Must be validated manually!
        [Display(Name = "Förnamn", Prompt = "Ange förnamn")]
        public string Firstname { get; set; }

        // Must be validated manually!
        [Display(Name = "Efternamn", Prompt = "Ange efternamn")]
        public string Lastname { get; set; }

        // Must be validated manually!
        [Display(Name = "Personnummer", Prompt = "Ange personnummer")]
        public long PersonalId { get; set; }

        // Must be validated manually!
        [Display(Name = "Företagsnamn", Prompt = "Ange företagsnamn")]
        public string OrganizationName { get; set; }

        // Must be validated manually!
        [Display(Name = "Prenumerationsnummer", Prompt = "Ange prenumerationsnummer")]
        public long? SubscriberId { get; set; }

        // Must be validated manually!
        [Display(Name = "Organisationsnummer", Prompt = "Ange organisationsnummer")]
        public long? OrganizationNumber { get; set; }


        // Used to check if the user wants to change the subscriber 
        // data for in the subscriber API
        [Display(Name = "Uppdatera information för prenumeration", Prompt = "Uppdatera information")]
        public bool updateSubscriberAPIInfo { get; set; }
    }
}