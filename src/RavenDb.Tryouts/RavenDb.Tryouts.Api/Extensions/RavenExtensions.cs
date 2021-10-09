using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using RavenDb.Tryouts.DataLayer.Models;

namespace RavenDb.Tryouts.Api.Extensions
{
    public static class RavenExtensions
    {
        public static IDocumentStore EnsureExists(this IDocumentStore store)
        {
            try
            {
                using var dbSession = store.OpenSession();
                dbSession.Query<RavenItem>().Take(0).ToList();
            }
            catch (Raven.Client.Exceptions.Database.DatabaseDoesNotExistException)
            {
                store.Maintenance.Server.Send(new Raven.Client.ServerWide.Operations.CreateDatabaseOperation(new Raven.Client.ServerWide.DatabaseRecord
                {
                    DatabaseName = store.Database
                }));
            }

            return store;
        }
    }
}