using GifAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GifAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class FactGifController : ControllerBase
    {
        private readonly IFactGifService _factGifService;
        public FactGifController(IFactGifService factGifService)
        {
            _factGifService = factGifService;
        }

        //[HttpGet("factAndGif")]
        //public async Task<IActionResult> GetFactAndGif()
        //{
        //    try
        //    {
        //        var result = await _factGifService.GetFactWithGifAsync();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Error retrieving fact", error = ex.Message });
        //    }
        //}

        [HttpGet("fact")]
        public async Task<IActionResult> GetFact()
        {
            try
            {
                var result = await _factGifService.GetFactAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving fact", error = ex.Message });
            }
        }

        [HttpGet("gif")]
        public async Task<IActionResult> GetGif([FromQuery] string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return BadRequest(new { message = "Query parameter is required" });
                }

                var gifUrl = await _factGifService.GetNewGifForFactAsync(query);
                return Ok(new { gifUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving gif", error = ex.Message });
            }
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            try
            {
                var history = await _factGifService.GetSearchHistoryAsync();
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving history", error = ex.Message });
            }
        }
    }
}
