using EmptySummer.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EmptySummer.Data.EntityFramework.Tests
{
    [TestClass]
    public class BooksDbContextTests
    {
        [TestMethod]
        public void BooksDbConext_CanCreateInstance()
        {
            using (var context = new BooksDbContext("don´t know how to write a connection string"))
            { }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void BooksDbContext_CheckTableNames()
        {
            var connectionString = "Server=.;Database=BooksDBContext_CheckTableNames_Tests;Trusted_Connection=true;Timeout=3";
            using (var context = new BooksDbContext(connectionString))
            {
                if (context.Database.Exists())
                    context.Database.Delete();

                // Act
                context.Database.Create();

                var tableNames = context.Database.SqlQuery<string>("SELECT Name FROM sys.Tables").ToList();

                CollectionAssert.Contains(tableNames, "Author");
                CollectionAssert.Contains(tableNames, "Book");

                context.Database.Delete();
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void BooksDbContext_Author_CRUD_Test()
        {
            var connectionString = "Server=.;Database=BooksDBContext_Author_CRUD_Tests;Trusted_Connection=true;Timeout=3";

            var testAuthor = new Author { Firstname = "Stanislaus", Lastname = "Maier" };
            var newFirstname = "Luis";
            var newLastname = "Huber";

            // INSERT
            using (var context = new BooksDbContext(connectionString))
            {
                if (context.Database.Exists())
                    context.Database.Delete();

                context.Authors.Add(testAuthor);
                context.SaveChanges();
            }

            // check INSERT
            using (var context = new BooksDbContext(connectionString))
            {
                var reloaded = context.Authors.Single(a => a.Id == testAuthor.Id);
                Assert.AreEqual(testAuthor.Firstname, reloaded.Firstname);
                Assert.AreEqual(testAuthor.Lastname, reloaded.Lastname);

                // UPDATE
                reloaded.Firstname = newFirstname;
                reloaded.Lastname = newLastname;
                context.SaveChanges();
            }

            // check UPDATE
            using (var context = new BooksDbContext(connectionString))
            {
                var reloaded = context.Authors.Single(a => a.Id == testAuthor.Id);
                Assert.AreEqual(newFirstname, reloaded.Firstname);
                Assert.AreEqual(newLastname, reloaded.Lastname);

                // DELETE
                context.Authors.Remove(reloaded);
                context.SaveChanges();
            }

            // check DELETE
            using (var context = new BooksDbContext(connectionString))
            {
                var reloaded = context.Authors.SingleOrDefault(a => a.Id == testAuthor.Id);
                Assert.IsNull(reloaded);
            }

            // Cleanup
            using (var context = new BooksDbContext(connectionString))
            {
                if (context.Database.Exists())
                    context.Database.Delete();
            }
        }
    }
}
