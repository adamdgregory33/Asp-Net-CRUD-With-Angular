using CardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Data
{
    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Card> Cards { get; set; }


    }
}
