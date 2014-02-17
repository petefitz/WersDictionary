using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WersDictionary.Models;

namespace WersDictionary.Controllers
{
    public class WersDictionaryController : Controller
    {
        private WersItemDBContext db = new WersItemDBContext();

        //
        // GET: /WersDictionary/

        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        //
        // GET: /WersDictionary/Details/5

        public ActionResult Details(int id = 0)
        {
            WersItem wersitem = db.Movies.Find(id);
            if (wersitem == null)
            {
                return HttpNotFound();
            }
            return View(wersitem);
        }

        //
        // GET: /WersDictionary/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WersDictionary/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WersItem wersitem)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(wersitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wersitem);
        }

        //
        // GET: /WersDictionary/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WersItem wersitem = db.Movies.Find(id);
            if (wersitem == null)
            {
                return HttpNotFound();
            }
            return View(wersitem);
        }

        //
        // POST: /WersDictionary/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WersItem wersitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wersitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wersitem);
        }

        //
        // GET: /WersDictionary/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WersItem wersitem = db.Movies.Find(id);
            if (wersitem == null)
            {
                return HttpNotFound();
            }
            return View(wersitem);
        }

        //
        // POST: /WersDictionary/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WersItem wersitem = db.Movies.Find(id);
            db.Movies.Remove(wersitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SearchIndex(string movieGenre, string searchString)
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.FamilyCode
                           select d.FamilyCode;
            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.FamilyDescription.Contains(searchString));
            }

            if (string.IsNullOrEmpty(movieGenre))
                return View(movies);
            else
            {
                return View(movies.Where(x => x.FamilyCode == movieGenre));
            }

        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}