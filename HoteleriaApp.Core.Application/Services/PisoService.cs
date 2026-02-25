using HoteleriaApp.Core.Application.DTOs;
using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Application.Services
{
    public class PisoService : IPisoService
    {
        private readonly IPisoRepository _repository;

        public PisoService(IPisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Piso>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Piso> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Piso piso)
        {
            if (piso == null)
                throw new ArgumentException("El piso no puede ser nulo");

            if (string.IsNullOrWhiteSpace(piso.Nombre))
                throw new ArgumentException("El nombre del piso es obligatorio");

            var existe = await _repository.ExistsByNameAsync(piso.Nombre);
            if (existe)
                throw new Exception("Ya existe un piso con ese nombre");

            await _repository.CreateAsync(piso);
        }


        public async Task DeleteAsync(Guid id)
        {
            var piso = await _repository.GetByIdAsync(id);

            if (piso == null)
                throw new Exception("El piso no existe");

            await _repository.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(Guid id, string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío");

            var piso = await _repository.GetByIdAsync(id);

            if (piso == null)
                return false;

            var existe = await _repository.ExistsByNameAsync(nombre);
            if (existe && piso.Nombre.ToLower() != nombre.ToLower())
                throw new Exception("Ya existe un piso con ese nombre");

            if (descripcion != null && descripcion.Length > 200)
                throw new ArgumentException("La descripción no puede exceder 200 caracteres");

            piso.Update(nombre, descripcion);

            return await _repository.UpdateAsync(piso);
        }
    }
  }
