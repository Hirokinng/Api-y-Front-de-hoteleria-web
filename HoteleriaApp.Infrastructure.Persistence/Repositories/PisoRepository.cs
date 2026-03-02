using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Core.Domain.Interfaces;
using HoteleriaApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Infrastructure.Persistence.Repositories
{
    public class PisoRepository : IPisoRepository
    {
        private readonly ApplicationDbContext _context;

        public PisoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Piso piso)
        {
            await _context.Pisos.AddAsync(piso);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Piso>> GetAllAsync()
        {
            return await _context.Pisos.ToListAsync();
        }

        public async Task<Piso?> GetByIdAsync(Guid id)
        {
            return await _context.Pisos.FindAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var piso = await _context.Pisos.FindAsync(id);

            if (piso == null)
                return;

            _context.Pisos.Remove(piso);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Piso piso)
        {
            await _context.Pisos.AddAsync(piso);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Piso piso)
        {
            _context.Pisos.Update(piso);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsByNameAsync(string nombre)
        {
            return await _context.Pisos
                .AnyAsync(p => p.Nombre.ToLower() == nombre.ToLower());
        }
    }
}