namespace APICatalogo.Context;

using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Categoria>? Categorias { get; set; }
    public DbSet<Produto>? Produtos {  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasKey(c=> c.CategoriaId);
        
        modelBuilder.Entity<Categoria>().Property(c => c.Nome)
                                        .HasMaxLength(100)
                                        .IsRequired();
        
        modelBuilder.Entity<Categoria>().Property(c => c.ImagemUrl)
                                        .HasMaxLength(300)
                                        .IsRequired();



        modelBuilder.Entity<Produto>().HasKey(p => p.ProdutoId);
        
        modelBuilder.Entity<Produto>().Property(p => p.Nome)
                                      .HasMaxLength(80)
                                      .IsRequired();

        modelBuilder.Entity<Produto>().Property(p => p.Descricao)
                                      .HasMaxLength(300)
                                      .IsRequired();

        modelBuilder.Entity<Produto>().Property(p => p.Preco)
                                      .HasPrecision(10,2)
                                      .IsRequired();

        modelBuilder.Entity<Produto>().Property(p => p.ImagemUrl)
                                      .HasMaxLength(300)
                                      .IsRequired();



        modelBuilder.Entity<Produto>()
            .HasOne<Categoria>(c=> c.Categoria)
            .WithMany(p=> p.Produtos)
            .HasForeignKey(c => c.CategoriaId);

    }
}
