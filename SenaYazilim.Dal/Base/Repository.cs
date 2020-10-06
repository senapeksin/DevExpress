using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SenaYazilim.Dal.Interfaces;
using System.Data.Entity;
using System.Linq;
using SenaYazilim.OgrenciTakip.Common.Enums;
using SenaYazilim.OgrenciTakip.Common.Functions;

namespace SenaYazilim.Dal.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Variables
        private readonly DbContext _context;

        #region bilgi
        /* yerel değişken atarken _ kullanılır.
         readonly : sadece okunabilir bir değişken , değer ataması yapılamaz sadece okuyabiliriz anlamına geliyor.
         Hiç mi değer atamayız? 2 türlü atabiliriz.
         *Readonly olarak tanımlanan değişkenlere tanımlandığı anda veya
         *ilgili  oluşturulduğu classın contructor ında değer ataması yapılabiliyor.
         onun haricinde değer ataması yapılamıyor sadece okuma yapılabiliyor.*/
        #endregion

        private readonly DbSet<T> _dbSet;                   //Dbset bizim entitylerimizi temsil etmiş oluyor. 
        #endregion

        public Repository(DbContext context)
        {
            if (context == null) return;
            _context = context;
            _dbSet = _context.Set<T>();                              //Artık hazır bir dbSet imiz olmus oldu.  (Tablolarımızı temsil ediyor)
        }

        public void Insert(T entity)
        #region açıklama
        /*insert işlemi için context'e : sana göndereceğimiz T tipindeki entity i edit olarak işaretleyeceğim
        Sen edit olarak gördüğün zaman gideceksin bunu yeni bir kayıt olarak ekleyeceksin demek istiyoruz.*/
        #endregion

        {
            _context.Entry(entity).State = EntityState.Added;
        }
        public void Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Added;
            }
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Update(T entity, IEnumerable<string> fields)        //sadece sana field olarak gösterilen bölgeyi update et demekti.
        {
            _dbSet.Attach(entity);                                      //ilk önce hangi entity ile çalışacağımızı dbset e söylüyoruz.
            var entry = _context.Entry(entity);
            #region açıklama
            /*Daha sonra bu entity i entity nin fieldları arasında dolaşıp bir update işlemi yapacağımızı
            yani komple entity i değil de  entity nin fieldları arasında dolasıp bir update işlemi yapacağımızı belirtmek için 
            bir tane entry olusturuyoruz.*/

            #endregion

            foreach (var field in fields)                               //daha sonra fieldslar arasında dolaşıyoruz.
            {
                entry.Property(field).IsModified = true;
                #region açıklama
                /*bu entryden, entry e gelen entity nin property leri arasında dolaşıp hangisinde bir değişiklik varsa
               onların modified ını true çekiyoruz.*/
                #endregion
            }
        }
        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }
        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }
        public TResult Find<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return filter == null ? _dbSet.Select(selector).FirstOrDefault() : _dbSet.Where(filter).Select(selector).FirstOrDefault();
            #region açıklama
            //eğer filter null ise sadece git benim sana göndereceğim formattaki selector ü bunun içerisine koy selectordan gelen kayıtları ve select yaparak ilk kaydı bana döndür.Hiç kayıt yoksa null olarak döndür.
            #endregion 

        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return filter == null ? _dbSet.Select(selector) : _dbSet.Where(filter).Select(selector);
        }


        public string YeniKodVer(KartTuru kartTuru, Expression<Func<T, string>> filter, Expression<Func<T, bool>> where = null)
        {
            //eğer database de kayıt yoksa;

            string Kod()
            {
                string kod = null;
                var kodDizi = kartTuru.ToName().Split(' ');

               
                for (int i = 0; i < kodDizi.Length -1 ; i++)        //koddizi.lengh-1, uzunluğunun 1 eksiğine kadar işlemi devam ettir
                {
                    kod += kodDizi[i];                              //kodDizinin i ninci indexindeki kelimeyi al kod a aktar

                    if (i + 1 < kodDizi.Length - 1)
                        kod +=" ";
                }

                return kod += "-0001";
            }

            //eğer database de kayıt varsa ;

            string YeniKodVer(string kod) //Okul-0009
            {
                var sayisalDegerler = "";

                foreach (var karakter in kod )
                {
                    if (char.IsDigit(karakter))                      //eğer sayısal bir karakter ise;
                        sayisalDegerler += karakter;
                    else
                        sayisalDegerler = "";
                }

                var artisSonrasiDeger = (int.Parse(sayisalDegerler) + 1).ToString();
                var fark = kod.Length - artisSonrasiDeger.Length;
                if (fark < 0)
                    fark = 0;

                var yeniDeger = kod.Substring(0, fark);
                yeniDeger+= artisSonrasiDeger;

                return yeniDeger;
            }

            var maxKod = where == null ? _dbSet.Max(filter) : _dbSet.Where(where).Max(filter);
            //yukardan gelen where null ise  (herhangi bir where ile sorgu gönderilmediyse) yukardaki filtreye göre max ı getir.
            return maxKod == null ? Kod() : YeniKodVer(maxKod);

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
