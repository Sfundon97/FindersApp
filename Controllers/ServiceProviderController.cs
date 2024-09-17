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

        public ServiceProviderController()
        {
            // Initialize FirestoreDb using the FirestoreConfig helper method
            _firestoreDb = FirestoreConfig.InitializeFirestore();
        }

        // Index method for basic retrieval of all documents with search functionality
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string searchCompany)
        {
            var collection = _firestoreDb.Collection("Service Provider");
            Query query;

            // Apply filter if searchCompany is provided
            if (!string.IsNullOrEmpty(searchCompany))
            {
                query = collection.WhereEqualTo("companyName", searchCompany);
            }
            else
            {
                // If no search term, retrieve all documents
                query = collection;
            }

            // Retrieve documents from the collection
            var querySnapshot = await query.GetSnapshotAsync();

            // Convert documents to the ServiceProvider model
            var serviceProviders = querySnapshot.Documents
                .Select(doc => doc.ConvertTo<Models.ServiceProvider>())
                .ToList();

            ViewData["CurrentFilter"] = searchCompany;

            return View(serviceProviders);
        }

        public async Task<IActionResult> Service(string company)
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
        public async Task<IActionResult> Details(string company)
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
    }
}
