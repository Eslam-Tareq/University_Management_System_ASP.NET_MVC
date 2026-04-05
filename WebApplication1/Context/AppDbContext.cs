using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=ESLAMTAREK\\SQLEXPRESS;Database=MVCD1;Trusted_Connection=True;TrustServerCertificate=True");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudCourse>().HasKey(stc => new { stc.Std_Id, stc.Crs_Id });
            modelBuilder.Entity<InsCourse>().HasKey(stc => new { stc.Ins_Id, stc.Crs_Id });

            var converter = new ValueConverter<List<string>, string>(
                       v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                       v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            );

            modelBuilder.Entity<Course>()
                .Property(c => c.Topics)
                .HasConversion(converter);

            modelBuilder.Entity<Course>()
                .Property(c => c.Mindegree)
                .HasDefaultValue(100.0f);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Student> Students { set; get; }
        public DbSet<Instructor> Instructors { set; get; }

        public DbSet <Course> Courses { set; get; }


        public DbSet <Department> Departments { set; get; }

        public DbSet<StudCourse> StudCourses { set; get; }
        public DbSet<InsCourse> InsCourses { set; get; }



    }
}
