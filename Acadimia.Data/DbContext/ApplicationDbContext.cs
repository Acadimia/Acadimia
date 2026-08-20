using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Acadimia.Data.Models;
//using Acadimia.Data.SeedHeper;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Security.Principal;
using Acadimia.Data.SeedHeper;

namespace Acadimia.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedHelper.Seed(builder);
            base.OnModelCreating(builder);
            builder.Entity<Student>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Teacher>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Group>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Grade>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Father>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<UserType>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Page>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Group>(entity =>
            {
                entity.HasOne(g => g.Grade)
                      .WithMany()
                      .HasForeignKey(g => g.GradeId)
                      .OnDelete(DeleteBehavior.Cascade); // keep this one cascading

                entity.HasOne(g => g.Teacher)
                      .WithMany()
                      .HasForeignKey(g => g.TeacherId)
                      .OnDelete(DeleteBehavior.Restrict); // break the second cascade path
            });
            builder.Entity<TrackStudentTransfers>(entity =>
            {
                entity.HasOne(t => t.Student)
                      .WithMany()
                      .HasForeignKey(t => t.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Grade)
                      .WithMany()
                      .HasForeignKey(t => t.GradeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Teacher)
                      .WithMany()
                      .HasForeignKey(t => t.TeacherId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.User)
                      .WithMany()
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================================================
            // NEW - Wallet / Enrollment / Academic-tracking modules (added, nothing
            // above this line was changed). Every new FK is Restrict: the app deletes
            // via the existing IsDeleted soft-delete flag (see BaseModel), so no new
            // table should hard-cascade-delete its parent, and this also avoids any
            // SQL Server "multiple cascade paths" error since several of these
            // entities reach Student/Group/Course/User through more than one FK.
            // =====================================================================
            builder.Entity<Wallet>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<WalletTransaction>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<WalletTopUpRequest>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<WithdrawalRequest>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<PlatformCommissionSetting>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<PlatformRevenueLedger>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Subject>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<CourseCategory>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Course>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<JoinRequest>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Enrollment>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Lesson>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<LessonMaterial>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Attendance>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Exam>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<ExamResult>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Notification>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<AuditLog>().HasQueryFilter(x => !x.IsDeleted);

            builder.Entity<Wallet>(entity =>
            {
                entity.HasOne(w => w.User)
                      .WithMany()
                      .HasForeignKey(w => w.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(w => w.UserId).IsUnique();
            });

            builder.Entity<WalletTransaction>(entity =>
            {
                entity.HasOne(t => t.Wallet)
                      .WithMany()
                      .HasForeignKey(t => t.WalletId)
                      .OnDelete(DeleteBehavior.Cascade); // sole parent, single path - safe to cascade

                entity.HasOne(t => t.DecisionByUser)
                      .WithMany()
                      .HasForeignKey(t => t.DecisionBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(t => new { t.WalletId, t.CreatedOn }); // NFR-06: history queries
            });

            builder.Entity<WalletTopUpRequest>(entity =>
            {
                entity.HasOne(r => r.Student)
                      .WithMany()
                      .HasForeignKey(r => r.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.VerifiedByUser)
                      .WithMany()
                      .HasForeignKey(r => r.VerifiedBy)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<WithdrawalRequest>(entity =>
            {
                entity.HasOne(r => r.Instructor)
                      .WithMany()
                      .HasForeignKey(r => r.InstructorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.ApprovedByUser)
                      .WithMany()
                      .HasForeignKey(r => r.ApprovedBy)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<PlatformRevenueLedger>(entity =>
            {
                entity.HasOne(l => l.Enrollment)
                      .WithMany()
                      .HasForeignKey(l => l.EnrollmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Course>(entity =>
            {
                entity.HasOne(c => c.Teacher)
                      .WithMany()
                      .HasForeignKey(c => c.TeacherId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Subject)
                      .WithMany()
                      .HasForeignKey(c => c.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Category)
                      .WithMany()
                      .HasForeignKey(c => c.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<JoinRequest>(entity =>
            {
                entity.HasOne(j => j.Student)
                      .WithMany()
                      .HasForeignKey(j => j.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(j => j.Group)
                      .WithMany()
                      .HasForeignKey(j => j.GroupId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(j => j.Course)
                      .WithMany()
                      .HasForeignKey(j => j.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(j => j.DecisionByUser)
                      .WithMany()
                      .HasForeignKey(j => j.DecisionBy)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Enrollment>(entity =>
            {
                entity.HasOne(e => e.Student)
                      .WithMany()
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Group)
                      .WithMany()
                      .HasForeignKey(e => e.GroupId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Course)
                      .WithMany()
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.JoinRequest)
                      .WithMany()
                      .HasForeignKey(e => e.JoinRequestId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Lesson>(entity =>
            {
                entity.HasOne(l => l.Group)
                      .WithMany()
                      .HasForeignKey(l => l.GroupId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Course)
                      .WithMany()
                      .HasForeignKey(l => l.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<LessonMaterial>(entity =>
            {
                entity.HasOne(m => m.Lesson)
                      .WithMany()
                      .HasForeignKey(m => m.LessonId)
                      .OnDelete(DeleteBehavior.Cascade); // sole parent, single path - safe to cascade

                entity.HasOne(m => m.UploadedByUser)
                      .WithMany()
                      .HasForeignKey(m => m.UploadedBy)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Attendance>(entity =>
            {
                entity.HasOne(a => a.Student)
                      .WithMany()
                      .HasForeignKey(a => a.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Group)
                      .WithMany()
                      .HasForeignKey(a => a.GroupId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.RecordedByUser)
                      .WithMany()
                      .HasForeignKey(a => a.RecordedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(a => new { a.GroupId, a.SessionDate }); // NFR-06: daily attendance queries
            });

            builder.Entity<Exam>(entity =>
            {
                entity.HasOne(e => e.Group)
                      .WithMany()
                      .HasForeignKey(e => e.GroupId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Course)
                      .WithMany()
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ExamResult>(entity =>
            {
                entity.HasOne(r => r.Exam)
                      .WithMany()
                      .HasForeignKey(r => r.ExamId)
                      .OnDelete(DeleteBehavior.Cascade); // sole "owning" path once Student below is Restrict

                entity.HasOne(r => r.Student)
                      .WithMany()
                      .HasForeignKey(r => r.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.GradedByUser)
                      .WithMany()
                      .HasForeignKey(r => r.GradedBy)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Notification>(entity =>
            {
                entity.HasOne(n => n.User)
                      .WithMany()
                      .HasForeignKey(n => n.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<AuditLog>(entity =>
            {
                entity.HasOne(l => l.User)
                      .WithMany()
                      .HasForeignKey(l => l.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }


        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Constant> Constants { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageCategory> PageCategories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Father> Fathers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TrackStudentTransfers> TrackStudentTransfers { get; set; }

        public DbSet<Migration> Migrations { get; set; }

        // ==================== NEW - Wallet module ====================
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<WalletTopUpRequest> WalletTopUpRequests { get; set; }
        public DbSet<WithdrawalRequest> WithdrawalRequests { get; set; }
        public DbSet<PlatformCommissionSetting> PlatformCommissionSettings { get; set; }
        public DbSet<PlatformRevenueLedger> PlatformRevenueLedgers { get; set; }

        // ========== NEW - Enrollment, Courses & Content module ==========
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<JoinRequest> JoinRequests { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonMaterial> LessonMaterials { get; set; }

        // == NEW - Academic Tracking, Notifications & Audit module ==
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

    }

}
