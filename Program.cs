using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace ConsoleAppClassWork3Core
{
    internal class Program
    {
        static void Main()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection")).Options;
            using (ApplicationContext db = new ApplicationContext(options))
            {

                var newUser = new User
                {
                    Name = "Олег Петренко",
                    Age = 25,
                    UserPosition = Position.Разработчик,
                    DepartmentId = 1
                };

                db.Users.Add(newUser);
                db.SaveChanges();
                var allUsers = db.Users.ToList();
                foreach (var user in allUsers)
                {
                    Console.WriteLine($"Id: {user.UserId},Name: {user.Name}, Age: {user.Age}, Position: {user.UserPosition}");
                }
            }
        }
    }
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Position UserPosition { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }

    public enum Position
    {
        Разработчик,
        Дизайнер,
        Менеджер
    }
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public List<User> Users { get; set; }

    }
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasKey(e => e.UserId);
            //modelBuilder.Entity<User>().Property(e => e.Name).HasMaxLength(50);
            //modelBuilder.Entity<User>().HasCheckConstraint("Age", "Age >= 0 AND Age <= 120");
            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.HasKey(e => e.UserId);
            //    entity.Property(e => e.Name).HasMaxLength(50);
            //    entity.Property(e => e.UserPosition).IsRequired().HasConversion<int>();
            //    entity.Property(e => e.Age).IsRequired();
            //});
            //    modelBuilder.Entity<User>(entity =>
            //    {
            //        entity.HasKey(u => new { u.UserId, u.DepartmentId }).HasName("PK_Users"); 

            //        entity.Property(u => u.Name).HasMaxLength(50);

            //        entity.Property(u => u.UserPosition)
            //            .IsRequired()
            //            .HasConversion<int>();

            //        entity.Property(u => u.Age)
            //            .IsRequired();

            //        entity.HasOne(u => u.Department)
            //            .WithMany(d => d.Users)
            //            .HasForeignKey(u => u.DepartmentId)
            //            .OnDelete(DeleteBehavior.Cascade); 
            //    });

            //    modelBuilder.Entity<Department>(entity =>
            //    {
            //        entity.HasKey(d => d.DepartmentId);

            //        entity.Property(d => d.DepartmentName)
            //            .IsRequired()
            //            .HasMaxLength(100);


            //    });
            //}

        }
    }
}
    

