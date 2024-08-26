using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext
    {
        private const string conectionString = "Data Source=DESKTOP-952E37N\\SQLEXPRESS;Initial Catalog=filmeDb;Integrated Security=True";

        public DbSet<Filme> Filme { get; set; }

        public FilmeContext(DbContextOptions<FilmeContext> opt) : base(opt)
        {
        }

        //public FilmeContext()
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilmeContext).Assembly);
        }
    }
}
