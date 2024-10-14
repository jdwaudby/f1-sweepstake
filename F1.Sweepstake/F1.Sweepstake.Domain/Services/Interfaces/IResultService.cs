using System.Collections.Generic;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;

namespace F1.Sweepstake.Domain.Services.Interfaces;

public interface IResultService
{
    Task<IEnumerable<Result>> Get(IEnumerable<Assignment> assignments);
    Task<IEnumerable<Result>> Get(int round, IEnumerable<Assignment> assignments);
}
