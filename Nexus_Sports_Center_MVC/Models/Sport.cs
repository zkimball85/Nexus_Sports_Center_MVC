using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Nexus_Sports_Center_MVC.Models;   

namespace Nexus_Sports_Center_MVC.Models;

/// <summary>
/// Represents an individual sport in the Nexus Sports Center application.
/// </summary>
public class Sport
{
    /// <summary>
    /// Gets or sets the unique identifier for the sport.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the sport.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the sport.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the teams that participate in the sport.
    /// </summary>
    public List<Team> Teams { get; set; } = new();

}

