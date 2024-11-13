using Finders.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Finders.Controllers
{
    public class ServiceProviderController : Controller
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly FirebaseStorageService _firebaseStorageService;

        public ServiceProviderController(FirestoreDb firestoreDb, FirebaseStorageService firebaseStorageService)
        {
            _firestoreDb = firestoreDb; 
            _firebaseStorageService = firebaseStorageService;
        }


        // Index method for basic retrieval of all documents
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string searchCompany)
        {
            var collection = _firestoreDb.Collection("Service Provider");
            Query query = collection;

            // Apply filter if searchSurname is provided
            if (!string.IsNullOrEmpty(searchCompany))
            {
                query = collection.WhereEqualTo("companyName", searchCompany);
            }
            
            var querySnapshot = await query.GetSnapshotAsync();

            var serviceProviders = querySnapshot.Documents
                .Select(doc => doc.ConvertTo<Models.ServiceProvider>())
                .ToList();
            ViewData["CurrentFilter"] = searchCompany;
            return View(serviceProviders);
        }

        public async Task<IActionResult> GetImage(string fileName)
        {
            // Get the image URL from Cloud Storage
            var imageUrl = await _firebaseStorageService.GetImageUrlAsync(fileName);
            if (string.IsNullOrEmpty(imageUrl))
            {
                return NotFound();
            }

            // Return the image URL directly
            return Redirect(imageUrl); // This will redirect to the image
        }

        

        public async Task<IActionResult> Details(string company)
        {
            return await GetServiceProviderByCompanyName(company);
        }

        private async Task<IActionResult> GetServiceProviderByCompanyName(string company)
        {
            var collection = _firestoreDb.Collection("Service Provider");
            var query = collection.WhereEqualTo("companyName", company);
            var querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                var documentSnapshot = querySnapshot.Documents.FirstOrDefault();
                var client = documentSnapshot.ConvertTo<Models.ServiceProvider>();
                return View(client);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string company)
        {
            var collection = _firestoreDb.Collection("Service Provider");
            var query = collection.WhereEqualTo("companyName", company);
            var querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                var documentSnapshot = querySnapshot.Documents.FirstOrDefault();
                await documentSnapshot.Reference.DeleteAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Review(string serviceProviderId)
        {
            var reviewsSnapshot = await _firestoreDb.Collection("Reviews")
                .WhereEqualTo("serviceProviderId", serviceProviderId)
                .GetSnapshotAsync();

            var reviews = reviewsSnapshot.Documents
                .Select(doc => doc.ConvertTo<Models.ServiceProvider>())
                .ToList();

            return View(reviews);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteReview(string clientId)
        {
            var reviewsCollection = _firestoreDb.Collection("Reviews");
            var query = reviewsCollection.WhereEqualTo("clientId", clientId);
            var querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                // Assuming clientId is unique for a specific review
                var reviewToDelete = querySnapshot.Documents.FirstOrDefault();
                await reviewToDelete.Reference.DeleteAsync();
                return RedirectToAction("Index"); // Redirect to the Index after deletion
            }
            else
            {
                return NotFound();
            }
        }



    }


}
