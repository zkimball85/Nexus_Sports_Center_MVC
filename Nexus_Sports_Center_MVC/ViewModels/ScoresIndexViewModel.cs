using Microsoft.AspNetCore.Mvc.Rendering;
using Nexus_Sports_Center_MVC.Models;

namespace Nexus_Sports_Center_MVC.ViewModels;

/// <summary>
/// ViewModel for the Scores Index page, containing filtering options and a list of completed games.
/// </summary>
public class ScoresIndexViewModel
{
    /// <summary>
    /// Gets or sets the ID of the sport to filter by. If null, all sports are included.
    /// </summary>
    public int? SportId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the team to filter by. If null, all teams are included.
    /// </summary>
    public int? TeamId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the venue to filter by. If null, all venues are included.
    /// </summary>
    public int? VenueId { get; set; }

    /// <summary>
    /// Gets or sets the start date for filtering games. If null, no start date filter is applied.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for filtering games. If null, no end date filter is applied.
    /// </summary>
    public DateTime? EndDate { get; set; }

    // SelectList properties for dropdowns
    public IEnumerable<Game> Games { get; set; } = new List<Game>();

    /// <summary>
    /// Gets or sets the SelectList of sports for filtering. If null, no sports filter is applied.
    /// </summary>
    public SelectList? Sports { get; set; }

    /// <summary>
    /// Gets or sets the SelectList of teams for filtering. If null, no teams filter is applied.
    /// </summary>
    public SelectList? Teams { get; set; }

    /// <summary>
    /// Gets or sets the SelectList of venues for filtering. If null, no venues filter is applied.
    /// </summary>
    public SelectList? Venues { get; set; }
}

