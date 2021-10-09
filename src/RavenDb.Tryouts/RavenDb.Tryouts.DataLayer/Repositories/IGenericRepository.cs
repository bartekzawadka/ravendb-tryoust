using System.Threading;
using System.Threading.Tasks;
using RavenDb.Tryouts.DataLayer.Models;

namespace RavenDb.Tryouts.DataLayer.Repositories
{
    public interface IGenericRepository<T> where T : TryoutDoc
    {
        Task InsertAsync(T document, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}