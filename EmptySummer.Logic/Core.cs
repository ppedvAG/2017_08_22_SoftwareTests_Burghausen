using EmptySummer.Core.Interfaces;
using EmptySummer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmptySummer.Logic
{
    public class Core
    {
        public IRepository Repository { get; }

        public Core(IRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public IEnumerable<Author> GetAllAuthorsWithMoreBooksThan(int count)
        {
            if (count < 0)
                throw new ArgumentException("Count must be greater than 0.", nameof(count));

            return Repository.Query<Author>().Where(a => a.Books.Count() > count);
        }
    }
}
