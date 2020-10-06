using SenaYazilim.OgrenciTakip.Data.Contexts;
using System.Data.Entity.Migrations;

namespace SenaYazilim.OgrenciTakip.Data.OgrenciTakipMigration
{
    public class Configuration : DbMigrationsConfiguration<OgrenciTakipContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; //migration işlemlerini otomatik yap.
            AutomaticMigrationDataLossAllowed = true;//veri kaybı olacaksa izin ver.
        }
    }
}
