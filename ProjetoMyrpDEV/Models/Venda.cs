using System.Text.Json.Serialization;

namespace ProjetoMyrpDEV.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;

        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public ICollection<VendaProduto> VendaProdutos { get; set; } = new List<VendaProduto>();      
        public decimal ValorTotal
        {
            get
            {
                return VendaProdutos.Sum(vp => vp.ValorTotalProduto);
            }
        }
    }

    public class VendaProduto
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        [JsonIgnore]
        public Venda? Venda { get; set; }

        public int ProdutoId { get; set; }
        [JsonIgnore]
        public Produto? Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }

        public decimal ValorTotalProduto
        {
            get
            {
                return Quantidade * PrecoUnitario;
            }
        }

    }
}
