using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VotacionesApi.Controllers
{
    public class VoterController : Controller
    {
        // GET: VoterController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VoterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VoterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VoterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VoterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VoterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VoterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VoterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
