using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Faceplant.Core.Models;
using Faceplant.Models;

namespace Faceplant.Controllers
{   
    public class ResponsesController : Controller
    {
		private readonly IResponseRepository responseRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ResponsesController() : this(new ResponseRepository())
        {
        }

        public ResponsesController(IResponseRepository responseRepository)
        {
			this.responseRepository = responseRepository;
        }

        //
        // GET: /Responses/

        public ViewResult Index()
        {
            return View(responseRepository.All);
        }

        //
        // GET: /Responses/Details/5

        public ViewResult Details(int id)
        {
            return View(responseRepository.Find(id));
        }

        //
        // GET: /Responses/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Responses/Create

        [HttpPost]
        public ActionResult Create(Response response)
        {
            if (ModelState.IsValid) {
                responseRepository.InsertOrUpdate(response);
                responseRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Responses/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(responseRepository.Find(id));
        }

        //
        // POST: /Responses/Edit/5

        [HttpPost]
        public ActionResult Edit(Response response)
        {
            if (ModelState.IsValid) {
                responseRepository.InsertOrUpdate(response);
                responseRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Responses/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(responseRepository.Find(id));
        }

        //
        // POST: /Responses/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            responseRepository.Delete(id);
            responseRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

