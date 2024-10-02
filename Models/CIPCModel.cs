using Google.Cloud.Firestore;

namespace Finders.Models
{
    [FirestoreData]
    public class CIPCModel
    {
              
            [FirestoreProperty("address")]
            public string Address { get; set; }

            [FirestoreProperty("companyName")]
            public string CompanyName { get; set; }

            public string Rating { get; set; }

            [FirestoreProperty("photo")]
            public string Photo { get; set; }
            public string PhotoUrl { get; set; }
           

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
