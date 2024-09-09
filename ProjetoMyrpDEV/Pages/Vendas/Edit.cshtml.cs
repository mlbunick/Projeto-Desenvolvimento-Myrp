using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoMyrpDEV.Data;
using ProjetoMyrpDEV.Models;

namespace ProjetoMyrpDEV.Pages.Vendas
{
    public class EditModel : PageModel
    {
        private readonly ProjetoMyrpDEV.Data.ProjetoMyrpDEVContext _context;

        public EditModel(ProjetoMyrpDEV.Data.ProjetoMyrpDEVContext context)
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

            var venda =  await _context.Vendas.FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }
            Venda = venda;
           ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Documento");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Venda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(Venda.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.Id == id);
        }
    }
}
