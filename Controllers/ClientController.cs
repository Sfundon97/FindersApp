using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using Finders.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Finders.Controllers
{
    public class ClientController : Controller
    {
        private readonly FirestoreDb _firestoreDb;

        // Modify the constructor to accept FirestoreConfig via dependency injection
        public ClientController(FirestoreConfig firestoreConfig)
        {
            _firestoreDb = firestoreConfig.InitializeFirestore();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFAQ(string question, string answer)
        {
            if (string.IsNullOrEmpty(question) || string.IsNullOrEmpty(answer))
            {
                // Handle invalid input
                TempData["ErrorMessage"] = "Question and Answer cannot be empty.";
                return RedirectToAction("FAQ"); // Redirect to the FAQ action to reload the page
            }

            // Create a new FAQ object to store in Firestore
            var faq = new
            {
                Question = question,
                Answer = answer,
            };

            // Reference to your Firestore collection
            var collection = _firestoreDb.Collection("FAQs");

            // Add the new FAQ to Firestore
            await collection.AddAsync(faq);

            // Store the success message in TempData to show after redirect
            TempData["SuccessMessage"] = "FAQ added successfully!";

            // Redirect to the FAQ action to reload the page
            return RedirectToAction("FAQ");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FAQ()
        {
            var faqsList = new List<FAQModel>();

            // Fetch the collection for FAQs
            var collection = _firestoreDb.Collection("FAQs");

            // Get all documents in the collection
            var querySnapshot = await collection.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                foreach (var document in querySnapshot.Documents)
                {
                    var faq = document.ConvertTo<FAQModel>();  // Convert each document to your FAQ model
                    faqsList.Add(faq);
                }
            }

            // Pass the retrieved FAQs to the view
            return View(faqsList);
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
