using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace wykład_4.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetails> BookDetailsSet { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        private string DbPath;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "books-v1.db");
            optionsBuilder.UseSqlite($"DATA SOURCE={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
                .HasData(
                    new Book() {Id = 1, Title = "ASP.NET", EditionYear = 2020, PublisherId = 1},
                    new Book() {Id = 2, Title = "C#", EditionYear = 2022, PublisherId = 1},
                    new Book() {Id = 3, Title = "Java", EditionYear = 2021, PublisherId = 2}
                );
            modelBuilder.Entity<Author>()
                .HasData(
                    new Author() {Id = 1, Name = "Ewa"},
                    new Author() {Id = 2, Name = "Adam"}
                );
            modelBuilder.Entity<Book>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Book>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BookDetails>()
                .HasData(
                    new BookDetails() {Id = 1, Description = "Super", Rating = 23, BookId = 1},
                    new BookDetails() {Id = 2, Description = "Not bad", Rating = 2, BookId = 3}
                );

            modelBuilder.Entity<Publisher>()
                .HasData(
                    new Publisher() {Id = 1, Email = "contact@helion.com", Name = "Helion"},
                    new Publisher() {Id = 2, Email = "office@pwn.pl", Name = "Państwowe Wydawnictwo Naukowe"}
                );
            modelBuilder.Entity<Book>()
                .Property(b => b.Created)
                .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Book>()
                .HasMany<Author>(a => a.Authors)
                .WithMany(a => a.Books)
                .UsingEntity(join => join.HasData(
                    new {BooksId = 1, AuthorsId = 1},
                    new {BooksId = 2, AuthorsId = 1},
                    new {BooksId = 3, AuthorsId = 2},
                    new {BooksId = 1, AuthorsId = 2}
                ));
        }
    }
}