using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Faceplant.Core.Models;
using Faceplant.Models;

namespace Faceplant.Controllers
{   
	public class SpeakersController : Controller
	{
		private readonly ISpeakerRepository speakerRepository;

		// If you are using Dependency Injection, you can delete the following constructor
		public SpeakersController() : this(new SpeakerRepository())
		{
		}

		public SpeakersController(ISpeakerRepository speakerRepository)
		{
			this.speakerRepository = speakerRepository;
		}

		//
		// GET: /Speakers/

		public ViewResult Index()
		{
			var speakers = speakerRepository.AllIncluding(speaker => speaker.Sessions).ToList<Speaker>();
			
			return View(speakers);
		}

		//
		// GET: /Speakers/Details/5

		public ViewResult Details(int id)
		{
			var speaker = speakerRepository.Find(id);
			
			return View(speaker);
		}

		//
		// GET: /Speakers/Create

		public ActionResult Create()
		{
			return View();
		} 

		//
		// POST: /Speakers/Create

		[HttpPost]
		public ActionResult Create(Speaker speaker)
		{
			if (ModelState.IsValid) {
				speakerRepository.InsertOrUpdate(speaker);
				speakerRepository.Save();
				return Json("Speaker information saved!");
			} else {
				return View();
			}
		}
		
		//
		// GET: /Speakers/Edit/5
 
		public ActionResult Edit(int id)
		{
			var speaker = (Speaker)speakerRepository.Find(id);
			return View(speaker);
		}

		//
		// POST: /Speakers/Edit/5

		[HttpPost]
		public ActionResult Edit(Speaker speaker)
		{
			if (ModelState.IsValid) {
				speakerRepository.InsertOrUpdate(speaker);
				speakerRepository.Save();
				return RedirectToAction("Index");
			} else {
				return View();
			}
		}

		//
		// GET: /Speakers/Delete/5
 
		public ActionResult Delete(int id)
		{
			var speaker = speakerRepository.Find(id);
			return View(speaker);
		}

		//
		// POST: /Speakers/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			speakerRepository.Delete(id);
			speakerRepository.Save();

			return RedirectToAction("Index");
		}
	}
}

