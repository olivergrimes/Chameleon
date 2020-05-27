using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chameleon.Api.Services
{
    public class DefaultPaletteService : IPaletteService
    {
        public Task<IEnumerable<PaletteOption>> GetTargetPalette()
        {
            var palette = new[]
            {
                new PaletteOption
                {
                    Key = "Teal",
                    RGB =  new RGBPixel{

                    Red = 123,
                    Green = 123,
                    Blue = 123
                    }
                },
                new PaletteOption
                {
                    Key = "Grey",
                    RGB =  new RGBPixel{

                    Red = 0,
                    Green = 0,
                    Blue = 0
                    }
                },
            };

            return Task.FromResult(palette.AsEnumerable());
        }
    }
}
