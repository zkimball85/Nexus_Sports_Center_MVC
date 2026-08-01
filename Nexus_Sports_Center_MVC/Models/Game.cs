using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus_Sports_Center_MVC.Models;

/// <summary>
/// Represents a single game in the Nexus Sports Center application.
/// </summary>
public class Game
{

    /// <summary>
    /// Gets or sets the unique identifier for the game.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the game.
    /// </summary>
    public DateTime GameDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the unique identifier for the home team.
    /// </summary>
    [Required]
    public int HomeTeamId { get; set; }

    /// <summary>
    /// Gets or sets the home team for the game.
    /// </summary>
    public Team HomeTeam { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique identifier for the away team.
    /// </summary>
    [Required]
    public int AwayTeamId { get; set; }

    /// <summary>
    /// Gets or sets the away team for the game.
    /// </summary>

    public Team AwayTeam { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique identifier for the venue where the game is played.
    /// </summary>
    [Required]
    public int VenueId { get; set; }

    /// <summary>
    /// Gets or sets the venue where the game is played.
    /// </summary>

    public Venue Venue { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique identifier for the sport associated with the game.
    /// </summary>
    [Required]
    public int SportId { get; set; }

    /// <summary>
    /// Gets or sets the sport associated with the game.
    /// </summary>

    public Sport Sport { get; set; } = null!;

    /// <summary>
    /// Gets or sets the score of the game.
    /// </summary>
    public GameScore Score { get; set; } = null!;
}

