using api.Specifications;
using Core.Entities;
using Core.ViewModel;
using Framework.Core.Controllers;
using Framework.Core.Error;
using Framework.Core.Helpers.Pagination;
using Framework.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class GamesController : BaseApiController
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] GameParams prm)
    {
        var result = await _gameService.LoadAsync(prm.Search!, prm.PageIndex, prm.PageSize);

        var response = new Pagination<Game>(result.CurrentPage, result.PageSize, result.TotalCount, result);
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var response = await _gameService.GetAsync(id, null, false);

        return Ok(response);
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save(GameSaveVm model)
    {
        await _gameService.SaveAsync(model);

        return Ok(new ApiResponse(200, "Game has been saved successfully"));
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(long id)
    {
        await _gameService.DeleteAsync(id);
    }
    
    
}