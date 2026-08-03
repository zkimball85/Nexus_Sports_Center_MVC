using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus_Sports_Center_MVC.Models;
using Nexus_Sports_Center_MVC.Data;

public class TeamsController : Controller
{
    private readonly NexusDbContext _context;

    public TeamsController(NexusDbContext context)
    {
        _context = context;
    }

    // GET: TEAMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Teams.ToListAsync());
    }

    // GET: TEAMS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var team = await _context.Teams
            .FirstOrDefaultAsync(m => m.Id == id);
        if (team == null)
        {
            return NotFound();
        }

        return View(team);
    }

    // GET: TEAMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TEAMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,SportId,Sport,Players")] Team team)
    {
        if (ModelState.IsValid)
        {
            _context.Add(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(team);
    }

    // GET: TEAMS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var team = await _context.Teams.FindAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        return View(team);
    }

    // POST: TEAMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,SportId,Sport,Players")] Team team)
    {
        if (id != team.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(team);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(team.Id))
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
        return View(team);
    }

    // GET: TEAMS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var team = await _context.Teams
            .FirstOrDefaultAsync(m => m.Id == id);
        if (team == null)
        {
            return NotFound();
        }

        return View(team);
    }

    // POST: TEAMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team != null)
        {
            _context.Teams.Remove(team);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TeamExists(int? id)
    {
        return _context.Teams.Any(e => e.Id == id);
    }
}
