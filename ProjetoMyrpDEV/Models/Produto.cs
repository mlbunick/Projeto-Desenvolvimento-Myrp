using System.ComponentModel.DataAnnotations;

namespace ProjetoMyrpDEV.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public decimal Preco { get; set; }
    }
}
