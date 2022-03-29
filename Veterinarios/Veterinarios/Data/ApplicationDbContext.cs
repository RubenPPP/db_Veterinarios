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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Definir as 'tabelas'
        public DbSet<Animais> Animais { get; set; }
        public DbSet<Models.Veterinarios> Veterinarios { get; set; }
        public DbSet<Donos> Donos { get; set; }
        public DbSet<Consultas> Consultas{ get; set; }
    }
}