using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoMyrpDEV.Data;
using ProjetoMyrpDEV.Models;

namespace ProjetoMyrpDEV.Pages.Produtos
{
    public class CreateModel : PageModel
    {
        private readonly ProjetoMyrpDEV.Data.ProjetoMyrpDEVContext _context;

        public CreateModel(ProjetoMyrpDEV.Data.ProjetoMyrpDEVContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Produto Produto { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Produtos.Add(Produto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
