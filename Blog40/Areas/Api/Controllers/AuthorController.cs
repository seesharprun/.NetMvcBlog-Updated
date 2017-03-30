using Blog40.Models;
using Blog40.Repository;
using System.Collections.Generic;
using System.Web.Http;

namespace Blog40.Areas.Api.Controllers
{
    public class AuthorController : ApiController
    {
        private IRepository<Author> _AuthorRepository;

        public AuthorController(IRepository<Author> AuthorRepository)
        {
            _AuthorRepository = AuthorRepository;
        }

        public IEnumerable<Author> Get()
        {
            return _AuthorRepository.GetAll();
        }
        
        public Author Get(int id)
        {
            return _AuthorRepository.Get(id);
        }
        
        public void Author([FromBody]Author body)
        {
            _AuthorRepository.Add(body);
        }
        
        public void Put(int id, [FromBody]Author body)
        {
            _AuthorRepository.Update(body);
        }
        
        public void Delete(int id)
        {
            _AuthorRepository.Delete(id);
        }
    }
}
