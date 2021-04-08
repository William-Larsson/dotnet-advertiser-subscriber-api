using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advertising.Data;
using advertising.Models;

namespace advertising.Controllers
{
    public class AdvertiserController : Controller
    {
        private readonly AppDBContext _context;

        public AdvertiserController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Advertiser
        public async Task<IActionResult> Index()
        {
            return View(await _context.Advertisers.ToListAsync());
        }

        // GET: Advertiser/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertiser = await _context.Advertisers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertiser == null)
            {
                return NotFound();
            }

            return View(advertiser);
        }

        // GET: Advertiser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advertiser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,DistributionAddress,ZipCode,City,InvoiceAddress,InvoiceZipCode,InvoiceCity,isOrganization,Lastname,SubscriberId,OrganizationNumber")] Advertiser advertiser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertiser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertiser);
        }

        // GET: Advertiser/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertiser = await _context.Advertisers.FindAsync(id);
            if (advertiser == null)
            {
                return NotFound();
            }
            return View(advertiser);
        }

        // POST: Advertiser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,PhoneNumber,DistributionAddress,ZipCode,City,InvoiceAddress,InvoiceZipCode,InvoiceCity,isOrganization,Lastname,SubscriberId,OrganizationNumber")] Advertiser advertiser)
        {
            if (id != advertiser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertiser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertiserExists(advertiser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(advertiser);
        }

        // GET: Advertiser/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertiser = await _context.Advertisers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertiser == null)
            {
                return NotFound();
            }

            return View(advertiser);
        }

        // POST: Advertiser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var advertiser = await _context.Advertisers.FindAsync(id);
            _context.Advertisers.Remove(advertiser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertiserExists(long id)
        {
            return _context.Advertisers.Any(e => e.Id == id);
        }
    }
}
