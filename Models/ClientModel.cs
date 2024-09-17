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
            public string Photo { get; set; }
            [FirestoreProperty("address")]
            public string Address { get; set; }

            [FirestoreProperty("email")]
            public string Email { get; set; }
        }//end class
    }//end namespace

