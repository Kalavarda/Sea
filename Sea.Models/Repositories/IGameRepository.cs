using System.Threading;
using System.Threading.Tasks;

namespace Sea.Models.Repositories
{
    public interface IGameRepository
    {
        Task<Game> Load(CancellationToken cancellationToken);

        Task Save(Game game, CancellationToken cancellationToken);
    }
}
