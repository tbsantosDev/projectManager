using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Models;
using ProjectManager.Models;

namespace ProjectManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {     
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TeamModel> Teams { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<TeamMemberModel> TeamMembers { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<CostModel> Costs { get; set; }
        public DbSet<CommentModel> Comments { get; set; }

    }
}
