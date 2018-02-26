using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PodcastFeedGenerator.Models;
using PodcastFeedGenerator.Factories;
using PodcastFeedGenerator.ViewModels.Channels;
using PodcastFeedGenerator.Repositories;

namespace PodcastFeedGenerator.Controllers
{
    [Authorize]
    public class ChannelsController : Controller
    {
        private ApplicationDbContext db;
        private ChannelRepository repo;
        private CategoryRepository catRepo;

        public ChannelsController()
        {
            db = new ApplicationDbContext();
            repo = new ChannelRepository(db);
            catRepo = new CategoryRepository(db);
        }

        // GET: Channels
        public ActionResult Index(int feedDefinitionId)
        {
            var channels = repo.GetByFeedDefinition(feedDefinitionId);
            ViewBag.FeedDefinitionID = feedDefinitionId;
            return View(channels.ToViewModels(catRepo));
        }

        // GET: Channels/Details/5
        public ActionResult Details(int? id)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Channel channel = repo.Find(id.Value);

            if (channel == null)
            {
                return HttpNotFound();
            }

            return View(channel.ToViewModel(catRepo));
        }

        // GET: Channels/Create
        public ActionResult Create(int feedDefinitionId)
        {
            var model = ChannelFactory.Create(feedDefinitionId, new FeedDefinitionRepository(db));
            return View(model);
        }

        // POST: Channels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChannelBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var channel = model.ToChannel(catRepo);

                repo.CreateOrUpdate(channel);

                return RedirectToAction("Index", new { feedDefinitionId = model.FeedDefinitionID });
            }

            return View(model);
        }

        // GET: Channels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Channel channel = repo.Find(id.Value);
            if (channel == null)
            {
                return HttpNotFound();
            }

            return View(channel.ToBindingModel(catRepo));
        }

        // POST: Channels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChannelBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var channel = model.ToChannel(catRepo);

                repo.Update(channel);
                
                return RedirectToAction("Index", new { feedDefinitionId = model.FeedDefinitionID });
            }
            return View(model);
        }

        // GET: Channels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Channel channel = db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return View(channel.ToViewModel(catRepo));
        }

        // POST: Channels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int feedDefinitionId = repo.Find(id).FeedDefinitionID;
            repo.Delete(id);
            return RedirectToAction("Index", new { feedDefinitionId = feedDefinitionId });
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
