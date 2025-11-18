using Microsoft.EntityFrameworkCore;

namespace Interação_Medicamentosa.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> opt) : base(opt) { }
        public DbSet<Models.PrescriçãoModel> Prescrições { get; set; }
        public DbSet<Models.PacienteModel> Pacientes { get; set; }
        public DbSet<Models.MedicamentoModel> Medicamentos { get; set; }
        public DbSet<Models.InteraçãoModel> Interações { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.PacienteModel>()
                .HasMany(x => x.Prescrições)
                .WithOne()
                .HasForeignKey(u => u.IdPaciente)
                .IsRequired(false);

            modelBuilder.Entity<Models.PrescriçãoModel>()
                .HasOne(x => x.Medicamento)
                .WithMany()
                .HasForeignKey(p => p.IdMedicamento);

            modelBuilder.Entity<Models.InteraçãoModel>()
                .HasOne(x => x.Medicamento_1)
                .WithMany()
                .HasForeignKey(p => p.MedicamentoId_1);

            modelBuilder.Entity<Models.InteraçãoModel>()
                .HasOne(x => x.Medicamento_2)
                .WithMany()
                .HasForeignKey(p => p.MedicamentoId_2);
        }
    }
}
