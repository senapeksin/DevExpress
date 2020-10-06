using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SenaYazilim.OgrenciTakip.Data.OgrenciTakipMigration;
using SenaYazilim.OgrenciTakip.Model.Entities;

namespace SenaYazilim.OgrenciTakip.Data.Contexts
{
    public class OgrenciTakipContext : BaseDbContext<OgrenciTakipContext,Configuration>
    {
        public OgrenciTakipContext() 
        {
            Configuration.LazyLoadingEnabled = false; //select i�leminin daha uzun s�rmesini engelliyoruz.
        }

        public OgrenciTakipContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//table isimlerini database e g�nderirken �o�ulla�t�rmas�n� devre d�s� b�rak�yoruz.
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//bire �ok ili�kili tablolarda e�er verilerden biri silinirse ona ba�l� olan di�er tabloyu siler bunu engellemek i�in
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        //tablelar = entityleri tan�mlayaca��z.

        public DbSet<Il> Il { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<Okul> Okul { get; set; }
       

    }
}