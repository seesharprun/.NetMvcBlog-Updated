using AutoMapper;
using Blog40.Models;
using Blog40.Repository;
using Blog40.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog40.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        private IRepository<Author> _authorRepository;
        private IMapper _mapper;

        public AuthorController(IRepository<Author> authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Author> authorList = _authorRepository.GetAll();
            IEnumerable<AuthorViewModel> authors = _mapper.Map <IEnumerable<Author>, IEnumerable<AuthorViewModel>>(authorList);
            AuthorListViewModel model = new AuthorListViewModel
            {
                Authors = authors
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Author author = _authorRepository.Get(id);
            AuthorViewModel viewModel = _mapper.Map<Author, AuthorViewModel>(author);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(AuthorViewModel viewModel)
        {
            Author author = _authorRepository.Get(viewModel.AuthorId);
            _mapper.Map<AuthorViewModel, Author>(viewModel, author);
            _authorRepository.Update(author);
            return RedirectToAction("Index");
        }
    }
}
