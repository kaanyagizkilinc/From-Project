using Microsoft.EntityFrameworkCore;
using Modasayfasi.Model.Entity;
using Modasaydasi.WebApi;
using System.Security.Cryptography.X509Certificates;
using Microsoft.SqlServer;

namespace Modasaydasi.WebApi.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KullaniciMedia>()
                .HasKey(km => new { km.KullaniciId, km.MediaId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<CografyaKutuphanesi> CografyaKutuphanesi { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<KullaniciMedia> KullaniciMedia { get; set; }
        public DbSet<KullaniciOkulBilgileri> KullaniciOkulBilgileri { get; set; }

        public DbSet<OkulMedyalar> OkulMedyalar { get; set; }
        public DbSet<RegisterLogin> RegisterLogin { get; set; }
    }

}
