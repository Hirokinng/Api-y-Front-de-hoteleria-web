using HoteleriaApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Domain.Interfaces
{
    public interface IPisoRepository
    {
        Task<List<Piso>> GetAllAsync();
        Task<Piso?> GetByIdAsync(Guid id);
        Task AddAsync(Piso piso);
        Task DeleteAsync(Guid id);
        Task CreateAsync(Piso piso);
        Task<bool> UpdateAsync(Piso piso);
        Task<bool> ExistsByNameAsync(string nombre);
    }
}
