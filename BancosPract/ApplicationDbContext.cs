using BancosPract.Entities;
using Microsoft.EntityFrameworkCore;

namespace BancosPract
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bancos> Banco { get; set; }

    }
}