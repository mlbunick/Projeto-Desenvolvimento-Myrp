using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoMyrpDEV.Data;
using ProjetoMyrpDEV.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoMyrpDEV.Pages.Vendas
{
    public class IndexModel : PageModel
    {
        private readonly ProjetoMyrpDEVContext _context;

        public IndexModel(ProjetoMyrpDEVContext context)
        {
            _context = context;
        }

        public IList<Venda> Venda { get; set; }

        public async Task OnGetAsync()
        {
            Venda = await _context.Vendas
                .Include(v => v.Cliente)
                .Include(v => v.VendaProdutos)
                    .ThenInclude(vp => vp.Produto)
                .ToListAsync();
        }
    }
}
