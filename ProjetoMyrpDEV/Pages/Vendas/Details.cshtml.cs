using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoMyrpDEV.Data;
using ProjetoMyrpDEV.Models;

namespace ProjetoMyrpDEV.Pages.Vendas
{
    public class DetailsModel : PageModel
    {
        private readonly ProjetoMyrpDEVContext _context;

        public DetailsModel(ProjetoMyrpDEVContext context)
        {
            _context = context;
        }

        public Venda Venda { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Venda = await _context.Vendas
                .Include(v => v.Cliente)
                .Include(v => v.VendaProdutos)
                .ThenInclude(vp => vp.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Venda == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
