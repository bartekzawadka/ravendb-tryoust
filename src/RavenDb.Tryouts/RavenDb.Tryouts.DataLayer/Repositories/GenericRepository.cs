using System;
using System.Threading;
using System.Threading.Tasks;
using Raven.Client.Documents.Session;
using RavenDb.Tryouts.DataLayer.Models;

namespace RavenDb.Tryouts.DataLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : TryoutDoc
    {
        private readonly IAsyncDocumentSession _dbSession;

        public GenericRepository(IAsyncDocumentSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task InsertAsync(T document, CancellationToken cancellationToken)
        {
            await _dbSession.StoreAsync(document, Guid.NewGuid().ToString(), cancellationToken);
            await _dbSession.SaveChangesAsync(cancellationToken);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken) =>
            _dbSession.SaveChangesAsync(cancellationToken);
    }
}