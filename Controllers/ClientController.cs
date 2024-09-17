using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using Finders.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Finders.Controllers
{
    public class ClientController : Controller
    {
        private readonly FirestoreDb _firestoreDb;

        public ClientController()
        {
            // Initialize FirestoreDb using the FirestoreConfig helper method
            _firestoreDb = FirestoreConfig.InitializeFirestore();
        }

        // Index method with search functionality
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string searchSurname)
        {
            var collection = _firestoreDb.Collection("users");
            Query query = collection;

            // Apply filter if searchSurname is provided
            if (!string.IsNullOrEmpty(searchSurname))
            {
                query = collection.WhereEqualTo("surname", searchSurname);
            }

            var querySnapshot = await query.GetSnapshotAsync();
            var clients = querySnapshot.Documents.Select(doc => doc.ConvertTo<ClientModel>()).ToList();

            // Pass the search term back to the view to maintain it in the search box
            ViewData["CurrentFilter"] = searchSurname;

            return View(clients);
        }

        public async Task<IActionResult> Details(string surname)
        {
            var collection = _firestoreDb.Collection("users");
            var query = collection.WhereEqualTo("surname", surname);
            var querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                var documentSnapshot = querySnapshot.Documents.FirstOrDefault();
                var client = documentSnapshot.ConvertTo<ClientModel>();
                return View(client);
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string surname)
        {
            var collection = _firestoreDb.Collection("users");
            var query = collection.WhereEqualTo("surname", surname);
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
