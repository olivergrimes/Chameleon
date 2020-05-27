using System;
using System.Threading.Tasks;
using Chameleon.Api.Models;
using Chameleon.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColourMatchController : ControllerBase
    {
        private readonly IColourMatchService _colourMatchService;
        private readonly IPaletteService _paletteService;

        public ColourMatchController(
            IColourMatchService colourMatchService,
            IPaletteService paletteService)
        {
            _colourMatchService = colourMatchService ?? throw new ArgumentNullException(nameof(colourMatchService));
            _paletteService = paletteService ?? throw new ArgumentNullException(nameof(paletteService));
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody]ColourMatchRequest colourMatchRequest)
        {
            if (!Uri.IsWellFormedUriString(colourMatchRequest.ImageUrl, UriKind.Absolute))
            {
                ModelState.AddModelError(nameof(colourMatchRequest.ImageUrl), "Invalid uri string");
                return BadRequest(ModelState);
            }

            var targetPalette = await _paletteService.GetTargetPalette();
            var closestMatch = await _colourMatchService.GetMatch(
                new Uri(colourMatchRequest.ImageUrl), targetPalette);

            return Ok(new ColourMatchResult
            {
                MatchFound = closestMatch != null,
                MatchKey = closestMatch?.Colour?.Key
            });
        }
    }
}
