using Blog40.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog40.Repository
{
    public class PostRepository : BaseRepository, IRepository<Post>
    {
        private string postCategoryAuthorQuery = @"SELECT
	                                                    *
                                                    FROM
	                                                    Post AS P
	                                                    JOIN Category AS C ON P.CategoryId = C.CategoryId
	                                                    JOIN Author AS A ON P.AuthorId = A.AuthorId
                                                    WHERE
	                                                    P.IsDeleted = 0";

        public PostRepository(string connectionString)
            : base (connectionString)
        { }

        public int Add(Post model)
        {
            DateTime nowDate = DateTime.Now;
            model.CreatedAt = nowDate;
            model.UpdatedAt = nowDate;

            return base.Connection.Query<int>(
                @"INSERT INTO Post VALUES (@CategoryId, @AuthorId, @Title, @Slug, @Summary, @Content, @CreatedAt, @UpdatedAt, 0); SELECT CAST(SCOPE_IDENTITY() as int);", 
                new
                {
                    CategoryId = model.Category.CategoryId,
                    AuthorId = model.Author.AuthorId,
                    Title = model.Title,
                    Slug = model.Slug,
                    Summary = model.Summary,
                    Content = model.Content,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,
                }
            ).FirstOrDefault();
        }

        public IEnumerable<Post> GetAll()
        {
            return base.Connection.Query<Post,Category, Author, Post>(this.postCategoryAuthorQuery, (p, c, a) => { p.Category = c; p.Author = a; return p; }, splitOn: "CategoryId,AuthorId");
        }

        public Post Get(int id)
        {
            return base.Connection.Query<Post, Category, Author, Post>(this.postCategoryAuthorQuery + " AND PostId = @Id", (p, c, a) => { p.Category = c; p.Author = a; return p; }, new { Id = id }, splitOn: "CategoryId,AuthorId").FirstOrDefault();
        }
        public Post GetByString(string identifier)
        {
            return base.Connection.Query<Post, Category, Author, Post>(this.postCategoryAuthorQuery + " AND P.Slug = @Slug", (p, c, a) => { p.Category = c; p.Author = a; return p; }, new { Slug = identifier }, splitOn: "CategoryId,AuthorId").FirstOrDefault();
        }

        public int Update(Post model)
        {
            model.UpdatedAt = DateTime.Now;
            return base.Connection.Execute(
                @"UPDATE Post SET CategoryId = @CategoryId, AuthorId = @AuthorId, Title = @Title, Slug = @Slug, Summary = @Summary, Content = @Content, UpdatedAt = @UpdatedAt WHERE PostId = @PostId",
                new
                {
                    CategoryId = model.Category.CategoryId,
                    AuthorId = model.Author.AuthorId,
                    Title = model.Title,
                    Slug = model.Slug,
                    Summary = model.Summary,
                    Content = model.Content,
                    UpdatedAt = model.UpdatedAt,
                    PostId = model.PostId
                }
            );
        }

        public int Delete(int id)
        {
            DateTime updateTimeStamp = DateTime.Now;
            return base.Connection.Execute(
                @"UPDATE Post SET UpdatedAt = @UpdatedAt, IsDeleted = 1 WHERE PostId = @Id", 
                new
                {
                    UpdatedAt = updateTimeStamp,
                    Id = id
                }
            );
        }

        public int UndeleteAll()
        {
            return base.Connection.Execute("UPDATE Post SET IsDeleted = 0");
        }
    }
}