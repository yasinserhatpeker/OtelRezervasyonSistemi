using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OtelRezervasyon.Data;
using OtelRezervasyon.Models;

namespace OtelRezervasyon.Controllers
{
    public class RezervasyonController : Controller
    {
        private readonly HotelContext _context;

        public RezervasyonController(HotelContext context)
        {
            _context = context;
        }

        // GET: Rezervasyon/Index
        public async Task<IActionResult> Index()
        {
            var rezervasyonlar = await _context.Rezervasyonlar
                .Include(r => r.Musteri)
                .Include(r => r.Oda)
                .ToListAsync();
            return View(rezervasyonlar);
        }

        // GET: Rezervasyon/Create
        public async Task<IActionResult> Create()
        {
            // Rezervasyonu olmayan müşterileri getir
            var rezerveEdilmemisMusteriIdler = await _context.Rezervasyonlar
                .Select(r => r.MusteriId)
                .ToListAsync();

            var uygunMusteriler = await _context.Musteriler
                .Where(m => !rezerveEdilmemisMusteriIdler.Contains(m.MusteriId))
                .ToListAsync();

            // Rezerve edilmemiş odaları getir
            var rezerveEdilmisOdaIdler = await _context.Rezervasyonlar
                .Select(r => r.OdaId)
                .ToListAsync();

            var uygunOdalar = await _context.Odalar
                .Where(o => !rezerveEdilmisOdaIdler.Contains(o.OdaId))
                .ToListAsync();

            ViewBag.MusteriId = new SelectList(uygunMusteriler, "MusteriId", "AdSoyad");
            ViewBag.OdaId = new SelectList(uygunOdalar, "OdaId", "OdaNumarasi");

            return View();
        }

        // POST: Rezervasyon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RezervasyonId,MusteriId,OdaId,GirisTarihi,CikisTarihi")] Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                // Tarih kontrolü
                if (rezervasyon.CikisTarihi <= rezervasyon.GirisTarihi)
                {
                    ModelState.AddModelError("CikisTarihi", "Çıkış tarihi giriş tarihinden sonra olmalıdır.");
                }
                else
                {
                    try
                    {
                        _context.Add(rezervasyon);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Rezervasyon başarıyla oluşturuldu.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException ex)
                    {
                        // Unique constraint ihlali kontrolü
                        if (ex.InnerException?.Message.Contains("IX_Rezervasyonlar_MusteriId") == true)
                        {
                            ModelState.AddModelError("MusteriId", "Bu müşterinin zaten bir rezervasyonu bulunmaktadır.");
                        }
                        else if (ex.InnerException?.Message.Contains("IX_Rezervasyonlar_OdaId") == true)
                        {
                            ModelState.AddModelError("OdaId", "Bu oda zaten rezerve edilmiştir.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Rezervasyon oluşturulurken bir hata oluştu. Lütfen tekrar deneyiniz. Hata: " + ex.InnerException?.Message);
                        }
                    }
                }
            }

            // Hata durumunda dropdown listelerini tekrar doldur
            var rezerveEdilmemisMusteriIdler = await _context.Rezervasyonlar
                .Select(r => r.MusteriId)
                .ToListAsync();

            var uygunMusteriler = await _context.Musteriler
                .Where(m => !rezerveEdilmemisMusteriIdler.Contains(m.MusteriId))
                .ToListAsync();

            var rezerveEdilmisOdaIdler = await _context.Rezervasyonlar
                .Select(r => r.OdaId)
                .ToListAsync();

            var uygunOdalar = await _context.Odalar
                .Where(o => !rezerveEdilmisOdaIdler.Contains(o.OdaId))
                .ToListAsync();

            ViewBag.MusteriId = new SelectList(uygunMusteriler, "MusteriId", "AdSoyad", rezervasyon.MusteriId);
            ViewBag.OdaId = new SelectList(uygunOdalar, "OdaId", "OdaNumarasi", rezervasyon.OdaId);

