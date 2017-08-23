using System.Linq;
using EmptySummer.Core.Interfaces;
using EmptySummer.Core.Models;

namespace EmptySummer.Logic.Tests
{
    internal class TestRepository : IRepository
    {
        public void Add<T>(T entity) where T : Entity
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(int id) where T : Entity
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            if (typeof(T) == typeof(Author))
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

                return new[] { a1, a2, a3 }.Cast<T>().AsQueryable();
            }
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity
        {
            throw new System.NotImplementedException();
        }
    }
}
