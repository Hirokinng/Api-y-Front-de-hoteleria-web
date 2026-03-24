using HoteleriaApp.Core.Application.Interfaces;
using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Infrastructure.Persistence.Contexts;
using System.Linq;

namespace HoteleriaApp.Infrastructure.Persistence.Repositories
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ClienteRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Crear(Cliente cliente)
        {
            _db.Clientes.Add(cliente);
        }

        public IEnumerable<Cliente> GetClientes()
        {
            return _db.Clientes.ToList();
        }

        public Cliente? GetClientePorId(Guid id)
        {
            return _db.Clientes.FirstOrDefault(c => c.Id == id);
        }

        public Cliente? GetClientePorEmail(string email)
        {
            email = (email ?? "").Trim().ToLower();
            return _db.Clientes.FirstOrDefault(c => c.Email == email);
        }

        public void Guardar()
        {
            _db.SaveChanges();
        }
    }
}