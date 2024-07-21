using Microsoft.EntityFrameworkCore;

namespace LottoGenApi.Models
{
    public class LottoGenContext : DbContext
    {
        public LottoGenContext(DbContextOptions<LottoGenContext> options) : base(options) { }

        public DbSet<Lottery> Lotteries { get; set; }
    }
}
