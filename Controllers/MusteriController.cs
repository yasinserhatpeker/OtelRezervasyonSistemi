using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtelRezervasyon.Data;
using OtelRezervasyon.Models;

namespace OtelRezervasyon.Controllers
{
    public class MusteriController : Controller
    {
        private readonly HotelContext _context;

        public MusteriController(HotelContext context)
        {
            _context = context;
        }

        // GET: Musteri/Index
        public async Task<IActionResult> Index()
        {
            var musteriler = await _context.Musteriler.ToListAsync();
            return View(musteriler);
        }

        // GET: Musteri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musteri/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MusteriId,AdSoyad,Telefon,TC")] Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(musteri);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Müşteri başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Müşteri eklenirken bir hata oluştu. Lütfen tekrar deneyiniz. Hata: " + ex.InnerException?.Message);
                }
            }
            return View(musteri);
        }

        // GET: Musteri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musteri = await _context.Musteriler.FindAsync(id);
            if (musteri == null)
            {
                return NotFound();
            }
            return View(musteri);
        }

        // POST: Musteri/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MusteriId,AdSoyad,Telefon,TC")] Musteri musteri)
        {
            if (id != musteri.MusteriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musteri);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Müşteri başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusteriExists(musteri.MusteriId))
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
                    ModelState.AddModelError("", "Müşteri güncellenirken bir hata oluştu. Lütfen tekrar deneyiniz. Hata: " + ex.InnerException?.Message);
                }
            }
            return View(musteri);
        }

        // POST: Musteri/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var musteri = await _context.Musteriler.FindAsync(id);
                if (musteri == null)
                {
                    TempData["ErrorMessage"] = "Müşteri bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Musteriler.Remove(musteri);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Müşteri başarıyla silindi.";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Müşteri silinemedi. Bu müşterinin bir rezervasyonu bulunuyor olabilir. Hata: " + ex.InnerException?.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MusteriExists(int id)
        {
            return _context.Musteriler.Any(e => e.MusteriId == id);
        }
    }
}
