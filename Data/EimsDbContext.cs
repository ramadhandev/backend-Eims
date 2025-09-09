using EIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Data
{
    public class EimsDbContext : DbContext
    {
        public EimsDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users => Set<User>();
        public DbSet<CardType> CardTypes => Set<CardType>();
        public DbSet<HseCard> HseCards => Set<HseCard>();
        public DbSet<Training> Trainings => Set<Training>();
        public DbSet<PermitRequirement> PermitRequirements => Set<PermitRequirement>();
        public DbSet<PermitToWork> PermitToWorks => Set<PermitToWork>();
        public DbSet<Approval> Approvals => Set<Approval>();
        public DbSet<IncidentReport> IncidentReports => Set<IncidentReport>();
        public DbSet<Investigation> Investigations => Set<Investigation>();
        public DbSet<DocumentRequirement> DocumentRequirements => Set<DocumentRequirement>();
        public DbSet<UserDocument> UseDocuments => Set<UserDocument>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // -----------------------
            // USER
            // -----------------------
            modelBuilder.Entity<User>(e =>
            {
                e.Property(p => p.Name).IsRequired();
                e.Property(p => p.Role).IsRequired();
                e.Property(p => p.Department).IsRequired();
            });

            // -----------------------
            // CARD TYPE
            // -----------------------
            modelBuilder.Entity<CardType>(e =>
            {
                e.Property(p => p.Name).IsRequired();
            });

            // -----------------------
            // HSE CARD
            // dua FK ke User: UserId & IssuedBy → gunakan Restrict
            // -----------------------
            modelBuilder.Entity<HseCard>(e =>
            {
                e.Property(p => p.CardNumber).IsRequired();
                e.Property(p => p.Status).IsRequired();

                e.HasOne(p => p.User)
                    .WithMany(u => u.HseCards)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.IssueByUser)
                    .WithMany()
                    .HasForeignKey(p => p.IssuedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.CardType)
                    .WithMany(ct => ct.HseCards)
                    .HasForeignKey(p => p.CardTypeId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(p => p.CardNumber).IsUnique(); // optional: bantu integritas nomor kartu
            });

            // -----------------------
            // TRAINING
            // -----------------------
            modelBuilder.Entity<Training>(e =>
            {
                e.Property(p => p.TrainingName).IsRequired();
                e.Property(p => p.Status).IsRequired();

                e.HasOne(p => p.User)
                    .WithMany(u => u.Trainings)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // -----------------------
            // PERMIT REQUIREMENT (kebutuhan per WorkType & CardType)
            // -----------------------
            modelBuilder.Entity<PermitRequirement>(e =>
            {
                e.Property(p => p.WorkType).IsRequired();

                e.HasOne(p => p.RequiredCardType)
                    .WithMany(ct => ct.PermitRequirements)
                    .HasForeignKey(p => p.RequiredCardTypeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // -----------------------
            // PERMIT TO WORK
            // FK ke User (pemohon), CardType (syarat), dan optional AutoApprovedBy (User)
            // Hindari multiple cascade path: ke User pakai Restrict
            // -----------------------
            modelBuilder.Entity<PermitToWork>(e =>
            {
                e.Property(p => p.WorkType).IsRequired();
                e.Property(p => p.Status).IsRequired();

                e.HasOne(p => p.User)
                    .WithMany(u => u.Permits)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.RequiredCardType)
                    .WithMany()
                    .HasForeignKey(p => p.RequiredCardTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.AutoApprovedByUser)
                    .WithMany()
                    .HasForeignKey(p => p.AutoApprovedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // -----------------------
            // APPROVAL
            // FK ke Permit (boleh cascade), ke User (Restrict)
            // -----------------------
            modelBuilder.Entity<Approval>(e =>
            {
                e.Property(p => p.Role).IsRequired();
                e.Property(p => p.Decision).IsRequired();

                e.HasOne(p => p.Permit)
                    .WithMany(ptw => ptw.Approvals)
                    .HasForeignKey(p => p.PermitId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(p => p.Approver)
                    .WithMany(u => u.Approvals)
                    .HasForeignKey(p => p.ApproverId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // -----------------------
            // INCIDENT REPORT
            // FK ke User (Restrict agar data insiden tetap ada meski user dinonaktifkan)
            // -----------------------
            modelBuilder.Entity<IncidentReport>(e =>
            {
                e.Property(p => p.Category).IsRequired();

                e.HasOne(p => p.User)
                    .WithMany(u => u.IncidentReports)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.Investigation)
                    .WithOne(i => i.Incident)
                    .HasForeignKey<Investigation>(i => i.IncidentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // -----------------------
            // INVESTIGATION
            // FK ke Incident (Cascade), ke HSEOfficer (User) Restrict
            // -----------------------
            modelBuilder.Entity<Investigation>(e =>
            {
                e.HasOne(p => p.HSEOfficer)
                    .WithMany()
                    .HasForeignKey(p => p.HSEOfficerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // -----------------------
            // DOCUMENT REQUIREMENT (master requirement dokumen)
            // -----------------------
            modelBuilder.Entity<DocumentRequirement>(e =>
            {
                e.Property(p => p.Name).IsRequired();
            });

            // -----------------------
            // USE DOCUMENT (dokumen yang diunggah user)
            // FK ke User & DocumentRequirement
            // -----------------------
            modelBuilder.Entity<UserDocument>(e =>
            {
                e.HasOne(p => p.User)
                    .WithMany(p => p.UserDocuments)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(p => p.DocumentRequirement)
                    .WithMany()
                    .HasForeignKey(p => p.DocumentRequirementId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(p => new { p.UserId, p.DocumentRequirementId }) // bantu cek kelengkapan unik per user
                .IsUnique();
            });
        }
    }
}
