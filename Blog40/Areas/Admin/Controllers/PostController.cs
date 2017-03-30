using AutoMapper;
using Blog40.Models;
using Blog40.Repository;
using Blog40.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Blog40.Areas.Admin.ControllersPostEditViewModel
{
    public class PostController : Controller
    {
        private IRepository<Post> _postRepository;
        private IRepository<Author> _authorRepository;
        private IRepository<Category> _categoryRepository;
        private IMapper _mapper;

        public PostController(IRepository<Post> postRepository, IRepository<Author> authorRepository, IRepository<Category> categoryRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Post> postList = _postRepository.GetAll();
            IEnumerable<PostViewModel> posts = _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postList);
            PostListViewModel viewModel = new PostListViewModel
            {
                Posts = posts
            };
            return View(viewModel);
        }
            
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Post post = _postRepository.Get(id);
            PostEditViewModel viewModel = _mapper.Map<Post, PostEditViewModel>(post);
            viewModel.Categories = _categoryRepository.GetAll();
            viewModel.Authors = _authorRepository.GetAll();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel viewModel)
        {
            Post post = _postRepository.Get(viewModel.PostId);
            _mapper.Map<PostViewModel, Post>(viewModel, post);
            _postRepository.Update(post);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult New()
        {
            PostEditViewModel viewModel = new PostEditViewModel
            {
                Categories = _categoryRepository.GetAll(),
                Authors = _authorRepository.GetAll()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult New(PostViewModel viewModel)
        {
            Post post = _mapper.Map<PostViewModel, Post>(viewModel);
            _postRepository.Add(post);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _postRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UndeleteAll()
        {
            _postRepository.UndeleteAll();
            return RedirectToAction("Index");
        }
    }
}
