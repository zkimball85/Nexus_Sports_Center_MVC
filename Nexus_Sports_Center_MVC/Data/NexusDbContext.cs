using Microsoft.EntityFrameworkCore;
using Nexus_Sports_Center_MVC.Models;

namespace Nexus_Sports_Center_MVC.Data;

public class NexusDbContext : DbContext
{
    public NexusDbContext(DbContextOptions<NexusDbContext> options) : base(options)
    {
    }


}

