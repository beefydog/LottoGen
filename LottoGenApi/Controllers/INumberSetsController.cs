using Microsoft.AspNetCore.Mvc;
using LottoGenApi.Models;

namespace LottoGenApi.Controllers;

internal interface INumberSetsController
{
    Task<IActionResult> GetAsync(SetsRequest setsRequest);
}