using Finders.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finders.Controllers
{
    public class CIPCController : Controller
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly FirebaseStorageService _firebaseStorageService;

        public CIPCController(FirebaseStorageService firebaseStorageService)
        {
            _firestoreDb = FirestoreConfig.InitializeFirestore();
            _firebaseStorageService = firebaseStorageService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string searchCompany)
        {
            var collection = _firestoreDb.Collection("CIPC");
            Query query = collection;

            // Apply filter if searchSurname is provided
            if (!string.IsNullOrEmpty(searchCompany))
            {
                query = collection.WhereEqualTo("companyName", searchCompany);
            }

            var querySnapshot = await query.GetSnapshotAsync();
            var clients = querySnapshot.Documents.Select(doc => doc.ConvertTo<Models.CIPCModel>()).ToList();

            // Pass the search term back to the view to maintain it in the search box
            ViewData["CurrentFilter"] = searchCompany;

            return View(clients);
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

        public async Task<IActionResult> Service(string company)
        {
            return await GetServiceProviderByCompanyName(company);
        }

        public async Task<IActionResult> Details(string company)
        {
            return await GetServiceProviderByCompanyName(company);
        }

        private async Task<IActionResult> GetServiceProviderByCompanyName(string company)
        {
            var collection = _firestoreDb.Collection("CIPC");
            var query = collection.WhereEqualTo("companyName", company);
            var querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                var documentSnapshot = querySnapshot.Documents.FirstOrDefault();
                var client = documentSnapshot.ConvertTo<CIPCModel>();
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
            var collection = _firestoreDb.Collection("CIPC");
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
