using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nexus_Sports_Center_MVC.Data;
using Nexus_Sports_Center_MVC.Models;

namespace Nexus_Sports_Center_MVC.Controllers;

public class ScoresController : Controller
{

    private readonly NexusDbContext _context;

    public ScoresController(NexusDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Displays a list of completed games with optional filtering by sport, team, venue, and date range.
    /// </summary>
    /// <param name="sportId">The ID of the sport to filter by, or null to show all sports.</param>
    /// <param name="teamId">The ID of the team to filter by, or null to show all teams.</param>
    /// <param name="venueId">The ID of the venue to filter by, or null to show all venues.</param>
    /// <param name="startDate">The start date to filter by, or null to show all dates.</param>
    /// <param name="endDate">The end date to filter by, or null to show all dates.</param>
    /// <returns>Result of the action.</returns>
    public async Task<IActionResult> Index(int? sportId, int? teamId, int? venueId, DateTime? startDate, DateTime? endDate)
    {
        // Start with a base query for completed games
        var scoresQuery = _context.Games
            .Include(g => g.HomeTeam)
            .Include(g => g.AwayTeam)
            .Include(g => g.Sport)
            .Include(g => g.Venue)
            .Where(g => g.IsCompleted == true);

        // Apply filters based on the provided parameters
        if (sportId.HasValue)
        {
            scoresQuery = scoresQuery.Where(g => g.SportId == sportId);
        }

        if (teamId.HasValue)
        {
            scoresQuery = scoresQuery.Where(g => g.HomeTeamId == teamId || g.AwayTeamId == teamId);
        }

        if (venueId.HasValue)
        {
            scoresQuery = scoresQuery.Where(g => g.VenueId == venueId);
        }

        if (startDate.HasValue)
        {
            scoresQuery = scoresQuery.Where(s => s.GameDate >= startDate.Value);
        }
        if (endDate.HasValue)
        {
            scoresQuery = scoresQuery.Where(s => s.GameDate <= endDate.Value);
        }

        // Execute the query and get the filtered list of games
        var filteredGames = await scoresQuery.OrderByDescending(g => g.GameDate).ToListAsync();

        // Populate ViewData to build the dropdown menus in the UI
        ViewData["Sports"] = new SelectList(await _context.Sports.ToListAsync(), "Id", "Name", sportId);
        ViewData["Teams"] = new SelectList(await _context.Teams.ToListAsync(), "Id", "Name", teamId);
        ViewData["Venues"] = new SelectList(await _context.Venues.ToListAsync(), "Id", "Name", venueId);

        // Keep the date inputs populated after submitting
        ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd");
        ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");

        return View(filteredGames);
    }
}

