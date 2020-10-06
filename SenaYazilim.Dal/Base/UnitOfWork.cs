using SenaYazilim.Dal.Interfaces;
using SenaYazilim.OgrenciTakip.Common.Message;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaYazilim.Dal.Base
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        //UnitOfWork de save işlemi yapacağız.

        #region Variables
        private readonly DbContext _context; 
        #endregion

        public UnitOfWork(DbContext context)
        {
            if (context == null) return;
            _context = context;
        }
        //=> return anlamına geliyor.

        /*IUnitOfWork den Repository e ulaşabilmemiz lazım demiştik.
         * Bu şekilde artık UnitOfWork'ten  Repository e rahat bir şekilde ulaşabilmeyi sağlamış olduk.
         * 
         * */
        public IRepository<T> Rep => new Repository<T>(_context);

        /*Save işlemimizi yaparken çeşitli hatalar alabiliriz.Database server da olmayabilir, server kapalı olablir gibi..
         * Belli başlı hataları yakalayabilmemiz gerekir.Bu neden exception yakalabilmek için try catch arasına yazıyorum.
         * DbUpdateException hatasını yakalamak istiyoruz .
         * */
        public bool Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqlEx = (SqlException)ex.InnerException?.InnerException;
                //eğer bu null değilse o zaman InnerException ı al. Null sa herhangi bir işlem yapma.
                //ve bu gelen InnerException ı SqlException a cast edeceğiz.
                if (sqlEx==null)
                {
                    Messages.HataMesaji(ex.Message);
                    return false;
                }

                switch (sqlEx.Number)
                {
                    case 208: //herhangi bir hata durumunda  sql serverin 208 hatası karşılığı;
                        Messages.HataMesaji("İşlem Yapmak İstediğiniz Tablo Veritabanında Bulunamadı!");
                        break;
                    case 547:
                        Messages.HataMesaji("Seçilen Kartın İşlem Görmüş Hareketleri Var,Kart Silinemez!");
                        break;
                    case 2601:
                    case 2627:
                        Messages.HataMesaji("Girmiş Olduğunuz ID Daha Önce Kullanılmıştır!");
                        break;
                    case 4060:
                        Messages.HataMesaji("İşlem Yapmak İstediğiniz Veritabanı Sunucuda Bulunamadı!");
                        break;
                    case 18456:
                        Messages.HataMesaji("Server'a Bağlanılmak İstenilen Kullanıcı Adı ve Şifre Hatalıdır!");
                        break;
                    default:
                        Messages.HataMesaji(sqlEx.Message);
                        break;
                }
                return false;
            }
            catch(Exception ex)
            {
                Messages.HataMesaji(ex.Message);
                return false;
            }
            return true;
        }

        #region Dispose
        private bool _disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
