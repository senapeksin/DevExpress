using SenaYazilim.Dal.Base;
using SenaYazilim.Dal.Interfaces;
using SenaYazilim.OgrenciTakip.Model.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaYazilim.OgrenciTakip.Bll.Functions
{
    public static class GeneralFunctions
    {
        public static List<string> DegisenAlanlariGetir<T>(this T oldEntity ,T currentEntity)
        {
            List<string> alanlar = new List<string>(); //
            foreach (var prop in currentEntity.GetType().GetProperties())
            {
                if (prop.PropertyType.Namespace == "System.Collections.Generic") continue;
                var oldValue = prop.GetValue(oldEntity)?? string.Empty;//prop un oldentitydeki değerini almıs olduk 
                //Şunu demek istedik:Eğer prop.GetVAlue(oldEntity) le gelen değer null ise o zaman null değilde string.Empty olarak bu oldvalue değerini al.
                var currentValue = prop.GetValue(currentEntity) ?? string.Empty;

                if (prop.PropertyType == typeof(byte[]))
                {
                    if (string.IsNullOrEmpty(oldValue.ToString()))
                        oldValue = new byte[] { 0 };
                    if (string.IsNullOrEmpty(currentValue.ToString()))
                        currentValue = new byte[] { 0 }; //default değeri olarak 0 verdik.
                    //şimdi bu iki alanı karşılayacağız.
                    if (((byte[])oldValue).Length != ((byte[])currentValue).Length)
                        alanlar.Add(prop.Name);
                }
                else if (!currentValue.Equals(oldValue))
                    alanlar.Add(prop.Name);
            }
            return alanlar;
        }





        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["OgrenciTakipContext"].ConnectionString;
            #region amaç
            /*Burada ConfigurationManager aracılığı ile  connectionString e ulasacak.Hangi Connection String e?
            Bizim  AppConfig deki  OgrenciTakipConnectionString ine ulasacak.Oradaki valiu okuyacak ve buraya geri döndürecek.*/
            #endregion
        }
        private static TContext CreateContext<TContext>() where TContext:DbContext 
        {
            return (TContext)Activator.CreateInstance(typeof(TContext),GetConnectionString());
            #region CreateContext foksiyonunun amacı:
           /* bir tane Tcontext yani DbContext olusturduk tipimiz bu olacak dedik ve aynı zamanda nameorConnectionString de gönderebilirsiniz diyorrdu bu  nedenlede getconnectionString i olusturduk.Yani biz isim göndermiyoruz da connectionstring imizin son halini göndermiş oluyoruz.Bu şekilde biz DbContextten bir tane instance üretip geri göndermiş oluyoruz.
           ****
            Bizim Appconfig dosyamıza gidiyordu ve ordaki en son haliyle bizim connection string in en son halini alıp bir tane Context olusturup bunu geri gönderiyordu.*/
            #endregion 
        }
        public static void CreateUnitOfWork<T,TContext>(ref IUnitOfWork<T> uow) where T:class,IBaseEntity where TContext:DbContext
        {
            #region CreateUnitOfWork foksiyonunun amacı
            /*CreateUnitOfWork foksiyonunun amacı:Biz burada IUnıtOfWrok un son haline ulaşacağız bundan dolayı referans aldık.Son haline ulaşacağımız için ilk önce eğer mevcut bir instance varsa önce onu Dispose etmemiz lazım.Ki daha sonra sıfırdan temiz bir instance olusturalım.*/
            #endregion
            uow?.Dispose(); //unitofwork eğer null değilse Dispose yap.
            uow = new UnitOfWork<T>(CreateContext<TContext>()); //şimdide sıfırdan bir instance olusturduk.
        }
    }
}
