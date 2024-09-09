namespace ProjetoMyrpDEV.Data;

using Microsoft.EntityFrameworkCore;
using ProjetoMyrpDEV.Models;

public class ProjetoMyrpDEVContext : DbContext
{
    public ProjetoMyrpDEVContext(DbContextOptions<ProjetoMyrpDEVContext> options)
        : base(options)
    {
    }

    public DbSet<Venda> Vendas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<VendaProduto> VendaProdutos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<VendaProduto>()
            .HasKey(vp => vp.Id);

        modelBuilder.Entity<VendaProduto>()
            .HasOne(vp => vp.Venda)
            .WithMany(v => v.VendaProdutos)
            .HasForeignKey(vp => vp.VendaId)
            .OnDelete(DeleteBehavior.Cascade); //CASCADE DELETE

        modelBuilder.Entity<VendaProduto>()
            .HasOne(vp => vp.Produto)
            .WithMany()
            .HasForeignKey(vp => vp.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade); //CASCADE DELETE

        modelBuilder.Entity<Venda>()
            .HasOne(v => v.Cliente)
            .WithMany()
            .HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.Cascade); //CASCADE DELETE

        //Obs: Teria sido melhor usar a informação que iria persistir na Venda dentro do Model (Pra não excluir a venda), mas fiquei sem tempo :P
    }
}
