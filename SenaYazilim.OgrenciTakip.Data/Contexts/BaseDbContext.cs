using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace SenaYazilim.OgrenciTakip.Data.Contexts
{
    public class BaseDbContext<TContext, TConfiguration> : DbContext where TContext : DbContext where TConfiguration : DbMigrationsConfiguration<TContext>, new()
    {
        private static string _nameOrConnectionString = typeof(TContext).Name; //Tcontextin adını göndermiş olduk . Önemli olan bu kısmın boş olmaması.
        public BaseDbContext() : base(_nameOrConnectionString)
        {
        }
        public BaseDbContext(string connectionString):base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TContext, TConfiguration>());
            /*
             eğer ki bu constructor ile gelinirse ki sıklıkla bu ctor kullanıcaz napıcak?
             ilk database'e bağlanacak,modellerimize bakıcak bizim olusturmuş olduğumuz burdaki modellerler
             databasedeki tabloları karşılaştıracak eğer bir değişiklik varsa şu Context yoluyla ve şu Configuration'a
             göre bunları git güncelle anlamına geliyor.
             */
            _nameOrConnectionString = connectionString; 
        }
    }
}
