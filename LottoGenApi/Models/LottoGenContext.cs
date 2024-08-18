using Microsoft.EntityFrameworkCore;

namespace LottoGenApi.Models;

public class LottoGenContext(DbContextOptions<LottoGenContext> options) : DbContext(options)
{
    public DbSet<Lottery> Lotteries { get; set; }
}
