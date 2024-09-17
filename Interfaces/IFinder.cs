// Programmer name : S Nondwatyu
// Student nr : 220036624
// Assignment nr : GA1
// Purpose : The purpose of this IStudent interface is to define a contract for student-related operations.
// It declares methods for common CRUD (Create, Read, Update, Delete) operations
// and additional functionality such as querying students based on search criteria,
// retrieving student details by ID, and checking for the existence of a student.


using Finders.Models;

namespace Finders.Interfaces
{
    public interface IFinder
    {
        IQueryable<Finder> GetFinders(string searchString, string sortOrder);
        Finder Details(string id);
        Finder Create(Finder student);
        Finder ByEmail(string id);
        Finder Edit(Finder student);
        bool Delete(Finder student);
        bool IsExist(string id);
    }
}
