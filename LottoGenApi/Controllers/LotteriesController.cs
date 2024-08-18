using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LottoGenApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LottoGenApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LotteriesController(ILogger<LotteriesController> logger, LotteryRepository repository) : ControllerBase
{
    private readonly ILogger<LotteriesController> _logger = logger;
    private readonly LotteryRepository _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        try
        {
            var lotteries = await _repository.GetLotteriesAsync();
            return Ok(lotteries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching lotteries.");
            return StatusCode(500, "Internal server error");
        }
    }
}
