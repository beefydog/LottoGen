using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LottoGenApi.Models
{
    public class LotteryRepository
    {
        private readonly LottoGenContext _context;

        public LotteryRepository(LottoGenContext context)
        {
            _context = context;
        }

        public async Task<List<Lottery>> GetLotteriesAsync()
        {
            return await _context.Lotteries.FromSqlRaw("EXEC GetLotteries").ToListAsync();
        }
    }
}
