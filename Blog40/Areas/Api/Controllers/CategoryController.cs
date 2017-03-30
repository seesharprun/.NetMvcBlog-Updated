using Blog40.Models;
using Blog40.Repository;
using System.Collections.Generic;
using System.Web.Http;

namespace Blog40.Areas.Api.Controllers
{
    public class CategoryController : ApiController
    {
        private IRepository<Category> _CategoryRepository;

        public CategoryController(IRepository<Category> CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public IEnumerable<Category> Get()
        {
            return _CategoryRepository.GetAll();
        }
        
        public Category Get(int id)
        {
            return _CategoryRepository.Get(id);
        }
        
        public void Category([FromBody]Category body)
        {
            _CategoryRepository.Add(body);
        }
        
        public void Put(int id, [FromBody]Category body)
        {
            _CategoryRepository.Update(body);
        }
        
        public void Delete(int id)
        {
            _CategoryRepository.Delete(id);
        }
    }
}
