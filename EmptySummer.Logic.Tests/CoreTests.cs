using EmptySummer.Core.Interfaces;
using EmptySummer.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;
using System;
using System.Linq;

namespace EmptySummer.Logic.Tests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void Core_CanCreateInstance()
        {
            var core = new Core(new TestRepository());
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Core_Constructor_repository_null_ArgumentNullException()
        {
            var core = new Core(null);
        }
        [TestMethod]
        public void Core_GetAllAuthorsWithMoreBooksThan_3_Result_2_of_3()
        {
            var core = new Core(new TestRepository());

            var result = core.GetAllAuthorsWithMoreBooksThan(3);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result.First().Id);
        }
        [TestMethod]
        public void Core_GetAllAuthorsWithMoreBooksThan_3_Result_2_of_3_Moq()
        {
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.Query<Author>())
                          .Returns(() =>
                          {
                              var a1 = new Author { Id = 1, Firstname = "Hans", Lastname = "Huber" };
                              var a1b1 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter1", IBAN = "1234" };
                              var a1b2 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter2", IBAN = "2345" };
                              var a1b3 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter3", IBAN = "3456" };
                              a1.Books.Add(a1b1);
                              a1.Books.Add(a1b2);
                              a1.Books.Add(a1b3);

                              var a2 = new Author { Id = 2, Firstname = "Maria", Lastname = "Anders" };
                              var a2b1 = new Book { AutorId = a2.Id, Author = a2, Title = "Harry Potter1", IBAN = "4567" };
                              var a2b2 = new Book { AutorId = a2.Id, Author = a2, Title = "Harry Potter2", IBAN = "5678" };
                              var a2b3 = new Book { AutorId = a2.Id, Author = a2, Title = "Harry Potter3", IBAN = "6789" };
                              var a2b4 = new Book { AutorId = a2.Id, Author = a2, Title = "Harry Potter3", IBAN = "7890" };
                              var a2b5 = new Book { AutorId = a2.Id, Author = a2, Title = "Harry Potter3", IBAN = "8901" };
                              a2.Books.Add(a2b1);
                              a2.Books.Add(a2b2);
                              a2.Books.Add(a2b3);
                              a2.Books.Add(a2b4);
                              a2.Books.Add(a2b5);

                              var a3 = new Author { Id = 3, Firstname = "Franz", Lastname = "Whatever" };
                              var a3b1 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter1", IBAN = "9012" };
                              var a3b2 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter2", IBAN = "0123" };
                              var a3b3 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter3", IBAN = "abcd" };
                              var a3b4 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter3", IBAN = "bcde" };
                              a3.Books.Add(a3b1);
                              a3.Books.Add(a3b2);
                              a3.Books.Add(a3b3);
                              a3.Books.Add(a3b4);

                              return new[] { a1, a2, a3 }.AsQueryable();
                          });

            var core = new Core(repositoryMock.Object);

            var result = core.GetAllAuthorsWithMoreBooksThan(3);

            repositoryMock.Verify(r => r.Query<Author>(), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void Core_GetAllAuthorsWithMoreBooksThan_3_Result_1_of_3_NSubstitue()
        {
            var a1 = new Author { Id = 1, Firstname = "Hans", Lastname = "Huber" };
            var a1b1 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter1", IBAN = "1234" };
            var a1b2 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter2", IBAN = "2345" };
            var a1b3 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter3", IBAN = "3456" };
            a1.Books.Add(a1b1);
            a1.Books.Add(a1b2);
            a1.Books.Add(a1b3);

            var a2 = new Author { Id = 2, Firstname = "Maria", Lastname = "Anders" };
            var a2b1 = new Book { AutorId = a2.Id, Author = a2, Title = "Harry Potter1", IBAN = "4567" };
            a2.Books.Add(a2b1);

            var a3 = new Author { Id = 3, Firstname = "Franz", Lastname = "Whatever" };
            var a3b1 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter1", IBAN = "9012" };
            var a3b2 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter2", IBAN = "0123" };
            var a3b3 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter3", IBAN = "abcd" };
            var a3b4 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter3", IBAN = "bcde" };
            a3.Books.Add(a3b1);
            a3.Books.Add(a3b2);
            a3.Books.Add(a3b3);
            a3.Books.Add(a3b4);

            var repository = Substitute.For<IRepository>();
            repository.Query<Author>().Returns(new[] { a1, a2, a3 }.AsQueryable());

            var core = new Core(repository);

            var result = core.GetAllAuthorsWithMoreBooksThan(3);

            repository.Received().Query<Author>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void Core_GetAllAuthorsWithMoreBooksThan_0_Result_3_of_3()
        {
            var a1 = new Author { Id = 1, Firstname = "Hans", Lastname = "Huber" };
            var a1b1 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter1", IBAN = "1234" };
            var a1b2 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter2", IBAN = "2345" };
            var a1b3 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter3", IBAN = "3456" };
            a1.Books.Add(a1b1);
            a1.Books.Add(a1b2);
            a1.Books.Add(a1b3);

            var a2 = new Author { Id = 2, Firstname = "Maria", Lastname = "Anders" };
            var a2b1 = new Book { AutorId = a2.Id, Author = a2, Title = "Harry Potter1", IBAN = "4567" };
            a2.Books.Add(a2b1);

            var a3 = new Author { Id = 3, Firstname = "Franz", Lastname = "Whatever" };
            var a3b1 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter1", IBAN = "9012" };
            var a3b2 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter2", IBAN = "0123" };
            var a3b3 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter3", IBAN = "abcd" };
            var a3b4 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter3", IBAN = "bcde" };
            a3.Books.Add(a3b1);
            a3.Books.Add(a3b2);
            a3.Books.Add(a3b3);
            a3.Books.Add(a3b4);

            var repository = Substitute.For<IRepository>();
            repository.Query<Author>().Returns(new[] { a1, a2, a3 }.AsQueryable());

            var core = new Core(repository);

            var result = core.GetAllAuthorsWithMoreBooksThan(0);

            repository.Received().Query<Author>();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }
        [TestMethod]
        public void Core_GetAllAuthorsWithMoreBooksThan_MAX_Result_0_of_3()
        {
            var a1 = new Author { Id = 1, Firstname = "Hans", Lastname = "Huber" };
            var a1b1 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter1", IBAN = "1234" };
            var a1b2 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter2", IBAN = "2345" };
            var a1b3 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter3", IBAN = "3456" };
            a1.Books.Add(a1b1);
            a1.Books.Add(a1b2);
            a1.Books.Add(a1b3);

            var a2 = new Author { Id = 2, Firstname = "Maria", Lastname = "Anders" };
            var a2b1 = new Book { AutorId = a2.Id, Author = a2, Title = "Harry Potter1", IBAN = "4567" };
            a2.Books.Add(a2b1);

            var a3 = new Author { Id = 3, Firstname = "Franz", Lastname = "Whatever" };
            var a3b1 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter1", IBAN = "9012" };
            var a3b2 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter2", IBAN = "0123" };
            var a3b3 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter3", IBAN = "abcd" };
            var a3b4 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter3", IBAN = "bcde" };
            a3.Books.Add(a3b1);
            a3.Books.Add(a3b2);
            a3.Books.Add(a3b3);
            a3.Books.Add(a3b4);

            var repository = Substitute.For<IRepository>();
            repository.Query<Author>().Returns(new[] { a1, a2, a3 }.AsQueryable());

            var core = new Core(repository);

            var result = core.GetAllAuthorsWithMoreBooksThan(int.MaxValue);

            repository.Received().Query<Author>();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Core_GetAllAuthorsWithMoreBooksThan_M5_Result_ArgumentException()
        {
            var a1 = new Author { Id = 1, Firstname = "Hans", Lastname = "Huber" };
            var a1b1 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter1", IBAN = "1234" };
            var a1b2 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter2", IBAN = "2345" };
            var a1b3 = new Book { AutorId = a1.Id, Author = a1, Title = "Harry Potter3", IBAN = "3456" };
            a1.Books.Add(a1b1);
            a1.Books.Add(a1b2);
            a1.Books.Add(a1b3);

            var a2 = new Author { Id = 2, Firstname = "Maria", Lastname = "Anders" };
            var a2b1 = new Book { AutorId = a2.Id, Author = a2, Title = "Harry Potter1", IBAN = "4567" };
            a2.Books.Add(a2b1);

            var a3 = new Author { Id = 3, Firstname = "Franz", Lastname = "Whatever" };
            var a3b1 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter1", IBAN = "9012" };
            var a3b2 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter2", IBAN = "0123" };
            var a3b3 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter3", IBAN = "abcd" };
            var a3b4 = new Book { AutorId = a3.Id, Author = a3, Title = "Harry Potter3", IBAN = "bcde" };
            a3.Books.Add(a3b1);
            a3.Books.Add(a3b2);
            a3.Books.Add(a3b3);
            a3.Books.Add(a3b4);

            var repository = Substitute.For<IRepository>();
            repository.Query<Author>().Returns(new[] { a1, a2, a3 }.AsQueryable());

            var core = new Core(repository);

            var result = core.GetAllAuthorsWithMoreBooksThan(-5);   // expected: ArgumentException
        }
    }
}
