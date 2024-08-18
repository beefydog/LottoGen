using Microsoft.AspNetCore.Mvc;
using LottoGenApi.Models;

namespace LottoGenApi.Controllers;

internal interface ILotteriesController
{
    Task<IActionResult> GetAsync();
}
