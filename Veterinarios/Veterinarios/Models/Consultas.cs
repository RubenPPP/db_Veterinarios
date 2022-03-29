using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinarios.Models
{
    public class Consultas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Observacoes { get; set; }
        public decimal ValorConsulta { get; set; }
        [ForeignKey("Animal")]
        public int AnimalFK { get; set; }
        public Animais Animal { get; set; }
        [ForeignKey("Veterinario")]
        public int VeterinarioFK { get; set; }
        public Veterinarios Veterinario { get; set; }
    }
}
