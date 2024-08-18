using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LottoGenApi.Models;

public class LotteryRepository(LottoGenContext context)
{
    private readonly LottoGenContext _context = context;

    public async Task<List<Lottery>> GetLotteriesAsync()
    {
        return await _context.Lotteries.FromSqlRaw("EXEC GetLotteries").ToListAsync();
    }
}
