using Wipro.Locadora.Models;
using Microsoft.EntityFrameworkCore;

namespace Wipro.Locadora.Contexts
{
    public partial class Contexto : DbContext
    {
        public Contexto()
        {
        }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> TableCliente { get; set; }
        public virtual DbSet<Filme> TableFilme { get; set; }
        public virtual DbSet<Locacao> TableLocacao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_locadora;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__tb_Clien__885457EED9FB718D");

                entity.ToTable("tb_Cliente");

                entity.HasIndex(e => e.Cpf)
                    .HasName("UQ__tb_Clien__C1F89731D6B704B0")
                    .IsUnique();

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DtNascimento)
                    .HasColumnName("dtNascimento")
                    .HasColumnType("date");

                entity.Property(e => e.NomeCliente)
                    .IsRequired()
                    .HasColumnName("nomeCliente")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Filme>(entity =>
            {
                entity.HasKey(e => e.IdFilme)
                    .HasName("PK__tb_Filme__96709919538622C4");

                entity.ToTable("tb_Filme");

                entity.Property(e => e.IdFilme).HasColumnName("idFilme");

                entity.Property(e => e.Disponibilidade).HasColumnName("disponibilidade");

                entity.Property(e => e.DtLancamento)
                    .HasColumnName("dtLancamento")
                    .HasColumnType("date");

                entity.Property(e => e.NomeFilme)
                    .IsRequired()
                    .HasColumnName("nomeFilme")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Locacao>(entity =>
            {
                entity.HasKey(e => e.IdLocacao)
                    .HasName("PK__tb_Locac__906545EC94C3D836");

                entity.ToTable("tb_Locacao");

                entity.Property(e => e.IdLocacao).HasColumnName("idLocacao");

                entity.Property(e => e.DtEntrega)
                    .HasColumnName("dtEntrega")
                    .HasColumnType("date");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdFilme).HasColumnName("idFilme");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.TableLocacao)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Locaca__idCli__5812160E");

                entity.HasOne(d => d.IdFilmeNavigation)
                    .WithMany(p => p.TableLocacao)
                    .HasForeignKey(d => d.IdFilme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Locaca__idFil__571DF1D5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
