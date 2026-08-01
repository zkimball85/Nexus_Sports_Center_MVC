using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus_Sports_Center_MVC.Models;

/// <summary>
/// Represents the score of a game in the Nexus Sports Center application.
/// </summary>
public class GameScore
{
    /// <summary>
    /// Gets or sets the unique identifier for the game score.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the game associated with this score.
    /// </summary>
    [Required]
    public int GameId { get; set; }

    /// <summary>
    /// Gets or sets the game associated with this score.
    /// </summary>
    [Required]
    public Game? Game { get; set; }

    /// <summary>
    /// Gets or sets the score of the home team in the game.
    /// </summary>
    [Required]
    public int HomeTeamScore { get; set; }

    /// <summary>
    /// Gets or sets the score of the away team in the game.
    /// </summary>
    [Required]
    public int AwayTeamScore { get; set; }

    /// <summary>
    /// Gets or sets any additional notes or comments related to the game score.
    /// Can be null if there are no notes.
    /// </summary>
    [MaxLength(500)]
    public string? Notes { get; set; }
}

