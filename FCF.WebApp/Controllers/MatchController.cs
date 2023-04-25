using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FCF.Entities;
using FCF.Data;

namespace FCF.WebApp.Controllers
{
    public class MatchController : Controller
    {
        private readonly MainDBContext _context;

        public MatchController(MainDBContext context)
        {
            _context = context;
        }

        // GET: Match
        public async Task<IActionResult> Index()
        {
            var MainDBContext = _context.Matches.Include(m => m.Team1).Include(m => m.Team2).Include(m => m.Venue);
            return View(await MainDBContext.ToListAsync());
        }

        // GET: Match/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Match/Create
        public IActionResult Create()
        {
            ViewData["TeamId1"] = new SelectList(_context.Set<Team>(), "TeamId", "Name");
            ViewData["TeamId2"] = new SelectList(_context.Set<Team>(), "TeamId", "Name");
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Address");
            return View();
        }

        // POST: Match/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date_time,TeamId1,TeamId2,VenueId")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId1"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", match.TeamId1);
            ViewData["TeamId2"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", match.TeamId2);
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Address", match.VenueId);
            return View(match);
        }

        // GET: Match/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["TeamId1"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", match.TeamId1);
            ViewData["TeamId2"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", match.TeamId2);
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Address", match.VenueId);
            return View(match);
        }

        // POST: Match/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date_time,TeamId1,TeamId2,VenueId")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            ViewData["TeamId1"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", match.TeamId1);
            ViewData["TeamId2"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", match.TeamId2);
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Address", match.VenueId);
            return View(match);
        }

        // GET: Match/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Matches == null)
            {
                return Problem("Entity set 'MainDBContext.Match'  is null.");
            }
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
          return (_context.Matches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
