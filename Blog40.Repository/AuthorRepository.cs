using Blog40.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog40.Repository
{
    public class AuthorRepository: BaseRepository, IRepository<Author>
    {
        private string query = "SELECT * FROM Author WHERE IsDeleted = 0";

        public AuthorRepository(string connectionString)
            : base (connectionString)
        { }

        public int Add(Author model)
        {
            return base.Connection.Query<int>("INSERT INTO Author VALUES (@DisplayName, @FirstName, @LastName, @Slug, 0); SELECT CAST(SCOPE_IDENTITY() as int);", model).FirstOrDefault(); ;
        }

        public IEnumerable<Author> GetAll()
        {
            return base.Connection.Query<Author>(query);
        }

        public Author Get(int Id)
        {
            return base.Connection.Query<Author>(query + " AND AuthorId = @Id", new { Id }).FirstOrDefault();
        }

        public Author GetByString(string identifier)
        {
            return base.Connection.Query<Author>(query + " AND Slug = @Slug", new { Slug = identifier }).FirstOrDefault();
        }

        public int Update(Author model)
        {
            return base.Connection.Execute("UPDATE Author SET DisplayName = @DisplayName, FirstName = @FirstName, LastName = @LastName, Slug = @Slug WHERE AuthorId = @AuthorId", model);
        }

        public int Delete(int Id)
        {
            return base.Connection.Execute("UPDATE Author SET IsDeleted = 1 WHERE AuthorId = @Id", Id);
        }

        public int UndeleteAll()
        {
            throw new NotImplementedException();
        }
    }
}