using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>(e =>
            {
                e.HasKey(t => t.Id)
                    .HasName("PK_Tarefas");
                e.Property(t => t.Titulo)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);
                e.Property(t => t.Descricao)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                e.Property(t => t.Data)
                    .IsRequired()
                    .HasColumnType("datetime");
                e.Property(t => t.Status)
                    .IsRequired();
            });
        }
    }
}