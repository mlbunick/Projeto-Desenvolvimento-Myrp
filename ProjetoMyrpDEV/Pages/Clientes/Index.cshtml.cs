using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoMyrpDEV.Data;
using ProjetoMyrpDEV.Models;

namespace ProjetoMyrpDEV.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly ProjetoMyrpDEV.Data.ProjetoMyrpDEVContext _context;

        public IndexModel(ProjetoMyrpDEV.Data.ProjetoMyrpDEVContext context)
        {
            _context = context;
        }

        public IList<Cliente> Cliente { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Cliente = await _context.Clientes.ToListAsync();
        }
    }
}
