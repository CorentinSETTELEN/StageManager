using StageManager.DAL;
using StageManager.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Rotativa;

namespace StageManager.Controllers
{
    public class BackofficeController : Controller
    {
        private StageManagerContext db = new StageManagerContext();
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        public ActionResult CreateUser()
        {
            return View();
        }

        public ActionResult ManageStagiaires()
        {
            return View(db.Stagiaires.ToList());
        }

        public ActionResult AllNotesFromOne()
        {
            Stagiaire stagiaire = db.Stagiaires.Find(1);
            return View(stagiaire);
        }

        public ActionResult CreatePdf()
        {
            return new ActionAsPdf("AllNotesFromOne"){FileName = "Test.pdf"};
        }

        [HttpPost]
        public ActionResult CreateUser(User newUser)
        {
            var userAlreadyExist = db.Users.SingleOrDefault(user => user.Username == newUser.Username);

            if (userAlreadyExist != null)
            {
                ViewBag.errorRegister = "User with this username already exists";
                return View();
            }

            else
            {
                db.Users.Add(newUser);
                if (newUser.Role.ToString() == "stagiaire")
                {
                    var formationDateStages = new List<DateStage>
                    {
                        new DateStage() {Date = "8/5/2019"}, new DateStage() {Date = "8/5/2019"}
                    };
                    var entrepriseDateStages = new List<DateStage>
                    {
                        new DateStage() {Date = "8/5/2019"}, new DateStage() {Date = "8/5/2019"}
                    };
                    Stagiaire stagiaire = new Stagiaire
                    {
                        User = newUser,
                        StageName = "Nouveau Stage",
                        DateStart = "2/2/2018",
                        DateEnd = "2/2/2020",
                        FormationDates = formationDateStages,
                        EntrepriseDates = entrepriseDateStages

                    };
                    db.Stagiaires.Add(stagiaire);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditStagiaire(int? id)
        {
            Stagiaire stagiaire = db.Stagiaires.Find(id);
            if (stagiaire == null)
            {
                return RedirectToAction("Index");
            }
            return View(stagiaire);
        }

        [HttpPost]
        public ActionResult EditStagiaire(Stagiaire stagiaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stagiaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stagiaire);
        }

        public ActionResult AddDateInFormation(int? id,string newDate)
        {
            Stagiaire stagiaire = db.Stagiaires.Find(id);
            List<DateStage> listDateFormation = stagiaire.FormationDates;
            DateStage newDateStage = new DateStage();
            newDateStage.Date = newDate;
            listDateFormation.Add(newDateStage);
            stagiaire.FormationDates = listDateFormation;
            db.Entry(stagiaire).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("EditStagiaire", new { id = id});
        }

        public ActionResult AddDateInEntreprise(int? id,string newDate)
        {
            Stagiaire stagiaire = db.Stagiaires.Find(id);
            List<DateStage> listDateEntreprise = stagiaire.EntrepriseDates;
            DateStage newDateStage = new DateStage();
            newDateStage.Date = newDate;
            listDateEntreprise.Add(newDateStage);
            stagiaire.EntrepriseDates = listDateEntreprise;
            db.Entry(stagiaire).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("EditStagiaire", new { id = id});
        }

        public ActionResult DeleteDateInFormation(int? id, int idDate)
        {
            Stagiaire stagiaire = db.Stagiaires.Find(id);
            List<DateStage> listDateFormation = stagiaire.FormationDates;
            listDateFormation.RemoveAt(idDate);
            stagiaire.FormationDates = listDateFormation;
            db.Entry(stagiaire).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("EditStagiaire", new { id = id });
        }

        public ActionResult DeleteDateInEntreprise(int? id, int idDate)
        {
            Stagiaire stagiaire = db.Stagiaires.Find(id);
            List<DateStage> listDateEntreprise = stagiaire.EntrepriseDates;
            listDateEntreprise.RemoveAt(idDate);
            stagiaire.EntrepriseDates = listDateEntreprise;
            db.Entry(stagiaire).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("EditStagiaire", new { id = id });
        }

        public ActionResult ReadNote(int? id)
        {
            NoteStagiaire noteStagiaire = db.NotesStagiaires.Find(id);
            if (noteStagiaire == null)
            {
                return RedirectToAction("ManageStagiaires");
            }
            return View(noteStagiaire);
        }

        [HttpPost]
        public ActionResult ReadNote(NoteStagiaire noteStagiaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noteStagiaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noteStagiaire);
        }

    }
}