using HoteleriaApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Domain.Interfaces
{
    public interface IPisoService
    {
        Task<List<Piso>> GetAllAsync();
        Task<Piso?> GetByIdAsync(Guid id);
        Task CreateAsync( Piso piso);
        Task DeleteAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, string nombre, string descripcion, int numeroPiso);

    }
}
