using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            
        }
        
        public DbSet<Tarea> Tareas { get; set; }
    }
}