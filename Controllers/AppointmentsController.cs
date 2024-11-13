using Finders.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Finders.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly FirestoreDb _firestoreDb;

        // Modify the constructor to accept FirestoreConfig as a parameter
        public AppointmentsController(FirestoreConfig firestoreConfig)
        {
            // Use FirestoreConfig to initialize FirestoreDb
            _firestoreDb = firestoreConfig.InitializeFirestore();
        }

        // Index method with search functionality
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string searchCompanyName)
        {
            var collection = _firestoreDb.Collection("appointments");
            Query query = collection;

            // Apply filter if searchCompanyName is provided
            if (!string.IsNullOrEmpty(searchCompanyName))
            {
                query = collection.WhereEqualTo("companyName", searchCompanyName);
            }

            // Execute the query and get the snapshot
            var querySnapshot = await query.GetSnapshotAsync();

            // Check if there are any documents in the result
            if (!querySnapshot.Documents.Any())
            {
                ViewBag.Message = "Company does not exist!";
            }

            var appointments = querySnapshot.Documents.Select(doc =>
            {
                // Convert document to Appointment object
                var appointment = doc.ConvertTo<Appointments>();
                appointment.ReferenceNumber = doc.Id;
                return appointment;
            }).ToList();

            // Pass the search term back to the view to maintain it in the search box
            ViewData["CurrentFilter"] = searchCompanyName;

            return View(appointments);
        }

        public async Task<IActionResult> Details(string documentId)
        {
            try
            {
                var documentRef = _firestoreDb.Collection("appointments").Document(documentId);
                var documentSnapshot = await documentRef.GetSnapshotAsync();

                if (!documentSnapshot.Exists)
                {
                    return NotFound();
                }

                var appointments = documentSnapshot.ConvertTo<Appointments>();
                ViewBag.DocumentId = documentId; // Pass documentId to the view
                return View(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
