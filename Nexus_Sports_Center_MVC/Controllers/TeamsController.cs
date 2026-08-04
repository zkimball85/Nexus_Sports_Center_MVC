using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus_Sports_Center_MVC.Models;
using Nexus_Sports_Center_MVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nexus_Sports_Center_MVC.ViewModels;

namespace Nexus_Sports_Center_MVC.Controllers;

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
        ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Name");
        return View();
    }

    // POST: TEAMS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,VenueId")] Team team)
    {
        if (ModelState.IsValid)
        {
            _context.Add(team);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Roster), new { id = team.Id });
        }
        ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Name", team.VenueId);
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
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name")] Team team)
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

    // GET: TEAMS/Roster/5
    public async Task<IActionResult> Roster(int? id)
    {
        if(id == null) return NotFound();

        var team = await _context.Teams
            .Include(t => t.HomeVenue)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (team == null) return NotFound();

        var viewModel = new TeamRosterViewModel
        {
            Team = team,
            ExistingPlayers = await _context.Players.Where(p => p.TeamId == id).ToListAsync(),
            NewPlayer = new Player { TeamId = team.Id }
        };

        return View(viewModel);
    }

    public async Task<IActionResult> AddPlayerToRoster(TeamRosterViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _context.Players.Add(viewModel.NewPlayer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Roster), new { id = viewModel.NewPlayer.TeamId });
        }

        viewModel.Team = await _context.Teams.FindAsync(viewModel.NewPlayer.TeamId);
        viewModel.ExistingPlayers = await _context.Players.Where(p => p.TeamId == viewModel.NewPlayer.TeamId).ToListAsync();

        return View("Roster", viewModel);
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
