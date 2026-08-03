using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nexus_Sports_Center_MVC.Models;
using Nexus_Sports_Center_MVC.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


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
        // Populate the dropdown lists for HomeTeamId and AwayTeamId
        ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name");
        ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name");
        return View();
    }

    // POST: Games/Create
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create([Bind("Id,HomeTeamId,AwayTeamId,GameDate,SportId,VenueId,HomeScore,AwayScore,IsCompleted")] Game game)
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
        ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.HomeTeamId);
        ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.AwayTeamId);
        return View(game);
    }

}

