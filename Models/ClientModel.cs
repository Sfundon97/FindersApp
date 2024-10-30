using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace Finders.Models
{
    [FirestoreData]
    public class ClientModel
    {        
        [FirestoreProperty("name")]
        public string FirstName { get; set; }

        [FirestoreProperty("surname")]
        public string Surname { get; set; }
        [FirestoreProperty("contacts")]
        public string Phone { get; set; }    
        [FirestoreProperty("profilePicture")]
        public string Photo { get; set; }
        [FirestoreProperty("address")]
        public string Address { get; set; }
        [FirestoreProperty("email")]
        public string Email { get; set; }

        public string DefaultPhoto { get; set; } = "https://firebasestorage.googleapis.com/v0/b/findersmvc.appspot.com/o/DA33983229BB45D7B2196257BB8754DB.png?alt=media&token=938194f3-c008-4d41-8d6d-e929354b232a";
        }//end class
    }//end namespace

