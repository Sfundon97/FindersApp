using Google.Cloud.Firestore;

namespace Finders.Models
{
    [FirestoreData]
    public class ServiceProvider
    {
        [FirestoreProperty("address")]
        public string Address { get; set; }

        [FirestoreProperty("category")]
        public string Category { get; set; }

        [FirestoreProperty("companyName")]
        public string CompanyName { get; set; }

       public string  Rating { get; set; }
        public string Photo { get; set; }

        [FirestoreProperty("dateJoined")]
        public DateTime DateJoined { get; set; }

        [FirestoreProperty("email")]
        public string Email { get; set; }

        [FirestoreProperty("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [FirestoreProperty("service")]
        public string Service { get; set; }
    }
}
