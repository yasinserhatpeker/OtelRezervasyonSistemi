using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtelRezervasyon.Data;
using OtelRezervasyon.Models;

namespace OtelRezervasyon.Controllers
{
    public class OdaController : Controller
    {
        private readonly HotelContext _context;

        public OdaController(HotelContext context)
        {
            _context = context;
        }

        // GET: Oda/Index
        public async Task<IActionResult> Index()
        {
            var odalar = await _context.Odalar.ToListAsync();
            return View(odalar);
        }

        // GET: Oda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Oda/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OdaId,OdaNumarasi,OdaTipi,Fiyat")] Oda oda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(oda);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Oda başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Oda eklenirken bir hata oluştu. Lütfen tekrar deneyiniz. Hata: " + ex.InnerException?.Message);
                }
            }
            return View(oda);
        }

        // GET: Oda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oda = await _context.Odalar.FindAsync(id);
            if (oda == null)
            {
                return NotFound();
            }
            return View(oda);
        }

        // POST: Oda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OdaId,OdaNumarasi,OdaTipi,Fiyat")] Oda oda)
        {
            if (id != oda.OdaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oda);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Oda başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdaExists(oda.OdaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Oda güncellenirken bir hata oluştu. Lütfen tekrar deneyiniz. Hata: " + ex.InnerException?.Message);
                }
            }
            return View(oda);
        }

        // POST: Oda/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oda = await _context.Odalar.FindAsync(id);
                if (oda == null)
                {
                    TempData["ErrorMessage"] = "Oda bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Odalar.Remove(oda);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Oda başarıyla silindi.";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Oda silinemedi. Bu oda bir rezervasyonda kullanılıyor olabilir. Hata: " + ex.InnerException?.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool OdaExists(int id)
        {
            return _context.Odalar.Any(e => e.OdaId == id);
        }
    }
}
