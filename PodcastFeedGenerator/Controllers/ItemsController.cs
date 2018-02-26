using PodcastFeedGenerator.Factories;
using PodcastFeedGenerator.Helpers;
using PodcastFeedGenerator.Models;
using PodcastFeedGenerator.Repositories;
using PodcastFeedGenerator.ViewModels.Items;
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
    public class ItemsController : Controller
    {
        private ApplicationDbContext db;
        private ItemRepository repo;
        private CategoryRepository catRepo;

        public ItemsController()
        {
            db = new ApplicationDbContext();
            repo = new ItemRepository(db);
            catRepo = new CategoryRepository(db);
        }

        // GET: Items
        public ActionResult Index(int channelId)
        {
            var channel = new ChannelRepository(db).Find(channelId);

            ViewBag.ChannelID = channelId;
            ViewBag.FeedDefinitionID = channel.FeedDefinitionID;

            var items = repo.GetByChannel(channelId);
            

            return View(items.ToViewModels(catRepo));
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = repo.Find(id.Value);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item.ToViewModel(catRepo));
        }

        // GET: Items/Create
        public ActionResult Create(int channelId)
        {
            var model = ItemFactory.Create(channelId, new ChannelRepository(db), catRepo);

            return View(model.ToBindingModel(catRepo));
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var item = model.ToItem(catRepo);
                item.PublicationDate = DateTime.Now;
                repo.Create(item);

                return RedirectToAction("Index", new { channelId = model.ChannelID });
            }

            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

            return View(model);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = repo.Find(id.Value);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item.ToBindingModel(catRepo));
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var item = model.ToItem(catRepo);

                repo.Update(item);
                return RedirectToAction("Index", new { channelId = model.ChannelID });
            }
            return View(model);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = repo.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item.ToViewModel(catRepo));
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = repo.Find(id);
            var channelId = item.Channel.ID;
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index", new { channelId = channelId });
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
