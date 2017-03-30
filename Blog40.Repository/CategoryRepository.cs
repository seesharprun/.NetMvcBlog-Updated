using Blog40.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog40.Repository
{
    public class CategoryRepository: BaseRepository, IRepository<Category>
    {
        private string query = "SELECT * FROM Category WHERE IsDeleted = 0";

        public CategoryRepository(string connectionString)
            : base (connectionString)
        { }

        public int Add(Category model)
        {
            return base.Connection.Query<int>("INSERT INTO Category VALUES (@Name, @Slug, 0); SELECT CAST(SCOPE_IDENTITY() as int);", model).FirstOrDefault();
        }

        public IEnumerable<Category> GetAll()
        {
            return base.Connection.Query<Category>(query);
        }

        public Category Get(int Id)
        {
            return base.Connection.Query<Category>(query + " AND CategoryId = @CategoryId", new { CategoryId = Id }).FirstOrDefault();
        }

        public Category GetByString(string identifier)
        {
            return base.Connection.Query<Category>(query + " AND Slug = @Slug", new { Slug = identifier }).FirstOrDefault();
        }

        public int Update(Category model)
        {
            return base.Connection.Execute("UPDATE Category SET Name = @Name, Slug = @Slug WHERE CategoryId = @CategoryId", model);
        }

        public int Delete(int Id)
        {
            return base.Connection.Execute("UPDATE Category SET IsDeleted = 1 WHERE CategoryId = @Id", Id);
        }

        Category IRepository<Category>.GetByString(string identifier)
        {
            throw new NotImplementedException();
        }

        public int UndeleteAll()
        {
            throw new NotImplementedException();
        }
    }
}