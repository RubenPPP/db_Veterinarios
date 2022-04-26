using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Veterinarios.Models;

namespace Veterinarios.Data
{
    /// <summary>
    /// Esta classe funciona como a base de dados do nosso projeto
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// Este método é executado imediatamente antes da criação do Modelo.
        /// É utilizado para adicionar as últimas instruções à criação do modelo.
        /// </summary>
        /// <param name="modelBuilder"</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 'Importa' todo o comportamento do método, aquando da sua definição na SuperClasse.
            base.OnModelCreating(modelBuilder);
            // Adicionar registos que serão adicionados às tabelas da BD
            modelBuilder.Entity<Vets.Models.Veterinarios>().HasData(
                new Vets.Models.Veterinarios() { 
                    Id = 1, 
                    Nome = "José Silva", 
                    NumCedulaProf = "vet-001",
                    Fotografia = "Jose.jpg"
                },

                new Vets.Models.Veterinarios()
                {
                    Id = 2,
                    Nome = "Maria Gomes dos Santos",
                    NumCedulaProf = "vet-002",
                    Fotografia = "Maria.jpg"
                },

                new Vets.Models.Veterinarios()
                {
                    Id = 3,
                    Nome = "Ricardo Godinho Dias",
                    NumCedulaProf = "vet-003",
                    Fotografia = "Ricardo.jpg"
                });

        }
        // Definir as 'tabelas'
        public DbSet<Vets.Models.Animais> Animais { get; set; }
        public DbSet<Vets.Models.Veterinarios> Veterinarios { get; set; }
        public DbSet<Vets.Models.Donos> Donos { get; set; }
        public DbSet<Vets.Models.Consultas> Consultas{ get; set; }
    }
}