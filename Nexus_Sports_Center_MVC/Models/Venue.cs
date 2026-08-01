using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Nexus_Sports_Center_MVC.Models;

namespace Nexus_Sports_Center_MVC.Models;

/// <summary>
/// Represents a single venue in the Nexus Sports Center application.
/// </summary>
public class Venue
{
    /// <summary>
    /// Gets or sets the unique identifier for the venue.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the venue.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the location of the venue.
    /// </summary>
    public string Location { get; set; }
}

