using System.Collections.Generic;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;

namespace F1.Sweepstake.Domain.Services.Interfaces
{
    public interface IConstructorService
    {
        Task<IEnumerable<Constructor>> Get(int round);
    }
}
