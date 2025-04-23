using Microsoft.EntityFrameworkCore;
using xml_api.Models;

namespace xml_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<XmlOrigin> XmlOrigins { get; set; }
        public DbSet<JsonData> JsonDatas { get; set; }
    }
}
