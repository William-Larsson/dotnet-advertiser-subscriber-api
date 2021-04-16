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
        private readonly int _adPriceSubscriber = 0;
        private readonly int _adPriceCompany = 40;

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
        public IActionResult CreateAd()
        {
            ViewData["AdvertiserId"] = new SelectList(_context.Advertisers, "Id", "City");
            return View();
        }

        // POST: Ad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdSubscriber([FromForm] CreateAdViewModel formData)
        {
            if (ModelState.IsValid)
            {
                Advertiser existingAdvertiser = _context.Advertisers
                    .Where(adv => adv.SubscriberId == formData.SubscriberId)
                    .FirstOrDefault();

                Advertiser advertiser;    

                if (existingAdvertiser == null) 
                {
                    advertiser = new Advertiser () // OBS!!
                    {
                        Id = existingAdvertiser == null ? 0 : existingAdvertiser.Id,
                        PhoneNumber = formData.PhoneNumber,
                        DistributionAddress = formData.DistributionAddress,
                        ZipCode = formData.ZipCode,
                        City = formData.City,
                        InvoiceAddress = formData.DistributionAddress,
                        InvoiceZipCode = formData.ZipCode,
                        InvoiceCity = formData.City,
                        isOrganization = false,
                        PersonalId = formData.PersonalId,
                        Firstname = formData.Firstname,
                        Lastname = formData.Lastname,
                        SubscriberId = formData.SubscriberId
                    };
                    _context.Add(advertiser); // Insert new advertiser into database
                }
                else
                {            
                    advertiser = existingAdvertiser; // OBS!!
                    existingAdvertiser.PhoneNumber = formData.PhoneNumber;
                    existingAdvertiser.DistributionAddress = formData.DistributionAddress;
                    existingAdvertiser.ZipCode = formData.ZipCode;
                    existingAdvertiser.City = formData.City;
                    existingAdvertiser.InvoiceAddress = formData.DistributionAddress;
                    existingAdvertiser.InvoiceZipCode = formData.ZipCode;
                    existingAdvertiser.InvoiceCity = formData.City;
                    existingAdvertiser.PersonalId = formData.PersonalId;
                    existingAdvertiser.Firstname = formData.Firstname;
                    existingAdvertiser.Lastname = formData.Lastname;

                    _context.Entry(existingAdvertiser).State = EntityState.Modified; // Update existing advertiser entry
                }

                await _context.SaveChangesAsync();

                _context.Add(new Ad() // Insert ad with the advertiserID
                {
                    Price = formData.Price,
                    Content = formData.Content,
                    Headline = formData.Headline,
                    AdCost = _adPriceSubscriber,
                    AdvertiserId = advertiser.Id
                }); 

                await _context.SaveChangesAsync();

                // DB operations are done, check if subscriber data should be updated
                if (formData.updateSubscriberAPIInfo)
                {
                    var httpClientHelper = new HttpClientHelper();
                    httpClientHelper.UpdateSubscriber(formData);
                }

                return RedirectToAction(nameof(Index));
            }
                
            return View(); 
        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult CreateAdCompany([FromForm] CreateAdViewModel formData)
        {
            return View();
        }

        // POST Ad/Create 
        // Used when user inserts subscriber id number in order
        // to fetch data from the API about that subscriber.  
        [HttpPost]
        public IActionResult GetSubscriber (long subscriberId) {
            var httpClientHelper = new HttpClientHelper();
            var subscriber = httpClientHelper.GetSubscriber(subscriberId).Result;

            if (subscriber != null) 
                return View("CreateAd", subscriber);
            else 
            {
                ViewBag.invalidSubscriberId = $"* Ingen prenumerant kunde hittas med ID {subscriberId}"; 
                return View("CreateAd"); 
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
