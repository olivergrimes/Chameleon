using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chameleon.Api.Services
{
    public interface IPaletteService
    {
        Task<IEnumerable<PaletteOption>> GetTargetPalette();
    }
}
