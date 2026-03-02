using HoteleriaApp.Core.Application.Interfaces;
using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Infrastructure.Persistence.Contexto;

namespace HoteleriaApp.Infrastructure.Persistence.Repositories
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly HoteleriaDbContext _db;

        public ClienteRepositorio(HoteleriaDbContext db)
        {
            _db = db;
        }

        public void Crear(Cliente cliente)
        {
            _db.Cliente.Add(cliente);
        }

        public IEnumerable<Cliente> GetClientes()
        {
            return _db.Cliente.ToList();
        }

        public Cliente? GetClientePorId(int id)
        {
            return _db.Cliente.FirstOrDefault(c => c.id_cliente == id);
        }

        public Cliente? GetClientePorEmail(string email)
        {
            email = (email ?? "").Trim().ToLower();
            return _db.Cliente.FirstOrDefault(c => c.email == email);
        }

        public void Guardar()
        {
            _db.SaveChanges();
        }
    }
}