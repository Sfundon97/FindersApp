// Programmer name: S Nondwatyu
// Student nr: 220036624
// Assignment nr: GA1
// Purpose: Define a repository class, DBInitializerRepo, implementing the IDBInitializer interface,
// responsible for initializing the database with default data in the database.
// The Initialize method ensures database creation if it does not exist and seeds initial student data
// if the Student table is empty.

using Finders.Data;
using Finders.Interfaces;
using Finders.Models;

namespace Finders.Repositories
{
    public class DBInitializerRepo : IDBInitializer
    {
        public void Initialize(SQLiteDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Finders.Any())
            {
                return;   // DB has been seeded
            }

            var finders = new Finder[]
            {
                new Finder{RegNumber="2021000001",FirstName="Nomonde",Surname = "Jonga",
                Phone="0763484410", Photo = "DefaultPic.png", Email="DefaultEmail@gmail.com"},
                            new Finder{RegNumber="2012000002",
                FirstName="Kani",Surname="Tau",Phone= "0763484410", Photo = "DefaultPic.png", Email="DefaultEmail@gmail.com"},
                            new Finder{RegNumber="2021000003",
                FirstName="Mihlali",Surname="Touane",Phone="0763484410", Photo = "DefaultPic.png", Email="DefaultEmail@gmail.com"}
            };
            foreach (Finder s in finders)
            {
                context.Finders.Add(s);
            }
            context.SaveChanges();
        }//end method
    }//end class
}//end namespace
