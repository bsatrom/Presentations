using System.Web.Mvc;
using RazorLib.Core.Models;
using RazorLib.Core.Repositories;

namespace RazorLib.Site.Controllers
{
    public class HelpersController : Controller
    {
        private IHelperRepository _helperRepository;

        public HelpersController(IHelperRepository helperRepository)
        {
            _helperRepository = helperRepository;
        }

        public ActionResult Index()
        {
            return View("List", _helperRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            var helper = _helperRepository.Get(id);
            return View(helper);
        }

        public ActionResult Create()
        {
            return View(new Helper());
        } 

        [HttpPost]
        public ActionResult Create(Helper helper)
        {
            if (!ModelState.IsValid)
                return View(helper);

            _helperRepository.Save(helper);

            return Details(helper.Id);
        }
        
        public ActionResult Edit(int id)
        {
            var helper = _helperRepository.Get(id);
            return View(helper);
        }

        [HttpPost]
        public ActionResult Edit(Helper helper)
        {
            if (!ModelState.IsValid)
                return View(helper);

            _helperRepository.Save(helper);

            return Details(helper.Id);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var helper = _helperRepository.Get(id);
            _helperRepository.Delete(helper);

            return Index();
        }
    }
}
