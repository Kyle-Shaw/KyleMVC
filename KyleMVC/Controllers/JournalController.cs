using KyleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace KyleMVC.Controllers
{
    public class JournalController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Journal
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Editor() {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Submit(JournalModels journal) {
            if (ModelState.IsValid)
            {
                //Grabs Usermodel ID
                var usrID = User.Identity.GetUserId();
                journal.UserID = db.UserAccounts.Where(c => c.ApplicationUserId ==
                usrID).First().UserID;
                //...................
                db.Journals.Add(journal);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [Authorize]
        public ActionResult List()
        {
            IEnumerable<JournalModels> journals;
            if (ModelState.IsValid)
            {
                //Grabs Usermodel ID
                var usrID = User.Identity.GetUserId();
                var user = db.UserAccounts.Where(c => c.ApplicationUserId ==
                usrID).First().UserID;
                //...................

                journals = db.Journals.Where(c => c.UserID == user);
                return View(journals);
            }
            return View();
        }

        [ActionName("explore")]
        public ActionResult ListAllUsers()
        {
            IEnumerable<JournalModels> journals;
            if (ModelState.IsValid) {
                journals = db.Journals.ToList().OrderByDescending(c => c.Date);
                journals = journals.Where((a, b) => b >= 0 && b < 5);
                return View(journals);
            }
            return View();
        }

        [Authorize]
        public ActionResult Delete(int id, DateTime date) {
            try
            {
                var journal = db.Journals.Where(c => c.UserID == id && c.Date == date).First();
                db.Journals.Remove(journal);
                db.SaveChanges();
            }
            catch (ArgumentNullException e) {
                Console.Error.WriteLine($"Could not find the attempted journal to be" +
                    $"deleted: {e}");
                throw e;
                //TODO Alert User journal doesnt exist GUI and a error occured.
            }
            return RedirectToAction("List");
        }

        public ActionResult SearchDate(DateTime date) {
            var journals = db.Journals.ToList().Where(c => c.Date == date)
                .OrderByDescending(c => c.Date);
            return View("Explore", journals);
        }

        public ActionResult Details(DateTime journalDate, int user) {
            var journal = db.Journals.Where(j => j.Date == journalDate 
                && j.UserID == user).First();
            return View(journal);
        }

        public ActionResult update(DateTime journalDate, int user) {
            var journal = db.Journals.Where(j => j.Date == journalDate
                && j.UserID == user).First();
            return View(journal);
        }

        [HttpPost]
        public ActionResult update(JournalModels journal) {
            if (ModelState.IsValid)
            {
                db.Journals.Remove(db.Journals.Find(journal.UserID, journal.Date));
                db.Journals.Add(journal);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        [ActionName("explore")]
        public ActionResult ListAllUsers(DateTime? date, string numOfItems, string orderBy, int page = 1) {
            IEnumerable<JournalModels> journals;
            if (ModelState.IsValid)
            {
                int loadend = (int.Parse(numOfItems)) * page;
                int loadstart = loadend - (int.Parse(numOfItems));

                if (date != null)
                {
                    journals = db.Journals.ToList().Where(c => c.Date == date);
                }
                else {
                    journals = db.Journals.ToList();
                }

                if (orderBy.Equals("newest")) 
                {
                    journals = journals.OrderByDescending((c) => c.Date);
                }
                else {
                    journals = journals.ToList().OrderBy(c => c.Date);
                }
                journals = journals.Where((a,b) => b >= loadstart && b < loadend);
                return View(journals);
            }
            return View();
        }
        

    }
}