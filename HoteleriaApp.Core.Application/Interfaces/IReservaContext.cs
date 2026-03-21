using HoteleriaApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Application.Interfaces
{
    public interface IReservaContext
    {
        DbSet<Category> Categorias { get; }
        DbSet<Tarifa> Tarifas { get; }
        DbSet<CategoriaServicio> CategoriasServicio { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}