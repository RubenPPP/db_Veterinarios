using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vets.Models
{
    public class Consultas
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public string Observacoes { get; set; }

        /// <summary>
        /// Atributo auxiliar à introdução de dados no atributo 'decimal' ValorConsulta
        /// </summary>
        [Required(ErrorMessage = "Valor da consulta é obrigatório!")]
        [StringLength(11)]
        [RegularExpression("[0-9]{1,8}[,.]?[0-9]{0,2}", ErrorMessage = "Indique o valor da consulta!")]
        [NotMapped] // Indica à Entity-Framework que não deve representar este atributo na base de dados
        public string AuxValorConsulta { get; set; }
        public decimal ValorConsulta { get; set; }
        [ForeignKey("Animal")]
        public int AnimalFK { get; set; }
        public Animais Animal { get; set; }
        [ForeignKey("Veterinario")]
        public int VeterinarioFK { get; set; }
        public Veterinarios Veterinario { get; set; }
    }
}
