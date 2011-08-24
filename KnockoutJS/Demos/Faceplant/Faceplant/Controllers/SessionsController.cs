using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Faceplant.Core.Models;
using Faceplant.Models;

namespace Faceplant.Controllers
{   
    public class SessionsController : Controller
    {
		private readonly ITagRepository tagRepository;
		private readonly ISpeakerRepository speakerRepository;
		private readonly ISessionRepository sessionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public SessionsController() : this(new TagRepository(), new SpeakerRepository(), new SessionRepository())
        {
        }

        public SessionsController(ITagRepository tagRepository, ISpeakerRepository speakerRepository, ISessionRepository sessionRepository)
        {
			this.tagRepository = tagRepository;
			this.speakerRepository = speakerRepository;
			this.sessionRepository = sessionRepository;
        }

        //
        // GET: /Sessions/

        public ViewResult Index()
        {
            return View(sessionRepository.AllIncluding(session => session.Tag, session => session.Responses, session => session.Speaker));
        }

        //
        // GET: /Sessions/Details/5

        public ViewResult Details(int id)
        {
            return View(sessionRepository.Find(id));
        }

        //
        // GET: /Sessions/Create

        public ActionResult Create()
        {
			ViewBag.PossibleTags = tagRepository.All;
			ViewBag.PossibleSpeakers = speakerRepository.All;
            return View();
        } 

        //
        // POST: /Sessions/Create

        [HttpPost]
        public ActionResult Create(Session session)
        {
            if (ModelState.IsValid) {
                sessionRepository.InsertOrUpdate(session);
                sessionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleTags = tagRepository.All;
				ViewBag.PossibleSpeakers = speakerRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Sessions/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleTags = tagRepository.All;
			ViewBag.PossibleSpeakers = speakerRepository.All;
             return View(sessionRepository.Find(id));
        }

        //
        // POST: /Sessions/Edit/5

        [HttpPost]
        public ActionResult Edit(Session session)
        {
            if (ModelState.IsValid) {
                sessionRepository.InsertOrUpdate(session);
                sessionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleTags = tagRepository.All;
				ViewBag.PossibleSpeakers = speakerRepository.All;
				return View();
			}
        }

        //
        // GET: /Sessions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(sessionRepository.Find(id));
        }

        //
        // POST: /Sessions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            sessionRepository.Delete(id);
            sessionRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

