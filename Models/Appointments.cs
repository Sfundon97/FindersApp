using Google.Cloud.Firestore;

namespace Finders.Models
{
    [FirestoreData]
    public class Appointments
    {
        public string ReferenceNumber { get; set; }
        [FirestoreProperty("date")]
        public DateTime Date { get; set; }
        [FirestoreProperty("prices")]
        public List<int> Prices { get; set; }

        [FirestoreProperty("status")]
        public string Status { get; set; }
        [FirestoreProperty("companyName")]
        public string CompanyName { get; set; }
        [FirestoreProperty("address")]
        public string Address { get; set; }

        [FirestoreProperty("services")]
        public List<string> Services { get; set; }

        [FirestoreProperty("totalPrice")]
        public int TotalPrice { get; set; }

        [FirestoreProperty("quantities")]
        public List<int> Quantity { get; set; }

        public string Photo { get; set; } = "https://localhost:44378/images/finders.png";
        [FirestoreProperty("surname")]
        public ClientModel Surname { get; set; }

        [FirestoreProperty("name")]
        public ClientModel ClientName { get; set; }
        [FirestoreProperty("email")]
        public ClientModel ClientEmail { get; set; }
        [FirestoreProperty("contacts")]
        public ClientModel ClientPhone { get; set;}

       
    }
}