            return View(rezervasyon);
        }

        // GET: Rezervasyon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervasyon = await _context.Rezervasyonlar.FindAsync(id);
            if (rezervasyon == null)
            {
                return NotFound();
            }

            // Edit işleminde mevcut müşteri ve odayı da listeye ekle
            var rezerveEdilmemisMusteriIdler = await _context.Rezervasyonlar
                .Where(r => r.RezervasyonId != id) // Mevcut rezervasyonu hariç tut
                .Select(r => r.MusteriId)
                .ToListAsync();

            var uygunMusteriler = await _context.Musteriler
                .Where(m => !rezerveEdilmemisMusteriIdler.Contains(m.MusteriId))
                .ToListAsync();

            var rezerveEdilmisOdaIdler = await _context.Rezervasyonlar
                .Where(r => r.RezervasyonId != id) // Mevcut rezervasyonu hariç tut
                .Select(r => r.OdaId)
                .ToListAsync();

            var uygunOdalar = await _context.Odalar
                .Where(o => !rezerveEdilmisOdaIdler.Contains(o.OdaId))
                .ToListAsync();

            ViewBag.MusteriId = new SelectList(uygunMusteriler, "MusteriId", "AdSoyad", rezervasyon.MusteriId);
            ViewBag.OdaId = new SelectList(uygunOdalar, "OdaId", "OdaNumarasi", rezervasyon.OdaId);

            return View(rezervasyon);
        }

        // POST: Rezervasyon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RezervasyonId,MusteriId,OdaId,GirisTarihi,CikisTarihi")] Rezervasyon rezervasyon)
        {
            if (id != rezervasyon.RezervasyonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Tarih kontrolü
                if (rezervasyon.CikisTarihi <= rezervasyon.GirisTarihi)
                {
                    ModelState.AddModelError("CikisTarihi", "Çıkış tarihi giriş tarihinden sonra olmalıdır.");
                }
                else
                {
                    try
                    {
                        _context.Update(rezervasyon);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Rezervasyon başarıyla güncellendi.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RezervasyonExists(rezervasyon.RezervasyonId))
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
                        // Unique constraint ihlali kontrolü
                        if (ex.InnerException?.Message.Contains("IX_Rezervasyonlar_MusteriId") == true)
                        {
                            ModelState.AddModelError("MusteriId", "Bu müşterinin zaten bir rezervasyonu bulunmaktadır.");
                        }
                        else if (ex.InnerException?.Message.Contains("IX_Rezervasyonlar_OdaId") == true)
                        {
                            ModelState.AddModelError("OdaId", "Bu oda zaten rezerve edilmiştir.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Rezervasyon güncellenirken bir hata oluştu. Lütfen tekrar deneyiniz. Hata: " + ex.InnerException?.Message);
                        }
                    }
                }
            }

            // Hata durumunda dropdown listelerini tekrar doldur
            var rezerveEdilmemisMusteriIdler = await _context.Rezervasyonlar
                .Where(r => r.RezervasyonId != id)
                .Select(r => r.MusteriId)
                .ToListAsync();

            var uygunMusteriler = await _context.Musteriler
                .Where(m => !rezerveEdilmemisMusteriIdler.Contains(m.MusteriId))
                .ToListAsync();

            var rezerveEdilmisOdaIdler = await _context.Rezervasyonlar
                .Where(r => r.RezervasyonId != id)
                .Select(r => r.OdaId)
                .ToListAsync();

            var uygunOdalar = await _context.Odalar
                .Where(o => !rezerveEdilmisOdaIdler.Contains(o.OdaId))
                .ToListAsync();

            ViewBag.MusteriId = new SelectList(uygunMusteriler, "MusteriId", "AdSoyad", rezervasyon.MusteriId);
            ViewBag.OdaId = new SelectList(uygunOdalar, "OdaId", "OdaNumarasi", rezervasyon.OdaId);

            return View(rezervasyon);
        }

        // POST: Rezervasyon/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var rezervasyon = await _context.Rezervasyonlar.FindAsync(id);
                if (rezervasyon == null)
                {
                    TempData["ErrorMessage"] = "Rezervasyon bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Rezervasyonlar.Remove(rezervasyon);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Rezervasyon başarıyla silindi.";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Rezervasyon silinemedi. Lütfen tekrar deneyiniz. Hata: " + ex.InnerException?.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RezervasyonExists(int id)
        {
            return _context.Rezervasyonlar.Any(e => e.RezervasyonId == id);
        }
    }
}
