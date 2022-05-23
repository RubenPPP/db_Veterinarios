using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Veterinarios.Models;
using Vets.Models;

namespace Veterinarios.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string NomeDoUtilizador { get; set; }
        public DateTime DataRegisto { get; set; }
    }

    /// <summary>
    /// Esta classe funciona como a base de dados do nosso projeto
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// Yes.
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

            modelBuilder.Entity<Donos>().HasData(
                new Donos { Id = 1, Nome = "Luís Freitas", Sexo = "M", NIF = "813635582" },
                new Donos { Id = 2, Nome = "Andreia Gomes", Sexo = "F", NIF = "854613462" },
                new Donos { Id = 3, Nome = "Cristina Sousa", Sexo = "F", NIF = "265368715" },
                new Donos { Id = 4, Nome = "Sónia Rosa", Sexo = "F", NIF = "835623190" }
            );

            modelBuilder.Entity<Animais>().HasData(
               new Animais { Id = 1, Nome = "Bubi", Especie = "Cão", Raca = "Pastor Alemão", Peso = 24, Fotografia = "animal1.jpg", DonoFK = 1 },
               new Animais { Id = 2, Nome = "Pastor", Especie = "Cão", Raca = "Serra Estrela", Peso = 50, Fotografia = "animal2.jpg", DonoFK = 3 },
               new Animais { Id = 3, Nome = "Tripé", Especie = "Cão", Raca = "Serra Estrela", Peso = 4, Fotografia = "animal3.jpg", DonoFK = 2 },
               new Animais { Id = 4, Nome = "Saltador", Especie = "Cavalo", Raca = "Lusitana", Peso = 580, Fotografia = "animal4.jpg", DonoFK = 3 },
               new Animais { Id = 5, Nome = "Tareco", Especie = "Gato", Raca = "siamês", Peso = 1, Fotografia = "animal5.jpg", DonoFK = 3 },
               new Animais { Id = 6, Nome = "Cusca", Especie = "Cão", Raca = "Labrador", Peso = 45, Fotografia = "animal6.jpg", DonoFK = 2 },
               new Animais { Id = 7, Nome = "Morde Tudo", Especie = "Cão", Raca = "Dobermann", Peso = 39, Fotografia = "animal7.jpg", DonoFK = 4 },
               new Animais { Id = 8, Nome = "Forte", Especie = "Cão", Raca = "Rottweiler", Peso = 20, Fotografia = "animal8.jpg", DonoFK = 2 },
               new Animais { Id = 9, Nome = "Castanho", Especie = "Vaca", Raca = "Mirandesa", Peso = 652, Fotografia = "animal9.jpg", DonoFK = 3 },
               new Animais { Id = 10, Nome = "Saltitão", Especie = "Gato", Raca = "Persa", Peso = 2, Fotografia = "animal10.jpg", DonoFK = 1 }
            );
        }
        // Definir as 'tabelas'
        public DbSet<Vets.Models.Animais> Animais { get; set; }
        public DbSet<Vets.Models.Veterinarios> Veterinarios { get; set; }
        public DbSet<Vets.Models.Donos> Donos { get; set; }
        public DbSet<Vets.Models.Consultas> Consultas{ get; set; }
    }
}