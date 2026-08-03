using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nexus_Sports_Center_MVC.Models;
using Nexus_Sports_Center_MVC.Data;

namespace Nexus_Sports_Center_MVC.Controllers;

public class GamesController : Controller
{
    private readonly NexusDbContext _context;

    public GamesController(NexusDbContext context)
    {
        _context = context;
    }

    // GET: Games/Create
    public IActionResult Create()
    {
        // Populate the dropdown lists
        ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name");
        ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name");
        ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Name");
        ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Name");
        return View();
    }

    // POST: Games/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,GameDate,HomeTeamId,AwayTeamId,SportId,VenueId,HomeScore,AwayScore,IsCompleted")] Game game)
    {
        if (ModelState.IsValid)
        {
            if (game.HomeScore.HasValue && game.AwayScore.HasValue)
            {
                game.IsCompleted = true;
            }
            else
            {
                game.IsCompleted = false;
            }

            _context.Add(game);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), "Home");
        }

        // Reload dropdowns if validation fails
        ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.HomeTeamId);
        ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.AwayTeamId);
        ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Name", game.SportId);
        ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Name", game.VenueId);

        return View(game);
    }

    // POST: Games/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,GameDate,HomeTeamId,AwayTeamId,SportId,VenueId,HomeScore,AwayScore,IsCompleted")] Game game)
    {
        // Security check to ensure the URL ID matches the model ID
        if (id != game.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            // Case fixed: lowercase 'game'
            if (game.HomeScore.HasValue && game.AwayScore.HasValue)
            {
                game.IsCompleted = true;
            }
            else
            {
                game.IsCompleted = false;
            }

            // Fixed: Update instead of Add
            _context.Update(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Home");
        } // Fixed: Added missing closing brace

        // Reload dropdowns if validation fails
        ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.HomeTeamId);
        ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.AwayTeamId);
        ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Name", game.SportId);
        ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Name", game.VenueId);

        return View(game);
    }
}