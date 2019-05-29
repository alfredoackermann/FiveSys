using Microsoft.EntityFrameworkCore;

namespace FiveSysRetarguarda.Infrastructure.Data
{
    public class RetaguardaContext : DbContext
    {
        public RetaguardaContext (DbContextOptions<RetaguardaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
