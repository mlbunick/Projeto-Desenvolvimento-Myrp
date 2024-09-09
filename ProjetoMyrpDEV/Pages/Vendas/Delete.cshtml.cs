using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoMyrpDEV.Data;
using ProjetoMyrpDEV.Models;

namespace ProjetoMyrpDEV.Pages.Vendas
{
    public class DeleteModel : PageModel
    {
        private readonly ProjetoMyrpDEV.Data.ProjetoMyrpDEVContext _context;

        public DeleteModel(ProjetoMyrpDEV.Data.ProjetoMyrpDEVContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Venda Venda { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas.FirstOrDefaultAsync(m => m.Id == id);

            if (venda == null)
            {
                return NotFound();
            }
            else
            {
                Venda = venda;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas.FindAsync(id);
            if (venda != null)
            {
                Venda = venda;
                _context.Vendas.Remove(Venda);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
