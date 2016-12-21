using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ranaitfleur.Model
{
    public class RanaitfleurContext : DbContext
    {
        private readonly IConfigurationRoot _config;
        public DbSet<Item> Items { get; set; }

        public RanaitfleurContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:RanaitfleurContextConnection"]);
        }
    }
}
