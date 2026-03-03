using HoteleriaApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HoteleriaApp.Infrastructure.Persistence.Contexto
{
    public class HoteleriaDbContext : DbContext
    {
        public HoteleriaDbContext(DbContextOptions<HoteleriaDbContext> op) : base(op) { }

        public DbSet<Cliente> Cliente { get; set; }
    }
}