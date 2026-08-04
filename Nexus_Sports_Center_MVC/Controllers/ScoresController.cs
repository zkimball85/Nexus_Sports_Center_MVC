using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nexus_Sports_Center_MVC.Data;
using Nexus_Sports_Center_MVC.ViewModels;

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
    public async Task<IActionResult> Index(ScoresIndexViewModel viewModel)
    {
        // Start with a base query for completed games
        var scoresQuery = _context.Games
            .AsNoTracking()
            .Include(g => g.HomeTeam)
            .Include(g => g.AwayTeam)
            .Include(g => g.Sport)
            .Include(g => g.Venue)
            .Where(g => g.IsCompleted == true);

        // Apply filters based on the provided parameters
        if (viewModel.SportId.HasValue)
        {
            scoresQuery = scoresQuery.Where(g => g.SportId == viewModel.SportId);
        }

        if (viewModel.TeamId.HasValue)
        {
            scoresQuery = scoresQuery.Where(g => g.HomeTeamId == viewModel.TeamId || g.AwayTeamId == viewModel.TeamId);
        }

        if (viewModel.VenueId.HasValue)
        {
            scoresQuery = scoresQuery.Where(g => g.VenueId == viewModel.VenueId);
        }

        if (viewModel.StartDate.HasValue)
        {
            scoresQuery = scoresQuery.Where(s => s.GameDate >= viewModel.StartDate.Value);
        }
        if (viewModel.EndDate.HasValue)
        {
            var nextDay = viewModel.EndDate.Value.Date.AddDays(1);
            scoresQuery = scoresQuery.Where(g => g.GameDate < nextDay);
        }

        // Execute the query and get the filtered list of games
        var filteredGames = await scoresQuery.OrderByDescending(g => g.GameDate).ToListAsync();

        // Populate ViewData to build the dropdown menus in the UI
        ViewData["Sports"] = new SelectList(await _context.Sports.AsNoTracking().ToListAsync(), "Id", "Name", viewModel.SportId);
        ViewData["Teams"] = new SelectList(await _context.Teams.AsNoTracking().ToListAsync(), "Id", "Name", viewModel.TeamId);
        ViewData["Venues"] = new SelectList(await _context.Venues.AsNoTracking().ToListAsync(), "Id", "Name", viewModel.VenueId);

        // Keep the date inputs populated after submitting
        ViewData["StartDate"] = viewModel.StartDate?.ToString("yyyy-MM-dd");
        ViewData["EndDate"] = viewModel.EndDate?.ToString("yyyy-MM-dd");

        return View(viewModel);
    }
}

