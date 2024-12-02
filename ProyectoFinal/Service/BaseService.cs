using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Service
{
    public class BaseService<T> where T : class // Restringimos T a tipos de referencia
    {
        private readonly AppDbContext _context;

        public BaseService(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los registros
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync(); // Aquí T debe ser un tipo de referencia
        }

        // Obtener un registro por ID
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        // Crear un nuevo registro
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Actualizar un registro existente
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        // Eliminar un registro
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
