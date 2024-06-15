using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Models;

namespace WorkoutManager.Data
{
    public class WorkoutManagerContext : DbContext
    {
        public WorkoutManagerContext (DbContextOptions<WorkoutManagerContext> options)
            : base(options)
        {
        }

        public DbSet<WorkoutManager.Models.Lift> Lift { get; set; } = default!;
    }
}
