using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RavenDb.Tryouts.DataLayer.Models;
using RavenDb.Tryouts.DataLayer.Repositories;

namespace RavenDb.Tryouts.Api.Controllers
{
    public class TryoutController : BaseController
    {
        private readonly IGenericRepository<RavenItem> _genericRepository;

        public TryoutController(IGenericRepository<RavenItem> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        
        [HttpPost]
        public async Task InsertAsync([FromBody] RavenItem data, CancellationToken cancellationToken)
        {
            data.UpdatedAt = DateTime.UtcNow;
            await _genericRepository.InsertAsync(data, cancellationToken);
        }
    }
}