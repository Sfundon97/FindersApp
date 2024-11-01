using Firebase.Storage;
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

        [FirestoreProperty("ratings")]
        public int Rating { get; set; }
        [FirestoreProperty("reviews")]
        public string Review { get; set; }
        [FirestoreProperty("ratingCount")]

        public int RatingCount { get; set; }

        [FirestoreProperty("profilePicture")]
        public string Photo { get; set; } 
        
        [FirestoreProperty("price")]
        public int? Price {  get; set; }
        [FirestoreProperty("url")]
        public List<string> Images { get; set; }
        [FirestoreProperty("dateJoined")]
        public DateTime DateJoined { get; set; }

        [FirestoreProperty("email")]
        public string Email { get; set; }

        [FirestoreProperty("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [FirestoreProperty("service")]
        public string Service { get; set; }

        [FirestoreProperty("clientId")]
        public string ClientId { get; set; }

        //Reviews Part
        [FirestoreProperty("rating")]
        public int Ratings { get; set; }
        [FirestoreProperty("reviewText")]
        public string ReviewText { get; set; }

        [FirestoreProperty("username")]
        public string Username { get; set; }

        [FirestoreProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [FirestoreProperty("serviceProviderId")]
        public string ProviderId { get; set; }
    }

    public class FirebaseStorageService
    {
        private readonly string _firebaseStorageUrl;
        private readonly string _apiKey;

        public FirebaseStorageService(string firebaseStorageUrl, string apiKey)
        {
            _firebaseStorageUrl = firebaseStorageUrl ?? throw new ArgumentNullException(nameof(firebaseStorageUrl));
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public async Task<string> GetImageUrlAsync(string imagePath)
        {
            try
            {
                var storage = new FirebaseStorage(_firebaseStorageUrl, new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(_apiKey)
                });

                // Get the download URL for the image
                var url = await storage.Child(imagePath).GetDownloadUrlAsync();
                Console.WriteLine($"Retrieved URL: {url}"); // Log the URL
                return url;
            }
            catch (Exception ex)
            {
                // Consider using a logging framework instead of Console.WriteLine
                Console.WriteLine($"Error retrieving image URL: {ex.Message}");
                return null;
            }
        }
    }
}
