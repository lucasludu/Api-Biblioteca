using BibliotecaAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> op) : base(op)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Autor> Autores { get; set; }

    }
}
