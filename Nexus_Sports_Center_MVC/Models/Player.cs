using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus_Sports_Center_MVC.Models;

/// <summary>
/// Represents a player in the Nexus Sports Center application.
/// </summary>
public class Player
{
    /// <summary>
    /// Gets or sets the unique identifier for the player.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the player.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the player.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the jersey number of the player.
    /// Can be null if the player does not have a jersey number assigned.
    /// </summary>
    public int? JerseyNumber { get; set; }

    /// <summary>
    /// Gets or sets the position of the player.
    /// Can be null if the position is not specified.
    /// </summary>
    public string? Position { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the team the player belongs to.
    /// </summary>
    [Required]
    public int TeamId { get; set; }

    /// <summary>
    /// Gets or sets the team the player belongs to.
    /// </summary>
    public Team? Team { get; set; }
}

