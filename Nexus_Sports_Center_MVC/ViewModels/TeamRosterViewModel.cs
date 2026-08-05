using Nexus_Sports_Center_MVC.Models;

namespace Nexus_Sports_Center_MVC.ViewModels;

public class TeamRosterViewModel
{
    public Team? Team { get; set; }

    public IEnumerable<Player> ExistingPlayers { get; set; } = new List<Player>();

    public Player NewPlayer { get; set; } = new Player();
}

