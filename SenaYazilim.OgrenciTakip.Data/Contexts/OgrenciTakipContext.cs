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
            Configuration.LazyLoadingEnabled = false; //select iþleminin daha uzun sürmesini engelliyoruz.
        }

        public OgrenciTakipContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//table isimlerini database e gönderirken çoðullaþtýrmasýný devre dýsý býrakýyoruz.
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//bire çok iliþkili tablolarda eðer verilerden biri silinirse ona baðlý olan diðer tabloyu siler bunu engellemek için
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        //tablelar = entityleri tanýmlayacaðýz.

        public DbSet<Il> Il { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<Okul> Okul { get; set; }
       

    }
}