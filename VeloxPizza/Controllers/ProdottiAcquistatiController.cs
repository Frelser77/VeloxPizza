using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VeloxPizza.Models;

namespace VeloxPizza.Controllers
{
    public class ProdottiAcquistatiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: ProdottiAcquistati
        public ActionResult Index()
        {
            var prodottoAcquistato = db.ProdottoAcquistato.Include(p => p.Ordine).Include(p => p.Prodotto);
            return View(prodottoAcquistato.ToList());
        }

        // GET: ProdottiAcquistati/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdottoAcquistato prodottoAcquistato = db.ProdottoAcquistato.Find(id);
            if (prodottoAcquistato == null)
            {
                return HttpNotFound();
            }
            return View(prodottoAcquistato);
        }

        // GET: ProdottiAcquistati/Create
        public ActionResult Create()
        {
            ViewBag.IdOrdine = new SelectList(db.Ordine, "IdOrdine", "IndirizzoDiConsegna");
            ViewBag.IdProdotto = new SelectList(db.Prodotto, "IdProdotto", "NomeProdotto");
            return View();
        }

        // POST: ProdottiAcquistati/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrdine,IdProdotto,Quantita")] ProdottoAcquistato prodottoAcquistato)
        {
            if (ModelState.IsValid)
            {
                db.ProdottoAcquistato.Add(prodottoAcquistato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdOrdine = new SelectList(db.Ordine, "IdOrdine", "IndirizzoDiConsegna", prodottoAcquistato.IdOrdine);
            ViewBag.IdProdotto = new SelectList(db.Prodotto, "IdProdotto", "NomeProdotto", prodottoAcquistato.IdProdotto);
            return View(prodottoAcquistato);
        }

        // GET: ProdottiAcquistati/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdottoAcquistato prodottoAcquistato = db.ProdottoAcquistato.Find(id);
            if (prodottoAcquistato == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOrdine = new SelectList(db.Ordine, "IdOrdine", "IndirizzoDiConsegna", prodottoAcquistato.IdOrdine);
            ViewBag.IdProdotto = new SelectList(db.Prodotto, "IdProdotto", "NomeProdotto", prodottoAcquistato.IdProdotto);
            return View(prodottoAcquistato);
        }

        // POST: ProdottiAcquistati/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProdottoAcquistato,IdOrdine,IdProdotto,Quantita")] ProdottoAcquistato prodottoAcquistato)
        {
            ModelState.Remove("IdProdottoAcquistato");
            if (ModelState.IsValid)
            {
                db.Entry(prodottoAcquistato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdOrdine = new SelectList(db.Ordine, "IdOrdine", "IndirizzoDiConsegna", prodottoAcquistato.IdOrdine);
            ViewBag.IdProdotto = new SelectList(db.Prodotto, "IdProdotto", "NomeProdotto", prodottoAcquistato.IdProdotto);
            return View(prodottoAcquistato);
        }

        // GET: ProdottiAcquistati/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdottoAcquistato prodottoAcquistato = db.ProdottoAcquistato.Find(id);
            if (prodottoAcquistato == null)
            {
                return HttpNotFound();
            }
            return View(prodottoAcquistato);
        }

        // POST: ProdottiAcquistati/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProdottoAcquistato prodottoAcquistato = db.ProdottoAcquistato.Find(id);
            db.ProdottoAcquistato.Remove(prodottoAcquistato);
            db.SaveChanges();
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
