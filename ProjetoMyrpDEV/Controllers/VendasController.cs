using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMyrpDEV.Data;
using ProjetoMyrpDEV.Models;
using System.Text.Json.Serialization;

namespace ProjetoMyrpDEV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly ProjetoMyrpDEVContext _context;

        public VendasController(ProjetoMyrpDEVContext context)
        {
            _context = context;
        }

        // GET: api/Vendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendas()
        {
            return await _context.Vendas
                .Include(v => v.Cliente)
                .Include(v => v.VendaProdutos)
                .ThenInclude(vp => vp.Produto)
                .ToListAsync();
        }

        // GET: api/Vendas/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> GetVenda(int id)
        {
            var venda = await _context.Vendas
                .Include(v => v.Cliente)
                .Include(v => v.VendaProdutos)
                .ThenInclude(vp => vp.Produto)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venda == null)
            {
                return NotFound();
            }

            return venda;
        }

        // POST: api/Vendas
        [HttpPost]
        public async Task<ActionResult<Venda>> PostVenda(Venda venda)
        {
            if (venda.VendaProdutos == null || !venda.VendaProdutos.Any())
            {
                return BadRequest("A venda deve incluir pelo menos um produto.");
            }

            var produtoIds = venda.VendaProdutos.Select(vp => vp.ProdutoId).Distinct();
            var produtosExistentes = await _context.Produtos
                .Where(p => produtoIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync();

            if (produtosExistentes.Count != produtoIds.Count())
            {
                return BadRequest("Alguns produtos não existem.");
            }

            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            foreach (var item in venda.VendaProdutos)
            {
                item.VendaId = venda.Id;
                _context.VendaProdutos.Add(item);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVenda), new { id = venda.Id }, venda);
        }

        // PUT: api/Vendas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenda(int id, Venda venda)
        {
            if (id != venda.Id)
            {
                return BadRequest();
            }

            _context.Entry(venda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Vendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.Id == id);
        }
    }
}
