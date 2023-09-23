using Microsoft.EntityFrameworkCore;

namespace OuTouchFilms.Models
{
    public class OuTouchDbContext : DbContext
    {
        public OuTouchDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserFilms> UserFilms { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<FilmComment> FilmComments { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<FilmStaff> FilmStaffs { get; set; }
    }
}
