using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using StageManager.DAL;
using StageManager.Models;

namespace StageManager.Controllers
{
    public class StagiaireController : Controller
    {

        private StageManagerContext db = new StageManagerContext();
        public ActionResult Index()
        {
            Stagiaire stagiaire = db.Stagiaires.Find(Session["id"]);
            if (stagiaire == null)
            {
                return RedirectToAction("Index");
            }
            return View(stagiaire);
        }

        public ActionResult AddNote()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNote(NoteStagiaire noteStagiaire)
        {
            Stagiaire stagiaire = db.Stagiaires.Find(Session["id"]);
            List<NoteStagiaire> listNotes = stagiaire.ListNoteStagiaire;
            listNotes.Add(noteStagiaire);
            stagiaire.ListNoteStagiaire = listNotes;
            db.Entry(stagiaire).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}