using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus_Sports_Center_MVC.Models;

/// <summary>
/// Represents a team in the Nexus Sports Center application.
/// </summary>
public class Team
{
    /// <summary>
    /// Gets or sets the unique identifier for the team.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the team.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the sport associated with the team.
    /// </summary>
    [Required]
    [ForeignKey("Sport")]
    public int SportId { get; set; }

    /// <summary>
    /// Gets or sets the sport associated with the team.
    /// </summary>
    [Required]
    public Sport Sport { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique identifier for the venue associated with the team.
    /// </summary>
    public int? VenueId { get; set; }

    /// <summary>
    /// Gets or sets the venue associated with the team.
    /// </summary>
    public Venue? HomeVenue { get; set; }

    /// <summary>
    /// Gets or sets the list of players associated with the team.
    /// </summary>
    public List<Player> Players { get; set; } = new();
}

