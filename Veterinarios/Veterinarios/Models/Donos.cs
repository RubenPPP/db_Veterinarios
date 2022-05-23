using System.ComponentModel.DataAnnotations;

namespace Vets.Models
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
        [StringLength(30, ErrorMessage = "O {0} não pode ter mais do que {1} caracteres.")]
        [RegularExpression("[ÂÓA-Za-záéíóúàèìòùâêîôûãõäëïöüñç '-]+", ErrorMessage = "No {0} só são aceites letras.")]
        public string Nome { get; set; }
        /// <summary>
        /// NIF do dono do animal
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        [StringLength (9, MinimumLength = 9, ErrorMessage = "O {0} deve ter exatamente {1} caracteres.")]
        [RegularExpression("[123578]+[0-9]{8}", ErrorMessage = "O {0} deve começar por 1, 2, 3, 4, 5, 7 ou 8, e só ter algarismos.")]
        public string NIF { get; set; }
        /// <summary>
        /// Sexo do dono 
        /// Ff - Feminino ; Mm - Masculino
        /// </summary>
        [StringLength(1, ErrorMessage = "O {0} só aceita um carácter.")]
        [RegularExpression("[FfMm]", ErrorMessage = "No {0} só se aceitam as letras F ou M.")]
        public string Sexo { get; set; }
        /// <summary>
        /// Email 
        /// </summary>
        [EmailAddress(ErrorMessage = "Introduza um email correto, por favor.")]
        public string Email { get; set; }
        /// <summary>
        /// Lista dos animais do Dono
        /// </summary>
        public ICollection<Animais> ListaAnimais { get; set; }

        /// <summary>
        /// Atributo para executar a FK que permitirá ligar a tabela da
        /// autenticação à tabela de donos
        /// </summary>
        public string UserID { get; set; }
    }
}
