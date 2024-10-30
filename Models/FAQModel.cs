using Google.Cloud.Firestore;

namespace Finders.Models
{
    [FirestoreData]
    public class FAQModel
    {
        [FirestoreProperty("Question")]
        public string Question { get; set; }
        [FirestoreProperty("Answer")]
        public string Answer { get; set; }
    }
}
