using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advertising.Data;
using advertising.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace advertising.Controllers
{
    public class AdController : Controller
    {
        private readonly AppDBContext _context;

        public AdController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Ad
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Ads.Include(a => a.Advertiser);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Ad/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.Advertiser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // GET: Ad/Create
        public IActionResult Create()
        {
            ViewData["AdvertiserId"] = new SelectList(_context.Advertisers, "Id", "City");
            return View();
        }

        // POST: Ad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateAdViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var isNewAdvertiser = !(await _context.Advertisers
                    .AnyAsync(adv => adv.SubscriberId == formData.SubscriberId));

                if (isNewAdvertiser)
                {
                    // Insert advertiser into database
                }
                else
                {
                    // Update existing advertiser entry
                }

                // Insert ad with the advertiserID


                _context.Add(new Ad()); // TODO: change to a real Ad
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AdvertiserId"] = new SelectList(_context.Advertisers, "Id", "City", ad.AdvertiserId);
            return View(new Ad()); // TODO: change to a real Ad
        }

        // POST Ad/Create 
        // Used when user inserts subscriber id number in ordere
        // to fetch data from the API about that subscriber.  
        [HttpPost]
        public async Task<IActionResult> GetSubscriber (long SubscriberId) {
            var httpClient = new HttpClientHelper().HttpClient;
            var response = await httpClient.GetAsync($"/api/Subscriber/{SubscriberId}");
            Advertiser result = null;

            if (response.IsSuccessStatusCode) 
            {
                string json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Advertiser>(json);
                return View("Create", new CreateAdViewModel ()
                {
                    Firstname = result.Firstname,
                    Lastname = result.Lastname,
                    PhoneNumber = result.PhoneNumber,
                    DistributionAddress = result.DistributionAddress,
                    ZipCode = result.ZipCode,
                    City = result.City,
                    isOrganization = result.isOrganization,
                    SubscriberId = result.SubscriberId
                    // TODO: Do I need personal ID? 
                });
            }
            else 
            {
                ViewBag.invalidSubscriberId = $"* Ingen prenumerant kunde hittas med ID {SubscriberId}"; 
                return View("Create"); 
            } 
        }


        // GET: Ad/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            ViewData["AdvertiserId"] = new SelectList(_context.Advertisers, "Id", "City", ad.AdvertiserId);
            return View(ad);
        }

        // POST: Ad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Price,Content,Headline,AdCost,AdvertiserId")] Ad ad)
        {
            if (id != ad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.Id))
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
            ViewData["AdvertiserId"] = new SelectList(_context.Advertisers, "Id", "City", ad.AdvertiserId);
            return View(ad);
        }

        // GET: Ad/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.Advertiser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // POST: Ad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var ad = await _context.Ads.FindAsync(id);
            _context.Ads.Remove(ad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdExists(long id)
        {
            return _context.Ads.Any(e => e.Id == id);
        }
    }
}
