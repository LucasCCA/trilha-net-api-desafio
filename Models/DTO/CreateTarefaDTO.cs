using System.ComponentModel.DataAnnotations;
using TrilhaApiDesafio.Models.Enums;

namespace TrilhaApiDesafio.Models.DTO
{
    public class CreateTarefaDTO
    {
        [Required]
        [StringLength(120)]
        public string Titulo { get; set; }
        [StringLength(250)]
        public string Descricao { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public EnumStatusTarefa Status { get; set; }
    }
}