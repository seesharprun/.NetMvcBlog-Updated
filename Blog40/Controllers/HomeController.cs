using AutoMapper;
using Blog40.Models;
using Blog40.Repository;
using Blog40.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Blog40.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Post> _postRepository;
        private IMapper _mapper;

        public HomeController(IRepository<Post> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Post> postList = _postRepository.GetAll().Take(5);
            IEnumerable<PostViewModel> posts = _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postList);
            PostListViewModel viewModel = new PostListViewModel
            {
                Posts = posts
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Post(string slug)
        {
            if (String.IsNullOrEmpty(slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Post match = _postRepository.GetByString(slug);
                PostViewModel viewModel = _mapper.Map<Post, PostViewModel>(match);
                if (match == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(viewModel);
                }
            }
        }
    }
}
