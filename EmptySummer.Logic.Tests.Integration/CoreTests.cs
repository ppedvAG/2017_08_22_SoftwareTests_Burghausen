using EmptySummer.Core.Models;
using EmptySummer.Data.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EmptySummer.Logic.Tests.Integration
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        [TestCategory("EF_Integration")]
        public void Core_Ef_CanCreateInstance_WithSpecificRepository()
        {
            var connectionString = "Server=.;Database=Core_Ef_CanCreateInstance_WithSpecificRepository_Tests;Trusted_Connection=true;Timeout=3";
            var repository = new Repository(connectionString);

            var core = new Core(repository);

            Assert.IsNotNull(core);
        }
        [TestMethod]
        [TestCategory("EF_Integration")]
        public void Core_Ef_GetAllAuthorsWithMoreBooksThan_0_emptyDB_Result_emptyCollection()
        {
            var connectionString = "Server=.;Database=Core_Ef_GetAllAuthorsWithMoreBooksThan_0_emptyDB_Result_emptyCollection_Tests;Trusted_Connection=true;Timeout=3";
            var repository = new Repository(connectionString);

            var core = new Core(repository);

            if (repository.Context.Database.Exists())
                repository.Context.Database.Delete();

            var result = core.GetAllAuthorsWithMoreBooksThan(0);

            Assert.AreEqual(0, result.Count());

            if (repository.Context.Database.Exists())
                repository.Context.Database.Delete();
        }
        [TestMethod]
        [TestCategory("EF_Integration")]
        public void Core_Ef_GetAllAuthorsWithMoreBooksThan_3_Result_2_of_3()
        {
            var connectionString = "Server=.;Database=Core_Ef_GetAllAuthorsWithMoreBooksThan_3_Result_2_of_3_Tests;Trusted_Connection=true;Timeout=3";
            using (var repository = new Repository(connectionString))
            {
                var core = new Core(repository);

                if (repository.Context.Database.Exists())
                    repository.Context.Database.Delete();

                var a1 = new Author { Firstname = "Hans", Lastname = "Huber" };
                a1.Books.Add(new Book { Title = "Harry Potter1", IBAN = "1234" });
                a1.Books.Add(new Book { Title = "Harry Potter2", IBAN = "2345" });
                a1.Books.Add(new Book { Title = "Harry Potter3", IBAN = "3456" });

                var a2 = new Author { Firstname = "Maria", Lastname = "Anders" };
                a2.Books.Add(new Book { Title = "Harry Potter1", IBAN = "4567" });
                a2.Books.Add(new Book { Title = "Harry Potter2", IBAN = "5678" });
                a2.Books.Add(new Book { Title = "Harry Potter3", IBAN = "6789" });
                a2.Books.Add(new Book { Title = "Harry Potter3", IBAN = "7890" });
                a2.Books.Add(new Book { Title = "Harry Potter3", IBAN = "8901" });

                var a3 = new Author { Firstname = "Franz", Lastname = "Whatever" };
                a3.Books.Add(new Book { Title = "Harry Potter1", IBAN = "9012" });
                a3.Books.Add(new Book { Title = "Harry Potter2", IBAN = "0123" });
                a3.Books.Add(new Book { Title = "Harry Potter3", IBAN = "abcd" });
                a3.Books.Add(new Book { Title = "Harry Potter3", IBAN = "bcde" });

                repository.Add(a1);
                repository.Add(a2);
                repository.Add(a3);
                repository.Save();
            }
            using (var repository = new Repository(connectionString))
            {
                var core = new Core(repository);

                var result = core.GetAllAuthorsWithMoreBooksThan(3);

                Assert.AreEqual(2, result.Count());

                if (repository.Context.Database.Exists())
                    repository.Context.Database.Delete();
            }
        }
    }
}
