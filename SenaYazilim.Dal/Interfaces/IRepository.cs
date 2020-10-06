using SenaYazilim.OgrenciTakip.Common.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SenaYazilim.Dal.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Insert(T entity);
        void Insert(IEnumerable<T> entities);                       //birden cok entity gönderilmesi durumunda bunların insert işlemlerini yap.
        void Update(T entity);
        void Update(T entity, IEnumerable<string> fields);          //Sana gönderilen T tipindeki entity i değil bu entitynin değişen alanlarını(IEnurable<string> update et anlamına geliyor.)
        void Update(IEnumerable<T> entities);                       //birden fazla entity gönderirsem bunları update et.
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);


        //select etmek için bazı eklentiler;
        TResult Find<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector);
        #region açıklama

        /*biz sana T türünde bir tane sorgu göndereceğiz.Eğer bu sorgu sonucunda true değer dönüyorsa ki
        sorgumuzun karsısında bir valium (değer) olduğunu gösteriyor bu.O zaman bize bu veriyi geri gönder.
        Hangi tipte geri göndereceğini sana ben sorgulama aşamasında vereceğim(<TResult>) */

        //geriye dönen değeri bilmediğimiz için Tresult diyoruz. Function ismi Find olacak.


        //Expression<Func<T,TResult>> selector kısmı ise;
        //T tipinde değer alıyor bu gelen T tipinde fieldlar arasında dolaşıyor ihtiyacımız olanları seçip TResult olarak geri gönderiyor.

        //Find da tek bir kayıt geri dönerken , Select durumunda birden cok kayıt geri dönecek.

        #endregion

        //birden fazla kayıt seçmek için olusturacağımız fonksiyon;
        IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector);
        #region Açıklama
        /*biz sana içerisinde T bulunduran bir filtre göndereceğiz bu filtre ile gerekli sorguları yap  eğer sorgu
        sonucunda true dönüyorsa ki true dönmesi kayıt vardır demektir.Kaç tane kayıt varsa bunları bana geriye gönder
        yalnızca sadece kayıt göndermez IQueryable kullandığımız için geriye string türünde bir sorgu döndürür.*/
        #endregion


        string YeniKodVer(KartTuru kartTuru, Expression<Func<T, string>> filter, Expression<Func<T, bool>> where = null);
        


    }
}
