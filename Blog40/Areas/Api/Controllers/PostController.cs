using Blog40.Models;
using Blog40.Repository;
using System.Collections.Generic;
using System.Web.Http;

namespace Blog40.Areas.Api.Controllers
{
    public class PostController : ApiController
    {
        private IRepository<Post> _postRepository;

        public PostController(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<Post> Get()
        {
            return _postRepository.GetAll();
        }
        
        public Post Get(int id)
        {
            return _postRepository.Get(id);
        }
        
        public void Post([FromBody]Post body)
        {
            _postRepository.Add(body);
        }
        
        public void Put(int id, [FromBody]Post body)
        {
            _postRepository.Update(body);
        }
        
        public void Delete(int id)
        {
            _postRepository.Delete(id);
        }
    }
}
