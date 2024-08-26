using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CursoEFCore.Data
{ 
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p=>p.AddConsole());
        private const string V = "Data Source=DESKTOP-952E37N\\SQLEXPRESS;Initial Catalog=myTesteDataBase;Integrated Security=True";

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer(V,
                    P=>P.EnableRetryOnFailure(
                        maxRetryCount: 2, 
                        maxRetryDelay: TimeSpan.FromSeconds(5), 
                        errorNumbersToAdd: null).MigrationsHistoryTable("Curso_ef_core"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                foreach (var property in properties)
                {
                    if(string.IsNullOrEmpty(property.GetColumnType())
                        && !property.GetMaxLength().HasValue)
                        {
                        //property.SetMaxLength(100);
                        property.SetColumnType("VARCHAR(100)");
                        }
                }
            }
        }
    }
}
