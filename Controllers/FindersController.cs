// Programmer name : S Nondwatyu and S Jonga
// Purpose : The purpose of this FindersController is to provide basic functionality
// for rendering views and CRUD operations.


using Finders.Interfaces;
using Finders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finders.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class FindersController : Controller
    {

        private readonly IFinder _finderRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FindersController(IFinder finderRepo, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            // Name:   FindersController
            // Method Parameters :
            //   IFinder findertRepo
            //     - Interface for finder repository
            //   IHttpContextAccessor httpContextAccessor
            //     - Provides access to the HttpContext
            //   IWebHostEnvironment webHostEnvironment
            //     - Provides information about the web hosting environment
            try
            {
                _finderRepo = finderRepo;
                _httpContextAccessor = httpContextAccessor;
                _webHostEnvironment = webHostEnvironment;
            }
            catch (Exception ex)
            {
                throw new Exception("The constructor was not initialised!");
            }

          
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Name : IActionResult Index
            // Purpose : Renders the index view for admin records
            // Re-use : None
            // Input Parameters : string sortOrder, string currentFilter, string searchString, int? pageNumber
            //   - Sorting parameters, filter parameters, search string, and page number for pagination
            // Output Type : IActionResult
            //   - Returns the view result for the index view
            pageNumber = pageNumber ?? 1;
            int pageSize = 3;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["StudentNumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else 
            { 
                searchString = currentFilter; 
            }

            ViewData["CurrentFilter"] = searchString;

            ViewResult viewResult =  View();

            try
            {
                viewResult = View(PaginatedList<Finder>.Create(_finderRepo.GetFinders(searchString, sortOrder).AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            catch (Exception ex) 
            {
                throw new Exception("No records detected");
            }
                        
            return viewResult;
        }//end method
        public IActionResult Details(string id)
        {
            // Check if the ID is null or empty
            if (string.IsNullOrEmpty(id))
            {
                var finder = _finderRepo.ByEmail(this.User.Identity.Name.ToString());
                if (finder != null)
                {
                    return View(finder); // This will look for Views/Finders/Details.cshtml
                }
            }
            else
            {
                var finder = _finderRepo.Details(id);
                return View("Details", finder); // Corrected to just "Details"
            }

            // If the admin is not found or if id is null, redirect to the "NotRegistered" view
            return View("NotRegistered");
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            //Name : IActionResult Create
            // Purpose : Renders the create view for adding a new admin into the database
            // Re-use : None
            // Method Parameters : None
            // Output Type : IActionResult
            //   - Returns the view result for the create view
            var adminExist = _finderRepo.ByEmail(this.User.Identity.Name.ToString());    

             if (adminExist != null)
            {
                return RedirectToAction("Details", "Finders", adminExist.RegNumber);  
            }
            else
            {
            Finder admin = new Finder();
            string fileName = "Default.png";
            admin.Photo = fileName;
            return View(admin);
            }
             

        }//end method

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Finder admin)
        {
            //Name : IActionResult Create
            // Purpose : Handles form submission to create an admin
            // Re-use : None
            // Method Parameters : Student admin
            //   - Student object containing information for the new admin
            // Output Type : IActionResult
            //   - Returns the view result after processing the form submission
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            string upload = webRootPath + WebConstants.ImagePath;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            admin.Photo = fileName + extension;

            try
            {
                if(ModelState.IsValid)
                {
                    _finderRepo.Create(admin);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Admin Record not Saved!");
            }

            
            var finderExist = _finderRepo.ByEmail(this.User.Identity.Name.ToString());

            if(finderExist != null)
            {
                return RedirectToAction("Details", "Finders", new { id = finderExist.RegNumber });
            }
            else
            {
                return RedirectToAction("Create");
            }
            

            
        }//end method
        
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            //Name : IActionResult Edit
            // Purpose : Renders the edit view for modifying a admin's details
            // Re-use : None
            // Method Parameters : string id
            //   - ID of the admin to edit
            // Output Type : IActionResult
            //   - Returns the view result for the edit view
            ViewResult viewDetail = View();
            try
            {
                viewDetail = View(_finderRepo.Details(id));
            }
            catch (Exception ex)
            {
                throw new Exception("Student detail not found");
            }
            return viewDetail;
        }//end method

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Finder admin, string photoName)
        {
            //Name : IActionResult Edit
            // Purpose : Handles form submission to modify a admin's details
            // Re-use : None
            // Method Parameters : Student admin, string photoName
            //   - Student object containing modified details, and the name of the photo
            // Output Type : IActionResult
            //   - Returns the view result after processing the form submission

            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                string upload = webRootPath + WebConstants.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                if (!string.IsNullOrEmpty(photoName))
                {
                    var oldFile = Path.Combine(upload, photoName);

                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }


                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension),
                    FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                admin.Photo = fileName + extension;
            }
            else
            {
                admin.Photo = photoName;
            }
            try
            {
                _finderRepo.Edit(admin);
            }
            catch (Exception ex)
            {
                throw new Exception("Student detail could not be edited");
            }
            
            return RedirectToAction("Details");
        }//end method

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            //Name : IActionResult Delete
            // Purpose : Renders the delete view for removing a admin
            // Re-use : None
            // Method Parameters : string id
            //   - ID of the admin to delete
            // Output Type : IActionResult
            //   - Returns the view result for the delete view
            ViewResult viewDetail = View();
            try
            {
                viewDetail = View(_finderRepo.Details(id));
            }
            catch (Exception ex)
            {
                throw new Exception("Student detail not found");
            }
            return viewDetail;
        }//end method

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind("RegNumber, FirstName, Surname, Phone")] Finder student)
        {
            //Name : IActionResult Delete
            // Purpose : Handles form submission to remove a admin from the database
            // Re-use : None
            // Method Parameters : Student admin
            //   - Student object containing details of the admin to delete
            // Output Type : IActionResult
            //   - Returns the view result after processing the form submission
            try
            {
                _finderRepo.Delete(student);
            }
            catch (Exception ex) 
            {
                throw new Exception("Student could not be deleted");
            }
            
            return RedirectToAction(nameof(Index));
        }
        public IActionResult FAQ() {
        
        return View();
        }
        //end method


    }//end controller
}//end namespace
