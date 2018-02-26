using PodcastFeedGenerator.Factories;
using PodcastFeedGenerator.Models;
using PodcastFeedGenerator.ViewModels.FeedDefinitions;
using PodcastFeedGenerator.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace PodcastFeedGenerator.Controllers
{
    [Authorize]
    public class FeedDefinitionsController : Controller
    {
        private ApplicationDbContext db;
        private FeedDefinitionRepository repo;

        public FeedDefinitionsController()
        {
            db = new ApplicationDbContext();
            repo = new FeedDefinitionRepository(db);
        }

        // GET: FeedDefinitions
        public ActionResult Index()
        {
            var feedDefintions = repo.All();
            return View(feedDefintions.ToViewModels());
        }

        // GET: FeedDefinitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FeedDefinition feedDefinition = repo.Find(id.Value);

            if (feedDefinition == null)
            {
                return HttpNotFound();
            }

            return View(feedDefinition.ToViewModel());
        }

        // GET: FeedDefinitions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeedDefinitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeedDefinitionBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var feedDefinition = model.ToFeedDefinition();
                repo.Create(feedDefinition);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: FeedDefinitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FeedDefinition feedDefinition = repo.Find(id.Value);

            if (feedDefinition == null)
            {
                return HttpNotFound();
            }

            return View(feedDefinition.ToBindingModel());
        }

        // POST: FeedDefinitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FeedDefinitionBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var feedDefinition = bindingModel.ToFeedDefinition();
                repo.Update(feedDefinition);
                return RedirectToAction("Index");
            }
            return View(bindingModel);
        }

        // GET: FeedDefinitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedDefinition feedDefinition = repo.Find(id.Value);
            if (feedDefinition == null)
            {
                return HttpNotFound();
            }
            return View(feedDefinition.ToViewModel());
        }

        // POST: FeedDefinitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
