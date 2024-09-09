using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoMyrpDEV.Data;
using ProjetoMyrpDEV.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

namespace ProjetoMyrpDEV.Pages.Vendas
{
    public class CreateModel : PageModel
    {
        private readonly ProjetoMyrpDEVContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ProjetoMyrpDEVContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["Produtos"] = _context.Produtos.ToList();
            return Page();
        }

        [BindProperty]
        public Venda Venda { get; set; } = new Venda();

        [BindProperty]
        public List<VendaProduto> VendaProdutos { get; set; } = new List<VendaProduto>();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
                ViewData["Produtos"] = _context.Produtos.ToList();
                return Page();
            }

            try
            {
                _context.Vendas.Add(Venda);
                await _context.SaveChangesAsync();

                foreach (var item in VendaProdutos)
                {
                    item.VendaId = Venda.Id;
                    var produto = _context.Produtos.FirstOrDefault(p => p.Id == item.ProdutoId);

                    if (produto == null)
                    {
                        ModelState.AddModelError(string.Empty, $"Produto com ID {item.ProdutoId} não encontrado.");
                        ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
                        ViewData["Produtos"] = _context.Produtos.ToList();
                        return Page();
                    }

                    item.PrecoUnitario = produto.Preco; 
                    _context.VendaProdutos.Add(item);
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation("Venda criada com sucesso. Venda ID: {VendaId}", Venda.Id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar a venda.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a venda.");
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
                ViewData["Produtos"] = _context.Produtos.ToList();
                return Page();
            }
        }
    }
}
