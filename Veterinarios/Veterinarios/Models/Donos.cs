using System.ComponentModel.DataAnnotations;

namespace Veterinarios.Models
{
    public class Donos
    {
        public Donos()
        {
            ListaAnimais = new HashSet<Animais>();
        }
        /// <summary>
        /// PK para a tabela dos Donos
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nome do Dono do animal
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }
        /// <summary>
        /// NIF do dono do animal
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string NIF { get; set; }
        /// <summary>
        /// Sexo do dono 
        /// Ff - Feminino ; Mm - Masculino
        /// </summary>
        public string Sexo { get; set; }
        /// <summary>
        /// Lista dos animais do Dono
        /// </summary>
        public ICollection<Animais> ListaAnimais { get; set; }
    }
}
