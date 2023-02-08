using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using ShortenIt.Data;
using ShortenIt.Models;

namespace ShortenIt.Controllers
{
    public class LinkController : Controller
    {
        private readonly LinkContext _context;
        private readonly ILogger _logger;

        public LinkController(LinkContext context, ILogger<LinkController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string? id)
        {
            if (id is null) return View();
            else
            {
                var link = await _context.Link.FirstOrDefaultAsync(l => l.Id == id);
                return link is null ? NotFound() : Redirect(link.Url);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,Url")] Link link)
        {
            if (ModelState.IsValid)
            {
                link.Id = RandomString();
                _context.Add(link);
                await _context.SaveChangesAsync();
                return Redirect($"~/Link/Details/{link.Id}");
            }
            return View();
        }
        string RandomString()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 20)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Link == null)
            {
                return NotFound();
            }

            var link = await _context.Link
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            ViewBag.url = $"{this.Request.Host}";
            return View(link);
        }

        private bool LinkExists(string id)
        {
            return (_context.Link?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
