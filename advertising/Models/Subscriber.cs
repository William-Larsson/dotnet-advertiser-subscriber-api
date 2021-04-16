namespace advertising.Models
{
    // Class that represents a subscriber from the API. 
    // Used to make API Post/Put request
    public class Subscriber
    {
        public long SubscriberId { get; set; } // Prenumerationsnummer
        public long PersonalId { get; set; } // personnummer
        public string Firstname { get; set; } // förnamn
        public string Lastname { get; set; } // efternamn
        public string DistributionAddress { get; set; } // utdelningsadress
        public string ZipCode { get; set; } // postnummer
        public string City { get; set; } // ort
        public string PhoneNumber { get; set; } // telefonnummer
    }
}