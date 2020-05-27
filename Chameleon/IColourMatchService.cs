using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chameleon
{
    public interface IColourMatchService
    {
        Task<ColourMatch> GetMatch(Uri uri, IEnumerable<PaletteOption> targetPalette);
    }
}