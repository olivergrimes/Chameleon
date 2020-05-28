using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Chameleon.Api.Services
{
    public class DefaultPaletteService : IPaletteService
    {
        public Task<IEnumerable<PaletteOption>> GetTargetPalette()
        {
            var colours = new[]
            {
                Color.White,
                Color.Gray,
                Color.Black,
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Teal,
                Color.Orange,
                Color.Purple,
                Color.Pink
            };

            var palette = colours.Select(c => PaletteOption.FromColor(c));

            return Task.FromResult(palette.AsEnumerable());
        }
    }
}
