using System.ComponentModel.DataAnnotations;

namespace ProjetoMyrpDEV.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Documento { get; set; }

        [Required]
        public string Nome { get; set; }
    }
}
