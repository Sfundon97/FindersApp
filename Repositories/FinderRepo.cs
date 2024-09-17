// Programmer name: S Nondwatyu
// Student nr: 220036624
// Assignment nr: GA1
// Purpose: Implement the StudentRepo class, which implements the IStudent interface,
// to provide CRUD functionality for interacting with student data in an ASP.NET Core application.
// 

using Finders.Data;
using Finders.Interfaces;
using Finders.Models;


namespace Finders.Repositories
{
    public class FinderRepo : IFinder
    {
        private readonly SQLiteDBContext _context;

        public FinderRepo(SQLiteDBContext context)
        {
            _context = context;
        }

        public Finder ByEmail(string id)
        {
            // Name : Student ByEmail(string id)
            // Purpose : Retrieve a student from the database based on their email address.
            // Method Parameters : string id
            // - The email address of the student to retrieve.
            // Output Type : Student
            // - The student record corresponding to the given email address.

            var finder = _context.Finders?.FirstOrDefault(x => x.Email == id);
            return finder;
        }

        public Finder Create(Finder finder)
        {
            // Name : Student Create(Student student)
            // Purpose : Create a new student record in the database.
            // Method Parameters : Student student
            // - The student record to be created.
            // Output Type : Student
            // - The created student entity.
            _context.Add(finder);
            _context.SaveChanges();
            return finder;
        }

        public bool Delete(Finder finder)
        {
            // Name : bool Delete(Student student)
            // Purpose : Delete a student record from the database.
            // Method Parameters : Student student
            // - The student record to be deleted.
            // Output Type : bool
            // - Returns true if the student was successfully deleted, false otherwise.
            _context.Remove(finder);
            _context.SaveChanges();
            return IsExist(finder.RegNumber);
        }

        public Finder Details(string id)
        {
            // Name : Student Details(string id)
            // Purpose : Retrieve a student from the database based on their student number.
            // Method Parameters : string id
            // - The student number of the student to retrieve.
            // Output Type : Student
            // - The student record corresponding to the given student number.
            var finder = _context.Finders?.FirstOrDefault(x => x.RegNumber == id);
            return finder;
        }

        public Finder Edit(Finder finder)
        {
            // Name : Student Edit(Student student)
            // Purpose : Update an existing student record in the database.
            // Method Parameters : Student student
            // - The student entity containing updated information.
            // Output Type : Student
            // - The updated student record.
            _context.Update(finder);
            _context.SaveChanges();
            return finder;
        }

        public IQueryable<Finder> GetFinders(string searchString, string sortOrder)
        {
            // Name : IQueryable<Student> GetFinders(string searchString, string sortOrder)
            // Purpose : Retrieve a list of students from the database based on search criteria and sorting order.
            // Method Parameters : string searchString, string sortOrder
            // - searchString: The search string used to filter students by student number.
            // - sortOrder: The sorting order for the retrieved students.
            // Output Type : IQueryable<Student>
            // - A queryable collection of student entities matching the search criteria and sorted accordingly.
            var finder = _context.Finders
               .ToList();
            if(!String.IsNullOrEmpty(searchString)) 
            {
                finder = finder.Where(s => s.RegNumber.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "number_desc":
                    finder = finder.OrderByDescending(s => s.RegNumber).ToList();
                    break;
                case "name_desc":
                    finder = finder.OrderByDescending(s => s.Surname).ToList();
                    break;
                case "Date":
                    finder = finder.OrderBy(s => s.Phone).ToList();
                    break;
                case "date_desc":
                    finder = finder.OrderByDescending(s => s.Phone).ToList();
                    break;
                default:
                    finder = finder.OrderBy(s => s.Surname).ToList();
                    break;
            }

            return finder.AsQueryable();
        }

        public bool IsExist(string id)
        {
            // Name : bool IsExist(string id)
            // Purpose : Check if a student with the given student number exists in the database.
            // Method Parameters : string id
            // - The student number to check for existence.
            // Output Type : bool
            // - Returns true if the student exists, false otherwise.
            bool isExist = false;
            Finder existFinder = Details(id);
            if (existFinder == null)
            {
                isExist = true;
            }
            return isExist;
        }

    }
}
